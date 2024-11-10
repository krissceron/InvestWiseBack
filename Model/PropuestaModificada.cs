using System.Diagnostics.Contracts;

namespace InvestWiseProyecto.Model
{
    public class PropuestaModificada
    {
        public int idPropuesta {  get; set; }
        public int idProducto { get; set; }
        public int idEstadoPropuesta { get; set; }
        public int numInversionistasPropuesta { get; set; }
        public int presupuestoGastoPropuesta { get; set; }
        public int fechaFinPropuesta { get; set; }
        public int estaAprobado { get; set; }
    }
}
