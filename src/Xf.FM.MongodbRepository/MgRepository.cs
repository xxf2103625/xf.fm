using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Xf.FM.MongodbRepository
{
    public partial class MgRepository<TEntity> : IMgRepository<TEntity> where TEntity : BaseMgDbEntity
    {
        protected IMongoClient _client => _mgConfig.Client;
        protected IMongoDatabase _db => _client.GetDatabase(_mgConfig.GetDbName());
        IMgConfig _mgConfig;
        public MgRepository(IMgConfig mgConfig)
        {
            _mgConfig = mgConfig;
        }
        public IMongoQueryable<TEntity> Query => Col.AsQueryable();
        public IMongoCollection<TEntity> Col => _db.GetCollection<TEntity>(typeof(TEntity).Name);
        public TEntity FindById(string id)
        {
            return Col.FindSync(Builders<TEntity>.Filter.Eq(m => m.Id, id)).First();
        }
        public async Task<TEntity> FindByIdAsync(string id)
        {
            IAsyncCursor<TEntity> asyncCursor = await Col.FindAsync(Builders<TEntity>.Filter.Eq(m => m.Id, id));
            return await asyncCursor.FirstAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression, FindOptions<TEntity> findOptions = null)
        {
            IAsyncCursor<TEntity> asyncCursor = await Col.FindAsync(expression, findOptions);
            return asyncCursor.ToEnumerable();
        }
        public async Task<IEnumerable<TEntity>> FindAsync(FilterDefinition<TEntity> filter, FindOptions<TEntity> findOptions = null)
        {
            IAsyncCursor<TEntity> asyncCursor = await Col.FindAsync(filter, findOptions);
            return asyncCursor.ToEnumerable();
        }

        public void InsertOne(TEntity entity)
        {
            Col.InsertOne(entity);
        }
        public async Task InsertManyAsync(IEnumerable<TEntity> entities)
        {
            await Col.InsertManyAsync(entities);
        }

        public async Task UpdateOneAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            UpdateResult updateResult = await Col.UpdateOneAsync(filter, update);
            if (!updateResult.IsModifiedCountAvailable || updateResult.ModifiedCount != 1)
            {
                throw new Exception($"{nameof(UpdateOneAsync)} is not done,DbEntity is {typeof(TEntity).Name}");
            }
        }
        public async Task UpdateOneAsync(Expression<Func<TEntity, bool>> expression, UpdateDefinition<TEntity> update)
        {
            UpdateResult updateResult = await Col.UpdateOneAsync(expression, update);
            if (!updateResult.IsModifiedCountAvailable || updateResult.ModifiedCount != 1)
            {
                throw new Exception($"{nameof(UpdateOneAsync)} is not done,DbEntity is {typeof(TEntity).Name}");
            }
        }
        public async Task<TEntity> FindOneAndUpdateAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            return await Col.FindOneAndUpdateAsync(filter, update);
        }
        public async Task<TEntity> FindOneAndUpdateAsync(Expression<Func<TEntity, bool>> expression, UpdateDefinition<TEntity> update)
        {
            return await Col.FindOneAndUpdateAsync(expression, update);
        }
        public async Task<long> UpdateManyAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            UpdateResult updateResult = await Col.UpdateManyAsync(filter, update);
            return updateResult.ModifiedCount;
        }
        public async Task<long> UpdateManyAsync(Expression<Func<TEntity, bool>> expression, UpdateDefinition<TEntity> update)
        {
            UpdateResult updateResult = await Col.UpdateManyAsync(expression, update);
            return updateResult.ModifiedCount;
        }

        public async Task<TEntity> FindOneAndReplaceAsync(FilterDefinition<TEntity> filter,TEntity entity)
        {
            return await Col.FindOneAndReplaceAsync(filter, entity);
        }
        public async Task<TEntity> FindOneAndReplaceAsync(Expression<Func<TEntity, bool>> expression, TEntity entity)
        {
            return await Col.FindOneAndReplaceAsync(expression, entity);
        }

        public async Task DeleteOneAsync(FilterDefinition<TEntity> filter)
        {
            DeleteResult result = await Col.DeleteOneAsync(filter);
            if (!result.IsAcknowledged || result.DeletedCount != 1)
            {
                throw new Exception($"{nameof(DeleteOneAsync)} is not done,DbEntity is {typeof(TEntity).Name}");
            }
        }
        public async Task DeleteOneAsync(Expression<Func<TEntity, bool>> expression)
        {
            DeleteResult result = await Col.DeleteOneAsync(expression);
            if (!result.IsAcknowledged || result.DeletedCount != 1)
            {
                throw new Exception($"{nameof(DeleteOneAsync)} is not done,DbEntity is {typeof(TEntity).Name}");
            }
        }
        public async Task<long> DeleteManyAsync(FilterDefinition<TEntity> filter)
        {
            DeleteResult result = await Col.DeleteManyAsync(filter);
            if (result.IsAcknowledged)
            {
                return result.DeletedCount;
            }
            throw new Exception($"{nameof(DeleteManyAsync)} is not done,DbEntity is {typeof(TEntity).Name}");
        }
        public async Task<long> DeleteManyAsync(Expression<Func<TEntity, bool>> expression)
        {
            DeleteResult result = await Col.DeleteManyAsync(expression);
            if (result.IsAcknowledged)
            {
                return result.DeletedCount;
            }
            throw new Exception($"{nameof(DeleteManyAsync)} is not done,DbEntity is {typeof(TEntity).Name}");
        }

        public async Task<IEnumerable<TEntity>> FindPageAsync(FilterDefinition<TEntity> filter, FindOptions<TEntity> findOptions)
        {
            IAsyncCursor<TEntity> asyncCursor = await Col.FindAsync(filter, findOptions);
            return asyncCursor.ToEnumerable();
        }
        public async Task<IEnumerable<TEntity>> FindPageAsync(Expression<Func<TEntity, bool>> expression, FindOptions<TEntity> findOptions)
        {
            IAsyncCursor<TEntity> asyncCursor = await Col.FindAsync(expression, findOptions);
            return asyncCursor.ToEnumerable();
        }

        public async Task<long> CountDocumentsAsync(FilterDefinition<TEntity> filter)
        {
            return await Col.CountDocumentsAsync(filter);
        }
        public async Task<long> CountDocumentsAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Col.CountDocumentsAsync(expression);
        }
    }
}
