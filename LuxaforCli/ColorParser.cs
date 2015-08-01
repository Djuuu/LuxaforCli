using System;
using System.Drawing;

namespace LuxaforCli
{
    public class ColorParser
    {
        private string input;

        public Color color { get; private set; }

        public string error { get; private set; }

        public ColorParser(string input)
        {
            this.input = input;

            if (!this.parseByName()) 
            {
                this.parseByHex();
            }
        }

        private bool parseByName()
        {
            string knownColorName = findKnownColorName(input);

            if (knownColorName != null)
            {
                this.color = Color.FromName(knownColorName);
                return true;
            }

            this.error = "Unknown color name : " + this.input;

            return false;
        }

        public static string findKnownColorName(string input)
        {
            foreach (string knownColorName in Enum.GetNames(typeof(KnownColor)))
            {
                if (knownColorName.ToLower() == input.ToLower())
                {
                    return knownColorName;
                }
            }

            return null;
        }

        private bool parseByHex()
        {
            string colorCode = this.input;

            if (!colorCode[0].Equals('#'))
            {
                colorCode = '#' + colorCode;
            }

            try
            {
                this.color = ColorTranslator.FromHtml(colorCode);
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
