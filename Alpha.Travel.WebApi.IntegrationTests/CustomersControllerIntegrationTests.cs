namespace Alpha.Travel.WebApi.IntegrationTests.Controller
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using Newtonsoft.Json;
    using FluentAssertions;

    using Models;

    [TestFixture]
    public sealed class CustomersControllerIntegrationTests : AlphaTravelTestWebApplicationFactory
    {
        public const string _customersEndpoint = "/api/v1/customers";

        [Test]
        public async Task GetAsync_All_Customers_Returns_OK()
        {
            // Arrange & Act 
            var response = await Client.GetAsync(_customersEndpoint, CancellationToken.None);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task GetAsync_All_Customers_Returns_OK_With_Correct_Data()
        {
            // Arrange & Act 
            var response = await Client.GetAsync(_customersEndpoint, CancellationToken.None);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<PagedResponse<Customer>>(stringResponse);

            // Assert
            Assert.Contains(customers.Data.Where(c => c.Firstname == "John").FirstOrDefault(), customers.Data.ToList());
        }

        [Test]
        public async Task GetAsync_Paged_Customers_Returns_OK()
        {
            // Arrange & Act 
            var response = await Client.GetAsync($"{_customersEndpoint}/?pageSize=50&pageNumber=1", CancellationToken.None);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task GetAsync_Paged_Customers_Returns_OK_With_Correct_Data()
        {
            // Arrange & Act 
            var response = await Client.GetAsync($"{_customersEndpoint}/?pageSize=50&pageNumber=1", CancellationToken.None);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<PagedResponse<Customer>>(stringResponse);

            // Assert
            customers.Pagination.PageNumber.Should().Equals(1);
            customers.Pagination.PageSize.Should().Equals(50);
        }

        [Test]
        public async Task GetAsync_Single_Customer_Returns_OK()
        {
            // Arrange & Act 
            var response = await Client.GetAsync($"{_customersEndpoint}/1", CancellationToken.None);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task GetAsync_Single_Customer_Returns_OK_With_Correct_Data()
        {
            // Arrange & Act 
            var response = await Client.GetAsync($"{_customersEndpoint}/1", CancellationToken.None);
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var customerResponse = JsonConvert.DeserializeObject<Response<Customer>>(stringResponse, SerializerSettings);

            // Assert
            Assert.AreEqual(customerResponse.Data.Firstname, "John");
        }

        [Test]
        public async Task PostAsync_Customer_Returns_Created()
        {
            // Arrange
            var customer = new Customer
            {
                Id = 11,
                Email = "test@test.com",
                Firstname = "Firstname",
                Surname = "Surname",
                Password = "Password123",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "CreatedOn"
            };
            var content = new StringContent(JsonConvert.SerializeObject(customer), System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PostAsync(_customersEndpoint, content, CancellationToken.None);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public async Task PutAsync_Customer_Returns_NoContent()
        {
            // Arrange
            var customer = new Customer
            {
                Id = 1,
                Firstname = "John",
                Surname = "Richard",
                Email = "John@test.com",
                Password = "password456",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "CreatedOn"
            };
            var content = new StringContent(JsonConvert.SerializeObject(customer), System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PutAsync($"{_customersEndpoint}/1", content, CancellationToken.None);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test]
        public async Task DeleteAsync_Customer_Returns_NoContent()
        {
            // Arrange & Act
            var response = await Client.DeleteAsync($"{_customersEndpoint}/4", CancellationToken.None);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}