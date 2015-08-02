using System;
using LuxaforCli.Arguments;
using LuxaforSharp;

namespace LuxaforCli
{
    class CommandDefinition
    {
        public CommandType command { get; set; }

        public Target target { get; set; }

        public Color color { get; set; }

        public WaveType waveType { get; set; } // for Wave

        public PatternType patternType { get; set; } // for Pattern

        public byte speed { get; set; } // for Wave / Blink / Color

        public byte repeat { get; set; } // for Wave / Blink / Pattern


        public CommandDefinition()
        {
            this.command = CommandType.Color;
            this.target = Target.All;
            this.color = new Color(0, 0, 0);
            this.speed = 0;
            this.repeat = 0;
            this.waveType = 0;
            this.patternType = 0;
        }

        public string ToString()
        {
            switch (this.command)
            {
                case CommandType.Color:
                    return String.Format(
                        "{0} {1}  R{2} G{3} B{4}  {5}",
                        Enum.GetName(typeof(CommandType), this.command),
                        Enum.GetName(typeof(Target), this.target),
                        this.color.Red, this.color.Green, this.color.Blue,
                        this.speed
                    );
                    break;

                case CommandType.Blink:
                    return String.Format(
                        "{0} {1}  R{2} G{3} B{4}  {5} {6}",
                        Enum.GetName(typeof(CommandType), this.command),
                        Enum.GetName(typeof(Target), this.target),
                        this.color.Red, this.color.Green, this.color.Blue,
                        this.speed,
                        this.repeat
                    );
                    break;

                case CommandType.Wave:
                    return String.Format(
                        "{0} {1}  R{2} G{3} B{4}  {5} {6}",
                        Enum.GetName(typeof(CommandType), this.command),
                        Enum.GetName(typeof(WaveType), this.waveType),
                        this.color.Red, this.color.Green, this.color.Blue,
                        this.speed,
                        this.repeat
                    );
                    break;

                case CommandType.Pattern:
                    return String.Format(
                        "{0} {1} {2}",
                        Enum.GetName(typeof(CommandType), this.command),
                        Enum.GetName(typeof(PatternType), this.patternType),
                        this.repeat
                    );
                    break;

                default:
                    return "undefined command";
            }
        }
    }
}
