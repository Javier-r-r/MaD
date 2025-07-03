using System;
using Model.Services.CartService.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.HTTP.Session
{
    public class Cart
    {
        private List<CartLineDto> sesionCart;

        public Cart()
        {
            this.sesionCart = new List<CartLineDto>();
        }

        public List<CartLineDto> cart
        {
            get { return sesionCart ?? (sesionCart = new List<CartLineDto>()); }
        }


        public void AddToCart(CartLineDto cartLine)
        {
            if (sesionCart == null)
            {
                sesionCart = new List<CartLineDto>();
            }

            CartLineDto existingLine = sesionCart.FirstOrDefault(line => line.productName == cartLine.productName);

            if (existingLine != null)
            {
                existingLine.units += cartLine.units;
            }
            else
            {
                sesionCart.Add(cartLine);
            }
        }

        public void RemoveFromCart(string productName)
        {
            if (sesionCart == null) return;

            var itemToRemove = sesionCart.FirstOrDefault(line => line.productName == productName);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
            }
        }

        public void IncreaseProductUnits(string productName)
        {
            if (sesionCart == null) return;

            var item = sesionCart.FirstOrDefault(line => line.productName == productName);
            if (item != null)
            {
                item.units += 1;
            }
        }

        public void DecreaseProductUnits(string productName)
        {
            if (sesionCart == null) return;

            var item = sesionCart.FirstOrDefault(line => line.productName == productName);
            if (item != null)
            {
                item.units -= 1;

                if (item.units <= 0)
                {
                    sesionCart.Remove(item); // Elimina si ya no quedan unidades
                }
            }
        }
    }
}