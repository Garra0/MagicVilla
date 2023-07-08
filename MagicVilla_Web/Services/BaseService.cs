using System;
using System.Text;
using MagicVilla_Utility;
using MagicVilla_Web.models;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Newtonsoft.Json;

namespace MagicVilla_Web.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse responseModel { set; get; }

        public IHttpClientFactory httpClient { set; get; }

        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseModel = new();
            this.httpClient = httpClient;
        }

        // this fun get Request to access the DB
        // then its return a Response
        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                // we will call our client 'MagicAPI'
                var client = httpClient.CreateClient("MagicAPI");
                // we use for the Request message:
                // 1-Headers 2-RequestUri 3-Content(just in POST/PUT) 4-Method
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                //Console.WriteLine(message.Headers);
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null)
                {
                    // JsonConvert -> convert between .net & jsvon types
                    // SerializeObject -> represenation json string ( i think its make json string)
                    // StringContent(Data, Encoding, type)
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }

                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;

                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;

                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;

                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                // Response Message
                HttpResponseMessage apiResponse = null;

                apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                // DeserializeObject -> (i think its make .net string by json)
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;
            }
            catch (Exception e)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(e.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}

