using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace GuitarStore.Models
{
    public class GuitarStoreXmlReader
    {
        #region Data
        static InstrumentCollection instruments;
        static InstrumentTypeCollection instrumentTypes;
        #endregion

        #region Load
        public static void Load()
        {
            LoadInstruments();
            LoadInstrumentTypes();

            foreach(var item in instruments.Instruments)
            {
                Data.Instruments.Add(item);
            }

            foreach(var item in instrumentTypes.InstrumentTypes)
            {
                Data.InstrumentTypes.Add(item);
            }
        }
        #endregion

        #region LoadInstruments
        public static void LoadInstruments()
        {
            string path = HttpContext.Current.Server.MapPath("~/XMLFiles/Instruments.xml");
            
            XmlSerializer serializer = new XmlSerializer(typeof(InstrumentCollection));

            StreamReader reader = new StreamReader(path);
            instruments = (InstrumentCollection)serializer.Deserialize(reader);
            reader.Close();
        }
        #endregion

        #region LoadInstrumentTypes
        public static void LoadInstrumentTypes()
        {
            string path = HttpContext.Current.Server.MapPath("~/XMLFiles/InstrumentTypes.xml");

            XmlSerializer serializer = new XmlSerializer(typeof(InstrumentTypeCollection));

            StreamReader reader = new StreamReader(path);
            instrumentTypes = (InstrumentTypeCollection)serializer.Deserialize(reader);
            reader.Close();
        }
        #endregion

        #region GetInstruments
        public static List<Instrument> GetInstruments()
        {
            return instruments.Instruments.ToList();
        }
        #endregion

        #region GetInstrumentTypes
        public static List<InstrumentType> GetInstrumentTypes()
        {
            return instrumentTypes.InstrumentTypes.ToList();
        }
        #endregion
    }
}