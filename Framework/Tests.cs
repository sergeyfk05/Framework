using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Framework
{
    public class Tests : CommonConditions
    {
        [Fact]
        public void test1()
        {
            driver.Navigate().GoToUrl("https://google.com");
        }
    }
}
