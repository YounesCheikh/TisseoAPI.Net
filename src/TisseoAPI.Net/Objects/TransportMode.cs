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
    public class TransportMode
    {
        /// <summary>
        /// L'id de type du moyen de transport
        /// </summary>
        public long? Id { get; set; }
        /// <summary>
        /// L'article du moyen de transport ( généralement 'le' ou  'la' )
        /// </summary>
        public string Article { get; set; }
        /// <summary>
        /// Le nom du moyen de transport
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Créer une nouvelle instance de l'objet TransportMode
        /// </summary>
        //public TransportMode() { }

    }
}
