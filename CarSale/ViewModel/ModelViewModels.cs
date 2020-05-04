using System.ComponentModel.DataAnnotations;
namespace CarSale.ViewModels
{


    public class ModelVM
    {


        public int Id { get; set; }



        public MakeVM Make { get; set; }




        [Required(ErrorMessage = "Поле не може бути пустим")]
        public string Name { get; set; }
    }


    public class ModelAddVM
    {


        [Required(ErrorMessage = "Поле не може бути пустим")]
        public MakeVM Make { get; set; }




        [Required(ErrorMessage = "Поле не може бути пустим")]
        public string Name { get; set; }
    }


    public class ModelDeleteVM
    {


        public int Id { get; set; }
    }
}
