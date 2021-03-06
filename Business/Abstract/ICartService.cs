
using ProjeCoreOrnekOzellikler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.Business.Abstract
{
    public interface ICartService
    {
        void AddToCart(Cart cart, cdItem cdItem,int quantity);
        void RemoveFromCart(Cart cart, cdItem cdItem);
        void ClearCart(Cart cart);
    }
}
