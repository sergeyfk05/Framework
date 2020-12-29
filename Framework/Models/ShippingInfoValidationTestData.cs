// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using Pages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Models
{
    public class ShippingInfoValidationTestData
    {
        public ShippingInfo Info { get; set; }
        public string Link { get; set; }
        public User UserInfo { get; set; }
        public bool HasValidationErrors { get; set; }
    }
}
