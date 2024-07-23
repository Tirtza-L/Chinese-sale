using Project.Models;

namespace Project.DAL
{
    public interface ICategoryDal
    {
        Task<Category> AddCategory(Category category);
        Task<Category> GetCategoryById(int Id);
        Task<Category> GetCategoryByName(string name);
        Task<Category> ChangeName(Category c);
        Task<Category> DeleteCategory(int IdCategory);
        Task<List<Category>> GetAllCategories();

    }
}
