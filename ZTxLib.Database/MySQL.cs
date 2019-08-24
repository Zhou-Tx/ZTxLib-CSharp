using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ZTxLib.Database
{
    public class MySQL : IDatabase
    {
        internal class MySqlReader : IReader
        {
            private readonly MySqlDataReader reader;
            public MySqlReader(MySqlDataReader reader) => this.reader = reader;
            public bool Read() => reader.Read();
            public object this[int i] => reader[i];
            public object this[string s] => reader[s];
        }

        private readonly List<string> sqlList = new List<string>();
        private readonly MySqlConnection conn;

        public MySQL(
            string server = "127.0.0.1",
            short port = 3306,
            string database = "",
            string user = "",
            string password = "",
            string charset = "utf8",
            short timeout = 5) => conn =
            new MySqlConnection(
                $"server={server};" +
                $"port={port};" +
                $"database={database};" +
                $"user={user};" +
                $"password={password};" +
                $"charset={charset};" +
                $"connect Timeout={timeout}"
            );

        public void Close()
        {
            try { conn.Close(); } catch { }
        }

        public void Add(string sql, params object[] args) => sqlList.Add(string.Format(sql, args));

        public bool Execute()
        {
            Close();
            conn.Open();
            MySqlTransaction trans = conn.BeginTransaction();
            try
            {
                foreach (string sql in sqlList)
                    new MySqlCommand(sql, conn, trans).ExecuteNonQuery();
                trans.Commit();
                return true;
            }
            catch
            {
                trans.Rollback();
                return false;
            }
            finally
            {
                conn.Close();
                sqlList.Clear();
            }
        }

        public IReader Execute(string sql, params object[] args)
        {
            Close();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(string.Format(sql, args), conn);

            return new MySqlReader(cmd.ExecuteReader());
        }
    }
}
