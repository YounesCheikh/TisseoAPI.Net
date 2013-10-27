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
    public class TisseoStopAreas
    {
        private string STOP_AREAS_LIST_API = "stopAreasList";
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

        private bool _displayLinesChanged = false;
        private bool _displayLines;
        /// <summary>
        /// Retourne en plus les lignes de chaque arrêt
        /// </summary>
        public bool DisplayLines { get { return _displayLines; } set { _displayLines = value; _displayLinesChanged = true; } }

        private bool _displayCoordXYChanged = false;
        private bool _displayCoordXY;
        /// <summary>
        /// Retourne en plus les coordonnées de chaque arrêt. C’est le barycentre des arrêts de la zone
        /// </summary>
        public bool DisplayCoordXY { get { return _displayCoordXY; } set { _displayCoordXY = value; _displayCoordXYChanged = true; } }

        private bool _lineIdChanged = false;
        private long _lineId;
        /// <summary>
        /// Filtre sur une seule ligne.
        /// lineId est l’id récupéré par la requête de liste des lignes.
        /// Si lineId est passé en plus avec terminusId, dans ce cas le filtre porte sur tous les itinéraires de cette ligne ayant ce terminusId.
        /// </summary>
        public long LineId { get { return _lineId; } set { _lineId = value; _lineIdChanged = true; } }

        private bool _terminusIdChanged = false;
        private long _terminusId; 
        /// <summary>
        /// Filtre sur les zones d’arrêts arrivant et partant de ce terminus uniquement.
        /// Si lineId est passé en plus avec terminusId, dans ce cas le filtre porte sur tous les itinéraires de cette ligne ayant ce terminusId.
        /// </summary>
        public long TerminusId { get { return _terminusId; } set { _terminusId = value; _terminusIdChanged = true; } }

        /// <summary>
        /// Cette API permet d’obtenir des listes de zones d’arrêts.
        /// L’ensemble des zones d’arrêts d’un réseau, d’une zone géographique, ou d’une ligne.
        /// </summary>
        public TisseoStopAreas()
        {
            dataParser = new DataParser();
        }

        /// <summary>
        /// Cette API permet d’obtenir des listes de zones d’arrêts.
        /// L’ensemble des zones d’arrêts d’un réseau, d’une zone géographique, ou d’une ligne.
        /// </summary>
        /// <param name="key">La clé de l'API TISSEO</param>
        public TisseoStopAreas(string key)
        {
            this.API_KEY = key;
            dataParser = new DataParser();
        }

        /// <summary>
        /// Envoi la demande des données au serveur de Tisséo
        /// </summary>
        /// <returns>Les données des arrets Tisséo</returns>
        public StopAreasData GetStopAreasData()
        {
            var results = dataParser.UploadString(this.STOP_AREAS_LIST_API, GetParmasStr());
            StopAreasHelper stopAreasHelper = JsonConvert.DeserializeObject<StopAreasHelper>(Utilities.ReplaceRgbByHex(results));
            return new StopAreasData { ExpirationDate = stopAreasHelper.ExpirationDate, StopAreas = stopAreasHelper.Data.StopAreas };
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
                parameters += "&displayLines=" + (_displayLines?1:0);
            if( _displayCoordXYChanged)
                parameters += "&displayCoordXY=" + (_displayCoordXY ? 1 : 0);
            if (_terminusIdChanged)
                parameters += "&terminusId=" + _terminusId;

            return parameters;
        }
    }

    /// <summary>
    /// Obtenir les données de TisseoStopArea 
    /// </summary>
    public class StopAreasData  
    {
        /// <summary>
        /// La date d'expiration des données renvoyé par le serveur Tisséo
        /// </summary>
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// La liste des zones d'arret
        /// </summary> 
        public List<StopArea> StopAreas { get; set; }
    }
}
