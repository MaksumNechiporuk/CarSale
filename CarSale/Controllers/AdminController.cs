using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSale.Entities;
using CarSale.ViewModels;
using Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly DBContext _context;
        public AdminController( DBContext context)
        {
            _context = context;
        }
        [HttpPost("AddFilter")]
        public IActionResult AddFilter(FilterAddViewModel filter)
        {
            if (!ModelState.IsValid)
            {
                var errors = CustomValidator.GetErrorsByModel(ModelState);
                return BadRequest(errors);
            }
            if (_context.FilterNames.SingleOrDefault(f => f.Name == filter.Name) == null)
            {
                _context.FilterNames.Add(
                    new Entities.FilterName
                    {
                        Name = filter.Name
                    });
                _context.SaveChanges();
            }
            foreach (var item in filter.values)
            {
                if (_context.FilterValues
                          .SingleOrDefault(f => f.Name == item) == null)
                {
                    _context.FilterValues.Add(
                        new Entities.FilterValue
                        {
                            Name = item
                        });
                    _context.SaveChanges();
                }
            }
            
            var nId = _context.FilterNames
                        .SingleOrDefault(f => f.Name == filter.Name).Id;
            foreach (var item in filter.values)
            {
                var vId = _context.FilterValues
                .SingleOrDefault(f => f.Name == item).Id;
                if (_context.FilterNameGroups
                    .SingleOrDefault(f => f.FilterValueId == vId &&
                    f.FilterNameId == nId) == null)
                {
                    _context.FilterNameGroups.Add(
                        new Entities.FilterNameGroup
                        {
                            FilterNameId = nId,
                            FilterValueId = vId
                        });
                    _context.SaveChanges();
                }
            }
            
            return Ok();
        }
    }
}