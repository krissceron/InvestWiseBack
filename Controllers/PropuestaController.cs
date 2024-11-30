using InvestWiseProyecto.DataConnection;
using InvestWiseProyecto.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestWiseProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropuestaController : ControllerBase
    {

        [HttpPost]
        [Route("Crear")]
        public Respuesta CrearPropuesta([FromBody] Propuesta propuesta)
        {

            PropuestaConnection dbConexion = new PropuestaConnection();
            Respuesta res = dbConexion.InsertarPropuesta(propuesta);


            return res;
        }

        [HttpGet]
        [Route("ObtenerTodo")]
        public Respuesta ObtenerPropuesta()
        {
            PropuestaConnection dbConexion = new PropuestaConnection();
            Respuesta res = dbConexion.ObtenerPropuestas();
            return res;
        }

        [HttpGet]
        [Route("ObtenerPorId/{idPropuesta}")]
        public Respuesta ObtenerPropuestaPorId(int idPropuesta)
        {
            PropuestaConnection dbConexion = new PropuestaConnection();
            Respuesta res = dbConexion.ObtenerPropuestaPorId(idPropuesta);
            return res;
        }

        [HttpGet]
        [Route("ObtenerUsuariosPorPropuesta/{idPropuesta}")]
        public IActionResult ObtenerUsuariosPorPropuesta(int idPropuesta)
        {
            PropuestaConnection dbConexion = new PropuestaConnection();
            Respuesta respuesta = dbConexion.ObtenerUsuariosPorPropuesta(idPropuesta);
            return Ok(respuesta); // Devuelve el resultado en formato JSON
        }


        [HttpPost]
        [Route("AceptarPropuesta")]
        public IActionResult AceptarPropuesta([FromBody] AceptarPropuesta usuarioPropuesta)
        {
            PropuestaConnection dbConexion = new PropuestaConnection();
            Respuesta respuesta = dbConexion.AceptarPropuesta(usuarioPropuesta);
            return Ok(respuesta); // Devuelve el resultado en formato JSON
        }


        [HttpPut]
        [Route("Editar")]
        public Respuesta ActualizarPropuesta([FromBody] PropuestaModificada propuestaModi)
        {
            PropuestaConnection dbConexion = new PropuestaConnection();
            Respuesta res = dbConexion.ActualizarPropuesta(propuestaModi);
            return res;
        }

        [HttpDelete]
        [Route("Eliminar/{idPropuesta}")]
        public Respuesta EliminarPropuesta(int idPropuesta)
        {
            PropuestaConnection dbConexion = new PropuestaConnection();
            Respuesta res = dbConexion.EliminarPropuesta(idPropuesta);
            return res;
        }

        [HttpPost]
        [Route("SalirPropuesta")]
        public IActionResult SalirPropuesta([FromBody] AceptarPropuesta usuarioPropuesta)
        {
            PropuestaConnection dbConexion = new PropuestaConnection();
            Respuesta respuesta = dbConexion.SalirPropuesta(usuarioPropuesta);
            return Ok(respuesta); // Devuelve el resultado en formato JSON
        }


    }
}
