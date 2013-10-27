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
using TisseoAPI.Net.Helpers;
using TisseoAPI.Net.Objects;

namespace TisseoAPI.Net.Core
{
    /// <summary>
    /// Cette API permet d’obtenir la liste de toutes les lignes disponibles sur le réseau.
    /// </summary>
    public class TisseoLines
    {
        private string LINES_LIST_API = "linesList";
        private string API_KEY = "None";
        private DataParser dataParser;

        private bool _networkChanged = false;
        private string _network;
        /// <summary>
        /// Opérateur de transport
        /// </summary>
        public string Network { get { return _network; } set { _network = value; _networkChanged = true; } }

        private bool _lineIdChanged = false;
        private long _lineId;
        /// <summary>
        /// Filtre sur une seule ligne
        /// </summary>
        public long LineId { get { return _lineId; } set { _lineId = value; _lineIdChanged = true; } }

        private bool _displayTerminusChanged = false;
        private bool _displayTerminus;
        /// <summary>
        /// Retourne en plus les arrêts logiques terminus de chaque ligne
        /// </summary>
        public bool DisplayTerminus
        {
            get { return _displayTerminus; }
            set { _displayTerminus = value; _displayTerminusChanged = true; }
        }

        /// <summary>
        /// Cette API permet d’obtenir la liste de toutes les lignes disponibles sur le réseau.
        /// </summary>
        public TisseoLines()
        {
            dataParser = new DataParser();
        }

        /// <summary>
        /// Cette API permet d’obtenir la liste de toutes les lignes disponibles sur le réseau.
        /// </summary>
        /// <param name="key">API key</param>
        public TisseoLines(string key)
        {
            API_KEY = key;
            dataParser = new DataParser();
        }


        /// <summary>
        /// Envoi la demande des données au serveur de Tisséo
        /// </summary>
        /// <returns>Les données des places Tisséo</returns>
        public LinesData GetLinesData()
        {
            var results = dataParser.UploadString(this.LINES_LIST_API, GetParmasStr());
            LinesHelper linesHelper = JsonConvert.DeserializeObject<LinesHelper>(Utilities.ReplaceRgbByHex(results));
            return new LinesData { ExpirationDate = linesHelper.ExpirationDate, Lines = linesHelper.Data.Lines };
        }

        private string GetParmasStr()
        {
            string parameters = "key=" + API_KEY;
            if (_networkChanged)
                parameters += "&network=" + _network;
            if (_lineIdChanged)
                parameters += "&lineId=" + _lineId;
            if (_displayTerminusChanged)
                parameters += String.Format("&displayTerminus={0}", _displayTerminus ? 1 : 0);
            return parameters;
        }
    }

    /// <summary>
    /// Obtenir les données de TisseoPlaces 
    /// </summary>
    public class LinesData
    {
        /// <summary>
        /// La date d'expiration des données renvoyé par le serveur Tisséo
        /// </summary>
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// La liste des places
        /// </summary> 
        public List<Line> Lines { get; set; }
    }
}
