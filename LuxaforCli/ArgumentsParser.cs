using System;
using System.Linq;
using System.Collections.Generic;
using LuxaforSharp;
using LuxaforCli.Arguments;

namespace LuxaforCli
{
    class ArgumentsParser
    {
        private string[] args;

        public List<CommandDefinition> commands { get; private set; }

        CommandDefinition currentCommandDefinition;

        bool isCurrentCommandSet = false;
        bool isCurrentTargetSet = false;
        bool isCurrentColorSet = false;
        bool isCurrentWaveTypeSet = false;
        bool isCurrentPatternTypeSet = false;
        bool isCurrentSpeedSet = false;
        bool isCurrentRepeatSet = false;

        public ArgumentsParser(string[] args)
        {
            this.args = args;

            this.commands = new List<CommandDefinition>();

            this.parse();
        }

        public void parse()
        {
            this.currentCommandDefinition = new CommandDefinition();

            foreach (string arg in this.args)
            {
                if (this.parseCommand(arg))     { continue; }
                if (this.parseTarget(arg))      { continue; }
                if (this.parseWaveType(arg))    { continue; }
                if (this.parseColor(arg))       { continue; }
                if (this.parsePatternType(arg)) { continue; }
                if (this.parseSpeed(arg))       { continue; }
                if (this.parseRepetitions(arg)) { continue; }
            }

            if (this.isCurrentCommandSet)
            {
                this.appendCurrentCommandDefinition();
            }
        }

        private void appendCurrentCommandDefinition()
        {
            // add current CommandDefinition to the list 
            this.commands.Add(this.currentCommandDefinition);

            // reset current CommandDefinition 
            this.currentCommandDefinition = new CommandDefinition();

            // reset flags
            this.isCurrentCommandSet = false;
            this.isCurrentTargetSet = false;
            this.isCurrentColorSet = false;
            this.isCurrentWaveTypeSet = false;
            this.isCurrentPatternTypeSet = false;
            this.isCurrentSpeedSet = false;
            this.isCurrentRepeatSet = false;
        }


        #region Token identifiers

        private static bool isComandArg(string arg, out CommandType command)
        {
            Enum.TryParse(arg, true, out command);

            // only command names are accepted, not enum values (would conflict with other parameters)
            return Enum.GetNames(typeof(CommandType)).Contains(arg, StringComparer.InvariantCultureIgnoreCase);
        }

        private static bool isTargetArg(string arg, out Target target)
        {
            Enum.TryParse(arg, true, out target);

            // only target names are accepted, not enum values (would conflict with other parameters)
            return Enum.GetNames(typeof(Target)).Contains(arg, StringComparer.InvariantCultureIgnoreCase);
        }

        private static bool isColorArg(string arg, out Color color)
        {
            color = new Color(0, 0, 0);

            return (color = new ColorParser(arg).color) != null;
        }

        private static bool isWaveTypeArg(string arg, out WaveType wavetype)
        {
            wavetype = 0;

            return Enum.TryParse(arg, true, out wavetype)
                && Enum.IsDefined(typeof(WaveType), wavetype);
        }

        private static bool isPatternTypeArg(string arg, out PatternType patternType)
        {
            patternType = 0;

            return Enum.TryParse(arg, true, out patternType)
                && Enum.IsDefined(typeof(PatternType), patternType);
        }

        private static bool isByteArg(string arg, out byte byteValue)
        {
            try
            {
                byteValue = byte.Parse(arg);
                return true;
            }
            catch (Exception e)
            {
                byteValue = 0x00;
                return false;
            }
        }

        #endregion

        #region parsers

        private bool parseCommand(string arg)
        {
            CommandType command = CommandType.Color;

            // Looking for a new command or fallback to default
            if (!this.isCurrentCommandSet)
            {
                this.isCurrentCommandSet = true;

                if (isComandArg(arg, out command))
                {
                    this.currentCommandDefinition.command = command;
                    return true;
                }
                return false;
            }
            else
            {
                if (isComandArg(arg, out command))
                {
                    // previous command is complete
                    this.appendCurrentCommandDefinition();

                    // new command
                    this.currentCommandDefinition.command = command;
                    this.isCurrentCommandSet = true;
                    return true;
                }
                return false;
            }
        }

        private bool parseTarget(string arg)
        {
            var target = Target.All;

            if (this.currentCommandDefinition.command != CommandType.Color
                && this.currentCommandDefinition.command != CommandType.Blink)
            {
                return false; // Target is only used for Color and Blink commands
            }

            if (!this.isCurrentTargetSet)
            {
                this.isCurrentTargetSet = true;

                if (isTargetArg(arg, out target))
                {
                    this.currentCommandDefinition.target = target;
                    return true;
                }

                return false;
            }

            if (isTargetArg(arg, out target))
            {
                // previous command is complete
                this.appendCurrentCommandDefinition();

                // new command
                this.currentCommandDefinition.target = target;
                this.isCurrentCommandSet = true;
                this.isCurrentTargetSet = true;
                return true;
            }

            return false;
        }

        private bool parseColor(string arg)
        {
            if (this.currentCommandDefinition.command == CommandType.Pattern)
            {
                return false; // Color is not used for Pattern command
            }

            if (!this.isCurrentColorSet)
            {
                // Color is expected
                var color = new Color(0, 0, 0);
                if (isColorArg(arg, out color))
                {
                    this.currentCommandDefinition.color = color;
                    this.isCurrentColorSet = true;
                    return true;
                }

                throw new Exception(String.Format("Invalid color : {0}", arg));
            }

            return false;
        }

        private bool parseWaveType(string arg)
        {
            if (this.currentCommandDefinition.command != CommandType.Wave)
            {
                return false; // Wave type is used only for Wave command
            }

            if (!this.isCurrentWaveTypeSet)
            {
                var waveType = WaveType.Short;

                // Wave type expected
                if (isWaveTypeArg(arg, out waveType))
                {
                    this.currentCommandDefinition.waveType = waveType;
                    this.isCurrentWaveTypeSet = true;
                    return true;
                }

                throw new Exception(String.Format("Invalid wave type : {0}", arg));
            }

            return false;
        }

        private bool parsePatternType(string arg)
        {
            if (this.currentCommandDefinition.command != CommandType.Pattern)
            {
                return false; // Wave type is used only for Pattern command
            }

            if (!this.isCurrentPatternTypeSet)
            {
                var patternType = PatternType.Luxafor;

                // Wave type expected
                if (isPatternTypeArg(arg, out patternType))
                {
                    this.currentCommandDefinition.patternType = patternType;
                    this.isCurrentPatternTypeSet = true;
                    return true;
                }

                throw new Exception(String.Format("Invalid pattern type : {0}", arg));
            }

            return false;
        }

        private bool parseSpeed(string arg)
        {
            if (this.currentCommandDefinition.command == CommandType.Pattern)
            {
                return false; // Speed not used for Pattern command
            }

            if (!this.isCurrentSpeedSet)
            {
                byte speed = 0;
                if (isByteArg(arg, out speed))
                {
                    this.currentCommandDefinition.speed = speed;
                    this.isCurrentSpeedSet = true;
                    return true;
                }

                throw new Exception(String.Format("Invalid speed : {0}", arg));
            }

            return false;
        }

        private bool parseRepetitions(string arg)
        {
            if (this.currentCommandDefinition.command == CommandType.Color)
            {
                return false; // Speed not used for Color command
            }

            if (!this.isCurrentRepeatSet)
            {
                byte repeat = 0;
                if (isByteArg(arg, out repeat))
                {
                    this.currentCommandDefinition.repeat = repeat;
                    this.isCurrentRepeatSet = true;
                    return true;
                }

                throw new Exception(String.Format("Invalid repetitions : {0}", arg));
            }

            return false;
        }

        #endregion
    }
}
