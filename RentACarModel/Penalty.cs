namespace RentACarModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Penalty
    {
        [Key]
        public int PenId { get; set; }

        public int? CustId { get; set; }

        [StringLength(50)]
        public string PenCost { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
