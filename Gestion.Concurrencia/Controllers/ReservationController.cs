using Domain.DTOs;
using Domain.Ports.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Concurrencia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("reserve-room")]
        public async Task<IActionResult> ReserveRoom([FromBody] ReservationDto reservationDto)
        {
            var reservation = await _reservationService.ReserveRoomAsync(
                reservationDto.HabitacionId, reservationDto.FechaInicio, reservationDto.FechaFinal);

            return Ok(new { Message = "Reserva creada exitosamente.", ReservationId = reservation.Id });
        }

        [HttpDelete("cancel-reservation/{reservationId}")]
        public async Task<IActionResult> CancelReservation(Guid reservationId)
        {
            await _reservationService.CancelReservationAsync(reservationId);

            return Ok(new { Message = "Reserva cancelada exitosamente." });
        }

        [HttpPut("update-reservation/{reservationId}")]
        public async Task<IActionResult> UpdateReservation(Guid reservationId, [FromBody] ReservationDto reservationDto)
        {
            var updatedReservation = await _reservationService.UpdateReservationAsync(reservationId, reservationDto);

            return Ok(new { Message = "Reserva actualizada exitosamente.", ReservationId = updatedReservation.Id });
        }
    }
}
