namespace Domain.DTOs
{
    public class ReservationDto
    {
        public Guid HabitacionId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
    }

}
