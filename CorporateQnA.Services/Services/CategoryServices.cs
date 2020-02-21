using CorporateQnA.Models;
using CorporateQnA.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using CorporateQnA.Services.Extensions;
using System.Linq;

namespace CorporateQnA.Services.Services
{
    public class CategoryServices: ICategoryServices
    {
        private IDbConnection _connection;
        public CategoryServices(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<CategoryInfo> GetAllCategories()
        {
            using(IDbConnection Db = _connection)
            {
                string sQuery = "SELECT * FROM CategoriesView";
                return Db.Query<Data.CategoryInfo>(sQuery).MapCollectionTo<CategoryInfo>();
            }
        }

        public Category AddCategory(Category category)
        {
            using (IDbConnection Db = _connection)
            {
                string sQuery = "INSERT INTO Categories (Name , Description , CreatedBy, CreatedOn) VALUES(@Name , @Description,@CreatedBy, @CreatedOn)";
                var NewCategory = category.MapTo<Data.Category>();
                Db.Execute(sQuery, NewCategory);
                return Db.Query<Data.Category>("SELECT * FROM Categories WHERE Name =@Name", new { Name = NewCategory.Name }).FirstOrDefault().MapTo<Category>();
            }
        }
    }
}
