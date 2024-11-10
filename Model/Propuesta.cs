namespace InvestWiseProyecto.Model
{
    public class Propuesta
    {
        public int idProducto {  get; set; }
        public int idEstadoPropuesta { get; set; }
        public int numInversionistasPropuesta { get; set; }
        public int presupuestoGastoPropuesta { get; set; }
        public DateTime fechaFinPropuesta { get; set; }
        public int estaAprobado { get; set; }
    }
}
