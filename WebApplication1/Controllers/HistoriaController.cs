using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApplication1.Controllers
{
    public class HistoriaController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult GuardarHistoria(Models.Request.HistoriaRequest MHistoria)
        {
            using (Models.api_estadociudadEntities2 BD = new Models.api_estadociudadEntities2())
            {
                var OHistoria = new Models.historia();
                OHistoria.ciudad = MHistoria.Ciudad;
                OHistoria.fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                BD.historia.Add(OHistoria);
                BD.SaveChanges();
                return Ok("Registro exitoso");
            }
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public IHttpActionResult ListaDeHistoria()
        {
            using (Models.api_estadociudadEntities2 BD = new Models.api_estadociudadEntities2())
            {
                return Ok((from D in BD.historia select D).ToList());
            }
            
        }
    }
}
