using ProfitAndLoss.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xunit;

namespace ProfitAndLoss.Test.EndToEnds
{
    public class ActorControllerEndToEndTest
    {
        public HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44364/");
            var acceptType = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(acceptType);
            return httpClient;
        }
        [Fact]
        public async void GetActorByIdSmokeTest()
        {
            using (var httpClient = GetHttpClient())
            {
                var response = await httpClient.GetAsync("api/actors/1");
                string jsonString = null;
                if (response.IsSuccessStatusCode)
                {
                    jsonString = await response.Content.ReadAsStringAsync();
                    Assert.NotNull(jsonString);
                }
                Actor actor = JsonConvert.DeserializeObject<Actor>(jsonString);
                Assert.NotNull(actor);
                Assert.Equal(1, actor.ID);
            }
        }

        //[Fact]
        //public async void GetActionTest()
        //{
        //    var httpClient = GetHttpClient();
        //    var allProductsResponse = await httpClient.GetAsync("api/products");
        //    Assert.True(allProductsResponse.IsSuccessStatusCode);
        //    var allProductsJson = await allProductsResponse.Content.ReadAsStringAsync();
        //    var allProducts = JsonConvert.DeserializeObject<Product[]>(allProductsJson);
        //    Assert.NotNull(allProducts);
        //    Assert.True(allProducts.Length > 0);
        //}

        [Fact]
        public async void PostActionTest()
        {
            var httpClient = GetHttpClient();
            var product = new Actor { ID = 2, FirstName = "hungz", LastName = "hungnv" };
            var productJson = JsonConvert.SerializeObject(product);
            var httpContent = new StringContent(productJson, Encoding.UTF8, "application/json");
            var newProductReponse = await httpClient.PostAsync("api/actors", httpContent);
            Assert.True(newProductReponse.IsSuccessStatusCode);
            var newProductJson = await newProductReponse.Content.ReadAsStringAsync();
            var newProduct = JsonConvert.DeserializeObject<Actor>(newProductJson);
            Assert.True(newProduct.ID != 0);
        }

        //[Fact]
        //public async void PutActionTest()
        //{
        //    var httpClient = GetHttpClient();
        //    var product = Repository.Products[0];
        //    var productJson = JsonConvert.SerializeObject(product);
        //    var httpContent = new StringContent(productJson, Encoding.UTF8, "application/json");
        //    var putResponse = await httpClient.PutAsync("api/products/0", httpContent);
        //    Assert.True(putResponse.IsSuccessStatusCode);
        //}

        // [Fact]
        // public async void DeleteActionTest() {
        //     var httpClient = GetHttpClient();
        //     var deleteResponse = await httpClient.DeleteAsync("api/products/4");
        //     Assert.True(deleteResponse.IsSuccessStatusCode);
        //     deleteResponse = await httpClient.DeleteAsync("api/products/101");
        //     Assert.False(deleteResponse.IsSuccessStatusCode);
        // }
    }
}
