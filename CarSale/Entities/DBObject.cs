using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarSale.Entities
{
    public class DBObject
    {
        public static void Initial(DBContext content, IServiceProvider services, IHostingEnvironment env,
            IConfiguration config)
        {
            if (!content.Users.Any())
            {
                var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var managerUser = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string roleName = "Admin";
                var role = managerRole.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new IdentityRole
                    {
                        Name = roleName
                    };
                    var addRoleResult = managerRole.CreateAsync(role).Result;
                }
                roleName = "User";
                role = managerRole.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new IdentityRole
                    {
                        Name = roleName
                    };
                    var addRoleResult = managerRole.CreateAsync(role).Result;
                }
                var listUsers = new List<AppUser>()
                {
                    new AppUser
                   {   Email="robert@gmail.com",
                       Name = "Robert",
                       Surname = "Deniro",
                       Country = "USA",
                       City = "New York",
                       Img = "https://img.icons8.com/officel/2x/user.png",
                       UserName="robert@gmail.com",
                       PhoneNumber="380986654128"
                   },
                     new AppUser
                     {
                         Email="Bob@gmail.com",
                         Name = "Bob",
                         Surname = "Marley",
                         Country = "Jamaica",
                         City = "Nine Mile",
                         Img = "https://img.icons8.com/officel/2x/user.png",
                         UserName="Bob@gmail.com",
                                                PhoneNumber="380986664228"

                     },
                       new AppUser
                       {
                           Name = "Jon",
                           Surname = "Show",
                           Country = "Westeros",
                           City = " Winterfell",
                           Img = "https://img.icons8.com/officel/2x/user.png",
                           Email="Jon@gmail.com",
                           UserName="Jon@gmail.com",
                                                  PhoneNumber="384486654128"

                       },
                         new AppUser
                         {
                             Name = "John",
                             Surname = "Sparrow",
                             Country = "Great Britain",
                             City = "London",
                             Img = "https://img.icons8.com/officel/2x/user.png",
                             Email="John@gmail.com",
                             UserName="John@gmail.com",
                                                                               PhoneNumber="380986653418"

                         }
            };
                var roles = new List<string> { { "Admin" }, { "User" } };
                int i = 0;
                foreach (var item in listUsers)
                {

                    var user = managerUser.FindByEmailAsync(item.Email).Result;
                    if (user == null)
                    {
                        var result = managerUser.CreateAsync(item, "Qwerty1-").Result;
                        if (result.Succeeded)
                        {
                            if (i == 3)
                                result = managerUser.AddToRoleAsync(item, roles[1]).Result;
                            else
                                result = managerUser.AddToRoleAsync(item, roles[0]).Result;

                        }
                    }
                    i++;
                }



            }
            SeedFilters(content, env, config);
        }
        private static void SeedFilters(DBContext context, IHostingEnvironment _env,
           IConfiguration _config)
        {
            #region Make
            List<Make> listMake = new List<Make>
                {
                    new Make{ Name = "BMW"},
                    new Make{ Name = "Mazda"},
                    new Make{ Name = "Audi"},
                    new Make{ Name = "Mersedes-Benz"},
                    new Make{ Name = "Toyota"},
                    new Make{ Name = "Volkswagen"},
                    new Make{ Name = "Chevrolet"},
                    new Make{ Name = "Ford"},
                    new Make{ Name = "Peugeot"},
                    new Make{ Name = "Fiat"},
                    new Make{ Name = "Nissan"},
                    new Make{ Name = "Hyundai"},
                    new Make{ Name = "Opel"},
                    new Make{ Name = "Renault"},
                    new Make{ Name = "Subaru"},
                    new Make{ Name = "Skoda"},
                    new Make{ Name = "Honda"},
                    new Make{ Name = "Citroen"}
                };
            foreach (var item in listMake)
            {
                var make = context.Makes.SingleOrDefault(c => c.Name == item.Name);
                if (make == null)
                {
                    context.Makes.Add(item);
                    context.SaveChanges();
                }
            }
            #endregion
            #region tblFilterNames - Назви фільтрів
            string[] filterNames = { "Type of car", "Fuel", "Model", "Color", "State" };
            foreach (var type in filterNames)
            {
                if (context.FilterNames.SingleOrDefault(f => f.Name == type) == null)
                {
                    context.FilterNames.Add(
                        new Entities.FilterName
                        {
                            Name = type
                        });
                    context.SaveChanges();
                }
            }
            #endregion

            #region tblFilterValues - Значення фільтрів
            List<string[]> filterValues = new List<string[]> {
                new string [] { "Moto", "Lightweight", "Freight" },
                new string [] { "Diesel", "Gasoline", "Gas"},
                new string [] {"C6","SPACETOURER","C4 SEDAN","BERLINGO","C-CROSSER","CR-V","PILOT","PASSPORT","FIT","ACCORD","KAMIQ",
                 "YETI","ROOMSTER","OCTAVIA","CITIGO","FORESTER","JUSTY","ASCENT",
                 "TRIBECA","STELLA","Duster","TALISMAN","SANDERO","LATITUDE","KOLEOS","ESPACE","VIVARO","CORSA","FRONTERA",
                 "ANTARA","ADAM","GENESIS","SONATA","CRETA","GRANDEUR","KONA","TERRACAN","SENTRA",
                 "PATROL","ALMERA","GT-R","LAFESTA","LINEA","TORO","TIPO","PANDA","MOBI","BRAVO",
                 "508 RXH","TRAVELLER","208","PEUGEOT 1007","308 GT","EXPERT","TAURUS",
                 "MUSTANG","FOCUS RS","FIESTA","EXPLORER","ALERO","ORLANDO","VIVA","CORVETTE","COBALT","TOURAN","PASSAT",
                 "ATLAS","GOLF","CAMRY","SIENNA","GT 86","S-CLASS CABRIOLET","M-CLASS","V-CLASS","A SEDAN",
                 "AMG GT S","TTS","S3","Q2","A6","Axela","Tribute","MX-5","X5","750iL","3-series Coupe"},
                 new string []{ "Green", "Red", "Blue", "Black", "White", "Gray" },
                 new string[]{"New","Used"}
            };

            foreach (var items in filterValues)
            {
                foreach (var value in items)
                {
                    if (context.FilterValues
                        .SingleOrDefault(f => f.Name == value) == null)
                    {
                        context.FilterValues.Add(
                            new Entities.FilterValue
                            {
                                Name = value
                            });
                        context.SaveChanges();
                    }
                }
            }
            #endregion

            #region tblFilterNameGroups - Групування по групах фільтрів

            for (int i = 0; i < filterNames.Length; i++)
            {
                foreach (var value in filterValues[i])
                {
                    var nId = context.FilterNames
                        .SingleOrDefault(f => f.Name == filterNames[i]).Id;
                    var vId = context.FilterValues
                        .SingleOrDefault(f => f.Name == value).Id;
                    if (context.FilterNameGroups
                        .SingleOrDefault(f => f.FilterValueId == vId &&
                        f.FilterNameId == nId) == null)
                    {
                        context.FilterNameGroups.Add(
                            new Entities.FilterNameGroup
                            {
                                FilterNameId = nId,
                                FilterValueId = vId
                            });
                        context.SaveChanges();
                    }
                }
            }
            #endregion

            #region tblCars - Автомобілі
            var carList = new List<Car>()
            {
                        new Car
                        {
                            Date ="2018",
                            Price = 28000,
                            Mileage = 0,
                            Name = "Honda Accord LX",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2012",
                            Price = 20000,
                            Mileage = 200000,
                            Name = "BMW X5",
                            State="Used"
                        },
                        new Car
                        {
                            Date ="2020",
                            Price = 20223,
                            Mileage = 0,
                            Name = "Renault Duster",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2016",
                            Price = 15800,
                            Mileage = 49000,
                            Name = "Volkswagen Passat B8",
                            State="Used"
                        },                        new Car
                        {
                            //UniqueName = Guid.NewGuid().ToString(),
                            Date ="2018",
                            Price = 28000,
                            Mileage = 0,
                            Name = "Honda Accord LX",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2012",
                            Price = 20000,
                            Mileage = 200000,
                            Name = "BMW X5",
                            State="Used"
                        },
                        new Car
                        {
                            Date ="2020",
                            Price = 20223,
                            Mileage = 0,
                            Name = "Renault Duster",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2016",
                            Price = 15800,
                            Mileage = 49000,
                            Name = "Volkswagen Passat B8",
                            State="Used"
                        },
                         new Car
                        {
                            Date ="2018",
                            Price = 28000,
                            Mileage = 0,
                            Name = "Honda Accord LX",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2012",
                            Price = 20000,
                            Mileage = 200000,
                            Name = "BMW X5",
                            State="Used"
                        },
                        new Car
                        {
                            Date ="2020",
                            Price = 20223,
                            Mileage = 0,
                            Name = "Renault Duster",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2016",
                            Price = 15800,
                            Mileage = 49000,
                            Name = "Volkswagen Passat B8",
                            State="Used"
                        },                        new Car
                        {
                            //UniqueName = Guid.NewGuid().ToString(),
                            Date ="2018",
                            Price = 28000,
                            Mileage = 0,
                            Name = "Honda Accord LX",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2012",
                            Price = 20000,
                            Mileage = 200000,
                            Name = "BMW X5",
                            State="Used"
                        },
                        new Car
                        {
                            Date ="2020",
                            Price = 20223,
                            Mileage = 0,
                            Name = "Renault Duster",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2016",
                            Price = 15800,
                            Mileage = 49000,
                            Name = "Volkswagen Passat B8",
                            State="Used"
                        },
                         new Car
                        {
                            Date ="2018",
                            Price = 28000,
                            Mileage = 0,
                            Name = "Honda Accord LX",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2012",
                            Price = 20000,
                            Mileage = 200000,
                            Name = "BMW X5",
                            State="Used"
                        },
                        new Car
                        {
                            Date ="2020",
                            Price = 20223,
                            Mileage = 0,
                            Name = "Renault Duster",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2016",
                            Price = 15800,
                            Mileage = 49000,
                            Name = "Volkswagen Passat B8",
                            State="Used"
                        },                        new Car
                        {
                            //UniqueName = Guid.NewGuid().ToString(),
                            Date ="2018",
                            Price = 28000,
                            Mileage = 0,
                            Name = "Honda Accord LX",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2012",
                            Price = 20000,
                            Mileage = 200000,
                            Name = "BMW X5",
                            State="Used"
                        },
                        new Car
                        {
                            Date ="2020",
                            Price = 20223,
                            Mileage = 0,
                            Name = "Renault Duster",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2016",
                            Price = 15800,
                            Mileage = 49000,
                            Name = "Volkswagen Passat B8",
                            State="Used"
                        },
                         new Car
                        {
                            Date ="2018",
                            Price = 28000,
                            Mileage = 0,
                            Name = "Honda Accord LX",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2012",
                            Price = 20000,
                            Mileage = 200000,
                            Name = "BMW X5",
                            State="Used"
                        },
                        new Car
                        {
                            Date ="2020",
                            Price = 20223,
                            Mileage = 0,
                            Name = "Renault Duster",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2016",
                            Price = 15800,
                            Mileage = 49000,
                            Name = "Volkswagen Passat B8",
                            State="Used"
                        },                        new Car
                        {
                            //UniqueName = Guid.NewGuid().ToString(),
                            Date ="2018",
                            Price = 28000,
                            Mileage = 0,
                            Name = "Honda Accord LX",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2012",
                            Price = 20000,
                            Mileage = 200000,
                            Name = "BMW X5",
                            State="Used"
                        },
                        new Car
                        {
                            Date ="2020",
                            Price = 20223,
                            Mileage = 0,
                            Name = "Renault Duster",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2016",
                            Price = 15800,
                            Mileage = 49000,
                            Name = "Volkswagen Passat B8",
                            State="Used"
                        },
                         new Car
                        {
                            Date ="2018",
                            Price = 28000,
                            Mileage = 0,
                            Name = "Honda Accord LX",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2012",
                            Price = 20000,
                            Mileage = 200000,
                            Name = "BMW X5",
                            State="Used"
                        },
                        new Car
                        {
                            Date ="2020",
                            Price = 20223,
                            Mileage = 0,
                            Name = "Renault Duster",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2016",
                            Price = 15800,
                            Mileage = 49000,
                            Name = "Volkswagen Passat B8",
                            State="Used"
                        },                        new Car
                        {
                            //UniqueName = Guid.NewGuid().ToString(),
                            Date ="2018",
                            Price = 28000,
                            Mileage = 0,
                            Name = "Honda Accord LX",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2012",
                            Price = 20000,
                            Mileage = 200000,
                            Name = "BMW X5",
                            State="Used"
                        },
                        new Car
                        {
                            Date ="2020",
                            Price = 20223,
                            Mileage = 0,
                            Name = "Renault Duster",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2016",
                            Price = 15800,
                            Mileage = 49000,
                            Name = "Volkswagen Passat B8",
                            State="Used"
                        },
                         new Car
                        {
                            Date ="2018",
                            Price = 28000,
                            Mileage = 0,
                            Name = "Honda Accord LX",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2012",
                            Price = 20000,
                            Mileage = 200000,
                            Name = "BMW X5",
                            State="Used"
                        },
                        new Car
                        {
                            Date ="2020",
                            Price = 20223,
                            Mileage = 0,
                            Name = "Renault Duster",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2016",
                            Price = 15800,
                            Mileage = 49000,
                            Name = "Volkswagen Passat B8",
                            State="Used"
                        },                        new Car
                        {
                            //UniqueName = Guid.NewGuid().ToString(),
                            Date ="2018",
                            Price = 28000,
                            Mileage = 0,
                            Name = "Honda Accord LX",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2012",
                            Price = 20000,
                            Mileage = 200000,
                            Name = "BMW X5",
                            State="Used"
                        },
                        new Car
                        {
                            Date ="2020",
                            Price = 20223,
                            Mileage = 0,
                            Name = "Renault Duster",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2016",
                            Price = 15800,
                            Mileage = 49000,
                            Name = "Volkswagen Passat B8",
                            State="Used"
                        },
                         new Car
                        {
                            Date ="2018",
                            Price = 28000,
                            Mileage = 0,
                            Name = "Honda Accord LX",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2012",
                            Price = 20000,
                            Mileage = 200000,
                            Name = "BMW X5",
                            State="Used"
                        },
                        new Car
                        {
                            Date ="2020",
                            Price = 20223,
                            Mileage = 0,
                            Name = "Renault Duster",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2016",
                            Price = 15800,
                            Mileage = 49000,
                            Name = "Volkswagen Passat B8",
                            State="Used"
                        },                        new Car
                        {
                            //UniqueName = Guid.NewGuid().ToString(),
                            Date ="2018",
                            Price = 28000,
                            Mileage = 0,
                            Name = "Honda Accord LX",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2012",
                            Price = 20000,
                            Mileage = 200000,
                            Name = "BMW X5",
                            State="Used"
                        },
                        new Car
                        {
                            Date ="2020",
                            Price = 20223,
                            Mileage = 0,
                            Name = "Renault Duster",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2016",
                            Price = 15800,
                            Mileage = 49000,
                            Name = "Volkswagen Passat B8",
                            State="Used"
                        },
                         new Car
                        {
                            Date ="2018",
                            Price = 28000,
                            Mileage = 0,
                            Name = "Honda Accord LX",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2012",
                            Price = 20000,
                            Mileage = 200000,
                            Name = "BMW X5",
                            State="Used"
                        },
                        new Car
                        {
                            Date ="2020",
                            Price = 20223,
                            Mileage = 0,
                            Name = "Renault Duster",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2016",
                            Price = 15800,
                            Mileage = 49000,
                            Name = "Volkswagen Passat B8",
                            State="Used"
                        },                        new Car
                        {
                            //UniqueName = Guid.NewGuid().ToString(),
                            Date ="2018",
                            Price = 28000,
                            Mileage = 0,
                            Name = "Honda Accord LX",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2012",
                            Price = 20000,
                            Mileage = 200000,
                            Name = "BMW X5",
                            State="Used"
                        },
                        new Car
                        {
                            Date ="2020",
                            Price = 20223,
                            Mileage = 0,
                            Name = "Renault Duster",
                            State="New"
                        },
                        new Car
                        {
                            Date ="2016",
                            Price = 15800,
                            Mileage = 49000,
                            Name = "Volkswagen Passat B8",
                            State="Used"
                        }

            };
            List<string> cars = new List<string>{
             "154muv2f", "154m2fas","152m2fas","151m2fas",
             "154muv2f", "154m2fas","152m2fas","151m2fas",
             "154muv2f", "154m2fas","152m2fas","151m2fas",
             "154muv2f", "154m2fas","152m2fas","151m2fas",
             "154muv2f", "154m2fas","152m2fas","151m2fas",
             "154muv2f", "154m2fas","152m2fas","151m2fas",
             "154muv2f", "154m2fas","152m2fas","151m2fas",
             "154muv2f", "154m2fas","152m2fas","151m2fas",
             "154muv2f", "154m2fas","152m2fas","151m2fas",
             "154muv2f", "154m2fas","152m2fas","151m2fas",
             "154muv2f", "154m2fas","152m2fas","151m2fas",
             "154muv2f", "154m2fas","152m2fas","151m2fas",
             "154muv2f", "154m2fas","152m2fas","151m2fas",
             "154muv2f", "154m2fas","152m2fas","151m2fas",
             "154muv2f", "154m2fas","152m2fas","151m2fas",
             "154muv2f", "154m2fas","152m2fas","151m2fas"

            };
            var res = carList.Zip(cars, (n, w) => new { car = n, carName = w });

            foreach (var item in res)
            {
                string path = Path.Combine("images", item.carName);

                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                        item.car.UniqueName = item.carName;
                        context.Cars.Add(item.car);
                        context.SaveChanges();
                    }
                }
            }
            #endregion

            #region tblFilters -Фільтри
            Filter[] filters =
            {
                new Filter { FilterNameId = 1, FilterValueId=2, CarId=1 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=1 },
                new Filter { FilterNameId = 3, FilterValueId=16, CarId=1 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=1 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=1 },


                new Filter { FilterNameId = 1, FilterValueId=2, CarId=2 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=2 },
                new Filter { FilterNameId = 3, FilterValueId=90, CarId=2 },
                new Filter { FilterNameId = 4, FilterValueId=96, CarId=2 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=2 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=3 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=3 },
                new Filter { FilterNameId = 3, FilterValueId=86, CarId=3 },
                new Filter { FilterNameId = 4, FilterValueId=98, CarId=3 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=3 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=4 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=4 },
                new Filter { FilterNameId = 3, FilterValueId=27, CarId=4 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=4 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=4 },

                                new Filter { FilterNameId = 1, FilterValueId=2, CarId=5 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=5 },
                new Filter { FilterNameId = 3, FilterValueId=16, CarId=5 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=5 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=5 },


                new Filter { FilterNameId = 1, FilterValueId=2, CarId=6 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=6 },
                new Filter { FilterNameId = 3, FilterValueId=90, CarId=6 },
                new Filter { FilterNameId = 4, FilterValueId=96, CarId=6 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=6 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=7 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=7 },
                new Filter { FilterNameId = 3, FilterValueId=86, CarId=7 },
                new Filter { FilterNameId = 4, FilterValueId=98, CarId=7 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=7 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=8 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=8},
                new Filter { FilterNameId = 3, FilterValueId=27, CarId=8 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=8 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=8 },

                                new Filter { FilterNameId = 1, FilterValueId=2, CarId=9 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=9 },
                new Filter { FilterNameId = 3, FilterValueId=16, CarId=9 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=9 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=9 },


                new Filter { FilterNameId = 1, FilterValueId=2, CarId=10 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=10 },
                new Filter { FilterNameId = 3, FilterValueId=90, CarId=10 },
                new Filter { FilterNameId = 4, FilterValueId=96, CarId=10 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=10 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=10 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=10 },
                new Filter { FilterNameId = 3, FilterValueId=86, CarId=10 },
                new Filter { FilterNameId = 4, FilterValueId=98, CarId=10 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=10 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=11 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=11 },
                new Filter { FilterNameId = 3, FilterValueId=27, CarId=11 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=11 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=11 },

                                new Filter { FilterNameId = 1, FilterValueId=2, CarId=12 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=12 },
                new Filter { FilterNameId = 3, FilterValueId=16, CarId=12 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=12},
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=12 },


                new Filter { FilterNameId = 1, FilterValueId=2, CarId=13 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=13 },
                new Filter { FilterNameId = 3, FilterValueId=90, CarId=13 },
                new Filter { FilterNameId = 4, FilterValueId=96, CarId=13 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=13 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=14 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=14 },
                new Filter { FilterNameId = 3, FilterValueId=86, CarId=14 },
                new Filter { FilterNameId = 4, FilterValueId=98, CarId=14 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=14 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=15 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=15 },
                new Filter { FilterNameId = 3, FilterValueId=27, CarId=15 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=15 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=15 },


                                new Filter { FilterNameId = 1, FilterValueId=2, CarId=16 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=16 },
                new Filter { FilterNameId = 3, FilterValueId=16, CarId=16 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=16 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=16 },


                new Filter { FilterNameId = 1, FilterValueId=2, CarId=17 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=17 },
                new Filter { FilterNameId = 3, FilterValueId=90, CarId=17 },
                new Filter { FilterNameId = 4, FilterValueId=96, CarId=17 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=17 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=18 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=18 },
                new Filter { FilterNameId = 3, FilterValueId=86, CarId=18 },
                new Filter { FilterNameId = 4, FilterValueId=98, CarId=18 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=18 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=19 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=19 },
                new Filter { FilterNameId = 3, FilterValueId=27, CarId=19 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=19 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=19 },

                                new Filter { FilterNameId = 1, FilterValueId=2, CarId=20 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=20 },
                new Filter { FilterNameId = 3, FilterValueId=16, CarId=20 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=20 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=20 },


                new Filter { FilterNameId = 1, FilterValueId=2, CarId=21 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=21 },
                new Filter { FilterNameId = 3, FilterValueId=90, CarId=21 },
                new Filter { FilterNameId = 4, FilterValueId=96, CarId=21 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=21 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=22 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=22 },
                new Filter { FilterNameId = 3, FilterValueId=86, CarId=22 },
                new Filter { FilterNameId = 4, FilterValueId=98, CarId=22 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=22 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=23 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=23 },
                new Filter { FilterNameId = 3, FilterValueId=27, CarId=23 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=23 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=23 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=24 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=24 },
                new Filter { FilterNameId = 3, FilterValueId=16, CarId=24 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=24 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=24 },


                new Filter { FilterNameId = 1, FilterValueId=2, CarId=25 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=25 },
                new Filter { FilterNameId = 3, FilterValueId=90, CarId=25 },
                new Filter { FilterNameId = 4, FilterValueId=96, CarId=25 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=25 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=26 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=26 },
                new Filter { FilterNameId = 3, FilterValueId=86, CarId=26 },
                new Filter { FilterNameId = 4, FilterValueId=98, CarId=26 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=26 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=27 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=27 },
                new Filter { FilterNameId = 3, FilterValueId=27, CarId=27 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=27 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=27 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=28 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=28 },
                new Filter { FilterNameId = 3, FilterValueId=16, CarId=28 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=28 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=28 },


                new Filter { FilterNameId = 1, FilterValueId=2, CarId=29 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=29 },
                new Filter { FilterNameId = 3, FilterValueId=90, CarId=29 },
                new Filter { FilterNameId = 4, FilterValueId=96, CarId=29},
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=29 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=30 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=30 },
                new Filter { FilterNameId = 3, FilterValueId=86, CarId=30 },
                new Filter { FilterNameId = 4, FilterValueId=98, CarId=30 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=30 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=31 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=31 },
                new Filter { FilterNameId = 3, FilterValueId=27, CarId=31 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=31},
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=31},

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=32 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=32},
                new Filter { FilterNameId = 3, FilterValueId=16, CarId=32 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=32 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=1 },


                new Filter { FilterNameId = 1, FilterValueId=2, CarId=33 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=33 },
                new Filter { FilterNameId = 3, FilterValueId=90, CarId=33 },
                new Filter { FilterNameId = 4, FilterValueId=96, CarId=33 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=33 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=34 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=34 },
                new Filter { FilterNameId = 3, FilterValueId=86, CarId=34 },
                new Filter { FilterNameId = 4, FilterValueId=98, CarId=34 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=34 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=35 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=35 },
                new Filter { FilterNameId = 3, FilterValueId=27, CarId=35 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=35 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=35},

                                new Filter { FilterNameId = 1, FilterValueId=2, CarId=36 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=36 },
                new Filter { FilterNameId = 3, FilterValueId=16, CarId=36 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=36 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=36 },


                new Filter { FilterNameId = 1, FilterValueId=2, CarId=37 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=37 },
                new Filter { FilterNameId = 3, FilterValueId=90, CarId=37 },
                new Filter { FilterNameId = 4, FilterValueId=96, CarId=37 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=37 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=38 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=38 },
                new Filter { FilterNameId = 3, FilterValueId=86, CarId=38 },
                new Filter { FilterNameId = 4, FilterValueId=98, CarId=38 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=38 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=39 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=39 },
                new Filter { FilterNameId = 3, FilterValueId=27, CarId=39 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=39 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=39 },

                                new Filter { FilterNameId = 1, FilterValueId=2, CarId=40 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=40 },
                new Filter { FilterNameId = 3, FilterValueId=16, CarId=40 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=40 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=40 },


                new Filter { FilterNameId = 1, FilterValueId=2, CarId=41 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=41},
                new Filter { FilterNameId = 3, FilterValueId=90, CarId=41 },
                new Filter { FilterNameId = 4, FilterValueId=96, CarId=41 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=41 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=42 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=42 },
                new Filter { FilterNameId = 3, FilterValueId=86, CarId=42 },
                new Filter { FilterNameId = 4, FilterValueId=98, CarId=42 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=42 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=44 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=44 },
                new Filter { FilterNameId = 3, FilterValueId=27, CarId=44 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=44 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=44 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=45 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=45 },
                new Filter { FilterNameId = 3, FilterValueId=16, CarId=45 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=45 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=45 },


                new Filter { FilterNameId = 1, FilterValueId=2, CarId=46 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=46 },
                new Filter { FilterNameId = 3, FilterValueId=90, CarId=46 },
                new Filter { FilterNameId = 4, FilterValueId=96, CarId=46 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=46 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=47 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=47 },
                new Filter { FilterNameId = 3, FilterValueId=86, CarId=47 },
                new Filter { FilterNameId = 4, FilterValueId=98, CarId=47 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=47 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=48 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=48 },
                new Filter { FilterNameId = 3, FilterValueId=27, CarId=48 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=48 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=48 },

                                new Filter { FilterNameId = 1, FilterValueId=2, CarId=49 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=49 },
                new Filter { FilterNameId = 3, FilterValueId=16, CarId=49 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=49 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=49 },


                new Filter { FilterNameId = 1, FilterValueId=2, CarId=50 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=50 },
                new Filter { FilterNameId = 3, FilterValueId=90, CarId=50 },
                new Filter { FilterNameId = 4, FilterValueId=96, CarId=50 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=50 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=51 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=51 },
                new Filter { FilterNameId = 3, FilterValueId=86, CarId=51 },
                new Filter { FilterNameId = 4, FilterValueId=98, CarId=51 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=51 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=52 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=52 },
                new Filter { FilterNameId = 3, FilterValueId=27, CarId=52 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=52 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=52},
                                new Filter { FilterNameId = 1, FilterValueId=2, CarId=53 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=53 },
                new Filter { FilterNameId = 3, FilterValueId=16, CarId=53 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=53 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=53 },


                new Filter { FilterNameId = 1, FilterValueId=2, CarId=54 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=54 },
                new Filter { FilterNameId = 3, FilterValueId=90, CarId=54 },
                new Filter { FilterNameId = 4, FilterValueId=96, CarId=54 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=54 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=55 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=55 },
                new Filter { FilterNameId = 3, FilterValueId=86, CarId=55 },
                new Filter { FilterNameId = 4, FilterValueId=98, CarId=55 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=55 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=56 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=56 },
                new Filter { FilterNameId = 3, FilterValueId=27, CarId=56 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=56 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=56 },
                                new Filter { FilterNameId = 1, FilterValueId=2, CarId=57 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=57 },
                new Filter { FilterNameId = 3, FilterValueId=16, CarId=57 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=57 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=57 },


                new Filter { FilterNameId = 1, FilterValueId=2, CarId=58 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=58 },
                new Filter { FilterNameId = 3, FilterValueId=90, CarId=58 },
                new Filter { FilterNameId = 4, FilterValueId=96, CarId=58},
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=58 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=59 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=59 },
                new Filter { FilterNameId = 3, FilterValueId=86, CarId=59 },
                new Filter { FilterNameId = 4, FilterValueId=98, CarId=59 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=59 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=60 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=60 },
                new Filter { FilterNameId = 3, FilterValueId=27, CarId=60 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=60 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=60 },
                                new Filter { FilterNameId = 1, FilterValueId=2, CarId=61 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=61 },
                new Filter { FilterNameId = 3, FilterValueId=16, CarId=61 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=61 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=61 },


                new Filter { FilterNameId = 1, FilterValueId=2, CarId=62 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=62 },
                new Filter { FilterNameId = 3, FilterValueId=90, CarId=62 },
                new Filter { FilterNameId = 4, FilterValueId=96, CarId=62 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=62 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=63 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=63 },
                new Filter { FilterNameId = 3, FilterValueId=86, CarId=63 },
                new Filter { FilterNameId = 4, FilterValueId=98, CarId=63 },
                new Filter { FilterNameId = 5, FilterValueId=100, CarId=63 },

                new Filter { FilterNameId = 1, FilterValueId=2, CarId=64 },
                new Filter { FilterNameId = 2, FilterValueId=5, CarId=64 },
                new Filter { FilterNameId = 3, FilterValueId=27, CarId=64 },
                new Filter { FilterNameId = 4, FilterValueId=97, CarId=64 },
                new Filter { FilterNameId = 5, FilterValueId=99, CarId=64 },
            };
            foreach (var item in filters)
            {
                var f = context.Filters.SingleOrDefault(p => p == item);
                if (f == null)
                {
                    context.Filters.Add(new Filter { FilterNameId = item.FilterNameId, FilterValueId = item.FilterValueId, CarId = item.CarId });
                    context.SaveChanges();
                }
            }
            #endregion

            MakesAndModels[] makesAndModels =
            {
                new MakesAndModels { FilterMakeId = 18, FilterValueId=7 },
                new MakesAndModels { FilterMakeId = 18, FilterValueId=8 },
                new MakesAndModels { FilterMakeId = 18, FilterValueId=9 },
                new MakesAndModels { FilterMakeId = 18, FilterValueId=10 },
                new MakesAndModels { FilterMakeId = 18, FilterValueId=11},

                new MakesAndModels { FilterMakeId = 17, FilterValueId=12 },
                new MakesAndModels { FilterMakeId = 17, FilterValueId=13 },
                new MakesAndModels { FilterMakeId = 17, FilterValueId=14 },
                new MakesAndModels { FilterMakeId = 17, FilterValueId=15 },
                new MakesAndModels { FilterMakeId = 17, FilterValueId=16 },

                new MakesAndModels { FilterMakeId = 16, FilterValueId=17 },
                new MakesAndModels { FilterMakeId = 16, FilterValueId=18 },
                new MakesAndModels { FilterMakeId = 16, FilterValueId=19 },
                new MakesAndModels { FilterMakeId = 16, FilterValueId=20 },
                new MakesAndModels { FilterMakeId = 16, FilterValueId=21 },

                new MakesAndModels { FilterMakeId = 15, FilterValueId=22 },
                new MakesAndModels { FilterMakeId = 15, FilterValueId=23 },
                new MakesAndModels { FilterMakeId = 15, FilterValueId=24 },
                new MakesAndModels { FilterMakeId = 15, FilterValueId=25 },
                new MakesAndModels { FilterMakeId = 15, FilterValueId=26 },


                new MakesAndModels { FilterMakeId = 14, FilterValueId=27 },
                new MakesAndModels { FilterMakeId = 14, FilterValueId=28 },
                new MakesAndModels { FilterMakeId = 14, FilterValueId=29 },
                new MakesAndModels { FilterMakeId = 14, FilterValueId=30 },
                new MakesAndModels { FilterMakeId = 14, FilterValueId=31 },
                new MakesAndModels { FilterMakeId = 14, FilterValueId=32 },

                new MakesAndModels { FilterMakeId = 13, FilterValueId=33 },
                new MakesAndModels { FilterMakeId = 13, FilterValueId=34 },
                new MakesAndModels { FilterMakeId = 13, FilterValueId=35 },
                new MakesAndModels { FilterMakeId = 13, FilterValueId=36 },
                new MakesAndModels { FilterMakeId = 13, FilterValueId=37 },

                new MakesAndModels { FilterMakeId = 12, FilterValueId=38 },
                new MakesAndModels { FilterMakeId = 12, FilterValueId=39 },
                new MakesAndModels { FilterMakeId = 12, FilterValueId=40 },
                new MakesAndModels { FilterMakeId = 12, FilterValueId=41 },
                new MakesAndModels { FilterMakeId = 12, FilterValueId=42 },
                new MakesAndModels { FilterMakeId = 12, FilterValueId=43 },

                new MakesAndModels { FilterMakeId = 11, FilterValueId=44 },
                new MakesAndModels { FilterMakeId = 11, FilterValueId=45 },
                new MakesAndModels { FilterMakeId = 11, FilterValueId=46 },
                new MakesAndModels { FilterMakeId = 11, FilterValueId=47 },
                new MakesAndModels { FilterMakeId = 11, FilterValueId=48 },

                new MakesAndModels { FilterMakeId = 10, FilterValueId=49 },
                new MakesAndModels { FilterMakeId = 10, FilterValueId=50 },
                new MakesAndModels { FilterMakeId = 10, FilterValueId=51 },
                new MakesAndModels { FilterMakeId = 10, FilterValueId=52 },
                new MakesAndModels { FilterMakeId = 10, FilterValueId=53 },
                new MakesAndModels { FilterMakeId = 10, FilterValueId=54 },

                new MakesAndModels { FilterMakeId = 9, FilterValueId=55 },
                new MakesAndModels { FilterMakeId = 9, FilterValueId=56 },
                new MakesAndModels { FilterMakeId = 9, FilterValueId=57 },
                new MakesAndModels { FilterMakeId = 9, FilterValueId=58 },
                new MakesAndModels { FilterMakeId = 9, FilterValueId=59 },
                new MakesAndModels { FilterMakeId = 9, FilterValueId=60 },

                new MakesAndModels { FilterMakeId = 8, FilterValueId=61 },
                new MakesAndModels { FilterMakeId = 8, FilterValueId=62 },
                new MakesAndModels { FilterMakeId = 8, FilterValueId=63 },
                new MakesAndModels { FilterMakeId = 8, FilterValueId=64 },
                new MakesAndModels { FilterMakeId = 8, FilterValueId=65 },

                new MakesAndModels { FilterMakeId = 7, FilterValueId=66 },
                new MakesAndModels { FilterMakeId = 7, FilterValueId=67 },
                new MakesAndModels { FilterMakeId = 7, FilterValueId=68 },
                new MakesAndModels { FilterMakeId = 7, FilterValueId=69 },
                new MakesAndModels { FilterMakeId = 7, FilterValueId=70 },

                new MakesAndModels { FilterMakeId = 6, FilterValueId=71 },
                new MakesAndModels { FilterMakeId = 6, FilterValueId=72 },
                new MakesAndModels { FilterMakeId = 6, FilterValueId=73 },
                new MakesAndModels { FilterMakeId = 6, FilterValueId=74 },

                new MakesAndModels { FilterMakeId = 5, FilterValueId=75 },
                new MakesAndModels { FilterMakeId = 5, FilterValueId=76 },
                new MakesAndModels { FilterMakeId = 5, FilterValueId=77 },

                new MakesAndModels { FilterMakeId = 4, FilterValueId=78 },
                new MakesAndModels { FilterMakeId = 4, FilterValueId=79 },
                new MakesAndModels { FilterMakeId = 4, FilterValueId=80 },
                new MakesAndModels { FilterMakeId = 4, FilterValueId=81 },
                new MakesAndModels { FilterMakeId = 4, FilterValueId=82 },


                new MakesAndModels { FilterMakeId = 3, FilterValueId=83 },
                new MakesAndModels { FilterMakeId = 3, FilterValueId=84 },
                new MakesAndModels { FilterMakeId = 3, FilterValueId=85 },
                new MakesAndModels { FilterMakeId = 3, FilterValueId=86 },

                new MakesAndModels { FilterMakeId = 2, FilterValueId=87 },
                new MakesAndModels { FilterMakeId = 2, FilterValueId=88 },
                new MakesAndModels { FilterMakeId = 2, FilterValueId=89 },

                new MakesAndModels { FilterMakeId = 1, FilterValueId=90 },
                new MakesAndModels { FilterMakeId = 1, FilterValueId=91 },
                new MakesAndModels { FilterMakeId = 1, FilterValueId=92 },
            };
            foreach (var item in makesAndModels)
            {
                var f = context.MakesAndModels.SingleOrDefault(p => p == item);
                if (f == null)
                {
                    context.MakesAndModels.Add(new MakesAndModels { FilterMakeId = item.FilterMakeId, FilterValueId = item.FilterValueId });
                    context.SaveChanges();
                }
            }
            var Users = context.Users.ToList();
            for (int i = 1, j = 0; i <= 72; i++, j++)
            {
                var item = new UserCar { CarId = i, UserId = Users[j].Id };
                var check = context.userCars.SingleOrDefault(p => p == item);
                if (check == null)
                {

                    context.userCars.Add(item);
                    context.SaveChanges();

                }
                if (j == 3)
                    j = 0;
            }
        }
    }
}