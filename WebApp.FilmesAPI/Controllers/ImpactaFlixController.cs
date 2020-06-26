using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using WebApp.FilmesAPI.Models;

namespace WebApp.FilmesAPI.Controllers
{
    public class ImpactaFlixController : Controller
    {
        // GET: ImpactaFlix
        //Vamos consumir o WebAPI Filmes, que permite exibir uma lista de Filmes
        public ActionResult Index()
        {
            ////Criar uma lista 
            //IEnumerable<FilmeMOD> filmes = null;
            FilmeMOD filme = null;

            //Vamos criar um using para criar o nosso objeto HttpClient:
            //Objeto que permite a navegação, usar o protocolo Http, fazer teste de páginas, etc...
            //Permite navegar como se fosse o browser via back end
            using (var client = new HttpClient())
            {
                //http get no site de filmes
                //Definir a origem
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");

                var response = client.GetAsync("movie/550?api_key=f5c535362407d4358c0d43c3a2fe8b8b");
                //Esperar a resposta do site (aguardar)
                response.Wait();

                //Pegar o resultado
                var result = response.Result;

                //Verificamos se o código retornou com sucesso
                if (result.IsSuccessStatusCode)
                {
                    //Receber o retorno do JSON de filmes que é convertido para a classe FilmeMOD automaticamente pelo .net
                    filme = result.Content.ReadAsAsync<FilmeMOD>().Result;

                    //SE O JSON RETORNASSE UM ARRAY EU PRECISARIA RECEBER COM UM ARRAY
                    //var resultadoGET = result.Content.ReadAsAsync<IList<FilmeMOD>>();
                    //resultadoGET.Wait();
                    //filmes = resultadoGET.Result;
                }

                else
                {
                    //se não tiver sucesso devolver um model state com erro tratado - mensagem personalizada
                    filme = new FilmeMOD();
                    ModelState.AddModelError(string.Empty, "Erro ao tentar consultar dados no servidor de filmes");
                }

                //Devolver o resultado da consulta para a VIEW
                return View(filme);
            }
        }
    }
}