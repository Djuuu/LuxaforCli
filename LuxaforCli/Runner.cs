using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LuxaforSharp;
using LuxaforCli.Arguments;

namespace LuxaforCli
{
    class Runner
    {
        private IDevice device;

        public Runner(IDevice device)
        {
            this.device = device;
        }

        public static LedTarget getDeviceTarget(Target target)
        {
            switch (target)
            {
                case Target.All:   return LedTarget.All;          break;
                case Target.Front: return LedTarget.AllFrontSide; break;
                case Target.Back:  return LedTarget.AllBackSide;  break;
                case Target.Led1:  return LedTarget.OfIndex(1);   break;
                case Target.Led2:  return LedTarget.OfIndex(2);   break;
                case Target.Led3:  return LedTarget.OfIndex(3);   break;
                case Target.Led4:  return LedTarget.OfIndex(4);   break;
                case Target.Led5:  return LedTarget.OfIndex(5);   break;
                case Target.Led6:  return LedTarget.OfIndex(6);   break;
                default:           return LedTarget.All;
            }
        }

        public bool run(List<CommandDefinition> commands)
        {
            foreach (CommandDefinition command in commands)
            {
                this.run(command).Wait();
            }

            return true;
        }

        public async Task<bool> run(CommandDefinition command)
        {
            switch (command.type)
            {
                case CommandType.Color:
                    return await device.SetColor(getDeviceTarget(command.target), command.color, command.speed);
                    break;

                case CommandType.Blink:
                    return await device.Blink(getDeviceTarget(command.target), command.color, command.speed, command.repeat);
                    break;

                case CommandType.Wave:
                    return await device.Wave(command.waveType, command.color, command.speed, command.repeat);
                    break;

                case CommandType.Pattern:
                    return await device.CarryOutPattern(command.patternType, command.repeat);
                    break;
            }

            return false;
        }
    }
}
