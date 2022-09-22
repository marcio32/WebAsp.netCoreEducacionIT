using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Web.Data.Base
{
    public class BaseApi : ControllerBase
    {
        private readonly IHttpClientFactory _httpClient;

        public BaseApi(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> PostToApi(string ControllerName, object model)
        {
            try
            {
                var client = _httpClient.CreateClient("useApi");

                var response = await client.PostAsJsonAsync(ControllerName, model);

                if (response.IsSuccessStatusCode)
                {

                    var content = await response.Content.ReadAsStringAsync();
                    return Ok(content);
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IActionResult> GetToApi(string ControllerName)
        {
            try
            {
                var client = _httpClient.CreateClient("useApi");

                var response = await client.GetAsync(ControllerName);

                if (response.IsSuccessStatusCode)
                {

                    var content = await response.Content.ReadAsStringAsync();
                    return Ok(content);
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
