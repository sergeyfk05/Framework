using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Models
{
    public class CouponTestDataModel
    {
        public string Coupon { get; set; }
        public bool IsCouponValid { get; set; }
        public IEnumerable<string> Links { get; set; }
    }
}
