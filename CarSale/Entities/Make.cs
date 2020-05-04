using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CarSale.Entities
{



    [Table("tblMakes")]
    public class Make
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<MakesAndModels> MakesAndModels { get; set; }
    }
}
