using Domain.DTOs;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Ports.Repository;
using Domain.Ports.Services;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        //private readonly IEventPublisher _eventPublisher;

        public ReservationService(IReservationRepository reservationRepository/*, IEventPublisher eventPublisher*/)
        {
            _reservationRepository = reservationRepository;
            //_eventPublisher = eventPublisher;
        }

        public async Task<Reservation> ReserveRoomAsync(Guid habitacionId, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                var existingReservations = await _reservationRepository.GetReservationsByRoomAsync(habitacionId);

                if (existingReservations.Any(r => r.VerificarFecha(fechaInicio, fechaFinal)))
                {
                    throw new InvalidOperationException("La habitación no está disponible en el rango de fechas proporcionado.");
                }

                var reservation = new Reservation(habitacionId, fechaInicio, fechaFinal);
                await _reservationRepository.AddAsync(reservation);

                //// Publicar el evento de reserva creada.
                //await _eventPublisher.PublishAsync(new ReservationCreatedEvent(reservation.Id));

                return reservation;
            }
            catch(DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException("La habitación ya fue reservada por otro usuario.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error al procesar la reserva.", ex);
            }
            
        }

        public async Task CancelReservationAsync(Guid reservationId)
        {
            try
            {
                var reservation = await _reservationRepository.GetByIdAsync(reservationId);

                if (reservation == null)
                {
                    throw new KeyNotFoundException("Reserva no encontrada.");
                }

                 await _reservationRepository.DeleteAsync(reservation);
            }catch(NotFoundException ex)
            {
                throw new NotFoundException("Reserva no encontrada. " + ex.Message);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Ocurrió un error inesperado al intentar cancelar la reserva.", ex);
            }
            
        }

        public async Task<Reservation> UpdateReservationAsync(Guid reservationId, ReservationDto reservationDto)
        {
            try
            {
                var reservation = await _reservationRepository.GetByIdAsync(reservationId);

                if (reservation == null)
                {
                    throw new NotFoundException("Reserva no encontrada.");
                }

                var existingReservations = await _reservationRepository.GetReservationsByRoomAsync(reservationDto.HabitacionId);

                if (existingReservations.Any(r => r.VerificarFecha(reservationDto.FechaInicio, reservationDto.FechaFinal)))
                {
                    throw new InvalidOperationException("La habitación no está disponible en el rango de fechas proporcionado.");
                }

                reservation.FechaInicio = reservationDto.FechaInicio;
                reservation.FechaFinal = reservationDto.FechaFinal;
                reservation.HabitacionId = reservationDto.HabitacionId;

                await _reservationRepository.UpdateAsync(reservation);

                // Publicar evento de actualización
                //await _eventPublisher.PublishAsync(new ReservationCreatedEvent(reservation.Id));

                return reservation;
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Reserva no encontrada. " + ex.Message);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException("Conflicto de concurrencia al intentar actualizar la reserva.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error inesperado al intentar actualizar la reserva.", ex);
            }
        }
    }

}
