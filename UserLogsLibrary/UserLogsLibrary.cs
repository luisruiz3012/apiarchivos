using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using UserLogsLibrary.Database;
using UserLogsLibrary.Models;

namespace UserLogsLibrary
{
    public class Methods
    {
        private readonly DB _db;

        public Methods()
        {
            _db = new DB();
        }

        public dynamic Get()
        {
            using(var conn = _db.GetConnection())
            {
                conn.Open();

                string query = "EXEC get_logs".Replace("%", "").Replace(" --", "").Replace("'", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                List<dynamic> logs = new List<dynamic>();

                while (reader.Read())
                {
                    dynamic log = new {
                        Id = (int)reader["log_id"],
                        Usuario = reader["usuario"].ToString(),
                        Archivo = reader["archivo"].ToString(),
                        Accion = reader["Accion"].ToString()
                    };

                    logs.Add(log);
                }

                if (logs.Count > 0)
                {
                    conn.Close();
                    return logs;
                }

                conn.Close();

                return null;
            }
        }

        public dynamic GetById(int id)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();

                string query = "EXEC get_log @Id".Replace("%", "").Replace(" --", "").Replace("'", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                List<dynamic> logs = new List<dynamic>();

                while (reader.Read())
                {
                    dynamic log = new
                    {
                        Id = (int)reader["log_id"],
                        Usuario = reader["usuario"].ToString(),
                        Archivo = reader["archivo"].ToString(),
                        Accion = reader["Accion"].ToString()
                    };

                    logs.Add(log);
                }

                if (logs.Count > 0)
                {
                    conn.Close();
                    return logs;
                }

                conn.Close();

                return null;
            }
        }

        public dynamic Create(UserLog log)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();

                string query = "INSERT INTO user_logs (usuario_id, archivo_id, accion) VALUES (@usuario_id, @archivo_id, @accion)".Replace("%", "").Replace(" --", "").Replace("'", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@usuario_id", log.Usuario_Id);
                cmd.Parameters.AddWithValue("@archivo_id", log.Archivo_Id);
                cmd.Parameters.AddWithValue("@accion", log.Accion);
                int affectedRows = cmd.ExecuteNonQuery();


                if (affectedRows > 0)
                {
                    conn.Close();
                    return new { message = "Log created successfully" };
                }

                conn.Close();
                return null;
            }
        }

        public dynamic Update(int id, UserLog log)
        {
            using(var conn =  _db.GetConnection())
            {
                conn.Open();

                string query = "SELECT id FROM user_logs AS l WITH(NOLOCK) WHERE id = @Id".Replace("%", "").Replace(" --", "").Replace("'", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    conn.Close();
                    return null;
                }

                conn.Close();
                conn.Open();

                query = "UPDATE user_logs SET usuario_id = @usuario_id, archivo_id = @archivo_id, accion = @accion WHERE id = @Id".Replace("%", "").Replace(" --", "").Replace("'", "");
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@usuario_id", log.Usuario_Id);
                cmd.Parameters.AddWithValue("@archivo_id", log.Archivo_Id);
                cmd.Parameters.AddWithValue("@accion", log.Accion);
                cmd.Parameters.AddWithValue("@Id", id);
                int affectedRows = cmd.ExecuteNonQuery();


                if (affectedRows > 0)
                {
                    conn.Close();
                    return new { message = "Log updated successfully" };
                }

                conn.Close();
                return null;
            }
        }

        public dynamic Delete(int id)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();

                string query = "SELECT id FROM user_logs AS l WITH(NOLOCK) WHERE id = @Id".Replace("%", "").Replace(" --", "").Replace("'", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    conn.Close();
                    return null;
                }

                conn.Close();
                conn.Open();

                query = "DELETE FROM user_logs WHERE id = @Id".Replace("%", "").Replace(" --", "").Replace("'", "");
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                int affectedRows = cmd.ExecuteNonQuery();


                if (affectedRows > 0)
                {
                    conn.Close();
                    return new { message = "Log updated successfully" };
                }

                conn.Close();
                return null;
            }
        }
    }
}
