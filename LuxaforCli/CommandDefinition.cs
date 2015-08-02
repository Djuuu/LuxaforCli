using System;
using LuxaforCli.Arguments;
using LuxaforSharp;

namespace LuxaforCli
{
    class CommandDefinition
    {
        public CommandType type { get; private set; }

        public Target target { get; private set; }

        public Color color { get; private set; }

        // for wave / blink / color
        public byte speed { get; private set; }

        // for wave / blink
        public byte repeat { get; private set; }

        // for wave
        public WaveType waveType { get; private set; }

        // for pattern
        public PatternType patternType { get; private set; }

        //public CommandDefinition(Color color, CommandType type = CommandType.Color, Target target = Target.All)
        public CommandDefinition(Color color, CommandType type, Target target)
        {
            this.color = color;
            this.type = type;
            this.target = target;
        }

        public string ToString()
        {
            string str = "";

            str += "Type : " + Enum.GetName(typeof(CommandType), this.type);

            switch (type) 
            {
                case CommandType.Color:
                case CommandType.Blink:
                    str += " ; Target : " + Enum.GetName(typeof(Target), this.target);
                    break;
            }

            switch (type)
            {
                case CommandType.Color:
                case CommandType.Blink:
                case CommandType.Wave:
                    str += " ; Color : R" + this.color.Red + " G" + this.color.Green + " B" + this.color.Blue;
                    // TODO
                    //str += " ; Speed : ";
                    break;
            }

            switch (type)
            {
                case CommandType.Blink:
                case CommandType.Wave:
                    // TODO
                    //str += " ; Repeats : ";
                    break;
            }

            return str;
        }
    }
}
