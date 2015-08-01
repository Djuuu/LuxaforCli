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
            if (args.Length != 1)
            {
                Console.WriteLine("LuxaforCLI needs at least 1 argument");
                return;
            }
            
            // color
            ColorParser colorParser = new ColorParser(args[0]);
            Color color = colorParser.color;
            if (color  == null)
            {
                Console.WriteLine(colorParser.error);                
                return;
            }

            // device
            IDeviceList list = new DeviceList();
            list.Scan();
            if (list.Count() == 0) 
            {
                Console.WriteLine("Device not found.");
                return;
            }

            IDevice device = list.First();


            // device action
            device.SetColor(LedTarget.All, color, null, 1000);



            // seems needed
            System.Threading.Thread.Sleep(25);

            device.Dispose();
        }
    }
}
