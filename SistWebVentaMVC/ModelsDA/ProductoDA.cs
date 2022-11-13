using SistWebVentaMVC.Models;
using SistWebVentaMVC.Utils;
using System.Data;
using System.Data.SqlClient;

namespace SistWebVentaMVC.ModelsDA
{
    public class ProductoDA
    {
        string strConn = Config.Get("ConnectionStrings:cadena");

        public List<Producto> ListarTodos()
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select id_prod,nombre,precio,stock,estado,imagen" +
                        " from Producto", conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        lista.Add(new Producto()
                        {
                            id_prod = dr.GetInt32(0),
                            nombre = dr.GetString(1),
                            precio = dr.GetDecimal(2),
                            stock = dr.GetInt32(3),
                            estado = dr.GetInt32(4),
                            imagen = dr.GetString(5)
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

        public Producto BuscarPorId(int id)
        {
            Producto obj = null;
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select id_prod,nombre,precio,stock,estado,imagen" +
                        " from Producto where id_prod = @id", conn);
                    cmd.Parameters.AddWithValue("@id" , id);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        obj = new Producto();
                        obj.id_prod = dr.GetInt32(0);
                        obj.nombre = dr.GetString(1);
                        obj.precio = dr.GetDecimal(2);
                        obj.stock = dr.GetInt32(3);
                        obj.estado = dr.GetInt32(4);
;                       obj.imagen = dr.GetString(5);
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
            return obj;
        }

        public string Mantenimiento(Producto obj, int opc)
        {
            string msg = "";
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_mant_productos", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nId_prod", obj.id_prod);
                    cmd.Parameters.AddWithValue("@cNombre", obj.nombre.Trim());
                    cmd.Parameters.AddWithValue("@nPrecio", obj.precio);
                    cmd.Parameters.AddWithValue("@nStock", obj.stock);
                    cmd.Parameters.AddWithValue("@nEstado", obj.estado);
                    cmd.Parameters.AddWithValue("@cImagen", obj.imagen == null ? "": obj.imagen);
                    cmd.Parameters.AddWithValue("@nOpc", opc);

                    msg = cmd.ExecuteNonQuery() > 0 ? "OK" : "";
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
            return msg;
        }

        public int MaxId()
        {
            int res = 0;
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select ISNULL(max(id_prod),0)+1 from Producto", conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        res = dr.GetInt32(0);
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
            return res;
        }
    }
}
