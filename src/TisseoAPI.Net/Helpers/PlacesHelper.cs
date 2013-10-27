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
using TisseoAPI.Net.Objects;

namespace TisseoAPI.Net.Helpers
{
    class PlacesHelper
    {
        public DateTime ExpirationDate { get; set; }
        [JsonProperty("PlacesList")] 
        public PlacesHelerData Data { get; set; } 

        public PlacesHelper() { }
    }

    class PlacesHelerData
    {
        private List<Place> _places;
        [JsonProperty("Place")]
        public List<Place> Places
        {
            get { return _places; }
            set
            {
                _places = value;
                _places.RemoveAll(p => !p.IsInitialized());
            }
        }
        public PlacesHelerData() { } 
    }
}
