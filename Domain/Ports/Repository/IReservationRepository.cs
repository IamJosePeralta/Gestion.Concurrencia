using Domain.Entities;

namespace Domain.Ports.Repository
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetReservationsByRoomAsync(Guid habitacionId);
        Task<Reservation?> GetByIdAsync(Guid id);
        Task AddAsync(Reservation reservation);
        Task UpdateAsync(Reservation reservation);
        Task DeleteAsync(Reservation reservation);
    }
}
