using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CarSale.Entities
{


    [Table("tblMakesAndModels")]
    public class MakesAndModels
    {


        [ForeignKey("FilterMakeOf"), Key, Column(Order = 0)]
        public int FilterMakeId { get; set; }

        public virtual Make FilterMakeOf { get; set; }

        [ForeignKey("FilterValueOf"), Key, Column(Order = 1)]
        public int FilterValueId { get; set; }
        public virtual FilterValue FilterValueOf { get; set; }
    }
}
