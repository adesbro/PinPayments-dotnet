using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPayments.Model;

namespace PinPayments.Tests
{
    [TestClass]
    public class SpikeTests
    {
        [TestMethod]
        [TestCategory("Spike")]
        public void Spike_Create_Charge_With_Valid_CC_Returns_Charge_Details()
        {
            var card = new Card
            {
                Number = "5520000000000000", // Good
                Cvc = "111",
                ExpiryMonth = DateTime.Today.Month,
                ExpiryYear = (DateTime.Today.Year + 1),
                Name = "Gordon Bennet",
                AddressLine1 = "123 Kellogs St",
                AddressCity = "Perth",
                AddressPostcode = "6000",
                AddressState = "WA",
                AddressCountry = "Australia"
            };

            var charge = new Charge
            {
                Amount = 9500, // $95.00
                Card = card,
                Capture = true, // authorise AND charge 
                Currency = "AUD",
                Description = "This is a description of the product, isn't it awesome.",
                Email = "email@test.com",
                IpAddress = "127.0.0.1"
            };

            // TODO: set secret key here before running!
            var api = new PinPaymentsApi("?????");
            var response = api.CreateCharge(charge);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Charge);
            Assert.IsTrue(response.Charge.Success);
            Assert.IsTrue(!string.IsNullOrEmpty(response.Charge.Token));
        }
    }
}
