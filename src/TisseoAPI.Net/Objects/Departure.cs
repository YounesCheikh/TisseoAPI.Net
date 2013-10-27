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
    public class Departure
    {
        private bool _filedChanged = false;
        private DateTime _dateTime;

        /// <summary>
        /// Le temps de départ
        /// </summary>
        public DateTime DateTime { get { return _dateTime; } set { _dateTime = value; _filedChanged = true; } }

        private bool _realTime;
        /// <summary>
        /// Temps réel ? 
        /// </summary>
        public bool RealTime { get { return _realTime; } set { _realTime = value; _filedChanged = true; } }

        private Line _line;
        /// <summary>
        /// La ligne (Trams ou Bus )
        /// </summary>
        public Line Line { get { return _line; } set { _line = value; _filedChanged = true; } }

        private List<StopArea> _destinantions;
        /// <summary>
        /// Destinantion du prochain départ
        /// </summary>
        [JsonProperty("destination")]
        public List<StopArea> Destinations
        {
            get { return _destinantions; }
            set { _destinantions = value; _destinantions.RemoveAll(d => !d.IsInitialized()); _filedChanged = true; }
        }

        public bool IsInitialized()
        {
            return _filedChanged;
        }
    }


}
