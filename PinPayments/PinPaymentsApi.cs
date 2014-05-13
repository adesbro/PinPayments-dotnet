using System.Configuration;
using System.Threading.Tasks;
using PinPayments.Model;
using PinPayments.Serialization;
using RestSharp;
using Configuration = PinPayments.Config.Configuration;

namespace PinPayments
{
    public class PinPaymentsApi
    {
        private readonly string _secretKey;

        public PinPaymentsApi()
        {
            _secretKey = Configuration.RootSection.Authentication.SecretKey;
            if (string.IsNullOrEmpty(_secretKey))
            {
                throw new ConfigurationErrorsException("A 'secret key' was not specified and no default value can be found in the configuration.");
            }
        }

        public PinPaymentsApi(string secretKey)
        {
            _secretKey = secretKey;
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = CreateAuthenticatedClient();
            var response = client.Execute(request);
            return ValidateResponse<T>(response);
        }

        public async Task<T> ExecuteAsync<T>(RestRequest request) where T : new()
        {
            var client = CreateAuthenticatedClient();
            var response = await client.ExecuteTaskAsync(request);
            return ValidateResponse<T>(response);
        }

        private RestClient CreateAuthenticatedClient()
        {
            var client = new RestClient
            {
                BaseUrl = Configuration.RootSection.Server.BaseUrl,
                Authenticator = new HttpBasicAuthenticator(_secretKey, string.Empty)
            };
            client.AddHandler("application/json", new RestSharpDataContractJsonDeserializer());
            client.AddHandler("text/json", new RestSharpDataContractJsonDeserializer());
            client.AddHandler("text/x-json", new RestSharpDataContractJsonDeserializer());
            return client;
        }

        private static T ValidateResponse<T>(IRestResponse response) where T : new()
        {
            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var error = new PinError
                {
                    Error = "Unknown error",
                    ErrorDescription = message
                };
                throw new PinPaymentsException(error, message, response.ErrorException);
            }

            // NOTE: Would like to get deserializer from RestClient but there is no public access to it.
            // We know that the PinPayments API will _always_ return JSON, so its cool.
            var deserializer = new RestSharpDataContractJsonDeserializer();

            // A non-20x status code response is an error from the PinPayments API
            if (response.StatusCode.IsNon20XCode())
            {
                var pinError = deserializer.Deserialize<PinError>(response);
                throw new PinPaymentsException(pinError, pinError.ErrorDescription);
            }

            return deserializer.Deserialize<T>(response);
        }

        public ChargeResponse CreateCharge(Charge charge)
        {
            var request = CreateChargeRequest(charge);
            return Execute<ChargeResponse>(request);
        }

        public Task<ChargeResponse> CreateChargeAsync(Charge charge)
        {
            var request = CreateChargeRequest(charge);
            return ExecuteAsync<ChargeResponse>(request);
        }

        private static RestRequest CreateChargeRequest(Charge charge)
        {
            var request = new RestRequest(Method.POST)
            {
                Resource = "1/charges",
                RequestFormat = DataFormat.Json,
                RootElement = "charge",
                JsonSerializer = new RestSharpDataContractJsonSerializer()
            };
            request.AddBody(charge);
            return request;
        }

        public ChargeResponse GetCharge(string chargeToken)
        {
            var request = GetChargeRequest(chargeToken);
            return Execute<ChargeResponse>(request);
        }

        public Task<ChargeResponse> GetChargeAsync(string chargeToken)
        {
            var request = GetChargeRequest(chargeToken);
            return ExecuteAsync<ChargeResponse>(request);
        }

        private static RestRequest GetChargeRequest(string chargeToken)
        {
            var request = new RestRequest(Method.GET)
            {
                Resource = "1/charges/{charge-token}"
            };

            request.AddParameter("charge-token", chargeToken, ParameterType.UrlSegment);
            return request;
        }

        public ChargeResponse CaptureCharge(string chargeToken)
        {
            var request = CaptureChargeRequest(chargeToken);
            return Execute<ChargeResponse>(request);
        }

        public Task<ChargeResponse> CaptureChargeAsync(string chargeToken)
        {
            var request = CaptureChargeRequest(chargeToken);
            return ExecuteAsync<ChargeResponse>(request);
        }

        private static RestRequest CaptureChargeRequest(string chargeToken)
        {
            var request = new RestRequest(Method.PUT)
            {
                Resource = "1/charges/{charge-token}/capture"
            };

            request.AddParameter("charge-token", chargeToken, ParameterType.UrlSegment);
            return request;
        }

        public ChargeListResponse GetCharges()
        {
            var request = GetChargesRequest();
            return Execute<ChargeListResponse>(request);
        }

        public Task<ChargeListResponse> GetChargesAsync()
        {
            var request = GetChargesRequest();
            return ExecuteAsync<ChargeListResponse>(request);
        }

        private static RestRequest GetChargesRequest()
        {
            return new RestRequest(Method.GET)
            {
                Resource = "1/charges"
            };
        }

        public ChargeListResponse SearchCharges(ChargeSearch chargeSearch)
        {
            var request = SearchChargesRequest(chargeSearch);
            return Execute<ChargeListResponse>(request);
        }

        public Task<ChargeListResponse> SearchChargesAsync(ChargeSearch chargeSearch)
        {
            var request = SearchChargesRequest(chargeSearch);
            return ExecuteAsync<ChargeListResponse>(request);
        }

        private static RestRequest SearchChargesRequest(ChargeSearch chargeSearch)
        {
            var request = new RestRequest(Method.GET)
            {
                Resource = "1/charges/search"
            };

            request.AddParameter(chargeSearch, s => s.Query, ParameterType.QueryString);
            request.AddParameter(chargeSearch, s => s.StartDate, ParameterType.QueryString);
            request.AddParameter(chargeSearch, s => s.EndDate, ParameterType.QueryString);
            request.AddParameter(chargeSearch, s => s.Sort, ParameterType.QueryString);
            request.AddParameter(chargeSearch, s => s.Direction, ParameterType.QueryString);
            return request;
        }

        public CustomerResponse CreateCustomer(Customer customer)
        {
            var request = CreateCustomerRequest(customer);
            return Execute<CustomerResponse>(request);
        }

        public Task<CustomerResponse> CreateCustomerAsync(Customer customer)
        {
            var request = CreateCustomerRequest(customer);
            return ExecuteAsync<CustomerResponse>(request);
        }

        private static RestRequest CreateCustomerRequest(Customer customer)
        {
            var request = new RestRequest(Method.POST)
            {
                Resource = "1/customers",
                RequestFormat = DataFormat.Json,
                RootElement = "Customer",
                JsonSerializer = new RestSharpDataContractJsonSerializer()
            };
            request.AddBody(customer);
            return request;
        }

        public CustomerResponse GetCustomer(string customerToken)
        {
            var request = GetCustomerRequest(customerToken);
            return Execute<CustomerResponse>(request);
        }

        public Task<CustomerResponse> GetCustomerAsync(string customerToken)
        {
            var request = GetCustomerRequest(customerToken);
            return ExecuteAsync<CustomerResponse>(request);
        }

        private static RestRequest GetCustomerRequest(string customerToken)
        {
            var request = new RestRequest(Method.GET)
            {
                Resource = "1/customers/{customer-token}"
            };

            request.AddParameter("customer-token", customerToken, ParameterType.UrlSegment);
            return request;
        }

        public CustomerListResponse GetCustomers()
        {
            var request = GetCustomersRequest();
            return Execute<CustomerListResponse>(request);
        }

        public Task<CustomerListResponse> GetCustomersAsync()
        {
            var request = GetCustomersRequest();
            return ExecuteAsync<CustomerListResponse>(request);
        }

        private static RestRequest GetCustomersRequest()
        {
            return new RestRequest(Method.GET)
            {
                Resource = "1/customers"
            };
        }

        public CustomerResponse UpdateCustomer(string customerToken, CustomerUpdate customerUpdate)
        {
            var request = UpdateCustomerRequest(customerToken, customerUpdate);
            return Execute<CustomerResponse>(request);
        }

        public Task<CustomerResponse> UpdateCustomerAsync(string customerToken, CustomerUpdate customerUpdate)
        {
            var request = UpdateCustomerRequest(customerToken, customerUpdate);
            return ExecuteAsync<CustomerResponse>(request);
        }

        private static RestRequest UpdateCustomerRequest(string customerToken, CustomerUpdate customerUpdate)
        {
            var request = new RestRequest(Method.PUT)
            {
                Resource = "1/customers/{customer-token}",
                RequestFormat = DataFormat.Json,
                RootElement = "Customer",
                JsonSerializer = new RestSharpDataContractJsonSerializer()
            };

            request.AddParameter("customer-token", customerToken, ParameterType.UrlSegment);            
            request.AddBody(customerUpdate);
            return request;
        }

        public ChargeListResponse GetCustomerCharges(string customerToken)
        {
            var request = GetCustomerChargesRequest(customerToken);
            return Execute<ChargeListResponse>(request);
        }

        public Task<ChargeListResponse> GetCustomerChargesAsync(string customerToken)
        {
            var request = GetCustomerChargesRequest(customerToken);
            return ExecuteAsync<ChargeListResponse>(request);
        }

        private static RestRequest GetCustomerChargesRequest(string customerToken)
        {
            var request = new RestRequest(Method.GET)
            {
                Resource = "1/customers/{customer-token}/charges"
            };
            
            request.AddParameter("customer-token", customerToken, ParameterType.UrlSegment);
            return request;
        }

        public RefundResponse CreateRefund(string chargeToken, int? amount = null)
        {
            var request = CreateRefundRequest(chargeToken, amount);
            return Execute<RefundResponse>(request);
        }

        public Task<RefundResponse> CreateRefundAsync(string chargeToken, int? amount = null)
        {
            var request = CreateRefundRequest(chargeToken, amount);
            return ExecuteAsync<RefundResponse>(request);
        }

        private static RestRequest CreateRefundRequest(string chargeToken, int? amount)
        {
            var request = new RestRequest(Method.POST)
            {
                Resource = "1/charges/{charge-token}/refunds"
            };

            request.AddParameter("charge-token", chargeToken, ParameterType.UrlSegment);
            if (amount.HasValue)
            {
                request.AddParameter("amount", amount.Value, ParameterType.QueryString);
            }
            return request;
        }

        public RefundListResponse GetRefunds(string chargeToken)
        {
            var request = GetRefundsRequest(chargeToken);
            return Execute<RefundListResponse>(request);
        }

        public Task<RefundListResponse> GetRefundsAsync(string chargeToken)
        {
            var request = GetRefundsRequest(chargeToken);
            return ExecuteAsync<RefundListResponse>(request);
        }

        private static RestRequest GetRefundsRequest(string chargeToken)
        {
            var request = new RestRequest(Method.GET)
            {
                Resource = "1/charges/{charge-token}/refunds"
            };

            request.AddParameter("charge-token", chargeToken, ParameterType.UrlSegment);
            return request;
        }
    }
}
