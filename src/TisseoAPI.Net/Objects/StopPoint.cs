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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TisseoAPI.Net.Objects
{
    public class StopPoint
    {
        /// <summary>
        /// The Stop Id
        /// </summary>
        public long? Id { get; set; }
        /// <summary>
        /// The Stop Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Longtitude X
        /// </summary>
        public float? X { get; set; }
        /// <summary>
        /// Latitude Y
        /// </summary>
        public float? Y { get; set; }

        /// <summary>
        /// La valeur de l'operatorCode
        /// </summary>
        [JsonProperty("operatorCode")]
        public int OperatorCodeValue { get; set; }
        /// <summary>
        /// The Stop OperatorCode
        /// </summary>
        [JsonProperty("operatorCodes")]
        public List<OperatorCodesData> OperatorCodes { get; set; }
        private List<StopArea> _destinations;
        [JsonProperty("destinations")]
        public List<StopArea> Destinations
        {
            get { return _destinations; }
            set { _destinations = value; _destinations.RemoveAll(d => !d.IsInitialized()); }
        }

        [JsonProperty("stopArea")]
        public StopArea StopArea { get; set; }


        
    }

    public class OperatorCodesData
    {
        [JsonProperty("operatorCode")]
        public OperatorCode OperatorCode { get; set; }
    }
}
