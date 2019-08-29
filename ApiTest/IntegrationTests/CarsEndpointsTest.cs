using Xunit;
using System.Threading.Tasks;
using AntilopaApi.Data;
using AntilopaApiTest.Helpers;

namespace AntilopaApiTest
{
    public class CarsEndpointsTest : IClassFixture<CustomWebApplicationFactory<AntilopaApi.Startup>>
    {
        private readonly CustomWebApplicationFactory<AntilopaApi.Startup> _factory;

        public CarsEndpointsTest(CustomWebApplicationFactory<AntilopaApi.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetCars_DataSuccess()
        {
            var client = _factory.CreateClient().AppendValidAuthHeader(Utilities.testOwner1.Id);
            var carArray = new Car[] { Utilities.testCar1, Utilities.testCar2 };
            var expectedJson = carArray.AsJsonResponse();

            var response = await client.GetAsync("/api/cars/");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Equal(expectedJson, await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task GetCar_DataSuccess()
        {
            var client = _factory.CreateClient().AppendValidAuthHeader(Utilities.testOwner1.Id);;
            var expectedJson = Utilities.testCar1.AsJsonResponse();
            var response = await client.GetAsync($"/api/cars/{Utilities.testCar1.Id}");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Equal(expectedJson, await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task PostCar_Returns201AndIntIdGreater0InResponse()
        {
            var carDataPost = new
            {
                Model = "SAAB",
                Nickname = "Interceptor",
                RegistrationNr = "ABC123",
                PicUrl = "http://example.com/saab.png",
            };
            var client = _factory.CreateClient().AppendValidAuthHeader(Utilities.testOwner1.Id);;
            var response = await client.PostAsync("/api/cars/", Utilities.ToJsonBody(carDataPost));
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            var updResponse = await response.Content.FromJsonAsync();
            Assert.True(updResponse.Id > 0);
        }

        [Fact]
        public async Task PutCar_Returns204()
        {
            var carDataPut = new
            {
                Nickname = "Silver rainbow",
                RegistrationNr = "ASV321",
            };
            var client = _factory.CreateClient().AppendValidAuthHeader(Utilities.testOwner1.Id);;
            var response = await client.PutAsync($"/api/cars/{Utilities.testCar2.Id}", Utilities.ToJsonBody(carDataPut));
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
            var getCarResponseMsg = await client.GetAsync($"/api/cars/{Utilities.testCar2.Id}");
            var getCarResponse = await getCarResponseMsg.Content.FromJsonAsync();
            Assert.Equal(carDataPut.Nickname, getCarResponse.Nickname);
            Assert.Equal(carDataPut.RegistrationNr, getCarResponse.RegistrationNr);
        }

        [Fact]
        public async Task DeleteCar_Returns204()
        {
            var client = _factory.CreateClient().AppendValidAuthHeader(Utilities.testOwner1.Id);;
            var response = await client.DeleteAsync($"/api/cars/{Utilities.testCar2.Id}");
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
            var getCarResponseMsg = await client.GetAsync($"/api/cars/{Utilities.testCar2.Id}");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, getCarResponseMsg.StatusCode);
        }
    }
}
