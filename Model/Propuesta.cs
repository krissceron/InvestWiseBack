﻿namespace InvestWiseProyecto.Model
{
    public class Propuesta
    {
        public int idProducto { get; set; }
        public int numInversionistasPropuesta { get; set; }

        private float _presupuestoGastoPropuesta;
        public int idEstadoPropuesta { get; set; }
        public float presupuestoGastoPropuesta
        {
            get => (float)Math.Round(_presupuestoGastoPropuesta, 2); // Limitar a 2 decimales
            set => _presupuestoGastoPropuesta = (float)Math.Round(value, 2);
        }

        public string fechaInicioPropuesta { get; set; }
    }
}
