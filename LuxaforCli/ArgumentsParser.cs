using System;
using System.Collections.Generic;
using LuxaforSharp;
using LuxaforCli.Arguments;

namespace LuxaforCli
{
    class ArgumentsParser
    {
        private string[] args;

        public List<CommandDefinition> commands { get; private set; }

        public ArgumentsParser(string[] args)
        {
            this.args = args;

            this.commands = new List<CommandDefinition>();

            this.parse();
        }

        public void parse()
        {
            CommandType command = CommandType.Color;
            Target target = Target.All;
            Color color = new Color(0, 0, 0);

            bool commandIsSet = false;
            bool targetIsSet = false;
            bool colorIsSet = false;

            byte speed = 0;
            byte repeat = 0;
            WaveType waveType = 0;
            PatternType patternType = 0;

            foreach (string arg in this.args)
            {
                //Console.WriteLine("arg : " + arg);

                // Target arg ?
                if (!targetIsSet)
                {
                    targetIsSet = true;

                    if (Enum.TryParse(arg, true, out target) && Enum.IsDefined(typeof(Target), target))
                    {
                        //Console.WriteLine("Target found : " + Enum.GetName(typeof(Target), target));
                        continue;
                    }
                    else
                    {
                        target = Target.All;
                        //Console.WriteLine("Default Target : " + Enum.GetName(typeof(Target), target));
                    }
                }

                // Command arg ?
                if (!commandIsSet)
                {
                    commandIsSet = true;

                    if (Enum.TryParse(arg, true, out command) && Enum.IsDefined(typeof(CommandType), command))
                    {    
                        //Console.WriteLine("Command found : " + Enum.GetName(typeof(CommandType), command));
                        continue;
                    }
                    else
                    {
                        command = CommandType.Color;
                        //Console.WriteLine("Default Command : " + Enum.GetName(typeof(CommandType), command));
                    }
                }

                if (!colorIsSet) {

                    switch (command)
                    {
                        case CommandType.Color:
                        case CommandType.Blink:
                            // Color is expected
                            color = new Color(0, 0, 0);

                            ColorParser colorParser = new ColorParser(arg);
                            color = colorParser.color;

                            if (color == null)
                            {
                                Console.WriteLine(colorParser.error);
                                return;
                            }

                            colorIsSet = true;
                            break;
                    }
                }

                switch (command)
                {
                    case CommandType.Wave:
                        // Wave type expected
                        // TODO
                        break;

                    case CommandType.Pattern:
                        // Pattern type expected
                        //TODO
                        break;
                }
                

                // TODO : speed
                // TODO : repeat
                // TODO : waveType / patternType

                this.commands.Add(new CommandDefinition(color, command, target));


                // reset
                command = CommandType.Color;
                target = Target.All;
                color = new Color(0, 0, 0);

                commandIsSet = false;
                targetIsSet = false;
                colorIsSet = false;

                speed = 0;
                repeat = 0;
                waveType = 0;
                patternType = 0;
            }

            //Console.WriteLine(this.commands.ToString());
            foreach (CommandDefinition cmd in this.commands) 
            {
                Console.WriteLine("cmd : " + cmd.ToString());
            }
        }
    }
}
