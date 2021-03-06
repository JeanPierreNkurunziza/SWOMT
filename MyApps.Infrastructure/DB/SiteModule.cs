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
    
    public partial class SiteModule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SiteModule()
        {
            this.Examen = new HashSet<Examan>();
            this.ModuleInscriptions = new HashSet<ModuleInscription>();
            this.Presences = new HashSet<Presence>();
        }
    
        public int IdSiteModule { get; set; }
        public int IdModule { get; set; }
        public int IdSite { get; set; }
        public Nullable<System.DateTime> DateDebutModule { get; set; }
        public Nullable<System.DateTime> DateFinModule { get; set; }
        public Nullable<int> IdFormateurModule { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Examan> Examen { get; set; }
        public virtual FormateurModule FormateurModule { get; set; }
        public virtual Module Module { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ModuleInscription> ModuleInscriptions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Presence> Presences { get; set; }
        public virtual Site Site { get; set; }
    }
}
