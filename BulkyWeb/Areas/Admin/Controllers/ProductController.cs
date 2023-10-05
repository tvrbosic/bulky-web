﻿using Bulky.DataAccess.Repository.Interfaces;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(item => new SelectListItem
            {
                Text = item.Name,
                Value = item.Id.ToString()
            });

            //ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;
            ProductVM productVM = new()
            {
                CategoryList = CategoryList,
                Product = new Product()
            };

            return View("CreateOrEdit", productVM);
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(item => new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
                
                return View("CreateOrEdit", productVM);
            }
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? product = _unitOfWork.Product.Get(item => item.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(item => new SelectListItem
            {
                Text = item.Name,
                Value = item.Id.ToString()
            });

            ProductVM productVM = new()
            {
                CategoryList = CategoryList,
                Product = product
            };
            return View("CreateOrEdit", productVM);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product updated successfully!";
                return RedirectToAction("Index");
            }
            return View("CreateOrEdit");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? obj = _unitOfWork.Product.Get(item => item.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(item => item.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Delete(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
