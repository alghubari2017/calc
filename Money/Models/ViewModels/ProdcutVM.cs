using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Money.Models.ViewModels
{
    public class prodcutVM
    {
        public Product Prodcut  { get; set; }

       public IEnumerable<SelectListItem> CategorySelectListItem { get; set; }
    }
}
