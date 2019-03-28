using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Xf.FM.MongodbRepository
{
    public interface IMgRepository<TDb> where TDb : BaseDbEntity
    {
        IMongoQueryable<TDb> Query { get; }
        Task<long> CountDocumentsAsync(Expression<Func<TDb, bool>> expression);
        Task<long> CountDocumentsAsync(FilterDefinition<TDb> filter);
        Task<long> DeleteManyAsync(Expression<Func<TDb, bool>> expression);
        Task<long> DeleteManyAsync(FilterDefinition<TDb> filter);
        Task DeleteOneAsync(Expression<Func<TDb, bool>> expression);
        Task DeleteOneAsync(FilterDefinition<TDb> filter);
        Task<IEnumerable<TDb>> FindAsync(Expression<Func<TDb, bool>> expression, FindOptions<TDb> findOptions = null);
        Task<IEnumerable<TDb>> FindAsync(FilterDefinition<TDb> filter, FindOptions<TDb> findOptions = null);
        TDb FindById(string id);
        Task<TDb> FindByIdAsync(string id);
        Task<TDb> FindOneAndUpdateAsync(Expression<Func<TDb, bool>> expression, UpdateDefinition<TDb> update);
        Task<TDb> FindOneAndUpdateAsync(FilterDefinition<TDb> filter, UpdateDefinition<TDb> update);
        Task<IEnumerable<TDb>> FindPageAsync(Expression<Func<TDb, bool>> expression, FindOptions<TDb> findOptions);
        Task<IEnumerable<TDb>> FindPageAsync(FilterDefinition<TDb> filter, FindOptions<TDb> findOptions);
        Task InsertManyAsync(IEnumerable<TDb> entities);
        void InsertOne(TDb entity);
        Task<long> UpdateManyAsync(Expression<Func<TDb, bool>> expression, UpdateDefinition<TDb> update);
        Task<long> UpdateManyAsync(FilterDefinition<TDb> filter, UpdateDefinition<TDb> update);
        Task UpdateOneAsync(Expression<Func<TDb, bool>> expression, UpdateDefinition<TDb> update);
        Task UpdateOneAsync(FilterDefinition<TDb> filter, UpdateDefinition<TDb> update);
    }
}