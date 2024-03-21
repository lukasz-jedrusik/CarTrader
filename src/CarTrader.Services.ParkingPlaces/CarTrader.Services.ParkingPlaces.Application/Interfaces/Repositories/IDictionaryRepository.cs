using CarTrader.Services.ParkingPlaces.Domain.Models;

namespace CarTrader.Services.ParkingPlaces.Application.Interfaces.Repositories
{
    public interface IDictionaryRepository<TEntity> where TEntity : DictionaryEntity
    {
        Task<List<TEntity>> GetAll(DateTime? date);
        Task<TEntity> GetById(int itemId);
        Task<List<TEntity>> GetByIds(List<int> itemsId);
        Task<List<TEntity>> GetBySentence(DateTime? date, string sentence);
        Task Add(TEntity entity);
        Task<TEntity> GetAdded(string name);
        Task Delete(int itemId);
        Task Update(TEntity entity);
    }
}