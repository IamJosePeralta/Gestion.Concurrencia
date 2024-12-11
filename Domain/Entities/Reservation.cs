namespace Domain.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; } 
        public Guid HabitacionId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public byte[] RowVersion { get; set; }
    
    
        public Reservation(Guid habitacionId, DateTime fechaInicio, DateTime fechaFinal)
        {
            Id = Guid.NewGuid();
            HabitacionId = habitacionId;
            FechaInicio = fechaInicio;
            FechaFinal = fechaFinal;
            RowVersion = Array.Empty<byte>();
        }
    
        public bool VerificarFecha(DateTime otherStartDate, DateTime otherEndDate)
        {
            return FechaInicio < otherEndDate && FechaFinal > otherStartDate;
        }
    }
    
    
}
