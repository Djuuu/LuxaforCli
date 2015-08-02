using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuxaforSharp;

namespace LuxaforCli
{
    class Program    
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShouwUsage();
                return;
            }

            try
            {
                IDevice device = GetDevice();

                ArgumentsParser parser = new ArgumentsParser(args);

                foreach (CommandDefinition cmd in parser.commands)
                {
                    Console.WriteLine("Command : {0}", cmd.ToString());
                }

                new Runner(device).run(parser.commands);

                device.Dispose();
            }
            catch (Exception e)
            { 
                Console.WriteLine(e.Message);
                return;
            }
        }

        public static IDevice GetDevice()
        {
            IDeviceList list = new DeviceList();
            list.Scan();

            if (list.Count() == 0)
            {
                throw new Exception("No Luxafor device found");                
            }

            return list.First();
        }

        public static void ShouwUsage()
        {
            Console.WriteLine(@"
    LuxaforCli.exe COMMAND_GROUP...

        COMMAND_GROUP
            [color]   [TARGET]   COLOR [SPEED]
             blink    [TARGET]   COLOR [SPEED]  [REPETITIONS]
             wave     WAVETYPE   COLOR [SPEED]  [REPETITIONS]
             pattern  PATTERNID                 [REPETITIONS]

        TARGET
            all | front | back | led1 | led2 | led3 | led4 | led5 | led6
            (default : all)

        COLOR
            color name (red | green | blue | ...) | hexadecimal code | ""off""

        SPEED
            0-255

        REPETITIONS
            0-255

        WAVETYPE
            Short | Long  | OverlappingShort | OverlappingLong

        PATTERNID
            Luxafor | Police | Random1 | Random2 | Random3 | Random4 | Random5 | RainbowWave

    Examples:

        LuxaforCli.exe  red  

        LuxaforCli.exe  front dd4f00  

        LuxaforCli.exe  red   led1 green   led4 green
                        ^^^   ^^^^^^^^^^   ^^^^^^^^^^
        LuxaforCli.exe  back cyan   led1 green   led2 yellow   led3 red   blink led5 blue 20 5
                        ^^^^^^^^^   ^^^^^^^^^^   ^^^^^^^^^^^   ^^^^^^^^   ^^^^^^^^^^^^^^^^^^^^

    LED layout:

            +-------,
            |6 3    |
      back  |5 2    |  front
            |4 1    |
            |   +---'
            |   |
            |   |
            +---+
            ");
        }
    }
}
