using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoyaltyAppLibrary.App;

namespace LoyaltyAppLibrary.FrameWork
{
    public abstract class BaseManager<DataModel>
        where DataModel : BaseModel, new()
    {
        int _index;
        int _limit = 10;
        string _query;
        public string Query
        {
            get
            { return _query; }
            set
            {
                _query = value;
                _index = 0;
            }
        }
        public abstract DataModel GetItem(Guid uniqueId);
        public virtual List<DataModel> GetNext()
        {
            var list = Client.GetInstance().DbClient.GetList<DataModel>(string.Format("{0} LIMIT {1},{2}", Query, _index, _limit));
            _index = list.Count + _index;
            return list;
        }
        public abstract int SaveItem(DataModel model);
        public int SaveItems(List<DataModel> models)
        {
            var db = Client.GetInstance().DbClient;
            db.BeginTransaction();
            int rows = 0;
            foreach (var model in models)
            {
                var r = SaveItem(model);
                rows += r;
                if (r == 0)
                {
                    db.RollbackTransaction();
                    return 0;
                }
            }
            db.CommitTransaction();
            return rows;
        }
    }
}
