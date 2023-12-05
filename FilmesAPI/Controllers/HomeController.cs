using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;

namespace FilmesAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiKeyService _apiKeyService;

        public HomeController(ApiKeyService apiKeyService)
        {
            _apiKeyService = apiKeyService;
        }

        private async Task<RestResponse> GetApiResponse(string url)
        {
            var client = new RestClient();
            var request = new RestRequest(url, Method.Get);
            request.AddParameter("api_key", _apiKeyService.ApiKey);
            request.AddParameter("access_token", _apiKeyService.Token);
            request.AddParameter("language", "pt-BR");

            return await client.ExecuteAsync(request);
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var popularResponse = await GetApiResponse("https://api.themoviedb.org/3/movie/popular");
                var nowPlayingResponse = await GetApiResponse("https://api.themoviedb.org/3/movie/now_playing");
                var topRatedResponse = await GetApiResponse("https://api.themoviedb.org/3/movie/top_rated");

                if (popularResponse.StatusCode == System.Net.HttpStatusCode.OK &&
                    nowPlayingResponse.StatusCode == System.Net.HttpStatusCode.OK &&
                    topRatedResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Models.Root popularFilmes = JsonConvert.DeserializeObject<Models.Root>(popularResponse.Content);
                    Models.Root nowPlayingFilmes = JsonConvert.DeserializeObject<Models.Root>(nowPlayingResponse.Content);
                    Models.Root topRatedFilmes = JsonConvert.DeserializeObject<Models.Root>(topRatedResponse.Content);

                    ViewData["PopularFilmes"] = popularFilmes.results;
                    ViewData["NowPlayingFilmes"] = nowPlayingFilmes.results;
                    ViewData["TopRatedFilmes"] = topRatedFilmes.results;

                    return View();
                }
                else
                {
                    return View("Erro");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter a lista de filmes: {ex.Message}");
                return View("Erro");
            }
        }

        public async Task<IActionResult> Search(string searchString)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    var client = new RestClient();
                    var request = new RestRequest("https://api.themoviedb.org/3/search/movie", Method.Get);
                    request.AddParameter("api_key", _apiKeyService.ApiKey);
                    request.AddParameter("access_token", _apiKeyService.Token);
                    request.AddParameter("language", "pt-BR");
                    request.AddParameter("query", searchString);

                    RestResponse response = await client.ExecuteAsync(request);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var resultado = JsonConvert.DeserializeObject<Models.Root>(response.Content);

                        ViewData["searchString"] = searchString;

                        return View(resultado);
                    }
                    else
                    {
                        return View("Erro");
                    }
                }

                return View("Erro");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter a lista de filmes: {ex.Message}");
                return View("Erro");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest($"https://api.themoviedb.org/3/movie/{id}", Method.Get);
                request.AddParameter("api_key", _apiKeyService.ApiKey);
                request.AddParameter("access_token", _apiKeyService.Token);
                request.AddParameter("language", "pt-BR");

                RestResponse response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var resultado = JsonConvert.DeserializeObject<Models.Root>(response.Content);

                    return View(resultado);
                }
                else
                {
                    return View("Erro");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter detalhes do filme: {ex.Message}");
                return View("Erro");
            }
        }
    }
}