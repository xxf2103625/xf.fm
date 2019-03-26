using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Xf.FM.MongodbRepository
{
    public partial class MgRepository<TDb> where TDb : BaseDbEntity
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
            IAsyncCursor<TDb> asyncCursor =await _col.FindAsync(Builders<TDb>.Filter.Eq(m => m.Id, id));
            return await asyncCursor.FirstAsync();
        }

        public async Task<IEnumerable<TDb>> FindAsync(Expression<Func<TDb,bool>> expression)
        {
            IAsyncCursor<TDb> asyncCursor= await _col.FindAsync(expression);
            return asyncCursor.ToEnumerable();
        }
        public async Task<IEnumerable<TDb>> FindAsync(FilterDefinition<TDb> filter)
        {
            IAsyncCursor<TDb> asyncCursor = await _col.FindAsync(filter);
            return asyncCursor.ToEnumerable();
        }

        public async Task UpdateOneAsync(FilterDefinition<TDb> filter, UpdateDefinition<TDb> update)
        {
            UpdateResult updateResult= await _col.UpdateOneAsync(filter, update);
            if (!updateResult.IsModifiedCountAvailable || updateResult.ModifiedCount != 1)
            {
                throw new Exception($"{nameof(UpdateOneAsync)} is not done,DbEntity is {typeof(TDb).Name}");
            }
        }
    }
}
