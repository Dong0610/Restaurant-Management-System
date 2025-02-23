using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_System.Entity
{
    class Database
    {
        private static Database _instance;

        private static QuanlibanhangContext dbApp= null;

        public QuanlibanhangContext AppDatabase()
        {
            if (dbApp == null)
            {
                dbApp = new QuanlibanhangContext();
            }
            return dbApp;
        }

        public Dictionary<string, object> CachedData { get; private set; }
        public string CurrentUser { get; set; }
        private Database()
        {
            CachedData = new Dictionary<string, object>();
        }
        public static Database Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Database();
                }
                if (dbApp == null)
                {
                    dbApp = new QuanlibanhangContext();
                }
                return _instance;
            }
        }

        public Accounts Accounts { get; set; }
     

        public void SaveState(string key, object value)
        {
            if (CachedData.ContainsKey(key))
            {
                CachedData[key] = value;
            }
            else
            {
                CachedData.Add(key, value);
            }
        }
        public object GetState(string key)
        {
            return CachedData.TryGetValue(key, out var value) ? value : null;
        }


    }

}
