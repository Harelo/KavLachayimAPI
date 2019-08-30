using System.Data.SQLite;

namespace KavLachayimAPI.Services
{
    public static class DBService
    {
        public static object ExecuteSQLScalar(string SQLstr, SQLiteConnection Connection)
        {
            SQLiteCommand command = new SQLiteCommand(SQLstr, Connection);
            Connection.Open();
            object result = command.ExecuteScalar();
            Connection.Close();
            return result;
        }
    }
}
