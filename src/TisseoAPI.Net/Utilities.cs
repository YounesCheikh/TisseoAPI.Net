/*
 * Author: Younes Cheikh
 * Email: younes [dot] cheikh [at] gmail [dot] com
 * Visit: http://cyounes.com/ 
 * Cette oeuvre, création, site ou texte est sous licence Creative Commons  Attribution
 * - Pas d’Utilisation Commerciale 
 * - Partage dans les Mêmes Conditions 3.0 France. 
 * Pour accéder à une copie de cette licence, 
 * merci de vous rendre à l'adresse suivante
 * http://creativecommons.org/licenses/by-nc-sa/3.0/fr/ 
 * ou envoyez un courrier à Creative Commons, 
 * 444 Castro Street, Suite 900, Mountain View, California, 94041, USA.
 */

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TisseoAPI.Net.Objects;
namespace TisseoAPI.Net
{
    class Utilities
    {
        /// <summary>
        /// Parse color from string.
        /// </summary>
        /// <param name="rgb">color rgb string format "(r,g,b)" </param>
        /// <returns>Parsed color if parse succeeded else new color()</returns>
        [ObsoleteAttribute("This method is obsolete. Call GetColorFromRgbString instead.", true)] 
        public static Color ParseColorFromString(string rgb)
        {
            Regex RgbValuePattern = new Regex(@"(?<r>\d{1,3}) ?, ?(?<g>\d{1,3}) ?, ?(?<b>\d{1,3})",
                RegexOptions.Compiled | RegexOptions.ExplicitCapture);

            var match = RgbValuePattern.Match(rgb);

            if (match.Success)
            {
                int r = Int32.Parse(match.Groups["r"].Value, NumberFormatInfo.InvariantInfo);
                int g = Int32.Parse(match.Groups["g"].Value, NumberFormatInfo.InvariantInfo);
                int b = Int32.Parse(match.Groups["b"].Value, NumberFormatInfo.InvariantInfo);
                return Color.FromArgb(r, g, b);
            }
            else
            {
                Console.WriteLine("Error parsing color from string {0}", rgb);
                return new Color();
            }
        }

        /// <summary>
        /// Replace all (r,g,b) occurances in result string by an hexa color code 
        /// </summary>
        /// <param name="parsedString">The string</param>
        /// <returns>new string replaced rgb by hex</returns>
        public static string ReplaceRgbByHex(string parsedString)
        {
            string pattern = @"\((0*([0-9]{1,2}|1[0-9]{2}|2[0-4][0-9]|25[0-5]),){2}0*([0-9]{1,2}|1[0-9]{2}|2[0-4][0-9]|25[0-5])\)";
            string input = parsedString;

            MatchCollection matches = Regex.Matches(input, pattern);
            foreach (Match m in matches)
            {
                input = input.Replace(m.Value, GetColorFromRgbString(m.Value).ToString());
            }

            //TODO: CREATE NEW JSONCONVERTER TO CONVERT YES|NO Values to Boolean VaLEues
            matches = Regex.Matches(input, @"""yes""");
            foreach (Match m in matches)
            {
                input = input.Replace(m.Value, "true");
            }
            matches = Regex.Matches(input, @"""no""");
            foreach (Match m in matches)
            {
                input = input.Replace(m.Value, "false");
            }
            return input;
        }


        private static string GetColorFromRgbString(string str)
        {
            var copy = str;
            if (copy.StartsWith("("))
                copy = copy.Substring(1);
            if (copy.EndsWith(")"))
                copy = copy.Substring(0, copy.Length - 1);

            var rebStrList = copy.Split(',');
            if (rebStrList.Count() == 3)
            {
                Color retColor = Color.FromArgb(
                    Convert.ToInt32(rebStrList[0]),
                    Convert.ToInt32(rebStrList[1]),
                    Convert.ToInt32(rebStrList[2])
                    );
                return ColorTranslator.ToHtml(retColor);
            }

            return null;
        }


    }

    public class StrBoolConverter : JsonConverter
    {
        public override bool CanWrite { get { return false; } }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value;

            if (value == null || String.IsNullOrWhiteSpace(value.ToString()))
            {
                return false;
            }

            if ("yes".Equals(value.ToString().ToLower()))
            {
                return true;
            }

            return false;
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(String) || objectType == typeof(Boolean))
            {
                return true;
            }
            return false;
        }
    }
}
