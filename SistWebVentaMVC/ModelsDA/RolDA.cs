using System.Data.SqlClient;
using SistWebVentaMVC.Models;
using SistWebVentaMVC.Utils;

namespace SistWebVentaMVC.ModelsDA
{
    public class RolDA
    {
        string strConn = Config.Get("ConnectionStrings:cadena");

        public List<Rol> ListarTodos()
        {
            List<Rol> lista = new List<Rol>();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select id_rol , nombre_rol from Rol", conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        lista.Add(new Usuario()
                        {
                            id_rol = (int)dr["id_rol"],
                            nombre_rol = (string)dr["nombre_rol"]
                        });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    conn.Close();
                }
            }
            return lista;
        }
    }
}
