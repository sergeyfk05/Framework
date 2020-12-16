using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Framework.TestDataProviders
{
    public class AddingToCartTestDataProvider : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            string path = File.Exists(Environment.GetEnvironmentVariable("AddingToCartTestDataFile")) ? Environment.GetEnvironmentVariable("AddingToCartTestDataFile") : "TestDataProviders/TestData/AddingToCartTestData.json";

            List<string> data = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(path));
            return data.Select(x => new object[] { x }).GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
