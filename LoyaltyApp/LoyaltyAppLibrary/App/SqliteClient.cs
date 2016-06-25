using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using LoyaltyAppLibrary.FrameWork;

namespace LoyaltyAppLibrary.App
{
    public abstract class SqliteClient
    {
        public abstract void Upgrade(int currentVersion, int ollVersion);
        public abstract void Create();
        public abstract void BeginTransaction();
        public abstract void CommitTransaction();
        public abstract void RollbackTransactionTo(string savePoint);
        public abstract void RollbackTransaction();
        public abstract string SaveTransactionPoint();
        public abstract SQLiteConnection GetConnection();
        public async Task<List<DataModel>> GetListAsync<DataModel>(string query)
            where DataModel : BaseModel
        {
            return await Task.Run(() =>
            {
                try
                {
                    var connection = GetConnection();
                    var command = connection.CreateCommand(query);
                    lock (connection)
                    {
                        var result = command.ExecuteQuery<DataModel>();
                        return result;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            });

        }
        public List<DataModel> GetList<DataModel>(string query)
            where DataModel : BaseModel
        {
            try
            {
                var connection = GetConnection();
                var command = connection.CreateCommand(query);
                lock (connection)
                {
                    var result = command.ExecuteQuery<DataModel>();
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<DataModel> GetItemAsync<DataModel>(string query)
            where DataModel : BaseModel
        {
            return await Task.Run((Func<DataModel>)(() =>
            {
                try
                {
                    var connection = GetConnection();
                    var command = connection.CreateCommand(query);
                    List<DataModel> qResult;
                    lock (connection)
                    {
                        qResult = command.ExecuteQuery<DataModel>();
                    }
                    if (qResult.Count > 0)
                    {
                        return qResult.First();
                    }
                    return null;
                }
                catch (Exception)
                {
                    throw;
                }

            }));
        }
        public DataModel GetItem<DataModel>(string query)
            where DataModel : BaseModel
        {
            try
            {
                var connection = GetConnection();
                var command = connection.CreateCommand(query);
                List<DataModel> qResult;
                lock (connection)
                {
                    qResult = command.ExecuteQuery<DataModel>();
                }
                if (qResult.Count > 0)
                {
                    return qResult.First();
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateItemAsync(string query)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var connection = GetConnection();
                    var command = connection.CreateCommand(query);
                    lock (connection)
                    {
                        var qResult = command.ExecuteNonQuery();
                        return qResult;
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            });
        }
        public int UpdateItem(string query)
        {
            try
            {
                var connection = GetConnection();
                var command = connection.CreateCommand(query);
                lock (connection)
                {
                    var qResult = command.ExecuteNonQuery();
                    return qResult;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
