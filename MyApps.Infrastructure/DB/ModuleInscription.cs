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
    
    public partial class ModuleInscription
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ModuleInscription()
        {
            this.Resultats = new HashSet<Resultat>();
        }
    
        public int IdModuleInscription { get; set; }
        public int IdSiteModule { get; set; }
        public int IdParticipant { get; set; }
        public System.DateTime DateInscription { get; set; }
        public bool EstSurListeAttente { get; set; }
    
        public virtual Participant Participant { get; set; }
        public virtual SiteModule SiteModule { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resultat> Resultats { get; set; }
    }
}
