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
    
    public partial class Module
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Module()
        {
            this.FormateurModules = new HashSet<FormateurModule>();
            this.SiteModules = new HashSet<SiteModule>();
        }
    
        public int IdModule { get; set; }
        public Nullable<int> IdFormation { get; set; }
        public string NomModule { get; set; }
        public int CreditModule { get; set; }
        public int NombrPrévu { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FormateurModule> FormateurModules { get; set; }
        public virtual Formation Formation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SiteModule> SiteModules { get; set; }
    }
}