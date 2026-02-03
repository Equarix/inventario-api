using invetario_api.database;
using invetario_api.Exceptions;
using invetario_api.Modules.products.dto;
using invetario_api.Modules.products.entity;
using invetario_api.Modules.products.response;
using Microsoft.EntityFrameworkCore;

namespace invetario_api.Modules.products
{
    public class ProductService : IProductService
    {
        private Database _db;

        public ProductService(Database db)
        {
            _db = db;
        }

        public async Task<ProductResponse?> createProduct(ProductDto product)
        {
            var categoryId = product.categoryId;
            var unitId = product.unitId;
            var imageId = product.imageId;

            var finCategory = await _db.categories.Where(c => c.categoryId == categoryId).FirstOrDefaultAsync();

            if (finCategory == null)
            {
                throw new HttpException(404, "Category not found");
            }

            var findUnit = await _db.units.Where(u => u.unitId == unitId).FirstOrDefaultAsync();
            if (findUnit == null)
            {
                throw new HttpException(404, "Unit not found");
            }

            var findImage = await _db.images.Where(i => i.imageId == imageId).FirstOrDefaultAsync();
            if (findImage == null)
            {
                throw new HttpException(404, "Image not found");
            }

            var newProduct = new Product
            {
                codeInternal = product.codeInternal,
                code = product.code,
                name = product.name,
                description = product.description,
                categoryId = categoryId,
                unitId = unitId,
                priceBuy = product.priceBuy,
                priceSell = product.priceSell,
                minStock = product.minStock,
                imageId = imageId,
                status = true,
            };

            await _db.products.AddAsync(newProduct);
            await _db.SaveChangesAsync();

            var productResponse = ProductResponse.fromEntity(newProduct);

            return productResponse;
        }

        public async Task<ProductResponse?> deleteProduct(int productId)
        {
            var findProduct = await _db.products
                .Include(p => p.category)
                .Include(p => p.unit)
                .Include(p => p.image)
                .Where(p => p.productId == productId)
                .FirstOrDefaultAsync();
            if (findProduct == null)
            {
                throw new HttpException(404, "Product not found");
            }

            findProduct.status = false;
            await _db.SaveChangesAsync();
            return ProductResponse.fromEntity(findProduct);
        }

        public async Task<ProductResponse?> getProductById(int productId)
        {
            var findProduct = await _db.products
                .Include(p => p.category)
                .Include(p => p.unit)
                .Include(p => p.image)
                .Where(p => p.productId == productId)
                .FirstOrDefaultAsync();
            if (findProduct == null)
            {
                throw new HttpException(404, "Product not found");
            }

            return ProductResponse.fromEntity(findProduct);
        }

        public async Task<List<ProductResponse>> getProducts()
        {
            var products = await _db.products
                .Include(p => p.category)
                .Include(p => p.unit)
                .Include(p => p.image)
                .ToListAsync();
            return ProductResponse.fromEntityList(products);
        }

        public async Task<ProductResponse?> updateProduct(int productId, UpdateProductDto product)
        {
            var findProductTask = await _db.products
                .Where(p => p.productId == productId)
                .FirstOrDefaultAsync();

            if (findProductTask == null)
            {
                throw new HttpException(404, "Product not found");
            }

            var findCategory = await _db.categories
                .Where(c => c.categoryId == product.categoryId)
                .FirstOrDefaultAsync();

            if (findCategory == null)
            {
                throw new HttpException(404, "Category not found");
            }

            var findUnit = await _db.units
                .Where(u => u.unitId == product.unitId)
                .FirstOrDefaultAsync();

            if (findUnit == null)
            {
                throw new HttpException(404, "Unit not found");
            }


            var findImage = await _db.images.Where(i => i.imageId == product.imageId).FirstOrDefaultAsync();
            if (findImage == null)
            {
                throw new HttpException(404, "Image not found");
            }

            findProductTask.codeInternal = product.codeInternal;
            findProductTask.code = product.code;
            findProductTask.name = product.name;
            findProductTask.description = product.description;
            findProductTask.categoryId = product.categoryId;
            findProductTask.unitId = product.unitId;
            findProductTask.priceBuy = product.priceBuy;
            findProductTask.priceSell = product.priceSell;
            findProductTask.minStock = product.minStock;
            findProductTask.imageId = product.imageId;
            if (product.status.HasValue)
            {
                findProductTask.status = product.status.Value;
            }
            await _db.SaveChangesAsync();
            return ProductResponse.fromEntity(findProductTask);
        }
    }
}
