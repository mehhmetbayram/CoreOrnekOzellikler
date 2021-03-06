using Microsoft.AspNetCore.Http;
using ProjeCoreOrnekOzellikler.Business.Abstract;
using ProjeCoreOrnekOzellikler.Entities;
using ProjeCoreOrnekOzellikler.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.Business.Concrete
{
    public class CartSessionManager : ICartSessionService
    {
        private IHttpContextAccessor _httpContextAccessor;
        public CartSessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Cart GetCart()
        {
            var cartSessionCheck = _httpContextAccessor.HttpContext.Session.GetObject<Cart>("cart");

            if (cartSessionCheck==null)
            {
                SetCart(new Cart());
                cartSessionCheck = _httpContextAccessor.HttpContext.Session.GetObject<Cart>("cart");
            }

            return cartSessionCheck;
        }

        public void SetCart(Cart cart)
        {
            _httpContextAccessor.HttpContext.Session.SetObject("cart", cart);
        }
    }
}
