using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDMysql
{
    public class DBUser
    {
        private string sql = "datasource=localhost;port=3306;username=root;password=;database=user";
        public User GetUserInfo(string username)
        {
            User user = null;
            using (MySqlConnection connection = new MySqlConnection(sql))
            {
                connection.Open();

                string sql = "SELECT * FROM member WHERE Username = @username";
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@username", username);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        user = new User
                        {
                            Id = reader.GetInt32(0),
                            Fullname = reader.GetString(1),
                            Username = reader.GetString(2),
                            Password = reader.GetString(3),
                            CreatedDate = reader.GetDateTime(4),
                            UpdatedDate = reader.GetDateTime(5)
                        };
                    }
                }
            }

            return user;
        }

        /*public string HashPassword(string password)
        {
            //install BCrypt.Net-Next in package
            return BCrypt.Net.BCrypt.HashPassword(password);
        }*/
        public bool InsertUser(User user)
        {
            bool success = false;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(sql))
                {
                    connection.Open();

                    string sql = "INSERT INTO member (name, username, password) VALUES (@fullname,@username, @password)";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@fullname", user.Fullname);
                    command.Parameters.AddWithValue("@username", user.Username);
                    command.Parameters.AddWithValue("@password", user.Password);
                    /*command.Parameters.AddWithValue("@password", HashPassword(user.Password));*/

                    int rowsAffected = command.ExecuteNonQuery();
                    success = rowsAffected > 0;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Registration no much.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return success;
        }

        /*public bool VerifyPassword(string username, string enteredPassword)
        {
            User user = GetUserInfo(username);

            if (user == null)
            {
                return false; // User not found
            }

            return BCrypt.Net.BCrypt.Verify(enteredPassword, user.Password);
        }*/
    }
}
