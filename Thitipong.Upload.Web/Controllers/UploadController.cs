using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Thitipong.Upload.Service;
using Thitipong.Upload.Web.Models;

namespace Thitipong.Upload.Web.Controllers
{
    public class UploadController : Controller
    {

        private readonly App _app;

        public UploadController(App app)
        {
            this._app = app;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(FilePost model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44315/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var formData = new MultipartFormDataContent();
                    formData.Add(new StreamContent(model.MyFile.OpenReadStream()), "formFile", model.MyFile.FileName);

                    var response = client.PostAsync("api/transaction/upload", formData);
                    response.Wait();

                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Msg = $"Upload Success {result.StatusCode}";
                    }
                    else
                    {
                        ViewBag.Msg = $"Upload Not Success {result.StatusCode}";

                    }

                }

            }

            return View(model);
        }

    }
}
