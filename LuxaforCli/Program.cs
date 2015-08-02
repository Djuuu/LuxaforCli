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
        //   _____
        //  |6 3  |
        //  |5 2  |
        //  |4 1 _|
        //  |   |
        //  |___|
        //
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("LuxaforCLI needs at least 1 argument");
                return;
            }

            // parsing arguments
            ArgumentsParser argParser = new ArgumentsParser(args);

            // device
            IDeviceList list = new DeviceList();
            list.Scan();
            if (list.Count() == 0) 
            {
                Console.WriteLine("Device not found.");
                return;
            }
            IDevice device = list.First();

            // running commands

            Runner runner = new Runner(device);
            runner.run(argParser.commands);

            device.Dispose();
        }
    }
}
