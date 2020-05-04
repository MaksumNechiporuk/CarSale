using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CarSale.Entities
{

    [Table("tblFilters")]
    public class Filter
    {
        [ForeignKey("FilterNameOf"), Key, Column(Order = 0)]
        public int FilterNameId { get; set; }

        public virtual FilterName FilterNameOf { get; set; }

        [ForeignKey("FilterValueOf"), Key, Column(Order = 1)]
        public int FilterValueId { get; set; }
        public virtual FilterValue FilterValueOf { get; set; }
        [ForeignKey("CarOf"), Key, Column(Order = 2)]
        public int CarId { get; set; }
        public virtual Car CarOf { get; set; }
    }
}
