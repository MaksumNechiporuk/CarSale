using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CarSale.Entities
{



    [Table("tblFilterValues")]
    public class FilterValue
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(maximumLength: 250)]
        public string Name { get; set; }

        public virtual ICollection<Filter> Filtres { get; set; }

        public virtual ICollection<FilterNameGroup> FilterNameGroups { get; set; }

        public virtual ICollection<MakesAndModels> MakesAndModels { get; set; }

    }
}
