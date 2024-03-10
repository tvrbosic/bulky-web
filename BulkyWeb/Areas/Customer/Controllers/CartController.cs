using Bulky.DataAccess.Repository.Interfaces;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartEntries = _unitOfWork.ShoppingCart.GetAll(item => item.ApplicationUserId == userId, includeProperties: "Product"),
                OrderHeader = new()
            };

            foreach (var cartEntry in ShoppingCartVM.ShoppingCartEntries)
            {
                cartEntry.Price = GetPriceBasedOnQuantity(cartEntry);
                ShoppingCartVM.OrderHeader.OrderTotal += (cartEntry.Price * cartEntry.Count);
            }

            return View(ShoppingCartVM);
        }

        public IActionResult Summary() {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartEntries = _unitOfWork.ShoppingCart.GetAll(item => item.ApplicationUserId == userId, includeProperties: "Product"),
                OrderHeader = new()
            };

            ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(user => user.Id == userId);
            
            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
            ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
            ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
            ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;

            foreach (var cartEntry in ShoppingCartVM.ShoppingCartEntries)
            {
                cartEntry.Price = GetPriceBasedOnQuantity(cartEntry);
                ShoppingCartVM.OrderHeader.OrderTotal += (cartEntry.Price * cartEntry.Count);
            }

            return View(ShoppingCartVM);
        }

        public IActionResult IncrementProductQuantity(int entryId)
        {
            var cartEntry = _unitOfWork.ShoppingCart.Get(entry => entry.Id == entryId);
            cartEntry.Count += 1;
            _unitOfWork.ShoppingCart.Update(cartEntry);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DecrementProductQuantity(int entryId)
        {
            var cartEntry = _unitOfWork.ShoppingCart.Get(entry => entry.Id == entryId);
            if (cartEntry.Count <= 1)
            {
                _unitOfWork.ShoppingCart.Delete(cartEntry);
            }
            else
            {
                cartEntry.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cartEntry);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveProduct(int entryId)
        {
            var cartEntry = _unitOfWork.ShoppingCart.Get(entry => entry.Id == entryId);
            _unitOfWork.ShoppingCart.Delete(cartEntry);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));

        }

        private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            if (shoppingCart.Count <= 50)
            {
                return shoppingCart.Product.Price;
            }
            else if (shoppingCart.Count > 50 && shoppingCart.Count <= 100)
            {
                return shoppingCart.Product.Price50;
            }
            else
            {
                return shoppingCart.Product.Price100;
            }
        }
    }
}
