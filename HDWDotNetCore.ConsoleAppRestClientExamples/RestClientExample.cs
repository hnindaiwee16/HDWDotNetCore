using HDWDotNetCore.ConsoleAppHttpClientExamples;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HDWDotNetCore.ConsoleAppRestClientExamples
{
    internal class RestClientExample
    {
        private readonly RestClient _client = new RestClient(new Uri("https://localhost:7020"));
        private readonly string _blogEndPoint = "api/blog";

        public async Task RunAsync()
        {
            //await ReadAsync();
            //await EditAsync(17);
            //await CreateAsync("title_18", "author_18", "content_18");
            //await UpdateAsync(18, "title18", "author18", "content18");
            //await DeleteAsync(18);
            string blogTitle = "";
            string blogAuthor = "author17";
            string blogContent = "";
            await UpdateByEachAsync(17, blogTitle, blogAuthor, blogContent);
        }
        private async Task ReadAsync()
        {
            RestRequest restRequest = new RestRequest(_blogEndPoint,Method.Get);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr =  response.Content!;
                List<BlogDto> lst = JsonConvert.DeserializeObject<List<BlogDto>>(jsonStr!)!;
                foreach (var item in lst)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(item));
                    Console.WriteLine($"Title => {item.BlogTitle}");
                    Console.WriteLine($"Author =>{item.BlogAuthor}");
                    Console.WriteLine($"Content =>{item.BlogContent}");
                }
            }
            else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }

        private async Task EditAsync(int id)
        {
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{id}", Method.Get);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr =  response.Content!;
                var item = JsonConvert.DeserializeObject<BlogDto>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(item));
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}");
            }
            else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }

        }

        private async Task CreateAsync(string blogTitle, string blogAuthor, string blogContent)
        {
            BlogDto dto = new BlogDto()
            {
                BlogTitle = blogTitle,
                BlogAuthor = blogAuthor,
                BlogContent = blogContent
            };
            string blogJson = JsonConvert.SerializeObject(dto);
            var restRequest = new RestRequest(_blogEndPoint, Method.Post);
            restRequest.AddJsonBody(blogJson);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }
        private async Task UpdateAsync(int blogId, string blogTitle, string blogAuthor, string blogContent)
        {
            BlogDto dto = new BlogDto()
            {
                BlogTitle = blogTitle,
                BlogAuthor = blogAuthor,
                BlogContent = blogContent
            };
            string blogJson = JsonConvert.SerializeObject(dto);
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{blogId}",Method.Put);
            restRequest.AddJsonBody(blogJson);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }
        private async Task DeleteAsync(int blogId)
        {
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{blogId}", Method.Delete);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }
        private async Task UpdateByEachAsync(int blogId, string blogTitle, string blogAuthor, string blogContent)
        {
            BlogDto dto = new BlogDto();
            if (!string.IsNullOrEmpty(blogTitle)) dto.BlogTitle = blogTitle;
            if (!string.IsNullOrEmpty(blogAuthor)) dto.BlogAuthor = blogAuthor;
            if (!string.IsNullOrEmpty(blogContent)) dto.BlogContent = blogContent;

            string blogJson = JsonConvert.SerializeObject(dto);
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{blogId}", Method.Patch);
            restRequest.AddJsonBody(blogJson);
            var response = await _client.ExecuteAsync(restRequest!);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }
    }
}
