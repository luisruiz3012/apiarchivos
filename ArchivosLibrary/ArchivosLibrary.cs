using ArchivosLibrary.Database;
using ArchivosLibrary.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace ArchivosLibrary
{
    public class Methods
    {
        private readonly DB _db;
        public Methods()
        {
            _db  = new DB();
        }

        public dynamic Get()
        {
            using (var conn = _db.GetConnection() ) {
                conn.Open();

                string query = "SELECT id, nombre, origen, destino, disponible FROM archivos AS a WITH(NOLOCK) ".Replace("%", "").Replace(" --", "").Replace("'", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                List<Archivo> archivos = new List<Archivo>();

                while( reader.Read())
                {
                    if ((bool)reader["disponible"] == true)
                    {
                        Archivo archivo = new Archivo {
                            Id = (int)reader["Id"],
                            Nombre = reader["Nombre"].ToString(),
                            Origen = reader["Origen"].ToString(),
                            Destino = reader["Destino"].ToString(),
                            Disponible = (bool)reader["Disponible"]
                        };

                        archivos.Add(archivo);
                    }
                }

                if (archivos.Count > 0)
                {
                    conn.Close();
                    return archivos;
                }

                conn.Close();
                return null;
            }
        }

        public dynamic GetById(int id)
        {
            using(var conn = _db.GetConnection())
            {
                conn.Open();

                string query = "SELECT id, nombre, origen, destino, disponible FROM archivos AS a WITH(NOLOCK) WHERE id = @Id".Replace("%", "").Replace(" --", "").Replace("'", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    conn.Close();
                    return null;
                }

                Archivo archivo = new Archivo { 
                    Id = (int)reader["Id"],
                    Nombre = reader["Nombre"].ToString(),
                    Origen = reader["Origen"].ToString(),
                    Destino = reader["Destino"].ToString(),
                    Disponible = (bool)reader["Disponible"]
                };

                if (archivo.Disponible != true)
                {
                    conn.Close();
                    return null;
                }

                conn.Close();
                return archivo;
            }
        }

        public dynamic Create(Archivo archivo, int usuario_id)
        {
            using(var conn = _db.GetConnection())
            {
                conn.Open();

                string query = "EXEC upload_file @Nombre, @Origen, @Destino, @Usuario_id".Replace("%", "").Replace(" --", "").Replace("'", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", archivo.Nombre);
                cmd.Parameters.AddWithValue("@Origen", archivo.Origen);
                cmd.Parameters.AddWithValue("@Destino", archivo.Destino);
                cmd.Parameters.AddWithValue("@Usuario_id", usuario_id);
                int rowsAffected = cmd.ExecuteNonQuery();

                // If no rows were inserted, return null
                if (rowsAffected == 0)
                {
                    conn.Close();
                    return null;
                }

                conn.Close();
                return new { message = "File uploaded successfully" };
            }
        }

        public dynamic Update(int id, Archivo archivo) { 
            using(var conn = _db.GetConnection())
            {
                conn.Open();

                string query = "SELECT disponible FROM archivos AS a WITH(NOLOCK) WHERE id = @Id".Replace("%", "").Replace(" --", "").Replace("'", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read() || (bool)reader["Disponible"] == false)
                {
                    conn.Close();
                    return null;
                }

                conn.Close();
                conn.Open();

                query = "UPDATE archivos SET nombre = @Nombre, origen = @Origen, destino = @Destino, disponible = @Disponible WHERE id = @Id".Replace("%", "").Replace(" --", "").Replace("'", "");
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", archivo.Nombre);
                cmd.Parameters.AddWithValue("@Origen", archivo.Origen);
                cmd.Parameters.AddWithValue("@Destino", archivo.Destino);
                cmd.Parameters.AddWithValue("@Disponible", archivo.Disponible == true ? 1 : 0);
                cmd.Parameters.AddWithValue("@Id", id);
                int rowsAffected = cmd.ExecuteNonQuery();

                // If no rows were inserted, return null
                if (rowsAffected == 0)
                {
                    conn.Close();
                    return null;
                }

                conn.Close();
                return new { message = "Updated file successfully" };
            }
        }

        public dynamic Delete(int id, int usuario_id)
        {
            using(var conn = _db.GetConnection())
            {
                conn.Open();

                string query = "SELECT disponible FROM archivos AS a WITH(NOLOCK) WHERE id = @Id".Replace("%", "").Replace(" --", "").Replace("'", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read() || (bool)reader["Disponible"] == false)
                {
                    conn.Close();
                    return null;
                }

                conn.Close();
                conn.Open();

                query = "EXEC delete_file @Usuario_id, @Id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Usuario_id", usuario_id);
                cmd.Parameters.AddWithValue("@Id", id);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    conn.Close();
                    return null;
                }

                conn.Close();
                return new { message = "File deleted successfully" };
            }
        }

        public dynamic Restore(int id, int usuario_id)
        {
            using(var conn = _db.GetConnection())
            {
                conn.Open();

                string query = "SELECT disponible FROM archivos AS a WITH(NOLOCK) WHERE id = @Id ".Replace("%", "").Replace(" --", "").Replace("'", "");
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    return null;
                }

                conn.Close();
                conn.Open();

                query = "EXEC restore_file @User_id, @Id".Replace("%", "").Replace(" --", "").Replace("'", "");
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@User_id", usuario_id);
                cmd.Parameters.AddWithValue("@Id", id);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    conn.Close();
                    return null;
                }

                conn.Close();
                return new { message = "File restored successfully" };
            }
        }
    }
}
