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
    /// Cette API permet d’obtenir la liste de toutes les lignes perturbées du réseau.
    /// Cela permet par exemple d’afficher un pictogramme à chaque fois que vous affichez
    /// cette ligne à l’utilisateur.
    /// </summary>
    public class TisseoLinesDisrupted
    {
        private string LINES_DISRUPTED_LIST_API = "linesDisruptedList"; 
        private string API_KEY = "None";
        private DataParser dataParser;

        /// <summary>
        /// Cette API permet d’obtenir la liste de toutes les lignes perturbées du réseau.
        /// Cela permet par exemple d’afficher un pictogramme à chaque fois que vous affichez
        /// cette ligne à l’utilisateur.
        /// </summary>
        public TisseoLinesDisrupted()
        {
            dataParser = new DataParser();
        }

        /// <summary>
        /// Cette API permet d’obtenir la liste de toutes les lignes perturbées du réseau.
        /// Cela permet par exemple d’afficher un pictogramme à chaque fois que vous affichez
        /// cette ligne à l’utilisateur.
        /// </summary>
        public TisseoLinesDisrupted(string key)
        {
            dataParser = new DataParser();
            API_KEY = key;
        }

        private bool _networkChanged = false;
        private string _network;
        /// <summary>
        /// Opérateur de transport
        /// </summary>
        public string Network { get { return _network; } set { _network = value; _networkChanged = true; } }

        private bool _contentFormatChanged = false;
        private string _contentFormat = "Text";
        /// <summary>
        /// Format du contenu des messages
        /// </summary>
        public string ContentFormat
        {
            get { return _contentFormat; }
            set { _contentFormat = value.ToLower().Equals("html") ? "html" : "text"; _contentFormatChanged = true; }
        }

        private bool _lineShortNameChanged = false;
        private string _lineShortName;
        /// <summary>
        /// Sélection des messages d’une seule ligne commerciale
        /// </summary>
        public string LineShortName
        {
            get { return _lineShortName; }
            set { _lineShortName = value; _lineShortNameChanged = true; }
        }

        /// <summary>
        /// Récupérer les données structurées de cette API
        /// </summary>
        /// <returns>Les données structurées</returns>
        public LinesDisruptedData GetMessagesData()
        {
            var results = dataParser.DownloadString(this.LINES_DISRUPTED_LIST_API, GetParmasStr());
            LinesDisruptedHelper linesDisruptedHelper =
                JsonConvert.DeserializeObject<LinesDisruptedHelper>(Utilities.ReplaceRgbByHex(results));
            List<Line> parsedLines = new List<Line>();
            foreach (var data in linesDisruptedHelper.Data)
                parsedLines.Add(data.Line);
            return new LinesDisruptedData
            {
                ExpirationDate = linesDisruptedHelper.ExpirationDate,
                Lines = parsedLines
            };
        }

        private string GetParmasStr()
        {
            string parameters = "key=" + API_KEY;
            if (_networkChanged)
                parameters += "&network=" + Network;
            if (_contentFormatChanged)
                parameters += "&contentFormat=" + ContentFormat;
            if (_lineShortNameChanged)
                parameters += "&lineShortName=" + _lineShortName;
            return parameters;
        }


    }

    public class LinesDisruptedData 
    { 
        /// <summary>
        /// La date d'expiration des données renvoyé par le serveur Tisséo
        /// </summary>
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// La liste des lignes
        /// </summary>  
        public List<Line> Lines { get; set; }
    }
}
