using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CarSale.Entities;
using CarSale.Helpers;
using CarSale.ViewModel;
using CarSale.ViewModels;
using Entities.Models;
using Helpers;
using Image.Help;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopCarApi.Helpers;

namespace CarSale.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {

        private readonly DBContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _env;
        IServiceScope scope;
        UserManager<AppUser> managerUser;
        RoleManager<IdentityRole> managerRole;
        public CarController(IHostingEnvironment env,
            IConfiguration configuration,
            DBContext context, IServiceProvider services)
        {
            _configuration = configuration;
            _env = env;
            _context = context;
            scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            managerUser = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        }
        [HttpGet("OwnerByCarId")]
        public ShowUserViewModel OwnerByCarId(int id)
        {
            var query = _context.userCars.SingleOrDefault((car) => car.CarId == id);
            var user = _context.Users.SingleOrDefault((u) => u.Id == query.UserId);
            var showUser = new ShowUserViewModel()
            {
                City = user.City,
                Country = user.Country,
                Email = user.Email,
                Img = user.Img,
                Name = user.Name + " " + user.Surname,
                Phone = user.PhoneNumber
            };
            return showUser;
        }
        [HttpGet("CarsById")]
        public IActionResult CarsById(int id)
        {
            var _filters = (from g in _context.Filters
                            select g);
            var valueFilters = (from g in _context.FilterValues
                                select g).AsQueryable();
            var nameFilters = (from g in _context.FilterNames
                               select g).AsQueryable();
            var cars = (from g in _context.Cars
                        where g.Id == id
                        select g).AsQueryable();
            string path = "images";
            var resultCar = (from c in cars
                             join g in _filters on c.Id equals g.CarId into ua
                             from aEmp in ua.DefaultIfEmpty()
                             group ua by
                             new CarVM
                             {
                                 Id = c.Id,
                                 Date = c.Date,
                                 Image = Directory.GetFiles($"wwwroot/{path}/{c.UniqueName}").ToList(),
                                 Price = c.Price,
                                 Name = c.Name,
                                 UniqueName = c.UniqueName,
                                 filters = (from f in ua
                                            group f by new FNameGetViewModel
                                            {
                                                Id = f.FilterNameId,
                                                Name = f.FilterNameOf.Name,
                                                Children = new FValueViewModel { Id = f.FilterValueId, Name = f.FilterValueOf.Name }
                                            } into b
                                            select b.Key)
                                            .ToList()
                             } into b
                             select b.Key).LastOrDefault();
            int i = resultCar.filters.Where(p => p.Name == "Model").Select(p => p.Children.Id).SingleOrDefault();
            var m = GetMakes(i);
            if (m != null)
                resultCar.filters.Add(m);
            for (int j = 0; j < resultCar.Image.Count; j++)
            {
                resultCar.Image[j] = resultCar.Image[j].Replace("wwwroot/", "");
            }

            return Ok(resultCar);
        }
        [HttpGet("GetCarsForUpdate")]
        public IActionResult GetCarsForUpdate(int CarId)
        {
            var Id = _context.Filters.Where(p => p.CarId == CarId).Select(p => new Filter
            {
                FilterValueId = p.FilterValueId
            });
            var id = new List<int>();
            foreach (var item in Id)
            {
                id.Add(item.FilterValueId);
            }
            var car = _context.Cars.Where(p => p.Id == CarId).Select(p => new CarUpdateVM
            {
                Id = p.Id,
                Date = p.Date,
                FilterAdd = new FilterAddWithCarVM { IdCar = CarId, IdValue = id },
                Name = p.Name,
                Price = p.Price,
                UniqueName = p.UniqueName
            }).FirstOrDefault();
            return Ok(car);
        }
        [HttpGet("GetCars")]
        public Pagination GetCars(int page, int count, int[] value, int[] makeId)
        {
            string path = "images";
            List<Car> cars;
            if (value != null)
            {
                cars = FiltersHelpers.GetCarsByFilter(value, _context);
            }
            else
            {
                cars = (from g in _context.Cars
                        select g).ToList();
            }

            var resultCar = (from c in cars
                             select
                             new CarShowVM
                             {
                                 Id = c.Id,
                                 Image = $"{path}/{c.UniqueName}/{c.UniqueName}.jpg",
                                 Price = c.Price,
                                 Name = c.Name,
                                 UniqueName = c.UniqueName,
                                 State = c.State,
                                 Date = c.Date,
                                 Mileage = c.Mileage
                             }).AsQueryable();
            var pagination = new Pagination()
            {
                Cars = PagedList<CarShowVM>.ToPagedList(resultCar, page, count),
                CountPage = (int)Math.Ceiling(cars.Count / (double)count)
            };
            return pagination;
        }

        public FNameGetViewModel GetMakes(int id)
        {
            var make = _context.MakesAndModels.Where(p => p.FilterValueId == id).Select(f => new FNameGetViewModel
            {
                Id = f.FilterMakeId,
                Name = "Make",
                Children = new FValueViewModel { Id = f.FilterMakeId, Name = f.FilterMakeOf.Name }
            }).SingleOrDefault();
            if (make != null)
                return make;
            return null;
        }
        [HttpPost("UpdateFilterWithCars")]
        public IActionResult UpdateFilterWithCars([FromBody]FilterAddWithCarVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = CustomValidator.GetErrorsByModel(ModelState);
                return BadRequest(errors);
            }
            var filters = _context.Filters.Where(p => p.CarId == model.IdCar).ToList();
            foreach (var item in filters)
            {
                _context.Filters.Remove(item);
                _context.SaveChanges();
            }
            List<FilterNameGroup> l = new List<FilterNameGroup>();
            foreach (var item in model.IdValue)
            {
                l.Add(_context.FilterNameGroups.Where(p => p.FilterValueId == item).SingleOrDefault());
            }
            foreach (var item in l)
            {
                Filter filter = new Filter { CarId = model.IdCar, FilterNameId = item.FilterNameId, FilterValueId = item.FilterValueId };
                var f = _context.Filters.SingleOrDefault(p => p == filter);
                if (f == null)
                {
                    _context.Filters.Add(filter);
                    _context.SaveChanges();
                }
            }
            return Ok();
        }

        [HttpPost("CreateFilterWithCars")]
        public IActionResult CreateFilterWithCars([FromBody]FilterAddWithCarVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = CustomValidator.GetErrorsByModel(ModelState);
                return BadRequest(errors);
            }
            List<FilterNameGroup> l = new List<FilterNameGroup>();
            foreach (var item in model.IdValue)
            {
                l.Add(_context.FilterNameGroups.Where(p => p.FilterValueId == item).SingleOrDefault());
            }
            foreach (var item in l)
            {
                Filter filter = new Filter { CarId = model.IdCar, FilterNameId = item.FilterNameId, FilterValueId = item.FilterValueId };
                var f = _context.Filters.SingleOrDefault(p => p == filter);
                if (f == null)
                {
                    _context.Filters.Add(filter);
                    _context.SaveChanges();
                }
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult Create([FromBody]CarAddVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = CustomValidator.GetErrorsByModel(ModelState);
                return BadRequest(errors);
            }
            string dirName = "images";
            string dirPathSave = Path.Combine(dirName, model.UniqueName);
            if (!Directory.Exists(dirPathSave))
            {
                Directory.CreateDirectory(dirPathSave);
            }
            var bmp = model.MainImage.FromBase64StringToImage();
            var imageName = model.UniqueName;
            string fileSave = Path.Combine(dirPathSave, $"{imageName}");

            var bmpOrigin = new System.Drawing.Bitmap(bmp);
            string[] imageNames = {$"50_"+ imageName + ".jpg" ,
                    $"100_" + imageName + ".jpg",
                     $"300_" + imageName + ".jpg",
                   $"600_" + imageName + ".jpg",
                    $"1280_"+ imageName + ".jpg"};

            Bitmap[] imageSave = { ImageWorker.CreateImage(bmpOrigin, 50, 50),
                    ImageWorker.CreateImage(bmpOrigin, 100, 100),
                    ImageWorker.CreateImage(bmpOrigin, 300, 300),
                    ImageWorker.CreateImage(bmpOrigin, 600, 600),
                    ImageWorker.CreateImage(bmpOrigin, 1280, 1280)};

            for (int i = 0; i < imageNames.Count(); i++)
            {
                var imageSaveEnd = System.IO.Path.Combine(dirPathSave, imageNames[i]);
                imageSave[i].Save(imageSaveEnd, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            dirPathSave = Path.Combine(dirName, model.UniqueName, "Photo");
            if (!Directory.Exists(dirPathSave))
            {
                Directory.CreateDirectory(dirPathSave);
            }
            for (int i = 0; i < model.AdditionalImage.Count; i++)
            {
                bmp = model.AdditionalImage[i].FromBase64StringToImage();
                fileSave = Path.Combine(dirPathSave);

                bmpOrigin = new System.Drawing.Bitmap(bmp);
                string[] imageNamess = {$"50_{i+1}_"+ imageName + ".jpg" ,
                    $"100_{i+1}_" + imageName + ".jpg",
                     $"300_{i+1}_" + imageName + ".jpg",
                   $"600_{i+1}_" + imageName + ".jpg",
                    $"1280_{i+1}_"+ imageName + ".jpg"};

                Bitmap[] imageSaves = { ImageWorker.CreateImage(bmpOrigin, 50, 50),
                    ImageWorker.CreateImage(bmpOrigin, 100, 100),
                    ImageWorker.CreateImage(bmpOrigin, 300, 300),
                    ImageWorker.CreateImage(bmpOrigin, 600, 600),
                    ImageWorker.CreateImage(bmpOrigin, 1280, 1280)};

                for (int j = 0; j < imageNamess.Count(); j++)
                {
                    var imageSaveEnd = System.IO.Path.Combine(dirPathSave, imageNamess[j]);
                    imageSaves[j].Save(imageSaveEnd, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            var cars = _context.Cars.SingleOrDefault(p => p.UniqueName == model.UniqueName);
            if (cars == null)
            {
                Car car = new Car
                {
                    UniqueName = model.UniqueName,
                    Date = model.Date,
                    Name = model.Name,
                    Price = model.Price
                };
                _context.Cars.Add(car);
                _context.SaveChanges();
                return Ok(car.Id);
            }
            return BadRequest(new { name = "Даний автомобіль вже добалений" });
        }
        [HttpDelete]
        public IActionResult Delete([FromBody]CarDeleteVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = CustomValidator.GetErrorsByModel(ModelState);
                return BadRequest(errors);
            }
            var car = _context.Cars.SingleOrDefault(p => p.Id == model.Id);
            if (car != null)
            {
                DeleteValue(car.Id);
                _context.Cars.Remove(car);
                _context.SaveChanges();
            }
            return Ok();
        }
        private void DeleteValue(int id)
        {
            var filters = _context.Filters.Where(p => p.CarId == id).ToList();
            foreach (var item in filters)
            {
                _context.Filters.Remove(item);
                _context.SaveChanges();
            }
        }
        [HttpPut]
        public IActionResult Update([FromBody]CarUpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = CustomValidator.GetErrorsByModel(ModelState);
                return BadRequest(errors);
            }
            if (model.MainImage != null)
            {
                string dirName = "images";
                string dirPathSave = Path.Combine(dirName, model.UniqueName);
                if (!Directory.Exists(dirPathSave))
                {

                    Directory.CreateDirectory(dirPathSave);
                }
                else
                {
                    Directory.Delete(dirPathSave, true);
                    Directory.CreateDirectory(dirPathSave);
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                }
                var bmp = model.MainImage.FromBase64StringToImage();
                var imageName = model.UniqueName;
                string fileSave = Path.Combine(dirPathSave, $"{imageName}");

                var bmpOrigin = new System.Drawing.Bitmap(bmp);
                string[] imageNames = {$"50_"+ imageName + ".jpg" ,
                    $"100_" + imageName + ".jpg",
                    $"300_" + imageName + ".jpg",
                    $"600_" + imageName + ".jpg",
                    $"1280_"+ imageName + ".jpg"};

                Bitmap[] imageSave = { ImageWorker.CreateImage(bmpOrigin, 50, 50),
                    ImageWorker.CreateImage(bmpOrigin, 100, 100),
                    ImageWorker.CreateImage(bmpOrigin, 300, 300),
                    ImageWorker.CreateImage(bmpOrigin, 600, 600),
                    ImageWorker.CreateImage(bmpOrigin, 1280, 1280)};

                for (int i = 0; i < imageNames.Count(); i++)
                {
                    var imageSaveEnd = System.IO.Path.Combine(dirPathSave, imageNames[i]);
                    imageSave[i].Save(imageSaveEnd, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                if (model.AdditionalImage != null)
                {
                    dirPathSave = Path.Combine(dirName, model.UniqueName, "Photo");
                    if (!Directory.Exists(dirPathSave))
                    {
                        Directory.CreateDirectory(dirPathSave);
                    }
                    for (int i = 0; i < model.AdditionalImage.Count; i++)
                    {
                        bmp = model.AdditionalImage[i].FromBase64StringToImage();
                        fileSave = Path.Combine(dirPathSave);

                        bmpOrigin = new System.Drawing.Bitmap(bmp);
                        string[] imageNamess = {$"50_{i+1}_"+ imageName + ".jpg" ,
                    $"100_{i+1}_" + imageName + ".jpg",
                     $"300_{i+1}_" + imageName + ".jpg",
                   $"600_{i+1}_" + imageName + ".jpg",
                    $"1280_{i+1}_"+ imageName + ".jpg"};

                        Bitmap[] imageSaves = { ImageWorker.CreateImage(bmpOrigin, 50, 50),
                    ImageWorker.CreateImage(bmpOrigin, 100, 100),
                    ImageWorker.CreateImage(bmpOrigin, 300, 300),
                    ImageWorker.CreateImage(bmpOrigin, 600, 600),
                    ImageWorker.CreateImage(bmpOrigin, 1280, 1280)};

                        for (int j = 0; j < imageNamess.Count(); j++)
                        {
                            var imageSaveEnd = System.IO.Path.Combine(dirPathSave, imageNamess[j]);
                            imageSaves[j].Save(imageSaveEnd, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                    }
                }
            }
            var car = _context.Cars.SingleOrDefault(p => p.Id == model.Id);
            if (car != null)
            {

                car.Price = model.Price;
                car.Date = model.Date;
                if (model.Name != "")
                    car.Name = model.Name;
                car.UniqueName = model.UniqueName;

                _context.SaveChanges();
                return Ok(car.Id);
            }
            return BadRequest();

        }
    }
}