using MySql.Data.MySqlClient;

namespace Campus_pulse
{
    public class Database
    {
        public static string ConnectionString =
            "Server=localhost;" +
            "Port=3306;" +
            "Database=healthsystemdb;" +
            "Uid=root;" +
            "Pwd=nibbles123;";
    }
}