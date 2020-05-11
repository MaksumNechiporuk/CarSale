using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSale.Entities;
using CarSale.Helpers;
using CarSale.ViewModel;
using CarSale.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopCarApi.Helpers;

namespace CarSale.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FiltersController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _env;

        public FiltersController(IHostingEnvironment env,
            IConfiguration configuration,
            DBContext context)
        {
            _configuration = configuration;
            _env = env;
            _context = context;
        }
        [HttpGet("GetMakes")]
        public IActionResult GetMakes()
        {
            var model = _context.Makes.Select(
                p => new MakeVM
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList();
            return Ok(model);
        }

        //[HttpGet("CarsByFilter")]
        //public Pagination FilterData(int[] value)
        //{
        //    var filters = FiltersHelpers.GetListFilters(_context);
        //    var list = GetCarsByFilter(value, filters).AsQueryable();
        //    int count = 9;
        //    var pagination = new Pagination()
        //    {
        //        Cars = PagedList<CarShowVM>.ToPagedList(list, 1, 9),
        //        CountPage = 8
        //    };
        //    return pagination;
        //}
        [HttpGet]
        public IActionResult FilterData()
        {
            var filters = FiltersHelpers.GetListFilters(_context);

            return Ok(filters);
        }
        [HttpGet("GetFiltersByName")]
        public IActionResult GetFiltersByName(string name)
        {
            var filters = GetListFilters(_context, name);
            return Ok(filters);
        }
        [HttpGet("GetMakeByModels")]
        public IActionResult GetMakeByModels(int id)
        {
            var makeAndModels = (from g in _context.MakesAndModels
                                 select g).Where(g => g.FilterValueId == id).LastOrDefault();
            if (makeAndModels != null)
                return Ok(makeAndModels.FilterMakeId);
            return Ok();
        }
        [HttpGet("GetModelsByMakes")]
        public IActionResult GetModelsByMakes(int[] id)
        {
            var makeAndModels = (from g in _context.MakesAndModels
                                 select g).AsQueryable();
            var valueFilters = (from g in _context.FilterValues
                                select g).AsQueryable();
            var make = (from g in _context.Makes
                        select g).AsQueryable();
            var nameFilters = (from g in _context.FilterNames
                               select g).AsQueryable();
            var filters = FiltersHelpers.GetListFilters(_context);
            var modelsList = new List<FValueViewModel>();
            foreach (var item in id)
            {
                var models = (from f in _context.MakesAndModels
                              where f.FilterMakeId == item
                              select new FValueViewModel
                              {
                                  Id = f.FilterValueId,
                                  Name = f.FilterValueOf.Name,
                              }).ToList();
                if (models != null)
                    modelsList.AddRange(models);
            }
            return Ok(modelsList);
        }
        [HttpGet("GetModelsByMake")]
        public IActionResult GetModelsByMake(int id)
        {
            var makeAndModels = (from g in _context.MakesAndModels
                                 select g).AsQueryable();
            var valueFilters = (from g in _context.FilterValues
                                select g).AsQueryable();
            var make = (from g in _context.Makes
                        select g).AsQueryable();
            var nameFilters = (from g in _context.FilterNames
                               select g).AsQueryable();
            var filters = FiltersHelpers.GetListFilters(_context);
            var models = (from f in _context.MakesAndModels
                          where f.FilterMakeId == id
                          group f by new
                          {
                              Id = f.FilterMakeId,
                              Name = f.FilterMakeOf.Name,
                          } into g
                          select new FNameViewModel
                          {
                              Id = g.Key.Id,
                              Name = g.Key.Name,
                              Children = (from value in g
                                          select new FValueViewModel
                                          {
                                              Id = value.FilterValueId,
                                              Name = value.FilterValueOf.Name
                                          })
                                         .OrderBy(l => l.Name).ToList()
                          }).ToList();
            return Ok(models);
        }
        private FNameViewModel GetListFilters(DBContext context, string name)
        {

            var queryName = from f in context.FilterNames.AsQueryable()
                            where f.Name == name
                            select f;
            var queryGroup = from g in context.FilterNameGroups.AsQueryable()
                             select g;

            //Отримуємо загальну множину значень
            var query = from u in queryName
                        join g in queryGroup on u.Id equals g.FilterNameId into ua
                        from aEmp in ua.DefaultIfEmpty()
                        select new
                        {
                            FNameId = u.Id,
                            FName = u.Name,
                            FValueId = aEmp != null ? aEmp.FilterValueId : 0,
                            FValue = aEmp != null ? aEmp.FilterValueOf.Name : null,
                        };

            //Групуємо по іменам і сортуємо по спаданю імен
            var groupNames = (from f in query
                              group f by new
                              {
                                  Id = f.FNameId,
                                  Name = f.FName
                              } into g
                              //orderby g.Key.Name
                              select g).OrderByDescending(g => g.Key.Name);
            //По групах отримуємо
            var result = (from fName in groupNames
                          select
                          new FNameViewModel
                          {
                              Id = fName.Key.Id,
                              Name = fName.Key.Name,
                              Children = (from v in fName
                                          group v by new FValueViewModel
                                          {
                                              Id = v.FValueId,
                                              Name = v.FValue
                                          } into g
                                          select g.Key)
                                          .OrderBy(l => l.Name).ToList()
                          }).SingleOrDefault();
            return result;
        }

    }
}