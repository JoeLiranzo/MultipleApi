using MultipleApi.ApisDef;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Testing
{
    public class Tests
    {
        /*
         Attention: To run this test, it's neccessary to first RUN API1, API2 and API3.

        Example: 
            - Left click on Solution Explorer.
            - Right Click on API1 Project.
            - Click on Debug and Start New Instance Without Debugging.
            - Do the same with API2 and API3.
            - Then Run those tests.
         */

        private MultiApiApp multiApiApp;

        [SetUp]
        public void Setup()
        {
            multiApiApp = new MultiApiApp();
        }


        [Test]
        public async Task API1CalculatedResult()
        {
            var api1_Input = new Api1_Input { };
            api1_Input.contact_address = "1234 NW Bobcat Lane, St. Robert, MO 65584-5678";
            api1_Input.warehouse_address = "90210 Broadway Blvd. Nashville, TN 37011 - 5678";
            api1_Input.package_dimensions = new long[] { 20, 65 };

            var expected_result = (long)(20 * 65);
            var result = (await multiApiApp.RequestAPI1(api1_Input)).total;

            Assert.IsTrue(result == expected_result);
        }

        [Test]
        public async Task API1CalculatedResult2()
        {
            var api1_Input = new Api1_Input { };
            api1_Input.contact_address = "1234 NW Bobcat Lane, St. Robert, MO 65584-5678";
            api1_Input.warehouse_address = "90210 Broadway Blvd. Nashville, TN 37011 - 5678";
            api1_Input.package_dimensions = new long[] { 0, 65 };

            var expected_result = (long)(0 * 65);
            var result = (await multiApiApp.RequestAPI1(api1_Input)).total;

            Assert.IsFalse(result != expected_result);
        }

        [Test]
        public async Task API2CalculatedResult()
        {
            var api2_Input = new Api2_Input { };
            api2_Input.consignee = "1234 NW Bobcat Lane, St. Robert, MO 65584-5678";
            api2_Input.consignor = "90210 Broadway Blvd. Nashville, TN 37011 - 5678";
            api2_Input.cartons = new long[] { 885, 6554 };
         
            var expected_result = (long)(885 * 6554);
            var result = (await multiApiApp.RequestAPI2(api2_Input)).amount;

            Assert.IsTrue(result == expected_result);
        }

        [Test]
        public async Task API2CalculatedResult2()
        {
            var api2_Input = new Api2_Input { };
            api2_Input.consignee = "1234 NW Bobcat Lane, St. Robert, MO 65584-5678";
            api2_Input.consignor = "90210 Broadway Blvd. Nashville, TN 37011 - 5678";
            api2_Input.cartons = new long[] { 0, 6554 };

            var expected_result = (long)(0 * 6554);
            var result = (await multiApiApp.RequestAPI2(api2_Input)).amount;

            Assert.IsFalse(result != expected_result);
        }

        [Test]
        public async Task API3CalculatedResult()
        {
            var api3_Input = new Api3_Input { };
            api3_Input.source = "1234 NW Bobcat Lane, St. Robert, MO 65584-5678";
            api3_Input.destination = "90210 Broadway Blvd. Nashville, TN 37011 - 5678";
            api3_Input.packages = new packages { package = new long[] { 4234, 6456 } };

            var expected_result = (long)(6456 * 4234);
            var result = (await multiApiApp.RequestAPI3(api3_Input)).quote;

            Assert.IsTrue(result == expected_result);
        }

        [Test]
        public async Task API3CalculatedResult2()
        {
            var api3_Input = new Api3_Input { };
            api3_Input.source = "1234 NW Bobcat Lane, St. Robert, MO 65584-5678";
            api3_Input.destination = "90210 Broadway Blvd. Nashville, TN 37011 - 5678";
            api3_Input.packages = new packages { package = new long[] { 4234, 0 } };

            var expected_result = (long)(0 * 4234);
            var result = (await multiApiApp.RequestAPI3(api3_Input)).quote;

            Assert.IsFalse(result != expected_result);
        }

        [Test]
        public async Task GetBestDealMinimun()
        {
            var api1_Input = new Api1_Input { };
            api1_Input.contact_address = "1234 NW Bobcat Lane, St. Robert, MO 65584-5678";
            api1_Input.warehouse_address = "90210 Broadway Blvd. Nashville, TN 37011 - 5678";
            api1_Input.package_dimensions = new long[] { 20, 65 };

            var api2_Input = new Api2_Input { };
            api2_Input.consignee = "1234 NW Bobcat Lane, St. Robert, MO 65584-5678";
            api2_Input.consignor = "90210 Broadway Blvd. Nashville, TN 37011 - 5678";
            api2_Input.cartons = new long[] { 885, 6554 };

            var api3_Input = new Api3_Input { };
            api3_Input.source = "1234 NW Bobcat Lane, St. Robert, MO 65584-5678";
            api3_Input.destination = "90210 Broadway Blvd. Nashville, TN 37011 - 5678";
            api3_Input.packages = new packages { package = new long[] { 4234, 6456 } };

            var expected_result = (long)(20 * 65);
            var result = (await multiApiApp.GetBestDeal(api1_Input, api2_Input, api3_Input));

            Assert.IsTrue(result == expected_result);
        }

        [Test]
        public async Task GetBestDealMaximum()
        {
            var api1_Input = new Api1_Input { };
            api1_Input.contact_address = "1234 NW Bobcat Lane, St. Robert, MO 65584-5678";
            api1_Input.warehouse_address = "90210 Broadway Blvd. Nashville, TN 37011 - 5678";
            api1_Input.package_dimensions = new long[] { 20, 65 };

            var api2_Input = new Api2_Input { };
            api2_Input.consignee = "1234 NW Bobcat Lane, St. Robert, MO 65584-5678";
            api2_Input.consignor = "90210 Broadway Blvd. Nashville, TN 37011 - 5678";
            api2_Input.cartons = new long[] { 885, 6554 };

            var api3_Input = new Api3_Input { };
            api3_Input.source = "1234 NW Bobcat Lane, St. Robert, MO 65584-5678";
            api3_Input.destination = "90210 Broadway Blvd. Nashville, TN 37011 - 5678";
            api3_Input.packages = new packages { package = new long[] { 4234, 6456 } };

            var expected_result = (long)(4234 * 6456);
            var result = (await multiApiApp.GetBestDeal(api1_Input, api2_Input, api3_Input));

            Assert.IsFalse(result == expected_result);
        }

        [Test]
        public async Task GetBestDealMiddleValue()
        {
            var api1_Input = new Api1_Input { };
            api1_Input.contact_address = "1234 NW Bobcat Lane, St. Robert, MO 65584-5678";
            api1_Input.warehouse_address = "90210 Broadway Blvd. Nashville, TN 37011 - 5678";
            api1_Input.package_dimensions = new long[] { 20, 65 };

            var api2_Input = new Api2_Input { };
            api2_Input.consignee = "1234 NW Bobcat Lane, St. Robert, MO 65584-5678";
            api2_Input.consignor = "90210 Broadway Blvd. Nashville, TN 37011 - 5678";
            api2_Input.cartons = new long[] { 885, 6554 };

            var api3_Input = new Api3_Input { };
            api3_Input.source = "1234 NW Bobcat Lane, St. Robert, MO 65584-5678";
            api3_Input.destination = "90210 Broadway Blvd. Nashville, TN 37011 - 5678";
            api3_Input.packages = new packages { package = new long[] { 4234, 6456 } };

            var expected_result = (long)(885 * 6554);
            var result = (await multiApiApp.GetBestDeal(api1_Input, api2_Input, api3_Input));

            Assert.IsFalse(result == expected_result);
        }

    }
}