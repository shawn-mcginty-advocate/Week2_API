﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Week2_API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Week2_API.Controllers
{
    public class HomeController : Controller
    {
        HttpClient httpClient;

        static string BASE_URL = "https://developer.nps.gov/api/v1/";
        static string API_KEY = "negqHNjoffQ5Tvxf2M4dnzsROxoZ8MSuC3hDaXaZ"; //API Key

        // Obtaining the API key is easy. The same key should be usable across the entire
        // data.gov developer network, i.e. all data sources on data.gov.
        // https://www.nps.gov/subjects/developer/get-started.htm
        public IActionResult Index()
        {
            {
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string NATIONAL_PARK_API_PATH = BASE_URL + "/parks?limit=20";
                string parksData = "";

                Parks parks = null;

                httpClient.BaseAddress = new Uri(NATIONAL_PARK_API_PATH);

                try
                {
                    HttpResponseMessage response = httpClient.GetAsync(NATIONAL_PARK_API_PATH).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        parksData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    }

                    if (!parksData.Equals(""))
                    {
                        //JsonConvert is part of the NewtonSoft.Json Nuget package. Add Extension
                        parks = JsonConvert.DeserializeObject<Parks>(parksData);
                    }
                }
                catch (Exception e)
                {
                    // This is a useful place to insert a breakpoint and observe the error message
                    Console.WriteLine(e.Message);
                }

                return View(parks);
            }
        }
    }
}