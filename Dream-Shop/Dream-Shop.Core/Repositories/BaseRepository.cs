using Dream_Shop.Database;

namespace Dream_Shop.Core.Repositories;

public interface IBaseRepository<TEntity>
{
    void Add(TEntity entity);
    void AddMany(List<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveMany(List<TEntity> entities);
    void Update(TEntity entity);
    void UpdateMany(List<TEntity> entities);
    Task SaveChangesAsync();
}

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity: class
{
    internal readonly AppDbContext _db;

    public BaseRepository(AppDbContext db)
    {
        _db = db;
    }

    public void Add(TEntity entity)
    {
        _db.AddAsync(entity);
    }

    public void AddMany(List<TEntity> entities)
    {
        _db.AddRangeAsync(entities);
    }

    public void Remove(TEntity entity)
    {
        _db.Remove(entity);
    }

    public void RemoveMany(List<TEntity> entities)
    {
        _db.RemoveRange(entities);
    }

    public void Update(TEntity entity)
    {
         _db.Update(entity);
    }

    public void UpdateMany(List<TEntity> entities)
    {
        _db.UpdateRange(entities);
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}