// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
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
    public class RemoveFromCartTestDataProvider : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            string path = File.Exists(Environment.GetEnvironmentVariable("RemoveFromCartTestDataFile")) ? Environment.GetEnvironmentVariable("RemoveFromCartTestDataFile") : "TestDataProviders/TestData/RemoveFromCartTestData.json";

            List<List<string>> data = JsonSerializer.Deserialize<List<List<string>>>(File.ReadAllText(path));
            return data.Select(x => new object[] { (IEnumerable<string>)x }).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
