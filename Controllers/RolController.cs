using InvestWiseProyecto.Data;
using InvestWiseProyecto.DataConnection;
using InvestWiseProyecto.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestWiseProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        [HttpGet]
        [Route("ObtenerTodo")]
        public Respuesta ObtenerRol()
        {
            RolConnection dbConexion = new RolConnection();
            Respuesta res = dbConexion.ObtenerRol();
            return res;
        }
    }
}
