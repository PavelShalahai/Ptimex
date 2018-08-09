namespace PTimex
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaskTime")]
    public partial class TaskTime
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int ActivityId { get; set; }

        public int MemberId { get; set; }

        public bool? IsDeleted { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public TimeSpan TimeSpan { get; set; }

        public virtual ActivityType ActivityType { get; set; }

        public virtual Member Member { get; set; }
    }
}
