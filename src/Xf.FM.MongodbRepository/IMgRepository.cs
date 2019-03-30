using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Xf.FM.MongodbRepository
{
    public interface IMgRepository<TEntity> where TEntity : BaseMgDbEntity
    {
        IMongoCollection<TEntity> Col { get; }
        IMongoQueryable<TEntity> Query { get; }
        Task<long> CountDocumentsAsync(Expression<Func<TEntity, bool>> expression);
        Task<long> CountDocumentsAsync(FilterDefinition<TEntity> filter);
        Task<long> DeleteManyAsync(Expression<Func<TEntity, bool>> expression);
        Task<long> DeleteManyAsync(FilterDefinition<TEntity> filter);
        Task DeleteOneAsync(Expression<Func<TEntity, bool>> expression);
        Task DeleteOneAsync(FilterDefinition<TEntity> filter);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression, FindOptions<TEntity> findOptions = null);
        Task<IEnumerable<TEntity>> FindAsync(FilterDefinition<TEntity> filter, FindOptions<TEntity> findOptions = null);
        TEntity FindById(string id);
        Task<TEntity> FindByIdAsync(string id);
        Task<TEntity> FindOneAndUpdateAsync(Expression<Func<TEntity, bool>> expression, UpdateDefinition<TEntity> update);
        Task<TEntity> FindOneAndUpdateAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update);
        Task<IEnumerable<TEntity>> FindPageAsync(Expression<Func<TEntity, bool>> expression, FindOptions<TEntity> findOptions);
        Task<IEnumerable<TEntity>> FindPageAsync(FilterDefinition<TEntity> filter, FindOptions<TEntity> findOptions);
        Task InsertManyAsync(IEnumerable<TEntity> entities);
        void InsertOne(TEntity entity);
        Task<long> UpdateManyAsync(Expression<Func<TEntity, bool>> expression, UpdateDefinition<TEntity> update);
        Task<long> UpdateManyAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update);
        Task UpdateOneAsync(Expression<Func<TEntity, bool>> expression, UpdateDefinition<TEntity> update);
        Task UpdateOneAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update);
        Task<TEntity> FindOneAndReplaceAsync(FilterDefinition<TEntity> filter, TEntity entity);
        Task<TEntity> FindOneAndReplaceAsync(Expression<Func<TEntity, bool>> expression, TEntity entity);
    }
}