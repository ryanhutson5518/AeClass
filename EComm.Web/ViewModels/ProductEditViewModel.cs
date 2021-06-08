using EComm.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.Web.ViewModels
{
    public class ProductEditViewModel
    {
        public Product Product { get; set; }
        
        public List<SelectListItem> Suppliers { get; set; }
    }
}
