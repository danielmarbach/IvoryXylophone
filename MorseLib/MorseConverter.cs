using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorseLib
{
    public class MorseConverter
    {
        static readonly Dictionary<string, string> conversions;
        static readonly Dictionary<string, string> reverseConversions;
        

        static MorseConverter()
        {
            conversions = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            conversions.Add("a", ".-");
            conversions.Add("b", "-...");
            conversions.Add("c", "-.-.");
            conversions.Add("d", "-..");
            conversions.Add("e", ".");
            conversions.Add("f", "..-.");
            conversions.Add("g", "--.");
            conversions.Add("h", "....");
            conversions.Add("i", "..");
            conversions.Add("j", ".---");
            conversions.Add("k", "-.-");
            conversions.Add("l", ".-..");
            conversions.Add("m", "--");
            conversions.Add("n", "-.");
            conversions.Add("o", "---");
            conversions.Add("p", ".--.");
            conversions.Add("q", "--.-");
            conversions.Add("r", ".-.");
            conversions.Add("s", "...");
            conversions.Add("t", "-");
            conversions.Add("u", "..-");
            conversions.Add("v", "...-");
            conversions.Add("w", ".--");
            conversions.Add("x", "-..-");
            conversions.Add("y", "-.--");
            conversions.Add("z", "--..");
            conversions.Add(" ", "-...-.-");

            reverseConversions = conversions.ToDictionary(pair => pair.Value, pair => pair.Key,
                StringComparer.InvariantCultureIgnoreCase);
        }

        public static string FromMorse(string[] morseChars)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string morseChar in morseChars)
            {
                string realChar;
                if (reverseConversions.TryGetValue(morseChar, out realChar))
                {
                    sb.Append(realChar);
                }
                else
                {
                    sb.Append(morseChar);
                }
            }
            return sb.ToString();
        }

        public static string[] ToMorse(string text)
        {
            List<string> morseChars = new List<string>();
            foreach (char realChar in text)
            {
                string morseChar;
                if (conversions.TryGetValue(realChar.ToString(), out morseChar))
                {
                    morseChars.Add(morseChar);
                }
                else
                {
                    morseChars.Add(realChar.ToString());
                }
            }
            return morseChars.ToArray();
        }
    }
}
