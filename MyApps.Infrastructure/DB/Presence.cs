//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyApps.Infrastructure.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Presence
    {
        public int IdPresence { get; set; }
        public int IdParticipant { get; set; }
        public int IdSiteModule { get; set; }
        public System.DateTime DateHeureDePresence { get; set; }
        public bool EstPresent { get; set; }
    
        public virtual Participant Participant { get; set; }
        public virtual SiteModule SiteModule { get; set; }
    }
}
