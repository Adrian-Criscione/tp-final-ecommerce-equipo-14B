using Dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class UsuarioNegocio
    {
        //Metodo para listar los usuarios para la tabla 
        public List<Usuario> listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select idUsuario, nombre, correo, clave, direccion, telefono, localidad, fecha_nacimiento, estado, IdRol from Usuarios");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();

                    aux.Id = (int)datos.Lector["idUsuario"];
                    aux.Nombre = (string)datos.Lector["nombre"];
                    aux.Correo = (string)datos.Lector["correo"];
                    aux.Clave = (string)datos.Lector["clave"];
                    aux.Direccion = (string)datos.Lector["direccion"];
                    aux.Telefono = (string)datos.Lector["telefono"];
                    aux.Localidad = (string)datos.Lector["localidad"];
                    aux.FechaNacimiento = (DateTime)datos.Lector["fecha_nacimiento"];
                    aux.Estado = (bool)datos.Lector["estado"];
                    aux.IdRol = (int)datos.Lector["IdRol"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool loguear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select idUsuario, correo, clave, IdRol from Usuarios where correo = @correo AND clave = @clave");
                datos.setearParametro("@correo", usuario.Correo);
                datos.setearParametro("@clave", usuario.Clave);
                datos.ejecutarLectura();

                //para ver si hay un usuario logeado
                if (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["idUsuario"];
                    usuario.IdRol = (int)datos.Lector["IdRol"];
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }
        public void agregar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Usuarios (nombre, correo, clave, direccion, telefono, localidad, fecha_nacimiento,idrol, estado) VALUES(@Nombre, @Correo, @Clave, @Direccion, @Telefono, @Localidad, @FechaNacimiento, @IdRol, 1)");
                datos.setearParametro("@Nombre", usuario.Nombre);
                datos.setearParametro("@Correo", usuario.Correo);
                datos.setearParametro("@Clave", usuario.Clave);
                datos.setearParametro("@Direccion", usuario.Direccion);
                datos.setearParametro("@Telefono", usuario.Telefono);
                datos.setearParametro("@Localidad", usuario.Localidad);
                datos.setearParametro("@FechaNacimiento", usuario.FechaNacimiento);
                datos.setearParametro("@IdRol", usuario.IdRol);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw new Exception("Error al agregar el usuario: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void modificar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Update usuarios set nombre = @Nombre, correo = @Correo, clave = @Clave, direccion = @Direccion, telefono = @Telefono, localidad = @Localidad, fecha_Nacimiento = @FechaNacimiento, idRol = @IdRol where idUsuario = @IdUsuario");
                datos.setearParametro("@Nombre", usuario.Nombre);
                datos.setearParametro("@Correo", usuario.Correo);
                datos.setearParametro("@Clave", usuario.Clave);
                datos.setearParametro("@Direccion", usuario.Direccion);
                datos.setearParametro("@Telefono", usuario.Telefono);
                datos.setearParametro("@Localidad", usuario.Localidad);
                datos.setearParametro("@FechaNacimiento", usuario.FechaNacimiento);
                datos.setearParametro("@IdRol", usuario.IdRol);
                datos.setearParametro("@IdUsuario", usuario.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }
        public void bajaLogica(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("update usuarios set estado = 0 where idUsuario = @IdUsuario");
                datos.setearParametro("@IdUsuario", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ReactivacionLogica(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("update usuarios set estado = 1 where idUsuario = @IdUsuario");
                datos.setearParametro("@IdUsuario", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExisteEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Usuarios WHERE correo = @Email AND estado = 1");
                datos.setearParametro("@Email", email);
                datos.ejecutarLectura();

                // Verificar si el correo existe
                if (datos.Lector.Read())
                {
                    int count = (int)datos.Lector[0];
                    return count > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar el correo electrónico: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public string RecuperarPass(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT clave FROM Usuarios WHERE correo = @Email AND estado = 1");
                datos.setearParametro("@Email", email);
                datos.ejecutarLectura();

                // Verificar si el correo existe y devolver la contraseña
                if (datos.Lector.Read())
                {
                    return datos.Lector["clave"].ToString(); // Devuelve la contraseña
                }
                return null; // Si no existe, retorna null
            }
            catch (Exception ex)
            {
                throw new Exception("Error al recuperar la contraseña: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Usuario BuscarUsuarioPorId(int Id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT idUsuario, nombre, correo, clave, direccion, telefono, localidad, fecha_nacimiento, estado, IdRol FROM Usuarios WHERE idUsuario = @IdUsuario");
                datos.setearParametro("@IdUsuario", Id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario
                    {
                        Id = (int)datos.Lector["idUsuario"],
                        Nombre = (string)datos.Lector["nombre"],
                        Correo = (string)datos.Lector["correo"],
                        Clave = (string)datos.Lector["clave"],
                        Direccion = (string)datos.Lector["direccion"],
                        Telefono = (string)datos.Lector["telefono"],
                        Localidad = (string)datos.Lector["localidad"],
                        FechaNacimiento = (DateTime)datos.Lector["fecha_nacimiento"],
                        Estado = (bool)datos.Lector["estado"],
                        IdRol = (int)datos.Lector["IdRol"]
                    };
                    return usuario;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar el usuario: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }

        }
    }

}
