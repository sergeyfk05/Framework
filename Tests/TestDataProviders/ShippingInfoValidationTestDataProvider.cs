﻿// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using Framework.Models;
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
    public class ShippingInfoValidationTestDataProvider : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            string path = File.Exists(Environment.GetEnvironmentVariable("ShippingInfoValidationTestDataFile")) ? Environment.GetEnvironmentVariable("ShippingInfoValidationTestDataFile") : "TestDataProviders/TestData/ShippingInfoValidationTestData.json";

            List<ShippingInfoValidationTestData> data = JsonSerializer.Deserialize<List<ShippingInfoValidationTestData>>(File.ReadAllText(path));
            return data.Select(x => new object[] { (ShippingInfoValidationTestData)x }).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
