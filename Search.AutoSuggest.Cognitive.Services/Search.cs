using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Search.AutoSuggest.Cognitive.Services
{
    public class Search
    {
        public async Task<List<string>> AutoSuggestion(string textQuery, string endPoint = "https://api.cognitive.microsoft.com/bing/v7.0/suggestions")
        {
            List<string> suggestionsResult = new List<string>();
            HttpClient httpClient = new HttpClient();
            string param = "en-US";
            var result = await httpClient.GetAsync(string.Format("{0}/?q={1}&mkt={2}", endPoint, WebUtility.UrlEncode(textQuery), param));
            result.EnsureSuccessStatusCode();
            var json = await result.Content.ReadAsStringAsync();
            dynamic objectData = JObject.Parse(json);
            if (objectData.suggestionGroups != null && objectData.suggestionGroups.Count > 0 && objectData.suggestionGroups[0].searchSuggestions != null)
            {
                for (int compter = 0; compter < objectData.suggestionGroups[0].searchSuggestions.Count; compter++)
                {
                    suggestionsResult.Add(objectData.suggestionGroups[0].searchSuggestions[compter].displayText.Value);
                }
            }
            return suggestionsResult;
        }
    }
}
