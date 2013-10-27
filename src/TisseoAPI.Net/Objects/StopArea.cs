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
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TisseoAPI.Net.Objects 
{
    /// <summary>
    /// Cette API permet d’obtenir des listes de zones d’arrêts.
    /// L’ensemble des zones d’arrêts d’un réseau, d’une zone géographique, ou d’une ligne.
    /// </summary>
    public class StopArea
    {
        private bool _filedChanged = false;
        private long? _Id;
        /// <summary>
        /// Le id de l'arret
        /// </summary>
        public long? Id { get { return _Id; } set { _Id = value; _filedChanged = true; } }

        private string _CityName;
        /// <summary>
        /// Le nom de la ville
        /// </summary>
        public string CityName { get { return _CityName; } set { _CityName = value; _filedChanged = true; } }

        private long? _cityId;
        /// <summary>
        /// L'id de la ville
        /// </summary>
        public long? CityId { get { return _cityId; } set { _cityId = value; _filedChanged = true; } }

        private string _name;
        /// <summary>
        /// Le nom de l'arret
        /// </summary>
        public string Name { get { return _name; } set { _name = value; _filedChanged = true; } }

        private float? _x;
        /// <summary>
        /// Longtitude de l'arret
        /// </summary>
        public float? X { get { return _x; } set { _x = value; _filedChanged = true; } }

        private float? _y;
        /// <summary>
        /// Latitude de l'arret
        /// </summary>
        public float? Y { get { return _y; } set { _y = value; _filedChanged = true; } }

        private List<Line> _lines;
        [JsonProperty("line")]
        public List<Line> Lines { get { return _lines; } set { _lines = value; _filedChanged = true; _lines.RemoveAll(l => !l.IsInitialized()); } }

        public bool IsInitialized()
        {
            return _filedChanged;
        }

        
    }
}
