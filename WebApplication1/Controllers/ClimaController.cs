using System.Web.Http.Cors;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ClimaController : ApiController
    {
                [HttpPost]
        public IHttpActionResult ObtenerClima(Models.Request.HistoriaRequest MHistoria)
        {
            var request = (HttpWebRequest)WebRequest.Create($"https://api.openweathermap.org/data/2.5/weather?q="+
                MHistoria.Ciudad + "&APPID=e2dce522f2cb47dede3fa6891e5b3b5c");
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
