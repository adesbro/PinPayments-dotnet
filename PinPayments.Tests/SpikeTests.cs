using System;
using System.Linq;
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
                Email = "email@example.com",
                IpAddress = "127.0.0.1"
            };

            var api = new PinPaymentsApi();
            var response = api.CreateCharge(charge);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Charge);
            Assert.IsTrue(response.Charge.Success);
            Assert.IsTrue(!string.IsNullOrEmpty(response.Charge.Token));
        }

        [TestMethod]
        [TestCategory("Spike")]
        public void Spike_Create_Charge_With_Card_Token_Returns_Charge_Details()
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

            var api = new PinPaymentsApi();
            var response = api.CreateCardToken(card);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Card);
            Assert.IsNotNull(response.Card.Token);

            var charge = new Charge
            {
                Amount = 9500, // $95.00
                CardToken = response.Card.Token,
                Capture = true, // authorise AND charge 
                Currency = "AUD",
                Description = "This is a description of the product, isn't it awesome.",
                Email = "email@example.com",
                IpAddress = "127.0.0.1"
            };

            var response2 = api.CreateCharge(charge);

            Assert.IsNotNull(response2);
            Assert.IsNotNull(response2.Charge);
            Assert.IsTrue(response2.Charge.Success);
            Assert.IsTrue(!string.IsNullOrEmpty(response2.Charge.Token));
        }

        [TestMethod]
        [TestCategory("Spike")]
        public void Spike_Create_Customer_With_Card_Return_Token_And_Refetch()
        {
            var emailAddress = string.Format("test-{0}@example.com", Guid.NewGuid());
           
            var card = new Card
            {
                Number = "5520000000000000", // Good
                Cvc = "111",
                ExpiryMonth = DateTime.Today.Month,
                ExpiryYear = (DateTime.Today.Year + 1),
                Name = "Test Name",
                AddressLine1 = "555 Wicked St",
                AddressCity = "Perth",
                AddressPostcode = "6000",
                AddressState = "WA",
                AddressCountry = "Australia"
            };

            var customer = new Customer
            {
                Email = emailAddress,
                Card = card
            };

            var api = new PinPaymentsApi();
            var response = api.CreateCustomer(customer);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Customer);
            Assert.IsNotNull(response.Customer.Token);
            Assert.IsNotNull(response.Customer.Card);
            Assert.IsNotNull(response.Customer.Card.Token);
            Assert.IsNull(response.Customer.Card.Number);
            Assert.IsNotNull(response.Customer.Card.DisplayNumber);
            Assert.IsTrue(response.Customer.Email == emailAddress);

            var token = response.Customer.Token;
            var response2 = api.GetCustomer(token);

            Assert.IsNotNull(response2);
            Assert.IsNotNull(response2.Customer);
            Assert.IsNotNull(response2.Customer.Token);
            Assert.IsNotNull(response2.Customer.Card);
            Assert.IsNotNull(response2.Customer.Card.Token);
            Assert.IsNull(response2.Customer.Card.Number);
            Assert.IsNotNull(response2.Customer.Card.DisplayNumber);
            Assert.IsTrue(response2.Customer.Email == emailAddress);
        }

        [TestMethod]
        [TestCategory("Spike")]
        public void Spike_Create_Customer_With_Card_Token_Return_Customer_Token_And_Refetch()
        {
            var emailAddress = string.Format("test-{0}@example.com", Guid.NewGuid());

            var card = new Card
            {
                Number = "5520000000000000", // Good
                Cvc = "111",
                ExpiryMonth = DateTime.Today.Month,
                ExpiryYear = (DateTime.Today.Year + 1),
                Name = "Test Name",
                AddressLine1 = "555 Wicked St",
                AddressCity = "Perth",
                AddressPostcode = "6000",
                AddressState = "WA",
                AddressCountry = "Australia"
            };

            var api = new PinPaymentsApi();
            var response = api.CreateCardToken(card);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Card);
            Assert.IsNotNull(response.Card.Token);

            var customer = new Customer
            {
                Email = emailAddress,
                CardToken = response.Card.Token
            };

            var response2 = api.CreateCustomer(customer);

            Assert.IsNotNull(response2);
            Assert.IsNotNull(response2.Customer);
            Assert.IsNotNull(response2.Customer.Token);
            Assert.IsNotNull(response2.Customer.Card);
            Assert.IsNotNull(response2.Customer.Card.Token);
            Assert.IsNull(response2.Customer.Card.Number);
            Assert.IsNotNull(response2.Customer.Card.DisplayNumber);
            Assert.IsTrue(response2.Customer.Email == emailAddress);

            var token = response2.Customer.Token;
            var response3 = api.GetCustomer(token);

            Assert.IsNotNull(response3);
            Assert.IsNotNull(response3.Customer);
            Assert.IsNotNull(response3.Customer.Token);
            Assert.IsNotNull(response3.Customer.Card);
            Assert.IsNotNull(response3.Customer.Card.Token);
            Assert.IsNull(response3.Customer.Card.Number);
            Assert.IsNotNull(response3.Customer.Card.DisplayNumber);
            Assert.IsTrue(response3.Customer.Email == emailAddress);
        }

        [TestMethod]
        [TestCategory("Spike")]
        public void Spike_Create_Customer_Update_Customer()
        {
            var emailAddress = string.Format("test-{0}@example.com", Guid.NewGuid());

            var card = new Card
            {
                Number = "5520000000000000", // Good
                Cvc = "111",
                ExpiryMonth = DateTime.Today.Month,
                ExpiryYear = (DateTime.Today.Year + 1),
                Name = "Test Name",
                AddressLine1 = "555 Incorrect St",
                AddressCity = "Perth",
                AddressPostcode = "6000",
                AddressState = "WA",
                AddressCountry = "Australia"
            };

            var customer = new Customer
            {
                Email = emailAddress,
                Card = card
            };

            var api = new PinPaymentsApi();
            var response = api.CreateCustomer(customer);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Customer);
            Assert.IsNotNull(response.Customer.Token);
            Assert.IsNotNull(response.Customer.Card);
            Assert.IsNotNull(response.Customer.Card.Token);
            Assert.IsNull(response.Customer.Card.Number);
            Assert.IsNotNull(response.Customer.Card.DisplayNumber);
            Assert.IsTrue(response.Customer.Email == emailAddress);

            card.AddressLine1 = "555 Updated St";
            var customerUpdate = new CustomerUpdate
            {
                Card = card,
                Email = emailAddress
            };
            var response2 = api.UpdateCustomer(response.Customer.Token, customerUpdate);

            Assert.IsNotNull(response2);
            Assert.IsNotNull(response2.Customer);
            Assert.IsNotNull(response2.Customer.Token);
            Assert.IsNotNull(response2.Customer.Card);
            Assert.IsNotNull(response2.Customer.Card.Token);
            Assert.IsNull(response2.Customer.Card.Number);
            Assert.IsNotNull(response2.Customer.Card.DisplayNumber);
            Assert.IsTrue(response2.Customer.Card.AddressLine1 == card.AddressLine1);
            Assert.IsTrue(response2.Customer.Email == emailAddress);
        }

        [TestMethod]
        [TestCategory("Spike")]
        public void Spike_Get_Customer_List()
        {
            var api = new PinPaymentsApi();
            var response = api.GetCustomers();

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Customers);
            Assert.IsTrue(response.Customers.Count > 0);
            Assert.IsNotNull(response.Pages);
        }

        [TestMethod]
        [TestCategory("Spike")]
        public void Spike_Create_Customer_Create_Charge_And_Fetch_Customers_Charges()
        {
            var emailAddress = string.Format("test-{0}@example.com", Guid.NewGuid());

            var card = new Card
            {
                Number = "5520000000000000", // Good
                Cvc = "111",
                ExpiryMonth = DateTime.Today.Month,
                ExpiryYear = (DateTime.Today.Year + 1),
                Name = "Test Name",
                AddressLine1 = "555 Dopey St",
                AddressCity = "Perth",
                AddressPostcode = "6000",
                AddressState = "WA",
                AddressCountry = "Australia"
            };

            var customer = new Customer
            {
                Email = emailAddress,
                Card = card
            };

            var api = new PinPaymentsApi();
            var response = api.CreateCustomer(customer);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Customer);
            Assert.IsNotNull(response.Customer.Token);
            Assert.IsNotNull(response.Customer.Card);
            Assert.IsNotNull(response.Customer.Card.Token);
            Assert.IsNull(response.Customer.Card.Number);
            Assert.IsNotNull(response.Customer.Card.DisplayNumber);
            Assert.IsTrue(response.Customer.Email == emailAddress);

            var charge = new Charge
            {
                Amount = 2995, // $29.95
                CustomerToken = response.Customer.Token,
                Capture = true, // authorise AND charge 
                Currency = "AUD",
                Description = "This is a description of the product, isn't it awesome.",
                Email = "email@example.com",
                IpAddress = "127.0.0.1"
            };

            var response2 = api.CreateCharge(charge);

            Assert.IsNotNull(response2);
            Assert.IsNotNull(response2.Charge);
            Assert.IsTrue(response2.Charge.Success);
            Assert.IsTrue(!string.IsNullOrEmpty(response2.Charge.Token));

            var response3 = api.GetCustomerCharges(response.Customer.Token);

            Assert.IsNotNull(response3);
            Assert.IsNotNull(response3.Charges);
            Assert.IsTrue(response3.Charges.Count > 0);
            Assert.IsTrue(response3.Charges.Any(c => c.Amount == charge.Amount));
            Assert.IsNotNull(response3.Pages);
        }

        [TestMethod]
        [TestCategory("Spike")]
        public void Spike_Create_Charge_Get_Refund_List_Refunds()
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
                Amount = 10000, // $95.00
                Card = card,
                Capture = true, // authorise AND charge 
                Currency = "AUD",
                Description = "This is a description of the product, isn't it awesome.",
                Email = "email@example.com",
                IpAddress = "127.0.0.1"
            };

            var api = new PinPaymentsApi();
            var response = api.CreateCharge(charge);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Charge);
            Assert.IsTrue(response.Charge.Success);
            Assert.IsTrue(!string.IsNullOrEmpty(response.Charge.Token));

            var chargeToken = response.Charge.Token;

            var response2 = api.CreateRefund(chargeToken, charge.Amount / 2); // refund half of it back

            Assert.IsNotNull(response2);
            Assert.IsNotNull(response2.Refund);
            Assert.IsTrue(response2.Refund.ChargeToken == chargeToken);

            var response3 = api.GetRefunds(chargeToken);

            Assert.IsNotNull(response3);
            Assert.IsNotNull(response3.Refunds);
            Assert.IsTrue(response3.Refunds.Count == 1);
            Assert.IsTrue(response3.Refunds[0].Token == response2.Refund.Token);
        }
    }
}
