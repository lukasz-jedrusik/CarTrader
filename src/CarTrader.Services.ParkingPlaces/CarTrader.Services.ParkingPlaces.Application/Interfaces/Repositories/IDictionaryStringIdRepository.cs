using CarTrader.Services.ParkingPlaces.Domain.Models;

namespace CarTrader.Services.ParkingPlaces.Application.Interfaces.Repositories
{
    public interface IDictionaryStringIdRepository<TEntity> where TEntity : DictionaryStringIdEntity
    {
        Task<List<TEntity>> GetAll(DateTime? date);
        Task<TEntity> GetById(string itemId);
        Task<List<TEntity>> GetByIds(List<string> itemsId);
        Task<List<TEntity>> GetBySentence(DateTime? date, string sentence);
        Task Add(TEntity entity);
        Task<TEntity> GetAdded(string itemId);
        Task Delete(string itemId);
        Task Update(TEntity entity);
    }
}