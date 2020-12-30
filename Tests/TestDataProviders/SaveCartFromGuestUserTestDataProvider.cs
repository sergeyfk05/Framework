using Core.Models;
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
    class SaveCartFromGuestUserTestDataProvider : IEnumerable<object[]>
    {

        
        public IEnumerator<object[]> GetEnumerator()
        {
            string path = File.Exists(Environment.GetEnvironmentVariable("SaveCartFromGuestUserTestDataFile")) ? Environment.GetEnvironmentVariable("SaveCartFromGuestUserTestDataFile") : "TestDataProviders/TestData/SaveCartFromGuestUserTestData.json";

            List<SaveCartFromGuestUserTestDataModel> data = JsonSerializer.Deserialize<List<SaveCartFromGuestUserTestDataModel>>(File.ReadAllText(path));
            return data.Select(x => new object[] { x }).GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
