using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Models.DTO;

namespace Project.DAL
{
    public class CategoryDal : ICategoryDal
    {
        private readonly Context context;
        private readonly ILogger<CategoryDal> _logger;

        public CategoryDal(Context context, ILogger<CategoryDal> _logger)
        {
            this.context = context;
            this._logger = _logger;
        }
        public async Task<Category> GetCategoryById(int Id)
        {
            try
            {
                return await context.Categories.FirstAsync(c => c.Id == Id);
            }
            catch (Exception)
            {

                _logger.LogInformation("categoris not found");
                return null;
            }

        }
        public async Task<Category> GetCategoryByName(string name)
        {
            try
            {
                return await context.Categories.FirstAsync(c => c.Name == name);
            }
            catch (Exception)
            {

                _logger.LogInformation("categoris not found");
                return null;
            }

        }
        public async Task<Category> AddCategory(Category category)
        {
            try
            {
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                return category;
            }
            catch (Exception)
            {
                _logger.LogInformation("categories not added");
                return null;
            }
        }
        public async Task<Category> ChangeName(Category c)
        {
            try
            {
                context.Categories.Update(c);
                await context.SaveChangesAsync();
                return c;
            }
            catch (Exception)
            {
                _logger.LogInformation("categories not changed");
                return null;
            }
        }
        public async Task<Category> DeleteCategory(int IdCategory)
        {
            try
            {
                Category category = await context.Categories.FirstAsync(x => x.Id == IdCategory);
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return category;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("error on deletec category");
                return null;
            }
        }
        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                return await context.Categories.Select(x => x).ToListAsync();
            }
            catch (Exception)
            {
                _logger.LogInformation("categories not found");
                return null;

            }
        }
    }
}
