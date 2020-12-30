using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tests.Models;

namespace Tests.TestDataProviders
{
    class CouponTestDataProvider : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            string path = File.Exists(Environment.GetEnvironmentVariable("CouponTestDataFile")) ? Environment.GetEnvironmentVariable("CouponTestDataFile") : "TestDataProviders/TestData/CouponTestData.json";

            List<CouponTestDataModel> data = JsonSerializer.Deserialize<List<CouponTestDataModel>>(File.ReadAllText(path));
            return data.Select(x => new object[] { x }).GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
