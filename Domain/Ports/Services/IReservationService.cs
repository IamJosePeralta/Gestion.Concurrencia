using Domain.DTOs;
using Domain.Entities;

namespace Domain.Ports.Services
{
    public interface IReservationService
    {
        Task<Reservation> ReserveRoomAsync(Guid habitacionId, DateTime fechaInicio, DateTime fechaFinal);
        Task CancelReservationAsync(Guid reservationId);
        Task<Reservation> UpdateReservationAsync(Guid reservationId, ReservationDto reservationDto);
    }

}
