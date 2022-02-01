using System;

namespace ExercicioSala03.Entidades
{
    public class ReservaMesa
    {
        public DateTime InicioReserva { get; set; }
        public DateTime FimReserva { get; set; }
        public string Cliente { get; set; }
        public string Mesa { get; set; }
    }
}
