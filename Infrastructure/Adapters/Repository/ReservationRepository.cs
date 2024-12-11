using Domain.Entities;
using Domain.Ports.Repository;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Adapters.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly SqlContext _context;

        public ReservationRepository(SqlContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByRoomAsync(Guid habitacionId)
        {
            return await _context.Reservations
                .Where(r => r.HabitacionId == habitacionId)
                .ToListAsync();
        }

        public async Task<Reservation?> GetByIdAsync(Guid id)
        {
            return await _context.Reservations.FindAsync(id);
        }

        public async Task AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }
    }

}
