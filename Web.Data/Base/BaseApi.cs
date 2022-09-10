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
    public class BaseApi
    {

        public async Task<IActionResult> PostToApi(string ControllerName, object model)
        {
            try
            {
                //var client = _httpClient.CreateClient();

                //var response = await client.PostAsJsonAsync(ControllerName, model);

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
