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
    public enum MessageType
    {
        [StringValue("Trafic")]
        Trafic = 0,
        [StringValue("Communication")]
        Communication = 1
    }

    public enum MessageImportanceLevel
    {
        [StringValue("Normal")]
        Normal = 0,
        [StringValue("Important")]
        Important = 1
    }

    public enum MessageScop
    {
        [StringValue("Line")]
        Line = 0,
        [StringValue("Global")]
        Global = 1,
        [StringValue("Event")]
        Event = 2
    }

    /// <summary>
    /// Cette classe represente un message concrne une ligne, un evennement...
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Le message type ( Traffic, comminucation..)
        /// </summary>
        public MessageType Type { get; set; }
        /// <summary>
        /// L'importance du message ( normal, important )
        /// </summary>
        public MessageImportanceLevel ImportanceLevel { get; set; }
        /// <summary>
        /// Le scop ( ligne, global, evenement )
        /// </summary>
        public MessageScop Scop { get; set; }
        /// <summary>
        /// Le titre du message
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Le contenu du message
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Les lignes concernées 
        /// </summary>
        public List<Line> Lines { get; set; }
        /// <summary>
        /// L'url du message
        /// </summary>
        public string URL { get; set; }
    }


}
