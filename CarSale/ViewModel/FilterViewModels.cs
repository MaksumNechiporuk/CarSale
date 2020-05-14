using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CarSale.ViewModels
{


    public class FValueViewModel
    {


        public int Id { get; set; }




        public string Name { get; set; }
    }


    public class FNameGetViewModel
    {


        public int Id { get; set; }




        public string Name { get; set; }



        public FValueViewModel Children { get; set; }
    }


    public class FilterAddWithCarVM
    {


        [Required(ErrorMessage = "Поле не може бути пустим")]
        public int IdCar { get; set; }



        [Required(ErrorMessage = "Поле не може бути пустим")]
        public List<int> IdValue { get; set; }
    }


    public class FNameViewModel
    {


        public int Id { get; set; }




        public string Name { get; set; }



        public List<FValueViewModel> Children { get; set; }
    }

    public class FilterAddViewModel
    {


        public string Name { get; set; }



        public List<string> values { get; set; }
    }


    public class FilterVM
    {


        public int Id { get; set; }




        public string Name { get; set; }
    }


    public class FilterAddVM
    {


        public string Date { get; set; }



        public ModelVM Model { get; set; }



        public string Image { get; set; }




        public int Price { get; set; }
    }

    public class FilterDeleteVM
    {


        public int Id { get; set; }
    }
}
