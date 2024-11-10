using InvestWiseProyecto.Data;
using InvestWiseProyecto.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestWiseProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public Respuesta Login([FromBody] LoginUsuario loginUsuario)
        {
            UsuarioConection dbConexion = new UsuarioConection();
            Respuesta res = dbConexion.LoginUsuario(loginUsuario);
            return res;
        }


        [HttpPost]
        [Route("Crear")]
        public Respuesta CrearUsuario([FromBody] Usuario usuario)
        {

            UsuarioConection dbConexion = new UsuarioConection();
            Respuesta res = dbConexion.InsertarUsuario(usuario);


            return res;
        }

        [HttpGet]
        [Route("ObtenerTodo")]
        public Respuesta ObtenerUsuarios()
        {
            UsuarioConection dbConexion = new UsuarioConection();
            Respuesta res = dbConexion.ObtenerUsuario();
            return res;
        }

        [HttpGet]
        [Route("ObtenerPorId/{idUsuario}")]
        public Respuesta ObtenerUsuarioPorId(int idUsuario)
        {
            UsuarioConection dbConexion = new UsuarioConection();
            Respuesta res = dbConexion.ObtenerUsuarioPorId(idUsuario);
            return res;
        }


        [HttpPut]
        [Route("Editar")]
        public Respuesta ActualizarUsuario([FromBody] UsuarioModificado usuarioModi)
        {
            UsuarioConection dbConexion = new UsuarioConection();
            Respuesta res = dbConexion.ActualizarUsuario(usuarioModi);
            return res;
        }

        [HttpDelete]
        [Route("Eliminar/{idUsuario}")]
        public Respuesta EliminarUsuario(int idUsuario)
        {
            UsuarioConection dbConexion = new UsuarioConection();
            Respuesta res = dbConexion.EliminarUsuario(idUsuario);
            return res;
        }


    }
}
