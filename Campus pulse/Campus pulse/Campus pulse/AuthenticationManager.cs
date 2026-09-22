using MySql.Data.MySqlClient;
using System;

namespace Campus_pulse
{
    public class AuthenticationManager
    {
        public SystemUser Login(string username, string password)
        {
            SystemUser user = null;

            using (MySqlConnection connection =
                   new MySqlConnection(Database.ConnectionString))
            {
                connection.Open();

                string query = @"
                    SELECT
                        u.UserID,
                        u.RoleID,
                        u.Username,
                        u.PasswordHash,
                        u.FullName,
                        u.IsActive,
                        r.RoleName
                    FROM tbl_SystemUser u
                    INNER JOIN tbl_Role r
                        ON u.RoleID = r.RoleID
                    WHERE u.Username = @Username
                    LIMIT 1;";

                using (MySqlCommand command =
                       new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new SystemUser
                            {
                                UserID = reader.GetInt32("UserID"),
                                RoleID = reader.GetInt32("RoleID"),
                                Username = reader.GetString("Username"),
                                PasswordHash = reader.GetString("PasswordHash"),
                                FullName = reader.GetString("FullName"),
                                IsActive = reader.GetBoolean("IsActive"),
                                RoleName = reader.GetString("RoleName")
                            };
                        }
                    }
                }
            }

            if (user == null)
                return null;

            if (!user.IsActive)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return user;
        }
    }
}