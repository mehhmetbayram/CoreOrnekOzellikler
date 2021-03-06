using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjeCoreOrnekOzellikler.Business.Abstract;
using ProjeCoreOrnekOzellikler.Entities;

namespace ProjeCoreOrnekOzellikler.Controllers
{

    public class SepetIslemleriController : Controller
    {
        DataContext _context;
        private ICartService _cartService;
        private ICartSessionService _cartSessionService;


        public SepetIslemleriController(DataContext context, ICartService cartService, ICartSessionService cartSessionService)
        {
            _context = context;
            _cartService = cartService;
            _cartSessionService = cartSessionService;


        }

        public IActionResult Index()
        {
            var cdItems = _context.CdItems.OrderBy(x => x.Id).Take(300).ToList();
            return View(cdItems);
        }

        [HttpPost]
        public IActionResult AddCart(int itemId, int quantity = 1)
        {
            var cdItem = _context.CdItems.FirstOrDefault(x => x.Id == itemId);
            var cart = _cartSessionService.GetCart();
            _cartService.AddToCart(cart, cdItem, quantity);
            _cartSessionService.SetCart(cart);

            return RedirectToAction("Basket");
        }

        public IActionResult Basket()
        {
            var model = _cartSessionService.GetCart();

            return View(model);
        }


        public IActionResult RemoveCart(int id)
        {
            var cart = _cartSessionService.GetCart();
            var cdItem = _context.CdItems.FirstOrDefault(x => x.Id == id);
            _cartService.RemoveFromCart(cart, cdItem);
            _cartSessionService.SetCart(cart);
            return RedirectToAction("Basket");
        }

        public IActionResult ClearCart()
        {
            var cart = _cartSessionService.GetCart();
            _cartService.ClearCart(cart);
            _cartSessionService.SetCart(cart);
            return RedirectToAction("Basket");
        }
    }
}