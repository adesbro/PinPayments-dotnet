PinPayments-dotnet
==================

A simple .NET client for [Pin Payments][] REST API:
- All Pin Payments methods (Charges, Customers, Refunds, Card Tokens)
- Async versions of all methods 
- Uses RestSharp for the heavy lifting (a very well maintained REST API client)
[Pin Payments]: https://pin.net.au/

### Download

Get the [NuGet package][].
[NuGet package]: https://www.nuget.org/packages/PinPayments/

### Documentation

Please see the [Pin Payments API documentation][] for details on each method. You will also find test cards that can be used.
[Pin Payments API documentation]: https://pin.net.au/docs/api

### Configuration

In your application configuration file, set the URL of the Pin Payments server and set the Secret Key that you have been assigned with your Pin Payments account.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="pinPaymentsApi" type="PinPayments.Config.PinPaymentsApiSection, PinPayments" allowLocation="true" allowDefinition="Everywhere" />
  </configSections>
  <pinPaymentsApi>
    <server baseUrl="https://test-api.pin.net.au" />
    <!-- TODO: put your secret key here -->
    <authentication secretKey="????"/>
  </pinPaymentsApi>
</configuration>
```

### Basic Usage

This code will charge $95 to a specific Credit Card.

```c#
var card = new Card
{
    Number = "5520000000000000", // Good
    Cvc = "111",
    ExpiryMonth = 10,
    ExpiryYear = 2016,
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

// TODO: process charge result
```