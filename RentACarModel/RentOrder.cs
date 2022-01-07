namespace RentACarModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RentOrder")]
    public partial class RentOrder
    {
        public int RentOrderId { get; set; }

        public int? CustId { get; set; }

        public int? CarId { get; set; }

        [StringLength(50)]
        public string Cost { get; set; }

        public virtual Car Car { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
