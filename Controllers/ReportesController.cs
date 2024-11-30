using InvestWiseProyecto.DataConnection;
using InvestWiseProyecto.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestWiseProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        [HttpGet("ReportePorcentajeGanancia")]
        public IActionResult GenerarReporte()
        {
            var reporteService = new ReportePorcGananciaService();
            var resultado = reporteService.GenerarReporte();
            return Ok(resultado);
        }

        [HttpGet("ReportePorcentajeGananciaPorUsuario/{idUsuario}")]
        public IActionResult GenerarReportePorUsuario(int idUsuario)
        {
            var reporteService = new ReportePorcGananPorUsuService();
            var resultado = reporteService.GenerarReportePorUsuario(idUsuario);
            if (resultado.Count == 0)
            {
                return NotFound(new { mensaje = "No se encontraron datos para el usuario especificado." });
            }
            return Ok(resultado);
        }

    }
}
