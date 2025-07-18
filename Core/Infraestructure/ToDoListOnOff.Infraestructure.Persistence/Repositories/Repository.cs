using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ToDoListOnOff.Domain.Interfaces.Repositories;
using ToDoListOnOff.Domain.Primitives;
using ToDoListOnOff.Infraestructure.Persistence.Context;

namespace ToDoListOnOff.Infraestructure.Persistence.Repositories;

public class Repository<TEntity, TId>(
        ToDoListContext context,
        IDbContextFactory<ToDoListContext> dbContextFactory) : IRepository<TEntity, TId>
    where TEntity : EntityRoot<TId>, new() 
{
    /// <summary>
    /// Contexto de base de datos
    /// </summary>
    protected readonly ToDoListContext Context = context;
    /// <summary>
    /// Factory para crear contextos de base de datos
    /// </summary>
    protected readonly IDbContextFactory<ToDoListContext> DbContextFactory = dbContextFactory;

    #region Getters
    public virtual async Task<ResponseDataPaginate<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? where,
        Expression<Func<TEntity, TEntity>>? select,
        int actualPage, int pageSize)
    {
        List<TEntity> data = new List<TEntity>();
        using (var _context = await DbContextFactory.CreateDbContextAsync())
        {
            IQueryable<TEntity> dbSet = _context.Set<TEntity>().AsNoTracking().OrderByDescending(e => e.Id);
            
            if (where is not null)
                dbSet = dbSet.Where(where);
            if (select is not null)
                dbSet = dbSet.Select(select);
            

            var countData = dbSet.Count();
            int totalPages = (int)Math.Ceiling((double)countData / pageSize);
            int Index = (actualPage - 1) * pageSize;

            data = await dbSet.Skip(Index).Take(pageSize).ToListAsync();

            return new ResponseDataPaginate<TEntity> { Data = data, ActualPage = actualPage, CountData = countData, CountPages = totalPages };
        }
    }

    public virtual async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? where, Expression<Func<TEntity, TEntity>>? select)
    {
        List<TEntity> data = new List<TEntity>();
        using (var _context = await DbContextFactory.CreateDbContextAsync())
        {
            IQueryable<TEntity> dbSet = _context.Set<TEntity>().AsNoTracking().OrderByDescending(e => e.Id);
            if (where is not null)
                dbSet = dbSet.Where(where);
            if (select is not null)
                dbSet = dbSet.Select(select);

            data = await dbSet.ToListAsync();
        }
        return data;
    }

    public virtual async Task<TEntity?> GetFirstAsync(TId id)
    {
        TEntity? entity;
        using (var _context = await DbContextFactory.CreateDbContextAsync())
        {
            var dbSet = _context.Set<TEntity>();
            dbSet.AsNoTracking();
            entity = await dbSet.FindAsync(id);
        }

        return entity;
    }

    public virtual async Task<TEntity?> GetFirstAsync(TId id, Expression<Func<TEntity, TEntity>> select)
    {
        TEntity? entity;
        using (var _context = await DbContextFactory.CreateDbContextAsync())
        {
            entity = await _context.Set<TEntity>().AsNoTracking().Where(e => e.Id!.Equals(id)).Take(1).Select(select).FirstOrDefaultAsync();
        }

        return entity;
    }

    public virtual async Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> firstOrDefault, Expression<Func<TEntity, TEntity>> select)
    {
        TEntity? entity;
        using (var _context = await DbContextFactory.CreateDbContextAsync())
        {
            entity = await _context.Set<TEntity>().AsNoTracking().Where(firstOrDefault).Take(1).Select(select).FirstOrDefaultAsync();
        }

        return entity;
    }

    #endregion


    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
        return entity;
    }


    public virtual async Task UpdateAsync(TEntity entity)
    {
        var dbSet = Context.Set<TEntity>();
        var existingEntity = dbSet.Local.FirstOrDefault(x => x.Equals(entity));
        if (existingEntity is not null)
            dbSet.Local.Remove(existingEntity);

        var update = await Task.Run(() => dbSet.Update(entity));
    }


    public virtual async Task DeleteAsync(TId id)
    {
        var entityDelete = new TEntity { Id = id };
        await Task.Run(() =>
        {
            Context.Set<TEntity>().Attach(entityDelete);
            Context.Entry(entityDelete).Property(x => x.IsDeleted).CurrentValue = true;
        });
    }

    public virtual async Task RemoveAsync(TEntity entity)
    {
        var dbSet = Context.Set<TEntity>();
        await Task.Run(() => dbSet.Remove(entity));
    }

    public virtual async Task SaveChangesAsync() => await Context.SaveChangesAsync();
        
}