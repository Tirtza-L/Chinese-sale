using Project.Models;

namespace Project.BLL
{
    public interface ICategoryService
    {
        public Task<Category> AddCategory(Category category);
        public Task<Category> ChangeName(int categoryId, string name);
        public Task<Category> DeleteCategory(int IdCategory);
        public Task<List<Category>> GetAllCategories();
        public Task<Category> GetCategoryById(int id);
        public Task<Category> GetCategoryByName(string name);
    }
}
