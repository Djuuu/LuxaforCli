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

        private void parse()
        {
            if (this.parseSystemName()) { return; }
            if (this.parseCustomName()) { return; }
            this.parseHexCode();
        }

        #region Parsers

        private bool parseSystemName()
        {
            KnownColor knownColor;

            if (Enum.TryParse(input, true, out knownColor) && Enum.IsDefined(typeof(KnownColor), knownColor))
            {
                this.color = SystemToLuxColor(System.Drawing.Color.FromKnownColor(knownColor));
                return true;
            }

            return false;
        }

        private bool parseCustomName()
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

        #endregion

        private static LuxaforSharp.Color SystemToLuxColor(System.Drawing.Color systemColor)
        {
            return new LuxaforSharp.Color(systemColor.R, systemColor.G, systemColor.B);
        }

        public static string ColorToHex(LuxaforSharp.Color color)
        {
            return String.Format(
                "#{0}{1}{2}", 
                color.Red.ToString("X2"), 
                color.Green.ToString("X2"), 
                color.Blue.ToString("X2")
            );
        }
    }
}
