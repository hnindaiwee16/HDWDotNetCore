using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HDWDotNetCore.ConsoleAppHttpClientExamples
{
    internal class HttpClientExample
    {
        private readonly HttpClient _client = new HttpClient() { BaseAddress = new Uri("https://localhost:7020") };
        private readonly string _blogEndPoint = "api/blog";

        public async Task RunAsync()
        {
            //await ReadAsync();
            //await EditAsync(17);
            //await CreateAsync("title_18", "author_18", "content_18");
            //await UpdateAsync(18, "title18", "author18", "content18");
            //await DeleteAsync(18);
            string blogTitle = "";
            string blogAuthor = "@";
            string blogContent = "";
            await UpdateByEachAsync(17, blogTitle, blogAuthor, blogContent);
        }
        private async Task ReadAsync()
        {
            var response = await _client.GetAsync(_blogEndPoint);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                List<BlogDto> lst = JsonConvert.DeserializeObject<List<BlogDto>>(jsonStr!)!;
                foreach (var item in lst)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(item));
                    Console.WriteLine($"Title => {item.BlogTitle}");
                    Console.WriteLine($"Author =>{item.BlogAuthor}");
                    Console.WriteLine($"Content =>{item.BlogContent}");
                }
            }
        }

        private async Task EditAsync(int id)
        {
            var response = await _client.GetAsync($"{_blogEndPoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<BlogDto>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(item));
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}");
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
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
            HttpContent content = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            var response = await _client.PostAsync(_blogEndPoint, content);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
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
            HttpContent content = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            var response = await _client.PutAsync($"{_blogEndPoint}/{blogId}", content);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }
        private async Task DeleteAsync(int blogId)
        {
            var response = await _client.DeleteAsync($"{_blogEndPoint}/{blogId}");
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
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
            HttpContent content = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            var response = await _client.PatchAsync($"{_blogEndPoint}/{blogId}", content);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }
    }
}
