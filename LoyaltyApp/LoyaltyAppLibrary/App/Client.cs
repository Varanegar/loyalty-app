using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyAppLibrary.App
{
    public class Client
    {
        WebClient _webClient;
        public WebClient WebClient
        {
            get { return _webClient; }
        }
        SqliteClient _sqliteClient;
        public SqliteClient DbClient
        {
            get { return _sqliteClient; }
        }
        IFileClient _fileClient;
        public IFileClient FileClient
        {
            get { return _fileClient; }
        }
        static Client _instance;
        public static Client GetInstance()
        {
            if (_instance != null)
            {
                return _instance;
            }
            throw new NullReferenceException("CLiend is not initialized");
        }
        public static void Initialize(WebClient webClient, SqliteClient sqliteClient, IFileClient fileClient)
        {
            _instance = new Client(webClient, sqliteClient, fileClient);
        }
        private Client(WebClient webClient, SqliteClient sqliteClient, IFileClient fileClient)
        {
            _webClient = webClient;
            _sqliteClient = sqliteClient;
            _fileClient = fileClient;
        }
    }
}
