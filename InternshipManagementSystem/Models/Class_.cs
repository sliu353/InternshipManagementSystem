//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InternshipManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Class_
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Class_()
        {
            this.Students = new HashSet<Student>();
        }
    
        public string ClassName { get; set; }
        public string CompanyName { get; set; }
        public string TeacherEmail { get; set; }
        public Nullable<int> NumberOfStudents { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual Teacher Teacher { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }
    }
}