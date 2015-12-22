using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuitarStore.Models
{
    public class Data
    {
        #region Data
        public static List<InstrumentType> InstrumentTypes { get; set; }
        public static List<Instrument> Instruments { get; set; }
        public static List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public static List<Order> Orders { get; set; }
        #endregion

        #region Load
        public void Load()
        {
            InstrumentTypes = new List<InstrumentType>();
            Instruments = new List<Instrument>();
            ShoppingCartItems = new List<ShoppingCartItem>();
            Orders = new List<Order>();

            // Parse XML and load all data to the data structures above
            GuitarStoreXmlReader.Load();
        }
        #endregion
    }
}