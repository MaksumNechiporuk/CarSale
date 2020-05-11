using CarSale.Entities;
using CarSale.ViewModels;
using Microsoft.EntityFrameworkCore;
using ShopCarApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSale.Helpers
{
    static public class FiltersHelpers
    {
        static public List<FValueViewModel> GetModelsByMakes(int[] id, DBContext _context)
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
            return modelsList;
        }
        static public List<Car> GetCarsByMake(int[] values, DBContext _context)
        {
            int[] filterValueSearchList = values;
            var query = _context
                .Cars
                .Include(f => f.Filtres)
                .AsQueryable();
            List<FNameViewModel> filtersList = GetListFilters(_context);
            foreach (var fName in filtersList)
            {
                int count = 0; //Кількість співпадінь у даній групі фільтрів
                var predicate = PredicateBuilder.False<Car>();
                foreach (var fValue in fName.Children)
                {
                    for (int i = 0; i < filterValueSearchList.Length; i++)
                    {
                        var idV = fValue.Id;
                        if (filterValueSearchList[i] == idV)
                        {
                            predicate = predicate
                                .Or(p => p.Filtres
                                    .Any(f => f.FilterValueId == idV));
                            count++;
                        }
                    }
                }
                if (count != 0)
                    query = query.Where(predicate);
            }


            var listProductSearch = query.Select(p => new Car
            {
                Id = p.Id,
                Price = p.Price,
                UniqueName = p.UniqueName,
                Name = p.Name,
                Date = p.Date,
                Mileage = p.Mileage,
                State = p.State
            }).ToList();
            return listProductSearch;
        }
        static public List<Car> GetCarsByFilter(int[] values, DBContext _context)
        {
            int[] filterValueSearchList = values;
            var query = _context
                .Cars
                .Include(f => f.Filtres)
                .AsQueryable();
            List<FNameViewModel> filtersList = GetListFilters(_context);
            foreach (var fName in filtersList)
            {
                int count = 0; //Кількість співпадінь у даній групі фільрів
                var predicate = PredicateBuilder.False<Car>();
                foreach (var fValue in fName.Children)
                {
                    for (int i = 0; i < filterValueSearchList.Length; i++)
                    {
                        var idV = fValue.Id;
                        if (filterValueSearchList[i] == idV)
                        {
                            predicate = predicate
                                .Or(p => p.Filtres

                                    .Any(f => f.FilterValueId == idV));
                            count++;
                        }
                    }
                }
                if (count != 0)
                    query = query.Where(predicate);
            }


            var listProductSearch = query.Select(p => new Car
            {
                Id = p.Id,
                Price = p.Price,
                UniqueName = p.UniqueName,
                Name = p.Name,
                Date = p.Date,
                Mileage = p.Mileage,
                State = p.State
            }).ToList();
            return listProductSearch;
        }
        static public List<FNameViewModel> GetListFilters(DBContext context)
        {

            var queryName = from f in context.FilterNames.AsQueryable()
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
            var result = from fName in groupNames
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
                         };
            return result.ToList();
        }
    }
}
