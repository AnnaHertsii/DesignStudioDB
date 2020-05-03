//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DesignStudioCoursework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            this.Item = new HashSet<Item>();
        }
    
        public int Task_ID { get; set; }
        public string Task_name { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> Start_date { get; set; }
        public Nullable<System.DateTime> End_date { get; set; }
        public Nullable<int> Employee_Ref { get; set; }
        public Nullable<int> Project_Ref { get; set; }
        public Nullable<int> Task_status_Ref { get; set; }
    
        public virtual Design_Project Design_Project { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Status Status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Item> Item { get; set; }
    }
}
