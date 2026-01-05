using invetario_api.Modules.categories.entity;

namespace invetario_api.Modules.categories.response
{
    public class CategorySingleResponse
    {
        public int categoryId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool status { get; set; }

        public static CategorySingleResponse fromEntity(Category category)
        {
            return new CategorySingleResponse
            {
                categoryId = category.categoryId,
                name = category.name,
                description = category.description,
                status = category.status
            };
        }

        public static List<CategorySingleResponse> fromEntityList(List<Category> categories)
        {
            var responseList = new List<CategorySingleResponse>();

            foreach (var category in categories)
            {
                responseList.Add(fromEntity(category));
            }

            return responseList;
        }
    }
}
