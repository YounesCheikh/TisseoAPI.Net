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
using System.Xml;
using TisseoAPI.Net.Helpers;
using TisseoAPI.Net.Objects;

namespace TisseoAPI.Net.Core
{
    /// <summary>
    /// Cette API permet d’obtenir la liste des messages d’information trafic et d’information 
    /// commerciale des réseau de transport (pour le moment Tisséo uniquement).
    /// Ces informations sont accessibles depuis la page d’accueil de tisseo.fr.
    /// </summary>
    public class TisseoMessages
    {
        private string MESSAGES_LIST_API = "messagesList";
        private string API_KEY = "None";
        private DataParser dataParser;

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

        private bool _displayImportantOnlyChanged = false;
        private bool _displayImportantOnly;
        /// <summary>
        /// N’affiche que les messages important (présent sur la home page de tisseo.fr)
        /// </summary>
        public bool DisplayImportantOnly
        {
            get { return _displayImportantOnly; }
            set { _displayImportantOnly = value; _displayImportantOnlyChanged = true; }
        }

        /// <summary>
        /// Cette API permet d’obtenir la liste des messages d’information trafic et d’information 
        /// commerciale des réseau de transport (pour le moment Tisséo uniquement).
        /// Ces informations sont accessibles depuis la page d’accueil de tisseo.fr.
        /// </summary>
        public TisseoMessages()
        {
            dataParser = new DataParser();
        }

        /// <summary>
        /// Cette API permet d’obtenir la liste des messages d’information trafic et d’information 
        /// commerciale des réseau de transport (pour le moment Tisséo uniquement).
        /// Ces informations sont accessibles depuis la page d’accueil de tisseo.fr.
        /// </summary>
        /// <param name="key">la clé de l'API Tisséo</param>
        public TisseoMessages(string key)
        {
            dataParser = new DataParser();
            API_KEY = key;
        }

        /// <summary>
        /// Récupérer les données 
        /// </summary>
        /// <returns></returns>
        public MessagesData GetMessagesData() 
        {
            var results = dataParser.DownloadString(this.MESSAGES_LIST_API, GetParmasStr());
            MessagesHelper messagesHelper =
                JsonConvert.DeserializeObject<MessagesHelper>(Utilities.ReplaceRgbByHex(results));
            return new MessagesData
            {
                ExpirationDate = messagesHelper.ExpirationDate,
                Messages = GetRepairedMessages(messagesHelper)
            };
        }

        private static List<Message> GetRepairedMessages(MessagesHelper messagesHelper)
        {
            List<Message> messages = new List<Message>();

            foreach (var data in messagesHelper.Data)
            {
                Message m = data.Message;
                m.Lines = new List<Line>();
                if (data.Lines != null)
                {
                    foreach (var v in data.Lines)
                        m.Lines.Add(v.Line);
                }
                messages.Add(m);
            }
            return messages;
        }

        private string GetParmasStr()
        {
            string parameters = "?key=" + API_KEY;
            if (_networkChanged)
                parameters += "&network=" + Network;
            if (_contentFormatChanged)
                parameters += "&contentFormat=" + ContentFormat;
            if (_displayImportantOnlyChanged)
                parameters += "&displayImportantOnly=" + (DisplayImportantOnly?1:0);
            return parameters;
        }
    }

    /// <summary>
    /// Obtenir les données de TisseoPlaces 
    /// </summary>
    public class MessagesData 
    { 
        /// <summary>
        /// La date d'expiration des données renvoyé par le serveur Tisséo
        /// </summary>
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// La liste des places
        /// </summary> 
        public List<Message> Messages { get; set; }
    }
}
