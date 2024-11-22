using Dominio;
using negocio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                
                datos.setearConsulta("select a.idArticulo, a.nombre, a.descripcion, a.precio, a.stock, a.categoria_id , i.url from Articulos a inner join imagenes i on a.idArticulo = i.idArticulo");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["idArticulo"];
                    aux.Nombre = (string)datos.Lector["nombre"];
                    aux.Descripcion = (string)datos.Lector["descripcion"];
                    aux.Precio = (decimal)datos.Lector["precio"];
                    aux.Stock = (int)datos.Lector["stock"];
                    aux.CategoriaId = (int)datos.Lector["categoria_id"];
                    aux.UrlImagen = (string)datos.Lector["url"];

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
        public List<Articulo> listarHome()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("SELECT TOP 3 A.idArticulo,A.NOMBRE, A.DESCRIPCION, A.PRECIO, A.STOCK, A.categoria_id, I.url FROM ARTICULOS A INNER JOIN Imagenes I ON A.idArticulo = I.idarticulo WHERE A.estado = 1 ORDER BY NEWID()");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["idArticulo"];
                    aux.Nombre = (string)datos.Lector["nombre"];
                    aux.Descripcion = (string)datos.Lector["descripcion"];
                    aux.Precio = (decimal)datos.Lector["precio"];
                    aux.Stock = (int)datos.Lector["stock"];
                    aux.CategoriaId = (int)datos.Lector["categoria_id"];
                    aux.UrlImagen = (string)datos.Lector["url"];

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
        public List<Articulo> listarConSP()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearProcedimiento("sp_ListarArticulos");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["idArticulo"];
                    aux.Nombre = (string)datos.Lector["nombre"];
                    aux.Descripcion = (string)datos.Lector["descripcion"];
                    aux.Precio = (decimal)datos.Lector["precio"];
                    aux.Stock = (int)datos.Lector["stock"];
                    aux.CategoriaId = (int)datos.Lector["categoria_id"];
                    aux.UrlImagen = (string)datos.Lector["url"];
                    aux.Activo = bool.Parse(datos.Lector["estado"].ToString());

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
        public List<Articulo> listarConSP(string sp, int categoriaID)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearProcedimiento(sp);
                datos.setearParametro("@categoria_id", categoriaID);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["idArticulo"];
                    aux.Nombre = (string)datos.Lector["nombre"];
                    aux.Descripcion = (string)datos.Lector["descripcion"];
                    aux.Precio = (decimal)datos.Lector["precio"];
                    aux.Stock = (int)datos.Lector["stock"];
                    aux.CategoriaId = (int)datos.Lector["categoria_id"];
                    aux.UrlImagen = (string)datos.Lector["url"];
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
        public void agregar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Articulos (nombre, descripcion, precio, stock, categoria_id) VALUES (@Nombre, @Descripcion, @Precio, @Stock, @Categoria)");
                datos.setearParametro("@Nombre", articulo.Nombre);
                datos.setearParametro("@Descripcion", articulo.Descripcion);
                datos.setearParametro("@Precio", articulo.Precio);
                datos.setearParametro("@Stock", articulo.Stock);
                datos.setearParametro("@Categoria", articulo.CategoriaId);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw new Exception("Error al agregar el artículo: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregarConSP(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_InsertarArticulo");
                datos.setearParametro("@Nombre", articulo.Nombre);
                datos.setearParametro("@Descripcion", articulo.Descripcion);
                datos.setearParametro("@Precio", articulo.Precio);
                datos.setearParametro("@Stock", articulo.Stock);
                datos.setearParametro("@Categoria", articulo.CategoriaId);
                datos.setearParametro("@UrlImagen", articulo.UrlImagen);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw new Exception("Error al agregar el artículo: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update articulos set nombre = @Nombre, descripcion = @Descripcion, precio = @Precio, stock = @Stock, categoria_id = @Categoria Where Idarticulo = @idarticulo");
                datos.setearParametro("@Nombre", articulo.Nombre);
                datos.setearParametro("@Descripcion", articulo.Descripcion);
                datos.setearParametro("@Precio", articulo.Precio);
                datos.setearParametro("@Stock", articulo.Stock);
                datos.setearParametro("@Categoria", articulo.CategoriaId);
                datos.setearParametro("@idarticulo", articulo.Id);
                datos.ejecutarAccion();

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
        public void modificarConSP(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_ModificarArticulo");
                datos.setearParametro("@Nombre", articulo.Nombre);
                datos.setearParametro("@Descripcion", articulo.Descripcion);
                datos.setearParametro("@Precio", articulo.Precio);
                datos.setearParametro("@Stock", articulo.Stock);
                datos.setearParametro("@Categoria", articulo.CategoriaId);
                datos.setearParametro("@UrlImagen", articulo.UrlImagen);
                datos.setearParametro("@Id", articulo.Id);
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
        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM Articulos WHERE idArticulo = @id");
                datos.setearParametro("@id", id);
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
                datos.setearProcedimiento("SP_BajaLogicaArticulo");
                datos.setearParametro("@idArticulo", id);
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
                datos.setearProcedimiento("SP_ReactivacionLogicaArticulo");
                datos.setearParametro("@idArticulo", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Articulo> listarPorCategoria(int idCategoria)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT a.Idarticulo, a.Nombre, a.Precio, i.url FROM Articulos A inner join imagenes I ON A.IdArticulo = I.IdArticulo WHERE Categoria_Id = @idCategoria");
                datos.setearParametro("@idCategoria", idCategoria);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo articulo = new Articulo
                    {
                        Id = (int)datos.Lector["Idarticulo"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Precio = (decimal)datos.Lector["Precio"],
                        UrlImagen = (string)datos.Lector["Url"]
                    };
                    lista.Add(articulo);
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
    }
}
