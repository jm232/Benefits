using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using NUnit.Framework;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Linq;

namespace BenefitsTA.API.StepDefinitions
{
    [Binding]
    public class ApiStepDefinitions
    {
        private string _url;
        private HttpResponseMessage _response;
        private HttpClient _httpClient;
        private string _requestBody;
        private readonly ScenarioContext _scenarioContext;
        private List<User> _users;

        public ApiStepDefinitions(ScenarioContext scenarioContext)
        {
            _httpClient = new HttpClient();
            _scenarioContext = scenarioContext;
        }

        // Step: Given I have the API URL "https://..."
        [Given(@"I have the API URL ""(.*)""")]
        public void GivenIHaveTheAPIURL(string url)
        {
            _url = url;
        }

        [Given(@"I have Basic Authentication token (.*)")]
        public void GivenIHaveBasicAuthentication(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", token);
        }

        [Given(@"I have the following request body:")]
        public void GivenIHaveTheFollowingRequestBody(Table table)
        {
            // Convert the DataTable into a dictionary
            var requestData = new Dictionary<string, string>();

            foreach (var row in table.Rows)
            {
                foreach (var column in table.Header)
                {
                    requestData[column] = row[column];
                }
            }

            // Convert the dictionary into a JSON string
            _requestBody = JsonConvert.SerializeObject(requestData, Formatting.Indented);
        }

        [When(@"I send a POST request")]
        public async Task WhenISendAPostRequest()
        {
            var content = new StringContent(_requestBody, System.Text.Encoding.UTF8, "application/json");
            _response = await _httpClient.PostAsync(_url, content);
        }

        [When(@"I send a GET request")]
        public async Task WhenISendAGetRequest()
        {
            _response = await _httpClient.GetAsync(_url);
        }

        [When(@"I send a DELETE request on stored employee ID")]
        public async Task WhenISendADeleteRequestOnStoredEmployeeID()
        {
            var userId = _scenarioContext["UserId"].ToString();

            _response = await _httpClient.DeleteAsync(_url + "/" + userId);
        }

        [When(@"I send a PUT request on stored employee ID")]
        public async Task WhenISendAPutRequestOnStoredEmployeeID()
        {
            var userId = _scenarioContext["UserId"].ToString();
            string updatedJson = _requestBody.TrimEnd('}') + ", \"id\": \"" + userId + "\" }";


            var content = new StringContent(updatedJson, System.Text.Encoding.UTF8, "application/json");

            _response = await _httpClient.PutAsync(_url, content);
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int statusCode)
        {
            Assert.AreEqual(statusCode, (int)_response.StatusCode);
        }

        [Then(@"the response body should contain ""(.*)""")]
        public async Task ThenTheResponseBodyShouldContain(string expectedValue)
        {
            var responseBody = await _response.Content.ReadAsStringAsync();
            Assert.IsTrue(responseBody.Contains(expectedValue),
                $"Expected '{expectedValue}' to be in the response body.");
        }

        public class User
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Dependants { get; set; }
        }

        [Then(@"the response body should match the following table:")]
        public void ThenTheResponseBodyShouldMatchTheFollowingTable(Table table)
        {
            // Extract the expected values from the SpecFlow table
            foreach (var row in table.Rows)
            {
                var firstname = row["firstname"];
                var lastname = row["lastname"];
                var dependents = row["dependents"];

                // Find the user in the actual API response
                var user = _users.FirstOrDefault(u => u.FirstName == firstname && u.LastName == lastname);


                // Validate the user data matches what was expected in the table
                Assert.AreEqual(firstname, user.FirstName);
                Assert.AreEqual(lastname, user.LastName);
                Assert.AreEqual(dependents, user.Dependants);
            }
        }


        [Then(@"I store the employee's ID from the response")]
        public void ThenIStoreTheEmployeeIDFromTheResponse()
        {
            // Deserialize the response body to get the list of users
            var content = _response.Content.ReadAsStringAsync().Result;
            var users = JsonConvert.DeserializeObject<dynamic>(content);

            var userId = users.id.ToString();
            _scenarioContext["UserId"] = userId;
            Console.WriteLine($"UserId: {userId}");
        }
    }
}