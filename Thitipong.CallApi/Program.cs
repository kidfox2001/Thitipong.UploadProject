using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace Thitipong.CallApi
{
    class Program
    {
        static void Main(string[] args)
        {
            // ex1Async();
            //ex2Async();
            var t1 = ex3Async();
            t1.Wait();
            var r1 = t1.Result;

            var d1  = DateTime.Now;
        }

        public static void ex1Async()
        {
            using var client = new HttpClient();

            client.BaseAddress = new Uri("https://api.github.com");
            client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

            var url = "repos/symfony/symfony/contributors";
            var response = client.GetAsync(url);
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var resp = result.Content.ReadAsStringAsync();
            }

            //response.EnsureSuccessStatusCode();
            ////var resp = await response.Content.ReadAsStringAsync();

            //List<Contributor> contributors = JsonConvert.DeserializeObject<List<Contributor>>(resp);
            //contributors.ForEach(Console.WriteLine);

            //record Contributor(string Login, short Contributions);
        }

        private static async System.Threading.Tasks.Task ex2Async()
        {
            var userName = "user7";
            var passwd = "passwd";
            var url = "https://httpbin.org/basic-auth/user7/passwd";

            using var client = new HttpClient();

            var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(authToken));

            // ถ้า await จะไม่รอทำให้ไม่เห็นผลลัพธ์ และ ผลลัพธ์จะเป็น object เลย
            // ถ้าเอาออก ผลลัพธ์จะเป็น task ทำให้เกิด method wait ให้ใช้
            var response = client.GetAsync(url);
            response.Wait();

            Console.WriteLine(response.IsCompleted.ToString());
            var content = await response.Result.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }

        public static async System.Threading.Tasks.Task<List<Book>> ex3Async()
        {
            var token = "46SHpigXDcByNQNEhTLvTKTp9IkE1Z6ANS9eZCeTRNzJcve7OF8r8CaUCNwuMukxhOflaqu475QNoPgvB6eu9e-d77sQOMrlO-IJzJy2DzT7_E4vAZlwFFiLUEi_FMP-4qsBofzKbcncIKcGEj87_Rwn1UvEbmM8tpENzQiHEt33xvZZC1X4UaAsHq6iV3qNImzKA4Ldr9ewQgLDHup5F9mB_jJqvu8_I-PY525-nPQ131omAf-TpOs9xDKpdVmGQuy5P37JwRo1PRMRTI6Oug";

            using var client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:33464");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = "api/books";

            var response1 = client.GetStringAsync(url);
            response1.Wait();
            var result1 = JsonConvert.DeserializeObject<List<Book>>(response1.Result);

            var response2 = client.GetFromJsonAsync<List<Book>>(url);
            response2.Wait();
            var result2 = response2.Result;

            var response3 = await client.GetFromJsonAsync<List<Book>>(url);
            return response3;

        }


    }

    public class Book
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
    }
}
