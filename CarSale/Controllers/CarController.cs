using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
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
using Microsoft.Extensions.Hosting;
using ShopCarApi.Helpers;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

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
            try
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
            catch
            {
                return null;
            }
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
        public Pagination GetCars(int page, int count, int[] value, int[] makeId, double minPrice, double maxPrice = Double.PositiveInfinity)
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
            if (makeId.Length != 0)
            {
                cars = FiltersHelpers.GetCarsByMakes(makeId, _context, cars);
            }
            cars = cars.Where((car) => car.Price >= minPrice && car.Price <= maxPrice).ToList();
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

        [HttpPost("CreateNewCar")]
        public IActionResult CreateNewCar([FromBody] CarAddVM model /*int[] filters, int makeId, int modelId*/)
        {
            if (!ModelState.IsValid)
            {
                var errors = CustomValidator.GetErrorsByModel(ModelState);
                return BadRequest(errors);
            }
            var cars = _context.Cars.SingleOrDefault(p => p.UniqueName == model.UniqueName);
            if (cars == null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", model.UniqueName);
                string state;
                var carModel_name = _context.FilterValues.Where((f) => f.Id == model.ModelId).SingleOrDefault();
                var carMake_name = _context.Makes.Where((f) => f.Id == model.MakeId).SingleOrDefault();

                if (model.Mileage == 0)
                {
                    state = "New";
                }
                else
                {
                    state = "Used";
                }
                Car car = new Car
                {
                    Date = model.Date,
                    Name = carMake_name.Name + " " + carModel_name.Name,
                    Price = model.Price,
                    UniqueName = model.UniqueName,
                    Mileage = model.Mileage,
                    State = state
                };

                _context.Cars.Add(car);
                _context.SaveChanges();
                var newCar = _context.Cars.Where((p) => p.UniqueName == model.UniqueName).SingleOrDefault();
                List<FilterNameGroup> l = new List<FilterNameGroup>();
                foreach (var item in model.Filters)
                {
                    l.Add(_context.FilterNameGroups.Where(p => p.FilterValueId == item).SingleOrDefault());
                }
                foreach (var item in l)
                {
                    Filter filter = new Filter { CarId = newCar.Id, FilterNameId = item.FilterNameId, FilterValueId = item.FilterValueId };
                    var f = _context.Filters.SingleOrDefault(p => p == filter);
                    if (f == null)
                    {
                        _context.Filters.Add(filter);
                        _context.SaveChanges();
                    }
                }
                _context.filterMakes.Add(new FilterMake { CarId = newCar.Id, MakeNameId = model.MakeId });
                _context.SaveChanges();

                if (!Directory.Exists(filePath))
                {
                    string[] files = System.IO.Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images"));
                    Directory.CreateDirectory(filePath);
                    int i = 0;
                    foreach (string s in files)
                    {
                        string fileName;
                        if (i == 0)
                        {
                            fileName = model.UniqueName;
                        }
                        else
                        {
                            fileName = System.IO.Path.GetFileName(s);
                        }
                        var destFile = System.IO.Path.Combine(filePath, fileName + ".jpg");
                        System.IO.File.Move(s, destFile);
                        i++;
                    }
                }
                // _context.userCars.Add(new UserCar { CarId = newCar.Id,UserOf=_context.Users[0]. })

                return Ok();
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
        private async static void UploadFile(IFormFile file)
        {
            string path = "/wwwrot/images";

            var uploads = Path.Combine(path);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", file.FileName);

            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {

                    await file.CopyToAsync(fileStream);

                }
            }

            FileInfo File = new FileInfo(filePath);

            File.MoveTo(Path.ChangeExtension(filePath, ".jpg"));

        }
        [HttpPost("Images")]
        public dynamic Images(IFormCollection form)
        {
            try
            {
                foreach (var file in form.Files)
                {
                    UploadFile(file);

                }
                return new { Success = true };
            }
            catch (Exception ex)
            {
                return new { Success = false, ex.Message };
            }
        }
    }
}