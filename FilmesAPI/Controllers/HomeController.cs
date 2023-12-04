using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;

namespace FilmesAPI.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                string apiKey = "4605392c2512f145b87dc33b6882bab5";
                var client = new RestClient();
                var request = new RestRequest($"https://api.themoviedb.org/3/movie/top_rated?api_key={apiKey}&language=pt-BR", Method.Get);

                RestResponse response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Models.Root listaDeFilmes = JsonConvert.DeserializeObject<Models.Root>(response.Content);
                    return View(listaDeFilmes);
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
                // Verifica se a string de pesquisa não é nula ou vazia antes de fazer a chamada à API
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    string apiKey = "4605392c2512f145b87dc33b6882bab5";
                    var client = new RestClient();
                    var request = new RestRequest("https://api.themoviedb.org/3/search/movie", Method.Get);
                    request.AddParameter("api_key", apiKey);
                    request.AddParameter("query", searchString);
                    request.AddParameter("language", "pt-BR");

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

        public async Task<IActionResult> Details() 
        {
            return View();
        }



    }
}