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
    public partial class MgRepository<TDb> : IMgRepository<TDb> where TDb : BaseDbEntity
    {
        protected IMongoClient _client => _mgConfig.Client;
        protected IMongoDatabase _db => _client.GetDatabase(_mgConfig.GetDbName());
        IMgConfig _mgConfig;
        public MgRepository(IMgConfig mgConfig)
        {
            _mgConfig = mgConfig;
        }
        public IMongoQueryable<TDb> Query => Col.AsQueryable();
        public IMongoCollection<TDb> Col => _db.GetCollection<TDb>(typeof(TDb).Name);
        public TDb FindById(string id)
        {
            return Col.FindSync(Builders<TDb>.Filter.Eq(m => m.Id, id)).First();
        }
        public async Task<TDb> FindByIdAsync(string id)
        {
            IAsyncCursor<TDb> asyncCursor = await Col.FindAsync(Builders<TDb>.Filter.Eq(m => m.Id, id));
            return await asyncCursor.FirstAsync();
        }

        public async Task<IEnumerable<TDb>> FindAsync(Expression<Func<TDb, bool>> expression, FindOptions<TDb> findOptions = null)
        {
            IAsyncCursor<TDb> asyncCursor = await Col.FindAsync(expression, findOptions);
            return asyncCursor.ToEnumerable();
        }
        public async Task<IEnumerable<TDb>> FindAsync(FilterDefinition<TDb> filter, FindOptions<TDb> findOptions = null)
        {
            IAsyncCursor<TDb> asyncCursor = await Col.FindAsync(filter, findOptions);
            return asyncCursor.ToEnumerable();
        }

        public void InsertOne(TDb entity)
        {
            Col.InsertOne(entity);
        }
        public async Task InsertManyAsync(IEnumerable<TDb> entities)
        {
            await Col.InsertManyAsync(entities);
        }

        public async Task UpdateOneAsync(FilterDefinition<TDb> filter, UpdateDefinition<TDb> update)
        {
            UpdateResult updateResult = await Col.UpdateOneAsync(filter, update);
            if (!updateResult.IsModifiedCountAvailable || updateResult.ModifiedCount != 1)
            {
                throw new Exception($"{nameof(UpdateOneAsync)} is not done,DbEntity is {typeof(TDb).Name}");
            }
        }
        public async Task UpdateOneAsync(Expression<Func<TDb, bool>> expression, UpdateDefinition<TDb> update)
        {
            UpdateResult updateResult = await Col.UpdateOneAsync(expression, update);
            if (!updateResult.IsModifiedCountAvailable || updateResult.ModifiedCount != 1)
            {
                throw new Exception($"{nameof(UpdateOneAsync)} is not done,DbEntity is {typeof(TDb).Name}");
            }
        }
        public async Task<TDb> FindOneAndUpdateAsync(FilterDefinition<TDb> filter, UpdateDefinition<TDb> update)
        {
            return await Col.FindOneAndUpdateAsync(filter, update);
        }
        public async Task<TDb> FindOneAndUpdateAsync(Expression<Func<TDb, bool>> expression, UpdateDefinition<TDb> update)
        {
            return await Col.FindOneAndUpdateAsync(expression, update);
        }
        public async Task<long> UpdateManyAsync(FilterDefinition<TDb> filter, UpdateDefinition<TDb> update)
        {
            UpdateResult updateResult = await Col.UpdateManyAsync(filter, update);
            return updateResult.ModifiedCount;
        }
        public async Task<long> UpdateManyAsync(Expression<Func<TDb, bool>> expression, UpdateDefinition<TDb> update)
        {
            UpdateResult updateResult = await Col.UpdateManyAsync(expression, update);
            return updateResult.ModifiedCount;
        }

        public async Task DeleteOneAsync(FilterDefinition<TDb> filter)
        {
            DeleteResult result = await Col.DeleteOneAsync(filter);
            if (!result.IsAcknowledged || result.DeletedCount != 1)
            {
                throw new Exception($"{nameof(DeleteOneAsync)} is not done,DbEntity is {typeof(TDb).Name}");
            }
        }
        public async Task DeleteOneAsync(Expression<Func<TDb, bool>> expression)
        {
            DeleteResult result = await Col.DeleteOneAsync(expression);
            if (!result.IsAcknowledged || result.DeletedCount != 1)
            {
                throw new Exception($"{nameof(DeleteOneAsync)} is not done,DbEntity is {typeof(TDb).Name}");
            }
        }
        public async Task<long> DeleteManyAsync(FilterDefinition<TDb> filter)
        {
            DeleteResult result = await Col.DeleteManyAsync(filter);
            if (result.IsAcknowledged)
            {
                return result.DeletedCount;
            }
            throw new Exception($"{nameof(DeleteManyAsync)} is not done,DbEntity is {typeof(TDb).Name}");
        }
        public async Task<long> DeleteManyAsync(Expression<Func<TDb, bool>> expression)
        {
            DeleteResult result = await Col.DeleteManyAsync(expression);
            if (result.IsAcknowledged)
            {
                return result.DeletedCount;
            }
            throw new Exception($"{nameof(DeleteManyAsync)} is not done,DbEntity is {typeof(TDb).Name}");
        }

        public async Task<IEnumerable<TDb>> FindPageAsync(FilterDefinition<TDb> filter, FindOptions<TDb> findOptions)
        {
            IAsyncCursor<TDb> asyncCursor = await Col.FindAsync(filter, findOptions);
            return asyncCursor.ToEnumerable();
        }
        public async Task<IEnumerable<TDb>> FindPageAsync(Expression<Func<TDb, bool>> expression, FindOptions<TDb> findOptions)
        {
            IAsyncCursor<TDb> asyncCursor = await Col.FindAsync(expression, findOptions);
            return asyncCursor.ToEnumerable();
        }

        public async Task<long> CountDocumentsAsync(FilterDefinition<TDb> filter)
        {
            return await Col.CountDocumentsAsync(filter);
        }
        public async Task<long> CountDocumentsAsync(Expression<Func<TDb, bool>> expression)
        {
            return await Col.CountDocumentsAsync(expression);
        }
    }
}
