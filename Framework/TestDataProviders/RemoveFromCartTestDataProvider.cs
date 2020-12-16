using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.TestDataProviders
{
    public class RemoveFromCartTestDataProvider : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new List<string>()
            {
                "https://www.dell.com/en-us/shop/cty/pdp/spd/xps-15-9500-laptop/xn9500cto210s",
                "https://www.dell.com/en-us/shop/dell-laptops/alienware-m17-r3-gaming-laptop/spd/alienware-m17-r3-laptop/wnm17r312sbf"
            }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
