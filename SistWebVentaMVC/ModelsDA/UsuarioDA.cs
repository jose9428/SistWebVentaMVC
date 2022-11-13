using System.Data;
using System.Data.SqlClient;
using SistWebVentaMVC.Models;
using SistWebVentaMVC.Utils;

namespace SistWebVentaMVC.ModelsDA
{
    public class UsuarioDA
    {
        string strConn = Config.Get("ConnectionStrings:cadena");
     
        public List<Usuario> ListarTodos()
        {
            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select id_user,nombres,apellidos,correo,fechaNac," +
                        " sueldo,genero,r.id_rol,nombre_rol,estado from Usuario u inner join rol r on r.id_rol = u.id_rol", conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        lista.Add(new Usuario()
                        {
                            idUser = dr.GetInt32(0),
                            nombres = dr.GetString(1),
                            apellidos = dr.GetString(2),
                            correo = dr.GetString(3),
                            fechaNac = dr.GetDateTime(4),
                            sueldo = dr.GetDecimal(5),
                            genero = dr.GetString(6),
                            id_rol = dr.GetInt32(7),
                            nombre_rol = dr.GetString(8),
                            estado = dr.GetInt32(9)
                        });
                    }
                }
                catch (Exception ex) { 
                    throw new Exception(ex.Message, ex); 
                }
                finally
                {
                    conn.Close();
                }
            }
            return lista;
        }
        public Usuario BuscarPorId(int id)
        {
            Usuario obj = null;
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select id_user,nombres,apellidos,correo,fechaNac," +
                      " sueldo,genero,r.id_rol,nombre_rol,estado from Usuario u inner join rol r on r.id_rol = u.id_rol" +
                      "  where id_user = @id", conn);

                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        obj = new Usuario()
                        {
                            idUser = dr.GetInt32(0),
                            nombres = dr.GetString(1),
                            apellidos = dr.GetString(2),
                            correo = dr.GetString(3),
                            fechaNac = dr.GetDateTime(4),
                            sueldo = dr.GetDecimal(5),
                            genero = dr.GetString(6),
                            id_rol = dr.GetInt32(7),
                            nombre_rol = dr.GetString(8),
                            estado = dr.GetInt32(9)
                        };
                    }
                }
                catch (Exception ex) { }
                finally
                {
                    conn.Close();
                }
            }
            return obj;
        }
        public string Agregar(Usuario obj)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(strConn))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("insert into Usuario(nombres,apellidos,correo,fechaNac,sueldo) " +
                        " values(@nombres,@apellidos,@correo,@fechaNac,@sueldo)", cn);
                    cmd.Parameters.AddWithValue("@nombres", obj.nombres);
                    cmd.Parameters.AddWithValue("@apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("@correo", obj.correo);
                    cmd.Parameters.AddWithValue("@fechaNac", obj.fechaNac);
                    cmd.Parameters.AddWithValue("@sueldo", obj.sueldo);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        mensaje = "OK";
                    }
                    else
                    {
                        mensaje = "Lo sentimos no se pudieron guardaron datos.";
                    }

                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally
                {
                    cn.Close();
                }
            }
            return mensaje;
        }

        public string Editar(Usuario obj)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(strConn))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("update Usuario set nombres=@nombres,apellidos=@apellidos," +
                        " correo=@correo,fechaNac=@fechaNac,sueldo=@sueldo where idUser=@id", cn);
                    cmd.Parameters.AddWithValue("@nombres", obj.nombres);
                    cmd.Parameters.AddWithValue("@apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("@correo", obj.correo);
                    cmd.Parameters.AddWithValue("@fechaNac", obj.fechaNac);
                    cmd.Parameters.AddWithValue("@sueldo", obj.sueldo);
                    cmd.Parameters.AddWithValue("@id", obj.idUser);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        mensaje = "OK";
                    }
                    else
                    {
                        mensaje = "Lo sentimos no se pudieron editar datos.";
                    }

                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally
                {
                    cn.Close();
                }
            }
            return mensaje;
        }

        public string Eliminar(int id)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(strConn))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("delete from Usuario where idUser = @id", cn);
                    cmd.Parameters.AddWithValue("@id", id);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        mensaje = "OK";
                    }
                    else
                    {
                        mensaje = "Lo sentimos no se pudieron eliminar datos.";
                    }
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally
                {
                    cn.Close();
                }
            }
            return mensaje;
        }

        

        public int ExisteCorreo(int id , string correo)
        {
            int existe = 0;
            using (SqlConnection cn = new SqlConnection(strConn))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("sp_existeCorreo", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@correo", correo.Trim());
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        existe = dr.GetInt32(0);
                    }
                }
                catch (Exception ex) { }
                finally
                {
                    cn.Close();
                }
            }
            return existe;
        }

        public string Mantenimiento(Usuario obj, int opc)
        {
            string msg = "";
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_mant_usuarios", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nId_user", obj.idUser);
                    cmd.Parameters.AddWithValue("@cNombres", obj.nombres.Trim());
                    cmd.Parameters.AddWithValue("@cApellidos", obj.apellidos.Trim());
                    cmd.Parameters.AddWithValue("@cCorreo", obj.correo.Trim());
                    cmd.Parameters.AddWithValue("@dFechaNac", obj.fechaNac);
                    cmd.Parameters.AddWithValue("@nSueldo", obj.sueldo);
                    cmd.Parameters.AddWithValue("@cGenero", obj.genero);
                    cmd.Parameters.AddWithValue("@nId_rol", obj.id_rol);
                    cmd.Parameters.AddWithValue("@nEstado", obj.estado);
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

        public Usuario Autentificar(Usuario objUser)
        {
            Usuario obj = null;
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select id_user,nombres,apellidos,correo,fechaNac," +
                      " sueldo,genero,r.id_rol,nombre_rol,estado from Usuario u inner join rol r on r.id_rol = u.id_rol" +
                      "  where correo = @correo", conn);

                    cmd.Parameters.AddWithValue("@correo", objUser.correo);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        obj = new Usuario()
                        {
                            idUser = dr.GetInt32(0),
                            nombres = dr.GetString(1),
                            apellidos = dr.GetString(2),
                            correo = dr.GetString(3),
                            fechaNac = dr.GetDateTime(4),
                            sueldo = dr.GetDecimal(5),
                            genero = dr.GetString(6),
                            id_rol = dr.GetInt32(7),
                            nombre_rol = dr.GetString(8),
                            estado = dr.GetInt32(9)
                        };
                    }
                }
                catch (Exception ex) { }
                finally
                {
                    conn.Close();
                }
            }
            return obj;
        }
    }
}
