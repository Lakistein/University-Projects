using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GAPT.Models
{
    public class OutfitItem
    {
        public int Id { get; set; }
        public int OutfitId { get; set; }
        public int ItemId { get; set; }
    }
}