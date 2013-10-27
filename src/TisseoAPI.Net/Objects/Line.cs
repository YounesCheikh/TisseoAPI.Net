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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TisseoAPI.Net.Objects 
{
    public class Line
    {
        private bool _filedChanged = false; 
        private Color _color;
        /// <summary>
        /// La couleur de la ligne
        /// </summary>
        public Color Color { get { return _color; } set { _color = value; _filedChanged = true; } }

        private long? _id;
        /// <summary>
        /// Le numéro Id de la ligne
        /// </summary> 
        public long? Id { get { return _id; } set { _id = value; _filedChanged = true; } }
 
        private string _name;
        /// <summary>
        /// Le nom de la ligne
        /// </summary>
        public string Name { get { return _name; } set { _name = value; _filedChanged = true; } }

        private string _shortName;
        /// <summary>
        /// Le nom ( code ) de la ligne
        /// </summary>
        public string ShortName { get { return _shortName; } set { _shortName = value; _filedChanged = true; } }

        private string _network;
        /// <summary>
        /// Operateur de transport
        /// </summary>
        public string Network { get { return _network; } set { _network = value; _filedChanged = true; } }

        private TransportMode _transportMode;
        /// <summary>
        /// Le type du moyen de transport
        /// </summary>
        [JsonProperty("transportMode")]
        public TransportMode TransportMode { get { return _transportMode; } set { _transportMode = value; _filedChanged = true; } }

        private List<Terminus> _terminuses;
        /// <summary>
        /// La liste des terminuses de la ligne
        /// </summary>
        [JsonProperty("Terminus")]
        public List<Terminus> Terminuses { get { return _terminuses; } set { _terminuses = value; _filedChanged = true; } }

        private Message _distrubMessage;
        /// <summary>
        /// Le message concerne la ligne
        /// </summary>
        public Message DisturbMessage { get { return _distrubMessage; } set { _distrubMessage = value; _filedChanged = true; } }

        public bool IsInitialized() 
        {
            return _filedChanged;
        }

    }
}
