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
using TisseoAPI.Net.Helpers;
using TisseoAPI.Net.Objects;

namespace TisseoAPI.Net.Core
{
    /// <summary>
    /// Cette API permet à partir d’un texte (ou d’un point X,Y) d’obtenir une liste de lieux pouvant correspondre.
    /// Les lieux retournés peuvent être des rues, des arrêts, des lieux publics ou des communes connus par notre système.
    /// Cette API ne se contente pas de rechercher les lieux contenant exactement la chaîne de caractères transmise,
    /// mais effectue une recherche plus large intégrant des prononciations proches par exemple.<br />
    /// Les requêtes d’autocomplétion (paramètre Term) avec moins de 3 caractères sont 
    /// interdites, tenez-en compte dans votre implémentation
    ///  Un des 2 paramètres term ou coordinatesXY doit être fourni.
    /// Ils doivent être utilisés de manière exclusive, dans le cas contraire 
    /// c’est l’option coordinatesXY qui est prise en compte uniquement.
    ///  Le srid permet à la fois de modifier le système de coordonnées des XY des zones d’arrêts 
    /// et de préciser dans quel référentiel les coordonnées x, y (coordinatesXY) du point sont exprimées.
    ///  Avec l’option displayBestPlace le meilleur résultat est toujours celui qui est affiché en premier.
    ///  Les options displayOnlyStopAreas, displayOnlyRoads, displayOnlyAdresses, displayOnlyPublicPlaces, 
    /// displayOnlyCities ne peuvent pas êtres combinées entre elles, elles sont à utiliser de manière exclusive.
    ///  Si coordinatesXY est utilisé alors seulement des Roads ou des Address seront retournées
    /// (pour connaitre les arrêts autour d’un XY, utilisez le service StopAreasList avec une bbox).
    ///  Toutes les autres options peuvent êtres combinées entre elles.
    /// </summary>
    public class TisseoPlaces
    {
        private string PLACES_LIST_API = "placesList";
        private string API_KEY = "None";
        private DataParser dataParser;


        private bool _termChanged = false;
        private string _term;
        /// <summary>
        /// Texte saisi (3 caractères minimum)
        /// </summary>
        public string Term { get { return _term; } set { _term = value; _termChanged = true; } }

        private bool _networkChanged = false;
        private string _network;
        /// <summary>
        /// Opérateur de transport
        /// </summary>
        public string Network { get { return _network; } set { _network = value; _networkChanged = true; } }

        private bool _coordinatesXYChanged = false;
        private PointF _coordinatesXY;
        /// <summary>
        /// Retourne la liste des adresses les plus proches (triées par distance) de ce point de coordonnées x, y
        /// </summary>
        public PointF CoordinatesXY
        {
            get { return _coordinatesXY; }
            set { _coordinatesXY = value; _coordinatesXYChanged = true; }
        }

        private bool _maxDistanceChanged = false;
        private int _maxDistance;
        /// <summary>
        /// Distance au tour de laquelle s’effectue la recherche
        /// </summary>
        public int MaxDistance
        {
            get { return _maxDistance; }
            set { _maxDistance = value; _maxDistanceChanged = true; }
        }

        private bool _sridChanged = false;
        private int _srid;
        /// <summary>
        /// Numéro SRID du référentiel de projection spatial. Voir http://en.wikipedia.org/wiki/SRID
        /// </summary>
        public int Srid { get { return _srid; } set { _srid = value; _sridChanged = true; } }

        private bool _numberChanged = false;
        private int _number;
        /// <summary>
        /// Filtre sur le nb maxi de résultats à retourner par type de lieu
        /// </summary>
        public int Number { get { return _number; } set { _number = value; _numberChanged = true; } }

        private bool _displayBestPlaceChanged = false;
        private bool _displayBestPlace;
        /// <summary>
        /// Retourne le meilleur résultat en premier
        /// </summary>
        public bool DisplayBestPlace
        {
            get { return _displayBestPlace; }
            set { _displayBestPlace = value; _displayBestPlaceChanged = true; }
        }

        private bool _displayOnlyStopAreasChanged = false;
        private bool _displayOnlyStopAreas;
        /// <summary>
        /// Retourne uniquement les lignes dont le type de lieu est un stop
        /// </summary>
        public bool DisplayOnlyStopAreas
        {
            get { return _displayOnlyStopAreas; }
            set { _displayOnlyStopAreas = value; _displayOnlyStopAreasChanged = true; }
        }

        private bool _displayOnlyRoadsChanged = false;
        private bool _displayOnlyRoads;
        /// <summary>
        /// Retourne uniquement les lignes dont le type de lieu est une rue
        /// </summary>
        public bool DisplayOnlyRoads
        {
            get { return _displayOnlyRoads; }
            set { _displayOnlyRoads = value; _displayOnlyRoadsChanged = true; }
        }

        private bool _displayOnlyAdressesChanged = false;
        private bool _displayOnlyAdresses;
        /// <summary>
        /// Retourne uniquement les lignes dont le type de lieu est une adresse
        /// </summary>
        public bool DisplayOnlyAdresses
        {
            get { return _displayOnlyAdresses; }
            set { _displayOnlyAdresses = value; _displayOnlyAdressesChanged = true; }
        }

        private bool _displayOnlyPublicPlacesChanged = false;
        private bool _displayOnlyPublicPlaces;
        /// <summary>
        /// Retourne uniquement les lignes dont le type de lieu est une place publique
        /// </summary>
        public bool DisplayOnlyPublicPlaces
        {
            get { return _displayOnlyPublicPlaces; }
            set { _displayOnlyPublicPlaces = value; _displayOnlyPublicPlacesChanged = true; }
        }

        private bool _displayOnlyCitiesChanged = false;
        private bool _displayOnlyCities;
        /// <summary>
        /// Retourne uniquement les lignes dont le type de lieu est une ville
        /// </summary>
        public bool DisplayOnlyCities
        {
            get { return _displayOnlyCities; }
            set { _displayOnlyCities = value; _displayOnlyCitiesChanged = true; }
        }

        /// <summary>
        /// RECHERCHE DE LIEUX<br />
        /// Cette API permet à partir d’un texte (ou d’un point X,Y) d’obtenir une liste de
        /// lieux pouvant correspondre.
        /// Les lieux retournés peuvent être des rues, des arrêts,
        /// des lieux publics ou des communes connus par notre système.
        /// Cette API ne se contente pas de rechercher les lieux contenant exactement la chaîne de caractères transmise,
        /// mais effectue une recherche plus large intégrant des prononciations proches par exemple.
        /// Elle peut être efficacement utilisée :
        ///  dans un objectif d’autocomplétion sur un champ de type lieu
        ///  pour de la géolocalisation
        /// </summary>
        /// <param name="key">Tisséo API Key</param>
        public TisseoPlaces(string key)
        {
            API_KEY = key;
            dataParser = new DataParser();
        }

        /// <summary>
        /// RECHERCHE DE LIEUX<br />
        /// Cette API permet à partir d’un texte (ou d’un point X,Y) d’obtenir une liste de lieux pouvant correspondre.
        /// Les lieux retournés peuvent être des rues, des arrêts, 
        /// des lieux publics ou des communes connus par notre système.
        /// Cette API ne se contente pas de rechercher les lieux contenant exactement la chaîne de caractères transmise,
        /// mais effectue une recherche plus large intégrant des prononciations proches par exemple.
        /// Elle peut être efficacement utilisée :
        ///  dans un objectif d’autocomplétion sur un champ de type lieu
        ///  pour de la géolocalisation
        /// </summary>
        public TisseoPlaces()
        {
            dataParser = new DataParser();
        }


        /// <summary>
        /// Envoi la demande des données au serveur de Tisséo
        /// </summary>
        /// <returns>Le données des places Tisséo</returns>
        public PlacesData GetPlacesData()
        {
            var results = dataParser.UploadString(this.PLACES_LIST_API, GetParmasStr());
            PlacesHelper placesHelper = JsonConvert.DeserializeObject<PlacesHelper>(Utilities.ReplaceRgbByHex(results));
            return new PlacesData { ExpirationDate = placesHelper.ExpirationDate, Places = placesHelper.Data.Places };
        }

        

        private string GetParmasStr()
        {
            string parameters = "key=" + API_KEY;
            if (_termChanged)
                parameters += "&term=" + _term;
            if (_networkChanged)
                parameters += "&network=" + _network;
            if (_coordinatesXYChanged)
                parameters = String.Format("&coordinatesXY={0},{1}", _coordinatesXY.X, _coordinatesXY.Y);
            if (_maxDistanceChanged)
                parameters += "&maxDistance=" + _maxDistance;
            if (_sridChanged)
                parameters += "&srid=" + _srid;
            if (_numberChanged)
                parameters += "&number=" + _number;
            if (_displayBestPlaceChanged)
                parameters += "&displayBestPlace=" + (_displayBestPlace ? 1 : 0);
            if (_displayOnlyStopAreasChanged)
                parameters += "&displayOnlyStopAreas=" + (_displayOnlyStopAreas ? 1 : 0);
            if (_displayOnlyRoadsChanged)
                parameters += "&displayOnlyRoads=" + (_displayOnlyRoads ? 1 : 0);
            if (_displayOnlyAdressesChanged)
                parameters += "&displayOnlyAdresses=" + (_displayOnlyAdresses ? 1 : 0);
            if (_displayOnlyPublicPlacesChanged)
                parameters += "&displayOnlyPublicPlaces=" + (_displayOnlyPublicPlaces ? 1 : 0);
            if (_displayOnlyCitiesChanged)
                parameters += "&displayOnlyCities=" + (_displayOnlyCities ? 1 : 0);
            return parameters;
        }

    }

    public class PlacesData
    {
        /// <summary>
        /// La date d'expiration des données renvoyé par le serveur Tisséo
        /// </summary>
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// La liste des places
        /// </summary>
        public List<Place> Places { get; set; }
        /// <summary>
        /// Obtenir les données de TisseoPlaces 
        /// </summary>
        //public TisseoPlacesData() { }

    }
}
