using MongoDB.Driver;
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
        protected IMongoClient _client;
        protected IMongoDatabase _db;
        public IMongoCollection<TDb> _col;
        public MgRepository(IMgConfig mgConfig)
        {
            _client = new MongoClient(mgConfig.GetMgConfig());
            _db = _client.GetDatabase(mgConfig.GetDbName());
            _col = _db.GetCollection<TDb>(typeof(TDb).Name);
        }
        public TDb FindById(string id)
        {
            return _col.FindSync(Builders<TDb>.Filter.Eq(m => m.Id, id)).First();
        }
        public async Task<TDb> FindByIdAsync(string id)
        {
            IAsyncCursor<TDb> asyncCursor = await _col.FindAsync(Builders<TDb>.Filter.Eq(m => m.Id, id));
            return await asyncCursor.FirstAsync();
        }

        public async Task<IEnumerable<TDb>> FindAsync(Expression<Func<TDb, bool>> expression, FindOptions<TDb> findOptions = null)
        {
            IAsyncCursor<TDb> asyncCursor = await _col.FindAsync(expression, findOptions);
            return asyncCursor.ToEnumerable();
        }
        public async Task<IEnumerable<TDb>> FindAsync(FilterDefinition<TDb> filter, FindOptions<TDb> findOptions = null)
        {
            IAsyncCursor<TDb> asyncCursor = await _col.FindAsync(filter,findOptions);
            return asyncCursor.ToEnumerable();
        }

        public void InsertOne(TDb entity)
        {
            _col.InsertOne(entity);
        }
        public async Task InsertManyAsync(IEnumerable<TDb> entities)
        {
            await _col.InsertManyAsync(entities);
        }

        public async Task UpdateOneAsync(FilterDefinition<TDb> filter, UpdateDefinition<TDb> update)
        {
            UpdateResult updateResult = await _col.UpdateOneAsync(filter, update);
            if (!updateResult.IsModifiedCountAvailable || updateResult.ModifiedCount != 1)
            {
                throw new Exception($"{nameof(UpdateOneAsync)} is not done,DbEntity is {typeof(TDb).Name}");
            }
        }
        public async Task UpdateOneAsync(Expression<Func<TDb, bool>> expression, UpdateDefinition<TDb> update)
        {
            UpdateResult updateResult = await _col.UpdateOneAsync(expression, update);
            if (!updateResult.IsModifiedCountAvailable || updateResult.ModifiedCount != 1)
            {
                throw new Exception($"{nameof(UpdateOneAsync)} is not done,DbEntity is {typeof(TDb).Name}");
            }
        }
        public async Task<TDb> FindOneAndUpdateAsync(FilterDefinition<TDb> filter, UpdateDefinition<TDb> update)
        {
            return await _col.FindOneAndUpdateAsync(filter, update);
        }
        public async Task<TDb> FindOneAndUpdateAsync(Expression<Func<TDb, bool>> expression, UpdateDefinition<TDb> update)
        {
            return await _col.FindOneAndUpdateAsync(expression, update);
        }
        public async Task<long> UpdateManyAsync(FilterDefinition<TDb> filter, UpdateDefinition<TDb> update)
        {
            UpdateResult updateResult = await _col.UpdateManyAsync(filter, update);
            return updateResult.ModifiedCount;
        }
        public async Task<long> UpdateManyAsync(Expression<Func<TDb, bool>> expression, UpdateDefinition<TDb> update)
        {
            UpdateResult updateResult = await _col.UpdateManyAsync(expression, update);
            return updateResult.ModifiedCount;
        }

        public async Task DeleteOneAsync(FilterDefinition<TDb> filter)
        {
            DeleteResult result = await _col.DeleteOneAsync(filter);
            if (!result.IsAcknowledged || result.DeletedCount != 1)
            {
                throw new Exception($"{nameof(DeleteOneAsync)} is not done,DbEntity is {typeof(TDb).Name}");
            }
        }
        public async Task DeleteOneAsync(Expression<Func<TDb, bool>> expression)
        {
            DeleteResult result = await _col.DeleteOneAsync(expression);
            if (!result.IsAcknowledged || result.DeletedCount != 1)
            {
                throw new Exception($"{nameof(DeleteOneAsync)} is not done,DbEntity is {typeof(TDb).Name}");
            }
        }
        public async Task<long> DeleteManyAsync(FilterDefinition<TDb> filter)
        {
            DeleteResult result = await _col.DeleteManyAsync(filter);
            if (result.IsAcknowledged)
            {
                return result.DeletedCount;
            }
            throw new Exception($"{nameof(DeleteManyAsync)} is not done,DbEntity is {typeof(TDb).Name}");
        }
        public async Task<long> DeleteManyAsync(Expression<Func<TDb, bool>> expression)
        {
            DeleteResult result = await _col.DeleteManyAsync(expression);
            if (result.IsAcknowledged)
            {
                return result.DeletedCount;
            }
            throw new Exception($"{nameof(DeleteManyAsync)} is not done,DbEntity is {typeof(TDb).Name}");
        }

        public async Task<IEnumerable<TDb>> FindPageAsync(FilterDefinition<TDb> filter, FindOptions<TDb> findOptions)
        {
            IAsyncCursor<TDb> asyncCursor = await _col.FindAsync(filter, findOptions);
            return asyncCursor.ToEnumerable();
        }
        public async Task<IEnumerable<TDb>> FindPageAsync(Expression<Func<TDb, bool>> expression, FindOptions<TDb> findOptions)
        {
            IAsyncCursor<TDb> asyncCursor = await _col.FindAsync(expression, findOptions);
            return asyncCursor.ToEnumerable();
        }

        public async Task<long> CountDocumentsAsync(FilterDefinition<TDb> filter)
        {
            return await _col.CountDocumentsAsync(filter);
        }
        public async Task<long> CountDocumentsAsync(Expression<Func<TDb, bool>> expression)
        {
            return await _col.CountDocumentsAsync(expression);
        }
    }
}
