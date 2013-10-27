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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TisseoAPI.Net.Objects 
{ 
    public enum PlaceCategory
    {
        [StringValue("Unknown")]
        Unknown = 0,
        [StringValue("Roads")]
        Roads = 1,
        [StringValue("Adresses")]
        Adresses = 2,
        [StringValue("Stops")]
        Stops = 3,
        [StringValue("Cities")]
        Cities = 4
    }

    /// <summary>
    /// Cette class represente un objet Place
    /// </summary>
    public class Place
    {

        private bool _filedChanged = false;

        private string _label;
        /// <summary>
        /// Le nom de la Place
        /// </summary>
        public string Label { get { return _label; } set { _label = value; _filedChanged = true; } }

        private PlaceCategory _cateogry;
        /// <summary>
        /// La catégorie de la place
        /// </summary>
        public PlaceCategory Category { get { return _cateogry; } set { _cateogry = value; _filedChanged = (_cateogry != PlaceCategory.Unknown); } }

        private string _key;
        /// <summary>
        /// La clé
        /// </summary>
        public string Key { get { return _key; } set { _key = value; _filedChanged = true; } }

        private string _className;
        /// <summary>
        /// Le nom de la classe (road,city,adress...)
        /// </summary>
        public string ClassName { get { return _className; } set { _className = value; _filedChanged = true; } }

        private float? _x;
        /// <summary>
        /// Coordonnée GPS X 
        /// </summary>
        public float? X { get { return _x; } set { _x = value; _filedChanged = true; } }

        private float? _y;
        /// <summary>
        /// Coordonnée GPS Y
        /// </summary>
        public float? Y { get { return _y; } set { _y = value; _filedChanged = true; } }

        private int? _rank;
        /// <summary>
        /// rang
        /// </summary>
        public int? Rank { get { return _rank; } set { _rank = value; _filedChanged = true; } }

        private int? _distanceToOrigine;
        /// <summary>
        /// Distance par rapport l'origine
        /// </summary>
        public int? DistanceToOrigine { get { return _distanceToOrigine; } set { _distanceToOrigine = value; _filedChanged = true; } }

        private long? _id;
        /// <summary>
        /// L'id de la place
        /// </summary>
        public long? Id { get { return _id; } set { _id = value; _filedChanged = true; } }

        /// <summary>
        /// Créer une nouvelle instance de l'objet Place
        /// </summary>
        public Place() { }
        
        public bool IsInitialized()
        {
            return _filedChanged;
        }

        public override bool Equals(object objt)
        {
            var obj = objt as Place;
            return this.Id == obj.Id
                && this.Label == obj.Label
                && this.Category == obj.Category
                && this.ClassName == obj.ClassName
                && this.DistanceToOrigine == obj.DistanceToOrigine
                && this.Rank == obj.Rank
                && this.X == obj.X
                && this.Y == obj.Y;
        }
    }

    class PlaceComparer : IEqualityComparer<Place>
    {
        public bool Equals(Place x, Place y)
        {
            return x.Id == y.Id
                && x.Label == y.Label
                && x.Category == y.Category
                && x.ClassName == y.ClassName
                && x.DistanceToOrigine == y.DistanceToOrigine
                && x.Rank == y.Rank
                && x.X == y.X
                && x.Y == y.Y;
        }

        public int GetHashCode(Place obj)
        {
            return
                obj.Id.GetHashCode() ^
                obj.Label.GetHashCode() ^
                obj.Category.GetHashCode() ^
                obj.ClassName.GetHashCode() ^
                obj.DistanceToOrigine.GetHashCode() ^
                obj.Rank.GetHashCode() ^
                obj.X.GetHashCode() ^
                obj.Y.GetHashCode();
        }
    }
}
