using System;
using System.Drawing;
using LuxaforSharp;

namespace LuxaforCli
{
    public class ColorParser
    {
        private string input;

        public LuxaforSharp.Color color { get; private set; }

        public string error { get; private set; }

        public ColorParser(string input)
        {
            this.input = input;

            this.parse();
        }

        private static LuxaforSharp.Color SystemToLuxColor(System.Drawing.Color systemColor)
        {
            return new LuxaforSharp.Color(systemColor.R, systemColor.G, systemColor.B);
        }

        private void parse()
        {
            KnownColor knownColor;

            if (Enum.TryParse(input, true, out knownColor) && Enum.IsDefined(typeof(KnownColor), knownColor))
            {
                this.color = SystemToLuxColor(System.Drawing.Color.FromKnownColor(knownColor));
                return;
            }

            this.parseCustomNames();    

            this.parseHexCode();
        }

        private bool parseCustomNames()
        {
            switch (input.ToLower())
            {
                case "off":
                    this.color = new LuxaforSharp.Color(0, 0, 0);
                    return true;
                    break;
            }

            return false;
        }

        private bool parseHexCode()
        {
            string colorCode = this.input;

            if (!colorCode[0].Equals('#'))
            {
                colorCode = '#' + colorCode;
            }

            try
            {
                this.color = SystemToLuxColor(ColorTranslator.FromHtml(colorCode));
                return true;
            }
            catch (Exception e)
            {
                this.error = "Invalid color code : " + this.input;
            }

            return false;
        }
    }
}
