using invetario_api.Modules.categories.dto;
using invetario_api.Modules.categories.entity;
using invetario_api.Modules.categories.response;
using invetario_api.utils;

namespace invetario_api.Modules.categories
{
    public interface ICategoryService
    {
        public Task<List<CategorySingleResponse>> getCategories();

        public Task<CategorySingleResponse?> getCategoryById(int categoryId);

        public Task<CategorySingleResponse> createCategory(CategoryDto data);

        public Task<CategorySingleResponse?> updateCategory(int categoryId, UpdateCategoryDto data);

        public Task<CategorySingleResponse?> deleteCategory(int categoryId);
    }
}
