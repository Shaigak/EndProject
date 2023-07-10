using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyEndProjectCode.Areas.View_Models;
using MyEndProjectCode.Data;
using MyEndProjectCode.Helpers;
using MyEndProjectCode.Models;
using MyEndProjectCode.Services.Interfaces;

namespace MyEndProjectCode.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]

    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IProductService _productService;
        private readonly ITagService _tagService;
        private readonly ICategoryService _categoryService;

        public ProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment, IProductService productService, ITagService tagService, ICategoryService categoryService)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _productService = productService;
            _tagService = tagService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await _context.Products.Include(m => m.ProductTags).Include(m => m.ProductImages).ToListAsync();

            return View(products);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest();

            Product product = await _context.Products.Include(m => m.ProductImages).Include(m => m.ProductCategories).Include(m => m.ProductTags).FirstOrDefaultAsync(m => m.Id == id);

            if (product is null) return NotFound();

            return View(product);

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            ViewBag.Tags = await GetTagAsync();
            ViewBag.Categories = await GetCategoryAsync();
            ViewBag.Brands = await GetBrandAsync();


            return View();
        }


        public async Task<IActionResult> Delete(int? id)
        {

            Product product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }




        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ProductCreateVM model)
        {
            ViewBag.Tags = await GetTagAsync();
            ViewBag.Categories = await GetCategoryAsync();
            ViewBag.Brands = await GetBrandAsync();


            try
            {
                if (!ModelState.IsValid) return View(model);
                Product newProduct = new();
                List<ProductImage> productImages = new();
                List<ProductTag> productTags = new();
                List<ProductBrand> productBrands = new();
                List<ProductCategory> productCategories = new();

                //if (model.Photos == null)
                //{
                //    return View(model);

                    
                //}


                foreach (var photo in model.Photos)
                {

                    //if (photo == null)
                    //{
                    //    return View(model);
                    //}

                    string fileName = Guid.NewGuid().ToString() + " " + photo.FileName; // herdefe yeni ad duzeldirik . 
                    string root = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images", fileName); // root duzeldirik . 

                    await FileHelper.SaveFileAsync(root, photo);

                    


                    ProductImage productImage = new()
                    {
                        Image = fileName
                    };

                    productImages.Add(productImage);
                }
               
                newProduct.ProductImages = productImages;


                if (model.CategoryIds.Count > 0)
                {
                    foreach (var item in model.CategoryIds)
                    {
                        ProductCategory productCategory = new()
                        {
                            CategoryId = item
                        };

                    }

                    newProduct.ProductCategories = productCategories;
                }
                else
                {

                    ModelState.AddModelError("CategoryIds", "Dont be empty");
                   
                    
                    
                }

                if (model.TagIds.Count > 0)
                {
                    foreach (var item in model.TagIds)
                    {
                        ProductTag product = new()
                        {
                            TagId = item
                        };

                    }
                    newProduct.ProductTags = productTags;
                }
                else
                {
                    ModelState.AddModelError("TagIds", "Dont be empty");
                }


                if (model.BrandIds.Count > 0)
                {
                    foreach (var item in model.BrandIds)
                    {
                        ProductBrand product = new()
                        {
                            BrandProId = item
                        };

                    }
                    newProduct.ProductBrands = productBrands;
                }
                else
                {
                    ModelState.AddModelError("TagIds", "Dont be empty");
                }



                Random random = new();

                var converTedprice = decimal.Parse(model.Price);


                newProduct.Name = model.Name;
                newProduct.Price = converTedprice;
                newProduct.Description = model.Description;
                newProduct.Rate = model.Rate;




                await _context.ProductImages.AddRangeAsync(productImages);

                await _context.Products.AddAsync(newProduct);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Categories = await GetCategoryAsync();
            ViewBag.Tags = await GetTagAsync();
            ViewBag.Brands = await GetBrandAsync();


            if (id == null) return BadRequest();
            Product dbProduct = await _productService.GetById(id);
            if (dbProduct == null) return NotFound();

            ProductUpdateVM model = new()
            {
                Id = dbProduct.Id,
                Images = dbProduct.ProductImages.ToList(),
                Name = dbProduct.Name,
                Description = dbProduct.Description, 
                Price = dbProduct.Price,
                SaleCount = dbProduct.SaleCount,
                StockCount = dbProduct.StockCount,
                TagIds = dbProduct.ProductTags.Select(m => m.Tag.Id).ToList(),
                CategoryIds = dbProduct.ProductCategories.Select(m => m.Category.Id).ToList(),
                BrandIds = dbProduct.ProductBrands?.Select(m => m.BrandPro.Id).ToList()

            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ProductUpdateVM model)
        {
            ViewBag.categories = await GetCategoryAsync();
            ViewBag.tags = await GetTagAsync();
            ViewBag.Brands = await GetBrandAsync();



            if (id is null) return BadRequest();


            Product product = await _productService.GetById((int)id);

            if (product is null) return NotFound();

            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}

            List<ProductImage> productImages = new();
            List<ProductCategory> productCategories = new();
            List<ProductTag> productTags = new();
            List<ProductBrand> productBrands = new();

           
            if (model.Photos is not null)
            {
                foreach (var photo in model.Photos)
                {
                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View();
                    }

                    //if (!photo.CheckFileSize(500))
                    //{
                    //    ModelState.AddModelError("Photo", "Image size must be max 200kb");
                    //    return View();
                    //}
                }

                foreach (var photo in model.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string path = FileHelper.GetFilePath(_webHostEnvironment.WebRootPath, "assets/images", fileName);
                    await FileHelper.SaveFileAsync(path, photo);

                    ProductImage productImage = new()
                    {
                        Image = fileName
                    };

                    productImages.Add(productImage);
                }
                product.ProductImages = productImages;
            }
            else
            {
                model.Images = product.ProductImages.ToList();
            }

            if (model.CategoryIds.Count > 0)
            {
                foreach (var cateId in model.CategoryIds)
                {
                    ProductCategory productCategory = new()
                    {
                        CategoryId = cateId
                    };

                    productCategories.Add(productCategory);
                }
                product.ProductCategories = productCategories;
            }
            else
            {
                ModelState.AddModelError("CategoryIds", "Don't be empty");
                return View();
            }



            if (model.TagIds.Count > 0)
            {
                foreach (var tagId in model.TagIds)
                {
                    ProductTag productTag = new()
                    {
                        TagId = tagId
                    };

                    productTags.Add(productTag);
                }
                product.ProductTags = productTags;
            }
            else
            {
                ModelState.AddModelError("TagIds", "Don't be empty");
                return View(model);
            }



            if (model.BrandIds.Count > 0)
            {
                foreach (var brandId in model.BrandIds)
                {
                    ProductBrand productBrand = new()
                    {
                         BrandProId = brandId
                    };

                    productBrands.Add(productBrand);
                }
                product.ProductBrands = productBrands;
            }
            else
            {
                ModelState.AddModelError("BrandIds", "Don't be empty");
                return View();
            }

            product.Id = model.Id;
            product.Name = model.Name;
            product.Price = model.Price;
            product.SaleCount = model.SaleCount;
            product.StockCount = model.StockCount;
            product.Description = model.Description;


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));



        }




        private async Task<SelectList> GetCategoryAsync()
        {
            List<Category> categories = await _context.Categories.ToListAsync();
            return new SelectList(categories, "Id", "Name");
        }

        private async Task<SelectList> GetTagAsync()
        {
            List<Tag> tags = await _context.Tags.ToListAsync();
            return new SelectList(tags, "Id", "Year");
        }

        private async Task<SelectList> GetBrandAsync()
        {
            List<BrandPro> brands = await _context.BrandPros.ToListAsync();
            return new SelectList(brands, "Id", "Name");
        }

    }






}
