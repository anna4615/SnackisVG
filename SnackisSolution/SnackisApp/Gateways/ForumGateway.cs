using Microsoft.Extensions.Configuration;
using SnackisApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace SnackisApp.Gateways
{
    public class ForumGateway : IForumGateway
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public ForumGateway(IConfiguration configuration, HttpClient client)
        {
            _configuration = configuration;
            _client = client;
        }


        public async Task<List<Forum>> GetForums()
        {
            var respons = await _client.GetAsync(_configuration["ForumAPI"]);
            string apiRespons = await respons.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Forum>>(apiRespons);
        }

        public async Task<Forum> PostForum(Forum forum)
        {
            var response = await _client.PostAsJsonAsync(_configuration["ForumAPI"], forum);
            Forum returnValue = await response.Content.ReadFromJsonAsync<Forum>();

            return returnValue;
        }

        public async Task<Forum> DeleteForum(int deleteId)
        {
            throw new NotImplementedException();
        }

        public async Task<Forum> PutForum(int editId, Forum forum)
        {
            throw new NotImplementedException();
        }
    }
}
