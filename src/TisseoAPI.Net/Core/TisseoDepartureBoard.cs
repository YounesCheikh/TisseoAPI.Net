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
    /// Cette API permet d’obtenir la liste des prochains passages à un poteau d’arrêt.
    /// <b>Ne concerne que les Bus et Tramway. Aucune information ne sera retournée pour 
    /// le métro ou les TAD.</b>
    /// Un paramètre permet de préciser si l’on souhaite obtenir les horaires théoriques 
    /// de la fiche horaires ou des horaires « temps réel »
    /// c'est-à-dire re-estimés en fonction des conditions de trafic.
    /// Lorsque le choix d’horaires temps réel a été fait, la réponse précise pour chaque 
    /// horaire si il a pu être re-estimé (realTime="True") 
    /// ou si l’horaire reste celui indicatif de la fiche horaire (realTime="False").
    /// </summary>
    public class TisseoDepartureBoard
    {
        private string DEPARTURE_BOARD_API = "departureBoard";
        private string API_KEY;
        private DataParser dataParser;

        private bool _operatorCodeChanged = false;
        private int _operatorCodeValue;
        /// <summary>
        /// Désigne le code opérateur
        /// </summary>
        public int OperatorCodeValue { get { return _operatorCodeValue; } set { _operatorCodeValue = value; _operatorCodeChanged = true; } }

        private bool _stopPointIdChanged = false;
        private long _stopPointId;
        /// <summary>
        /// Désigne le numéro de l’arrêt physique ou poteau
        /// </summary>
        public long StopPointId { get { return _stopPointId; } set { _stopPointId = value; _stopPointIdChanged = true; } }

        private bool _networkChanged = false;
        private string _network;
        /// <summary>
        /// Opérateur de transport
        /// </summary>
        public string Network { get { return _network; } set { _network = value; _networkChanged = true; } }

        private bool _numberChanged = false;
        private int _number;
        /// <summary>
        /// Filtre sur le nb maxi de résultats à retourner
        /// </summary>
        public int Number { get { return _number; } set { _number = value; _numberChanged = true; } }

        private bool _lineIdChanged = false;
        private long _lineId;
        /// <summary>
        /// Filtre les arrêts de la ligne uniquement
        /// </summary>
        public long LineId { get { return _lineId; } set { _lineId = value; _lineIdChanged = true; } }

        private bool _displayRealTimeChanged = false;
        private bool _displayRealTime;
        /// <summary>
        /// Permet de spécifier si on souhaite des horaires « théoriques » False ou « temps réels » True
        /// </summary>
        public bool DisplayRealTime { get { return _displayRealTime; } set { _displayRealTime = value; _displayRealTimeChanged = true; } }

        /// <summary>
        /// Cette API permet d’obtenir la liste des prochains passages à un poteau d’arrêt.
        /// <b>Ne concerne que les Bus et Tramway. Aucune information ne sera retournée pour 
        /// le métro ou les TAD.</b>
        /// Un paramètre permet de préciser si l’on souhaite obtenir les horaires théoriques 
        /// de la fiche horaires ou des horaires « temps réel »
        /// c'est-à-dire re-estimés en fonction des conditions de trafic.
        /// Lorsque le choix d’horaires temps réel a été fait, la réponse précise pour chaque 
        /// horaire si il a pu être re-estimé (realTime="True") 
        /// ou si l’horaire reste celui indicatif de la fiche horaire (realTime="False").
        /// </summary>
        /// <param name="operatorCode">Le numéro de l'arret</param>
        public TisseoDepartureBoard(int operatorCode, string key)
        {
            dataParser = new DataParser();
            this.OperatorCodeValue = operatorCode;
            this.API_KEY = key;
        }

        /// <summary>
        /// Cette API permet d’obtenir la liste des prochains passages à un poteau d’arrêt.
        /// <b>Ne concerne que les Bus et Tramway. Aucune information ne sera retournée pour le métro ou les TAD.</b>
        /// Un paramètre permet de préciser si l’on souhaite obtenir les horaires
        /// théoriques de la fiche horaires ou des horaires « temps réel »
        /// c'est-à-dire re-estimés en fonction des conditions de trafic.
        /// Lorsque le choix d’horaires temps réel a été fait, 
        /// la réponse précise pour chaque horaire si il a pu être re-estimé (realTime="True") 
        /// ou si l’horaire reste celui indicatif de la fiche horaire (realTime="False").
        /// </summary>
        /// <param name="stopPointId">L'id de l'arret</param>
        public TisseoDepartureBoard(long stopPointId, string key)
        {
            dataParser = new DataParser();
            this.StopPointId = stopPointId;
            this.API_KEY = key;
        }

        /// <summary>
        /// Cette API permet d’obtenir la liste des prochains passages à un poteau d’arrêt.
        /// <b>Ne concerne que les Bus et Tramway. Aucune information ne sera retournée 
        /// pour le métro ou les TAD.</b>
        /// Un paramètre permet de préciser si l’on souhaite obtenir les horaires théoriques 
        /// de la fiche horaires ou des horaires « temps réel »
        /// c'est-à-dire re-estimés en fonction des conditions de trafic.
        /// Lorsque le choix d’horaires temps réel a été fait, la réponse précise pour chaque
        /// horaire si il a pu être re-estimé (realTime="True") 
        /// ou si l’horaire reste celui indicatif de la fiche horaire (realTime="False").
        /// </summary>
        /// <param name="stopPointId">L'id de l'arret</param>
        /// /// <param name="operatorCode">Le numéro de l'arret</param>
        public TisseoDepartureBoard(int operatorCode, long stopPointId, string key)
        {
            dataParser = new DataParser();
            this.OperatorCodeValue = operatorCode;
            this.StopPointId = stopPointId;
            this.API_KEY = key;
        }

        /// <summary>
        /// Envoi la demande des données au serveur de Tisséo
        /// </summary>
        /// <returns>Les données des prochains départs Tisséo</returns>
        public DepartureBoardData GetDepartureBoardData() 
        {
            var results = dataParser.UploadString(this.DEPARTURE_BOARD_API, GetParmasStr());
            //results = results.Replace("yes", "true");
            //results = results.Replace("no", "false");
            DepartureBoardHelper departureBoardHelper =
                JsonConvert.DeserializeObject<DepartureBoardHelper>(Utilities.ReplaceRgbByHex(results));
            return new DepartureBoardData
            {
                ExpirationDate = departureBoardHelper.ExpirationDate,
                Departures = departureBoardHelper.Data.Departures,
                StopPoint = departureBoardHelper.Data.StopPoint,
                StopArea = departureBoardHelper.Data.StopArea
            };
        }

        private string GetParmasStr()
        {
            string parameters = "key=" + API_KEY;
            if (_networkChanged)
                parameters += "&network=" + _network;
            if (_lineIdChanged)
                parameters += "&lineId=" + _lineId;
            if (_operatorCodeChanged)
                parameters += "&operatorCode=" + _operatorCodeValue;
            if (_stopPointIdChanged )
                parameters += "&stopPointId=" + _stopPointId;
            if (_numberChanged)
                parameters += "&number=" + _number;
            if (_displayRealTimeChanged)
                parameters += "&displayRealTime=" + (_displayRealTime ? 1 : 0);
            return parameters;
        }
     }

    /// <summary>
    /// Obtenir les données de TisseoPlaces 
    /// </summary>
    public class DepartureBoardData
    {
        /// <summary>
        /// La date d'expiration des données renvoyé par le serveur Tisséo
        /// </summary>
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// La liste des departs 
        /// </summary> 
        public List<Departure> Departures { get; set; }

        public StopPoint StopPoint { get; set; }
        public StopArea StopArea { get; set; }
    }
}
