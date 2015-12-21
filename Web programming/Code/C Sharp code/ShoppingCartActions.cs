using GuitarStore.Models;
using System;
using System.Linq;
using System.Web;

namespace GuitarStore.Logic
{
    public class ShoppingCartActions
    {
        #region AddToCart
        public void AddToCart(int InstrumentId)
        {
            ShoppingCartItem cartItem = FindItem(InstrumentId);

            if(cartItem == null)
            {
                cartItem = new ShoppingCartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    InstrumentId = InstrumentId,
                    Instrument = Data.Instruments.FirstOrDefault(
                     p => p.InstrumentId == InstrumentId),
                    Quantity = 1,
                };

                Data.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
        }
        #endregion

        #region RemoveItem
        public void RemoveItem(int removeInstrumentId)
        {
            ShoppingCartItem myItem = FindItem(removeInstrumentId);

            if(myItem != null)
            {
                Data.ShoppingCartItems.Remove(myItem);
            }
        }
        #endregion

        #region UpdateItem
        public void UpdateItem(int updateInstrumentID, int quantity)
        {
            ShoppingCartItem myItem = FindItem(updateInstrumentID);

            if(myItem != null)
            {
                myItem.Quantity = quantity;
            }
        }
        #endregion

        #region FindItem
        private ShoppingCartItem FindItem(int instrumentId)
        {
            foreach(ShoppingCartItem item in Data.ShoppingCartItems)
                if(item.InstrumentId == instrumentId)
                    return item;
            return null;
        }
        #endregion

        #region EmptyCart
        public void EmptyCart()
        {
            Data.ShoppingCartItems.Clear();
        }
        #endregion

        #region UpdateShoppingCart
        public void UpdateShoppingCart(ShoppingCartUpdates[] CartItemUpdates)
        {
            foreach(var item in CartItemUpdates)
            {
                if(item.PurchaseQuantity < 1)
                    RemoveItem(item.InstrumentId);
                else UpdateItem(item.InstrumentId, item.PurchaseQuantity);
            }
        }
        #endregion

        #region GetTotal
        public double GetTotal()
        {
            double total = 0;
            foreach(var item in Data.ShoppingCartItems)
            {
                total += item.Quantity * item.Instrument.Price;
            }
            return total;
        }
        #endregion

        #region GetCount
        public int GetCount()
        {
            return Data.ShoppingCartItems.Count();
        }
        #endregion

        #region ShoppingCartUpdates
        public struct ShoppingCartUpdates
        {
            public int InstrumentId;
            public int PurchaseQuantity;
        }
        #endregion
    }
}