using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApplication1.Controllers
{
    public class NoticiasController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult ObtenerNoticias(Models.Request.HistoriaRequest MHistoria)
        {
            var request = (HttpWebRequest)WebRequest.Create($"https://newsapi.org/v2/everything?q="
                +MHistoria.Ciudad
                +"&from="+
                DateTime.Now.ToString("yyyy-MM-dd") + "&sortBy=publishedAt&apiKey=7c363a0b9c8d4928a04458656ec954c3");
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return Content(HttpStatusCode.BadRequest, "Null"); ;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            return Ok(JsonConvert.DeserializeObject<object>(responseBody));
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                return Content(HttpStatusCode.BadRequest, "Error");
            }
        }
    }
}
