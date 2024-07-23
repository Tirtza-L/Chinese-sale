using Project.DAL;
using Project.Models;

namespace Project.BLL
{
    public class CategoryService: ICategoryService
    {
        //add,change name,delete(if no gift used it)
        private readonly ICategoryDal categoryDal;

        public CategoryService(ICategoryDal categoryDal)
        {
            this.categoryDal = categoryDal;
        }
        public async Task<Category> AddCategory(Category category)
        {
            return await categoryDal.AddCategory(category);
        }
        public async Task<Category> ChangeName(int categoryId, string name)
        {
            Category c = await categoryDal.GetCategoryById(categoryId);
            c.Name = name;
            return await categoryDal.ChangeName(c);
        }
        public async Task<Category> DeleteCategory(int IdCategory)
        {
            //לבדוק תנאי - שאין שום מתנה בקטגוריה הזאת
            return await categoryDal.DeleteCategory(IdCategory);
        }
        public async Task<List<Category>> GetAllCategories()
        {
            return await categoryDal.GetAllCategories();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await categoryDal.GetCategoryById(id);
        }
        public async Task<Category> GetCategoryByName(string name)
        {
            return await categoryDal.GetCategoryByName(name);
        }
    }
}
