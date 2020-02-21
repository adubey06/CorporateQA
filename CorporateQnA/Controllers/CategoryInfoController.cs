using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorporateQnA.Models;
using CorporateQnA.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CorporateQnA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryInfoController : BaseController
    {
        private ICategoryServices _services; 
        public CategoryInfoController(ICategoryServices services)
        {
            _services = services;
        }

        [HttpGet]
        public IEnumerable<CategoryInfo> GetAllCategories()
        {
            return _services.GetAllCategories();
        }

        [HttpPost]
        public ActionResult<Category> Post(Category category)
        {
            return _services.AddCategory(category);
        }
    }
}