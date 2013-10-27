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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TisseoAPI.Net.Objects
{
    public class BBox
    {
        public PointF A { get; set; }
        public PointF B { get; set; }
        /// <summary>
        /// Créer une nouvelle instance de l'objet bbox
        /// </summary>
        public BBox() { } 
        /// <summary>
        /// Format attendu pour une bbox: longitude pt A, latitude pt A, longitude point B, latitude point B
        /// </summary>
        /// <param name="Point1">le point A</param>
        /// <param name="Point2">le point B</param>
        public BBox(PointF Point1, PointF Point2)
        {
            A = Point1;
            B = Point2;
        }

        /// <summary>
        /// Format attendu pour une bbox: longitude pt A, latitude pt A, longitude point B, latitude point B
        /// </summary>
        /// <param name="AX">Longtitude du Point A</param>
        /// <param name="AY">Latitude du point A</param>
        /// <param name="BX">Longtitude du point B</param>
        /// <param name="BY">Latitude du point B</param>
        public BBox(float AX, float AY, float BX, float BY)
        {
            A = new PointF(AX, AY);
            B = new PointF(BX, BY);
        }

    }
}
