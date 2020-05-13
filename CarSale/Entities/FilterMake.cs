using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarSale.Entities
{
    [Table("tblFilterMakes")]
    public class FilterMake
    {

        [ForeignKey("MakeNameOf"), Key, Column(Order = 0)]
        public int MakeNameId { get; set; }

        public virtual Make MakeNameOf { get; set; }


        [ForeignKey("CarOf"), Key, Column(Order = 2)]
        public int CarId { get; set; }
        public virtual Car CarOf { get; set; }

    }
}
