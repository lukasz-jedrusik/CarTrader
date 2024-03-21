using CarTrader.Services.ParkingPlaces.Application.Interfaces.Repositories;
using CarTrader.Services.ParkingPlaces.Domain.Models;
using CarTrader.Services.ParkingPlaces.Infrastructure.Extensions.EfCore;
using Microsoft.EntityFrameworkCore;

namespace CarTrader.Services.ParkingPlaces.Infrastructure.Repositories
{
    public class DictionaryRepository<TEntity> : IDictionaryRepository<TEntity> where TEntity : DictionaryEntity
    {
        private readonly DataContext _context;

        public DictionaryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetAll(DateTime? date)
        {
            if (date == null) date = DateTime.UtcNow;

            return await _context.Set<TEntity>()
                .Where(x => x.CreateDate <= date && (x.DeleteDate == null || x.DeleteDate >= date))
                .ToListAsync();
        }

        public async Task<TEntity> GetById(int itemId) =>
            await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == itemId);

        public async Task<List<TEntity>> GetByIds(List<int> itemsId) =>
            await _context.Set<TEntity>().Where(x => itemsId.Contains(x.Id)).ToListAsync();

        public async Task<List<TEntity>> GetBySentence(DateTime? date, string sentence)
        {
            if (date == null) date = DateTime.UtcNow;

            return await _context.Set<TEntity>()
                .Where(x => x.CreateDate <= date && (x.DeleteDate == null || x.DeleteDate >= date))
                .Where(x => x.Name.Contains(sentence))
                .ToListAsync();
        }

        public async Task Add(TEntity entity)
        {
            entity.CreateDate = DateTime.UtcNow;
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> GetAdded(string name) =>
            await _context.Set<TEntity>().OrderBy(x => x.CreateDate).LastOrDefaultAsync(x => x.Name == name);

        public async Task Delete(int itemId)
        {
            var itemToDelete = await _context.Set<TEntity>().Where(x => x.Id == itemId).FirstOrDefaultAsync();

            if (itemToDelete == null)
            {
                return;
            }

            itemToDelete.DeleteDate = DateTime.UtcNow;

            _context.Entry(itemToDelete).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            var itemToUpdate = await _context.Set<TEntity>().Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

            if (itemToUpdate == null)
            {
                return;
            }

            itemToUpdate.Name = entity.Name;

            _context.Entry(itemToUpdate).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}