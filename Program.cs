using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QBS.API.Example.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QBS.API.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<Company> companies = ListCompanies();
          
            foreach (var company in companies.Select((value, index) => new { value, index }))
                Console.WriteLine("(" + company.index.ToString() + ") Company found with name " + company.value.name + " and id " + company.value.id.ToString());

            int WhichCompanyToUse = companies.Count;

            ConsoleKeyInfo keypressed = Console.ReadKey();
            if (char.IsDigit(keypressed.KeyChar))
                WhichCompanyToUse = int.Parse(keypressed.KeyChar.ToString()); 
            
            if (WhichCompanyToUse >= companies.Count)
                throw new Exception("Not a valid selection");

            Console.WriteLine("Selected Company to read customers  = " + companies[WhichCompanyToUse].name);

            CreateNewCustomer(companies[WhichCompanyToUse].id);

            List<Customer> customers = ListCustomers(companies[WhichCompanyToUse].id);
            foreach (Customer customer in customers)
                Console.WriteLine(customer.displayName);


            Console.ReadKey();
        }
        static List<Company> ListCompanies()
        {
            RestSharp.RestClient client = CreateRestClient(GetCompanyParameters());

            RestSharp.RestRequest request = GetRequest(RestSharp.Method.GET, new JObject());

            RestSharp.IRestResponse response = client.Execute(request);
            List<Company> companies = JsonConvert.DeserializeObject<List<Company>>(ConvertToDataSet(response.Content.ToString()).ToString());
            return companies;
        }
        static List<Customer> ListCustomers(Guid CompanyId)
        {
            RestSharp.RestClient client = CreateRestClient(GetCustomerParameters(CompanyId));

            RestSharp.RestRequest request = GetRequest(RestSharp.Method.GET, new JObject());

            RestSharp.IRestResponse response = client.Execute(request);
            List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(ConvertToDataSet(response.Content.ToString()).ToString());
            return customers;
        }

        static void CreateNewCustomer(Guid CompanyId)
        {
            RestSharp.RestClient client = CreateRestClient(GetCustomerParameters(CompanyId));

            Customer cust = new Customer();
            cust.displayName = "QBS Group helps you out!";

            JObject json = (JObject)JToken.FromObject(cust);
            
            RestSharp.RestRequest request = GetRequest(RestSharp.Method.POST, json);

            RestSharp.IRestResponse response = client.Execute(request);

        }

        private static JArray ConvertToDataSet(string result)
        {
            dynamic json = JsonConvert.DeserializeObject(result);
            _ = new JArray();
            JArray myObject = json["value"];

            return myObject;
        }

        private static RestSharp.RestRequest GetRequest(RestSharp.Method method, JObject json)
        {
            var request = new RestSharp.RestRequest(method);

            Credentials credentials = new Credentials();

            byte[] UserInfo = System.Text.ASCIIEncoding.ASCII.GetBytes(String.Format("{0}:{1}", credentials.UserId, credentials.AccessKey));

            request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(UserInfo));
            request.AddHeader("Content-Type", "application/json");
            if (json.Count > 0)
                request.AddParameter("application/json", json.ToString(), RestSharp.ParameterType.RequestBody);
            return request;
        }

        private static RestSharp.RestClient CreateRestClient(APIEndpoint endpoint)
        {
            var client = new RestSharp.RestClient(endpoint.getURI())
            {
                Timeout = -1
            };
            return client;
        }

        private static APIEndpoint GetCompanyParameters()
        {
            APIEndpoint endpoint = new APIEndpoint();
            return (endpoint);
        }

        private static APIEndpoint GetCustomerParameters(Guid CompanyId)
        {
            APIEndpoint endpoint = new APIEndpoint();
            endpoint.companyId = CompanyId.ToString();
            endpoint.apiEndpoint = "customers";
            return (endpoint);
        }
    }
}
