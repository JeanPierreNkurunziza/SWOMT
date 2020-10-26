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
    
    public partial class Formateur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Formateur()
        {
            this.Evenements = new HashSet<Evenement>();
            this.FormateurModules = new HashSet<FormateurModule>();
        }
    
        public int IdFormateur { get; set; }
        public string NomFormateur { get; set; }
        public string Domaine { get; set; }
        public string TélFormateur { get; set; }
        public string EmailFormateur { get; set; }
        public System.DateTime DateEncodage { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evenement> Evenements { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FormateurModule> FormateurModules { get; set; }
    }
}
