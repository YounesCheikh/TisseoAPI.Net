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
    public class TisseoStopPoints
    {
        private string STOP_POINTS_LIST_API = "stopPointsList";
        private string API_KEY = "None";
        private DataParser dataParser;


        private bool _networkChanged = false;
        private string _network;
        /// <summary>
        /// Opérateur de transport
        /// </summary>
        public string Network { get { return _network; } set { _network = value; _networkChanged = true; } }

        private bool _sridChanged = false;
        private int _srid;
        /// <summary>
        /// Le srid permet à la fois de modifier le système de coordonnées des XY des zones d’arrêts
        /// et de préciser dans quel référentiel la bbox est exprimée.
        /// Numéro SRID du référentiel de projection spatial. Voir http://en.wikipedia.org/wiki/SRID
        /// </summary>
        public int Srid { get { return _srid; } set { _srid = value; _sridChanged = true; } }

        private bool _bboxChanged = false;
        private BBox _bbox;
        /// <summary>
        /// Filtre pour les arrêts dont les données GPS sont comprises dans ce bounding box.
        /// Format attendu pour une bbox: longitude pt A, latitude pt A, longitude point B, latitude point B
        /// </summary>
        public BBox BBox { get { return _bbox; } set { _bbox = value; _bboxChanged = true; } }

        private bool _sortByDistanceChanged = false;
        private bool _sortByDistance;
        /// <summary>
        /// Tri résultats selon la distance au centre de la bounding box
        /// </summary>
        public bool SortByDistance { get { return _sortByDistance; } set { _sortByDistance = value; _sortByDistanceChanged = true; } }

        private bool _numberChanged = false;
        private int _number;
        /// <summary>
        /// Filtre sur le nb maxi de résultats à retourner par type de lieu
        /// </summary>
        public int Number { get { return _number; } set { _number = value; _numberChanged = true; } }

        private bool _displayDestinationsChanged = false;
        private bool _displayDestinations;
        /// <summary>
        /// Retourne en plus les destinations de chaque poteau
        /// </summary>
        public bool DisplayDestinations { get { return _displayDestinations; } set { _displayDestinations = value; _displayDestinationsChanged = true; } }

        private bool _displayLinesChanged = false;
        private bool _displayLines;
        /// <summary>
        /// Retourne en plus les lignes de chaque arrêt
        /// </summary>
        public bool DisplayLines { get { return _displayLines; } set { _displayLines = value; _displayLinesChanged = true; } }

        private bool _displayCoordXYChanged = false;
        private bool _displayCoordXY;
        /// <summary>
        /// Retourne en plus les coordonnées de chaque arrêt (poteau d’arrêt et arrêt logique)
        /// </summary>
        public bool DisplayCoordXY { get { return _displayCoordXY; } set { _displayCoordXY = value; _displayCoordXYChanged = true; } }

        private bool _lineIdChanged = false;
        private long _lineId;
        /// <summary>
        /// Filtre sur les arrêts de la ligne uniquement
        /// </summary>
        public long LineId { get { return _lineId; } set { _lineId = value; _lineIdChanged = true; } }

        private bool _stopAreaIdChanged = false;
        private long _stopAreaId;
        /// <summary>
        /// Filtre sur la zone d’arrêt uniquement definie
        /// </summary>
        public long StopAreaId { get { return _stopAreaId; } set { _stopAreaId = value; _stopAreaIdChanged = true; } }

        /// <summary>
        /// Cette API permet d’obtenir des listes d’arrêts.
        /// L’ensemble des arrêts d’un réseau, d’une zone géographique, ou d’une zone d’arrêt.
        /// </summary>
        public TisseoStopPoints()
        {
            dataParser = new DataParser();
        }

        /// <summary>
        /// Cette API permet d’obtenir des listes d’arrêts.
        /// L’ensemble des arrêts d’un réseau, d’une zone géographique, ou d’une zone d’arrêt.
        /// </summary>
        /// <param name="key">La clé de l'API tisséo </param>
        public TisseoStopPoints(string key)
        {
            dataParser = new DataParser();
            this.API_KEY = key;
        }

        public StopPointsData GetStopPontsData()
        {
            var results = dataParser.UploadString(this.STOP_POINTS_LIST_API, GetParmasStr());
            StopPointsHelper stopPointsHelper = JsonConvert.DeserializeObject<StopPointsHelper>(Utilities.ReplaceRgbByHex(results));
            return new StopPointsData { ExpirationDate = stopPointsHelper.ExpirationDate, StopPoints = stopPointsHelper.Data.StopPoints };
        }

        private string GetParmasStr()
        {
            string parameters = "key=" + API_KEY;
            if (_networkChanged)
                parameters += "&network=" + _network;
            if (_lineIdChanged)
                parameters += "&lineId=" + _lineId;
            if (_sridChanged)
                parameters += "&srid=" + _srid;
            if (_bboxChanged)
                parameters += String.Format("&bbox={0},{1},{2},{3}", _bbox.A.X, _bbox.A.Y, _bbox.B.X, _bbox.B.Y);
            if (_displayLinesChanged)
                parameters += "&displayLines=" + (_displayLines ? 1 : 0);
            if (_displayCoordXYChanged)
                parameters += "&displayCoordXY=" + (_displayCoordXY ? 1 : 0);
            if (_numberChanged)
                parameters += "&number=" + _number;
            if (_sortByDistanceChanged)
                parameters += "&sortByDistance=" + (_sortByDistance ? 1 : 0);
            if (_displayDestinationsChanged)
                parameters += "&displayDestinations=" + (_displayDestinations ? 1 : 0);
            if (_stopAreaIdChanged)
                parameters += "&stopAreaId=" + _stopAreaId;
            return parameters;
        }
    }

    /// <summary>
    /// Obtenir les données de TisseoStopPoints 
    /// </summary>
    public class StopPointsData
    {
        /// <summary>
        /// La date d'expiration des données renvoyé par le serveur Tisséo
        /// </summary>
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// La liste des points d'arret
        /// </summary> 
        public List<StopPoint> StopPoints { get; set; } 
    }
}
