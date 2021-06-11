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
    public class OffensiveWordsGateway : IOffensiveWordsGateway
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public OffensiveWordsGateway(IConfiguration configuration, HttpClient client)
        {
            _configuration = configuration;
            _client = client;
        }


        public async Task<List<OffensiveWord>> GetWords()
        {
            var response = await _client.GetAsync(_configuration["OffensiveWordsAPI"]);
            string apiResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<OffensiveWord>>(apiResponse);
        }

        public async Task<OffensiveWord> GetWord(int id)
        {
            var response = await _client.GetAsync(_configuration["OffensiveWordsAPI"] + "/" + id);
            OffensiveWord returnValue = await response.Content.ReadFromJsonAsync<OffensiveWord>();

            return returnValue;
        }

        public async Task<OffensiveWord> PostWord(OffensiveWord word)
        {
            var response = await _client.PostAsJsonAsync(_configuration["OffensiveWordsAPI"], word);
            OffensiveWord returnValue = await response.Content.ReadFromJsonAsync<OffensiveWord>();

            return returnValue;
        }

        public async Task PutWord(int editId, OffensiveWord word)
        {
            var respons = await _client.PutAsJsonAsync(_configuration["OffensiveWordsAPI"] + "/" + editId, word);
        }

        public async Task DeleteWord(int deleteId)
        {
            var respons = await _client.DeleteAsync(_configuration["OffensiveWordsAPI"] + "/" + deleteId);
        }

        public async Task<string> GetCheckedText(string text)
        {

            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }
            List<OffensiveWord> forbiddenWords = await GetWords();
            List<string> wordList = forbiddenWords.Select(w => w.Word).ToList();

            string checkedText = text;
            string censoredText = "";

            //// Inget bra, hela texten får små bokstäver
            //foreach (var word in wordList)
            //{
            //    if (text.ToLower().Contains(word))
            //    {
            //        censoredText = text.ToLower().Replace(word, "****");
            //    }
            //}

            // -Kolla om texten innehåller förbjudet ord
            // -I så fall, splita stringen och ersätt förbjudet ord med ****
            // -Sätt ihop till en string igen
            foreach (var word in wordList)
            {
                if (text.ToLower().Contains(word))
                {
                    string[] splitString = text.Split(" ");

                    for (int i = 0; i < splitString.Length; i++)
                    {
                        if (splitString[i].ToLower().Contains(word))
                        {
                            splitString[i] = splitString[i].ToLower().Replace(word, "****");
                        }
                    }

                    censoredText = string.Join(" ", splitString);
                    checkedText = censoredText;
                }
            }

            return checkedText;
        }
    }
}
