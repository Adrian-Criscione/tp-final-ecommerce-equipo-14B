using Dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listar(bool soloHabilitadas = false)
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT idCategoria, nombre, estado FROM Categorias ";
                if (soloHabilitadas)
                {
                    consulta += " WHERE estado = 1";
                }
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)datos.Lector["idCategoria"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Activo = bool.Parse(datos.Lector["estado"].ToString());

                    lista.Add(aux);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
            return lista;
        }


        /*  public void verificarDuplicado(Categoria categoria)
          {
              AccesoDatos datos = new AccesoDatos();
              try
              {
                  datos.setearConsulta("SELECT COUNT(*) FROM Categorias WHERE nombre = @nombre");
                  datos.setearParametro("@nombre", categoria.Nombre);
                  int cantidad = (int)datos.ejecutarAccionScalar();

                  if (cantidad > 0)
                  {
                      throw new Exception($"Ya existe una CATEGORÍAS con nombre '{categoria.Nombre}' en la base de datos.");

                  }
              }
              catch (Exception)
              {

                  throw;
              }
              finally
              {
                  datos.cerrarConexion();
              }
          }*/

        public bool verificarDuplicado(Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Categorias WHERE nombre = @nombre");
                datos.setearParametro("@nombre", categoria.Nombre);
                int cantidad = (int)datos.ejecutarAccionScalar();

                return cantidad > 0; // Retorna true si hay duplicados
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar duplicados: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void agregar(Categoria categoria)
         {
             AccesoDatos datos = new AccesoDatos();

             try
             {

                 datos.setearConsulta("INSERT INTO Categorias (nombre) VALUES(@nombre)");
                 datos.setearParametro("@nombre", categoria.Nombre);
                 datos.ejecutarAccion();

             }
             catch (Exception ex)
             {

                 throw new Exception("Error al agregar la Categoría : " + ex.Message);
             }
             finally
             {
                 datos.cerrarConexion();
             }
         }
        
         public void eliminar(int id)
         {
             AccesoDatos datos = new AccesoDatos();
             try
             {
                 datos.setearConsulta(" DELETE FROM Categorias WHERE idCategoria = @id");
                 datos.setearParametro("id", id);
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

        public void bajaLogicaConSP(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearProcedimiento("SP_BajaLogicaCategoria");
                datos.setearParametro("@idCategoria", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void ReactivacionLogicaConSP(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearProcedimiento("SP_ReactivacionLogicaCategoria");
                datos.setearParametro("@idCategoria", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
         public void modificar(Categoria categoria)
         {
             AccesoDatos datos = new AccesoDatos();

             try
             {
                 datos.setearConsulta("UPDATE Categorias SET nombre = @nombre WHERE idCategoria = @id");
                 datos.setearParametro("@nombre", categoria.Nombre);
                 datos.setearParametro("@id", categoria.Id);
                 datos.ejecutarAccion();

             }
             catch (Exception ex)
             {

                 throw new Exception("Error al agregar la Categoría : " + ex.Message);
             }
             finally
             {
                 datos.cerrarConexion();
             }
         }

        public void modificarConSP(Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_ModificarCategoria");
                datos.setearParametro("@Nombre", categoria.Nombre);
                datos.setearParametro("@Id", categoria.Id);
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
    }
}

