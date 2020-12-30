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
    public class AuthorizationTestDataProvider : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            string path = File.Exists(Environment.GetEnvironmentVariable("AuthorizationTestDataFile")) ? Environment.GetEnvironmentVariable("AuthorizationTestDataFile") : "TestDataProviders/TestData/AuthorizationTestData.json";

            List<AuthorizationTestDataModel> data = JsonSerializer.Deserialize<List<AuthorizationTestDataModel>>(File.ReadAllText(path));
            return data.Select(x => new object[] { x }).GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
