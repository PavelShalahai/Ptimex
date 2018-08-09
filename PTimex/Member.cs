namespace PTimex
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Member")]
    public partial class Member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Member()
        {
            TaskTime = new HashSet<TaskTime>();
        }

        public int Id { get; set; }

        public int ProjectId { get; set; }

        public int UserId { get; set; }

        public int PositionId { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Position Position { get; set; }

        public virtual Project Project { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskTime> TaskTime { get; set; }
    }
}
