
using ProjeCoreOrnekOzellikler.Business.Abstract;
using ProjeCoreOrnekOzellikler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.Business.Concrete
{
    public class CartManager : ICartService
    {
        public void AddToCart(Cart cart, cdItem cdItem, int quantity)
        {
            var cartCheck = cart.CartLines.FirstOrDefault(x => x.cdItem.Id == cdItem.Id);
            if (cartCheck != null)
            {
                cartCheck.Quantity++;
            }
            else
            {
                cart.CartLines.Add(new CartLine { cdItem = cdItem, Quantity = quantity });
            }
        }

        public void ClearCart(Cart cart)
        {
            cart.CartLines.Clear();
        }

        public void RemoveFromCart(Cart cart, cdItem cdItem)
        {
            cart.CartLines.Remove(cart.CartLines.FirstOrDefault(x => x.cdItem.Id == cdItem.Id));
        }
    }
}
