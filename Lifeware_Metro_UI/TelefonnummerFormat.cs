using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lifeware_Metro_UI
{



    public class TelefonnummerFormat
    {

        public string nummer;
        public string format;

        public TelefonnummerFormat()
        {

        }
        public TelefonnummerFormat(string Telefonnummer, string Nummernformat)
        {
            nummer = Telefonnummer;
            format = Nummernformat;


            if (nummer == "")
            {
                // If phone format is empty, code will use default format (###) ###-####
                nummer = "(###) ###-####";
            }

            // First, remove everything except of numbers
            Regex regexObj = new Regex(@"[^\d]");
            nummer = regexObj.Replace(nummer, "");

            // Second, format numbers to phone string 
            if (nummer.Length > 0)
            {
                nummer = Convert.ToInt64(nummer).ToString(format);
            }

            
        }

        public override string ToString()
        {
            return nummer;
        }
    }





}
