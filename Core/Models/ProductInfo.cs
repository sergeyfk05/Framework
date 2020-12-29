using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ProductInfo
    {
        public double Price { get; set; }
        public string Title { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ProductInfo info &&
                   Price == info.Price &&
                   Title == info.Title;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Price, Title);
        }
    }
}
