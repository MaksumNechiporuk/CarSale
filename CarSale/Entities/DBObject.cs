using CarSale.Models;
using System.Linq;

namespace CarSale.Entities
{
    public class DBObject
    {
        public static void Initial(DBContext content)
        {
            if (!content.Users.Any())
            {
                content.AddRange(
                   new AppUser
                   {
                       Name = "Robert",
                       Surname = "Deniro",
                       Country = "USA",
                       City = "New York",
                       Img = "https://img.icons8.com/officel/2x/user.png"

                   },
                     new AppUser
                     {
                         Name = "Bob",
                         Surname = "Marley",
                         Country = "Jamaica",
                         City = "Nine Mile",
                         Img = "https://img.icons8.com/officel/2x/user.png"
                     },
                       new AppUser
                       {
                           Name = "Jon",
                           Surname = "Show",
                           Country = "Westeros",
                           City = " Winterfell",
                           Img = "https://img.icons8.com/officel/2x/user.png"
                       },
                         new AppUser
                         {
                             Name = "John",
                             Surname = "Sparrow",
                             Country = "Great Britain",
                             City = "London",
                             Img = "https://img.icons8.com/officel/2x/user.png"
                         }
                );
                content.SaveChanges();
            }

        }
    }
}
