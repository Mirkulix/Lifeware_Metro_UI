using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lifeware_Metro_UI
{
    public class NummerFormat
    {

        public string nummer;

        public NummerFormat()
        {

        }

        public NummerFormat(string strTelefonnummer)
        {
            nummer = strTelefonnummer;

            if (!string.IsNullOrEmpty(nummer))
            {
                if (nummer.StartsWith("+"))
                    nummer = nummer.Replace("+", "00").Replace("(0)", string.Empty);
                Regex regex = new Regex(@"[^0-9]+");
                nummer = regex.Replace(nummer, string.Empty);

            }

        }

        public override string ToString()
        {
            return nummer;
        }


    }

}

