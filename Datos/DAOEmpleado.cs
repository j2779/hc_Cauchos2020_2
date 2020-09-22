using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using Utilitarios;

namespace Datos
{
    public class DAOEmpleado
    {
        //METODO PARA VEIRIFICAR EL USUARIO ACTIVO
        public UEncapUsuario UsuarioActivo(string session)
        {
            using (var db = new Mapeo())
            {
                return db.usuario.Where(x => x.Sesion == session).FirstOrDefault();
            }
        }
        //METODO PARA VERIFICAR SI EXISTE REGISTRADO EL CORREO
        public UEncapUsuario verificarCorreo(UEncapUsuario verificar)
        {
            using (var db = new Mapeo())
            {
                return db.usuario.Where(x => x.Correo.Equals(verificar.Correo)).FirstOrDefault();
            }
        }
        //METODO PARA ACTUALIZAR EL USUARIO
        public void ActualizarUsuario(UEncapUsuario user)
        {
            using (var db = new Mapeo())
            {

                var resultado = db.usuario.SingleOrDefault(b => b.User_id == user.User_id);
                if (resultado != null)
                {
                    resultado.Nombre = user.Nombre;
                    resultado.Apellido = user.Apellido;
                    resultado.Correo = user.Correo;
                    resultado.Clave = user.Clave;
                    resultado.Fecha_nacimiento = user.Fecha_nacimiento;
                    resultado.Identificacion = user.Identificacion;
                    resultado.Token = user.Token;
                    resultado.Estado_id = user.Estado_id;
                    resultado.Rol_id = user.Rol_id;
                    resultado.Tiempo_token = user.Tiempo_token;
                    resultado.Sesion = user.Sesion;
                    resultado.Last_modify = DateTime.Now;
                    resultado.Ip_ = user.Ip_;
                    resultado.Mac_ = user.Mac_;

                    db.SaveChanges();
                }

            }

        }
        //OBTENGO CANTIDAD ACTUAL DEL INVENTARIO Y LE RESTO LA CANTIDAD QUE SE ENCUENTRA EN EL CARRITO
        public int ObtenerCantidadxProductoCarrito(int producto_id)
        {
            using (var db = new Mapeo())
            {
                //return db.inventario.Where(x => x.Id == producto_id).Select(x=> x.Ca_actual).First() - db.carrito.Where(x => x.Prod ucto_id == producto_id).Sum(x => x.Cantidad);
                Nullable<int> carrito = db.carrito.Where(x => x.Producto_id == producto_id).Sum(x => x.Cantidad);
                int cantidad = db.inventario.Where(x => x.Id == producto_id).Select(x => x.Ca_actual).First();
                return cantidad - (carrito != null ? carrito.Value : 0);
            }
        }
        //METODO PARA VERIFICAR SI EL USUARIO TIENE PEDIDO
        public UEncapCarrito verificarUsuarioTienePedido(UEncapCarrito verificar)
        {
            using (var db = new Mapeo())
            {
                return db.carrito.Where(x => x.User_id.Equals(verificar.User_id) && x.Estadocar.Equals(2)).FirstOrDefault();
            }
        }
        //METODO PARA VERIFICAR SI EL ITEM SELECCIONADO YA ESTA EN LA LISTA 
        public UEncapCarrito verificarArticuloEnCarrito(UEncapCarrito verificar)
        {
            using (var db = new Mapeo())
            {
                return db.carrito.Where(x => x.Producto_id.Equals(verificar.Producto_id) && x.User_id.Equals(verificar.User_id)).FirstOrDefault();
            }
        }
        //METODO ACTUALIZAR + ITEMS EN CARRITO 
        public void ActualizarCarritoItems(UEncapCarrito carrito)
        {
            using (var db = new Mapeo())
            {
                var resultado = db.carrito.FirstOrDefault(x => x.Producto_id == carrito.Producto_id && x.User_id == carrito.User_id);
                if (resultado != null)
                {
                    resultado.Id_Carrito = resultado.Id_Carrito;
                    resultado.User_id = resultado.User_id;
                    resultado.Producto_id = resultado.Producto_id;
                    resultado.Cantidad = resultado.Cantidad + carrito.Cantidad;
                    resultado.Fecha = resultado.Fecha;
                    resultado.Precio = resultado.Precio;
                    resultado.Total = resultado.Total + (carrito.Cantidad * resultado.Precio).Value;
                    resultado.Session = resultado.Session;
                    resultado.Last_modify = DateTime.Now;
                    db.SaveChanges();
                }
            }

        }
        //METODO PARA INSERTAR carrrito
        public void InsertarCarrito(UEncapCarrito carrito)
        {
            using (var db = new Mapeo())
            {
                db.carrito.Add(carrito);
                db.SaveChanges();
            }
        }
        public List<UEncapInventario> ConsultarInventario()
        {
            using (var db = new Mapeo())
            {
                return (from uu in db.inventario.Where(x => x.Ca_actual > 0)
                        join marca_carro in db.marca_carro on uu.Id_marca equals marca_carro.Id
                        join categoria in db.categoria on uu.Id_categoria equals categoria.Id
                        join estadoitem in db.estado_item on uu.Id_estado equals estadoitem.Id
                        let _cantCarrito = (from ss in db.carrito where ss.Producto_id == uu.Id select ss.Cantidad).Sum()

                        select new
                        {
                            uu,
                            marca_carro,
                            categoria,
                            estadoitem,
                            _cantCarrito


                        }).ToList().Select(m => new UEncapInventario
                        {
                            Id = m.uu.Id,
                            Imagen = m.uu.Imagen,
                            Titulo = m.uu.Titulo,
                            Precio = m.uu.Precio,
                            Referencia = m.uu.Referencia,
                            Ca_actual = m.uu.Ca_actual - (m._cantCarrito.HasValue ? m._cantCarrito.Value : 0),
                            Ca_minima = m.uu.Ca_minima,
                            Id_marca = m.uu.Id_marca,
                            Id_categoria = m.uu.Id_categoria,
                            Id_estado = m.uu.Id_estado,
                            Nombre_categoria = m.categoria.Categoria,
                            Nombre_marca = m.marca_carro.Marca,
                            Estado = m.estadoitem.Estado_item

                        }).ToList();
            }
        }
        //METODO CONSULTAR INVENTARIO CATEGORIA MENOS LA CANTIDAD DEL CARRITO 
        public List<UEncapInventario> ConsultarInventarioCategoria(int categ)
        {
            using (var db = new Mapeo())
            {
                return (from uu in db.inventario.Where(x => x.Ca_actual > 0 && x.Id_categoria == categ)
                        join marca_carro in db.marca_carro on uu.Id_marca equals marca_carro.Id
                        join categoria in db.categoria on uu.Id_categoria equals categoria.Id
                        join estadoitem in db.estado_item on uu.Id_estado equals estadoitem.Id
                        let _cantCarrito = (from ss in db.carrito where ss.Producto_id == uu.Id select ss.Cantidad).Sum()

                        select new
                        {
                            uu,
                            marca_carro,
                            categoria,
                            estadoitem,
                            _cantCarrito


                        }).ToList().Select(m => new UEncapInventario
                        {
                            Id = m.uu.Id,
                            Imagen = m.uu.Imagen,
                            Titulo = m.uu.Titulo,
                            Precio = m.uu.Precio,
                            Referencia = m.uu.Referencia,
                            Ca_actual = m.uu.Ca_actual - (m._cantCarrito.HasValue ? m._cantCarrito.Value : 0),
                            Ca_minima = m.uu.Ca_minima,
                            Id_marca = m.uu.Id_marca,
                            Id_categoria = m.uu.Id_categoria,
                            Id_estado = m.uu.Id_estado,
                            Nombre_categoria = m.categoria.Categoria,
                            Nombre_marca = m.marca_carro.Marca,
                            Estado = m.estadoitem.Estado_item

                        }).ToList();
            }
        }
        public List<UEncapInventario> ConsultarInventariMarca(int marca)
        {
            using (var db = new Mapeo())
            {
                return (from uu in db.inventario.Where(x => x.Ca_actual > 0 && x.Id_marca == marca)
                        join marca_carro in db.marca_carro on uu.Id_marca equals marca_carro.Id
                        join categoria in db.categoria on uu.Id_categoria equals categoria.Id
                        join estadoitem in db.estado_item on uu.Id_estado equals estadoitem.Id
                        let _cantCarrito = (from ss in db.carrito where ss.Producto_id == uu.Id select ss.Cantidad).Sum()

                        select new
                        {
                            uu,
                            marca_carro,
                            categoria,
                            estadoitem,
                            _cantCarrito


                        }).ToList().Select(m => new UEncapInventario
                        {
                            Id = m.uu.Id,
                            Imagen = m.uu.Imagen,
                            Titulo = m.uu.Titulo,
                            Precio = m.uu.Precio,
                            Referencia = m.uu.Referencia,
                            Ca_actual = m.uu.Ca_actual - (m._cantCarrito.HasValue ? m._cantCarrito.Value : 0),
                            Ca_minima = m.uu.Ca_minima,
                            Id_marca = m.uu.Id_marca,
                            Id_categoria = m.uu.Id_categoria,
                            Id_estado = m.uu.Id_estado,
                            Nombre_categoria = m.categoria.Categoria,
                            Nombre_marca = m.marca_carro.Marca,
                            Estado = m.estadoitem.Estado_item

                        }).ToList();
            }
        }
        public List<UEncapInventario> ConsultarInventariCombinado(int marca, int categ)
        {
            using (var db = new Mapeo())
            {
                return (from uu in db.inventario.Where(x => x.Ca_actual > 0 && x.Id_marca == marca && x.Id_categoria == categ)
                        join marca_carro in db.marca_carro on uu.Id_marca equals marca_carro.Id
                        join categoria in db.categoria on uu.Id_categoria equals categoria.Id
                        join estadoitem in db.estado_item on uu.Id_estado equals estadoitem.Id
                        let _cantCarrito = (from ss in db.carrito where ss.Producto_id == uu.Id select ss.Cantidad).Sum()

                        select new
                        {
                            uu,
                            marca_carro,
                            categoria,
                            estadoitem,
                            _cantCarrito


                        }).ToList().Select(m => new UEncapInventario
                        {
                            Id = m.uu.Id,
                            Imagen = m.uu.Imagen,
                            Titulo = m.uu.Titulo,
                            Precio = m.uu.Precio,
                            Referencia = m.uu.Referencia,
                            Ca_actual = m.uu.Ca_actual - (m._cantCarrito.HasValue ? m._cantCarrito.Value : 0),
                            Ca_minima = m.uu.Ca_minima,
                            Id_marca = m.uu.Id_marca,
                            Id_categoria = m.uu.Id_categoria,
                            Id_estado = m.uu.Id_estado,
                            Nombre_categoria = m.categoria.Categoria,
                            Nombre_marca = m.marca_carro.Marca,
                            Estado = m.estadoitem.Estado_item

                        }).ToList();
            }
        }
        public List<UEncapInventario> ConsultarInventarioPrecio(string valores)
        {
            //Metodo de Slip
            string[] split = valores.Split(',');
            int can1 = Convert.ToInt32(split[0]);
            int can2 = Convert.ToInt32(split[1]);


            using (var db = new Mapeo())
            {
                return (from uu in db.inventario.Where(x => x.Precio >= can1 && x.Precio <= can2)
                        join marca_carro in db.marca_carro on uu.Id_marca equals marca_carro.Id
                        join categoria in db.categoria on uu.Id_categoria equals categoria.Id
                        join estadoitem in db.estado_item on uu.Id_estado equals estadoitem.Id

                        let _cantCarrito = (from ss in db.carrito where ss.Producto_id == uu.Id select ss.Cantidad).Sum()

                        select new
                        {
                            uu,
                            marca_carro,
                            categoria,
                            estadoitem,

                            _cantCarrito

                        }).ToList().Select(m => new UEncapInventario

                        {
                            Id = m.uu.Id,
                            Imagen = m.uu.Imagen,
                            Titulo = m.uu.Titulo,
                            Precio = m.uu.Precio,
                            Referencia = m.uu.Referencia,
                            Ca_actual = m.uu.Ca_actual - (m._cantCarrito.HasValue ? m._cantCarrito.Value : 0),
                            Ca_minima = m.uu.Ca_minima,
                            Id_marca = m.uu.Id_marca,
                            Id_categoria = m.uu.Id_categoria,

                            Id_estado = m.uu.Id_estado,

                            Nombre_categoria = m.categoria.Categoria,
                            Nombre_marca = m.marca_carro.Marca,

                            Estado = m.estadoitem.Estado_item





                        }


                        ).ToList();

            }
        }
        public List<UEncapInventario> ConsultarInventarioPrecioCategoria(string valores, int categ)
        {
            //Metodo de Slip
            string[] split = valores.Split(',');
            int can1 = Convert.ToInt32(split[0]);
            int can2 = Convert.ToInt32(split[1]);


            using (var db = new Mapeo())
            {
                return (from uu in db.inventario.Where(x => x.Precio >= can1 && x.Precio <= can2 && x.Id_categoria == categ)
                        join marca_carro in db.marca_carro on uu.Id_marca equals marca_carro.Id
                        join categoria in db.categoria on uu.Id_categoria equals categoria.Id
                        join estadoitem in db.estado_item on uu.Id_estado equals estadoitem.Id

                        let _cantCarrito = (from ss in db.carrito where ss.Producto_id == uu.Id select ss.Cantidad).Sum()

                        select new
                        {
                            uu,
                            marca_carro,
                            categoria,
                            estadoitem,

                            _cantCarrito

                        }).ToList().Select(m => new UEncapInventario

                        {
                            Id = m.uu.Id,
                            Imagen = m.uu.Imagen,
                            Titulo = m.uu.Titulo,
                            Precio = m.uu.Precio,
                            Referencia = m.uu.Referencia,
                            Ca_actual = m.uu.Ca_actual - (m._cantCarrito.HasValue ? m._cantCarrito.Value : 0),
                            Ca_minima = m.uu.Ca_minima,
                            Id_marca = m.uu.Id_marca,
                            Id_categoria = m.uu.Id_categoria,

                            Id_estado = m.uu.Id_estado,

                            Nombre_categoria = m.categoria.Categoria,
                            Nombre_marca = m.marca_carro.Marca,

                            Estado = m.estadoitem.Estado_item





                        }


                        ).ToList();

            }
        }
        public List<UEncapInventario> ConsultarInventarioPrecioMarca(string valores, int marca)
        {
            //Metodo de Slip
            string[] split = valores.Split(',');
            int can1 = Convert.ToInt32(split[0]);
            int can2 = Convert.ToInt32(split[1]);


            using (var db = new Mapeo())
            {
                return (from uu in db.inventario.Where(x => x.Precio >= can1 && x.Precio <= can2 && x.Id_marca == marca)
                        join marca_carro in db.marca_carro on uu.Id_marca equals marca_carro.Id
                        join categoria in db.categoria on uu.Id_categoria equals categoria.Id
                        join estadoitem in db.estado_item on uu.Id_estado equals estadoitem.Id

                        let _cantCarrito = (from ss in db.carrito where ss.Producto_id == uu.Id select ss.Cantidad).Sum()

                        select new
                        {
                            uu,
                            marca_carro,
                            categoria,
                            estadoitem,

                            _cantCarrito

                        }).ToList().Select(m => new UEncapInventario

                        {
                            Id = m.uu.Id,
                            Imagen = m.uu.Imagen,
                            Titulo = m.uu.Titulo,
                            Precio = m.uu.Precio,
                            Referencia = m.uu.Referencia,
                            Ca_actual = m.uu.Ca_actual - (m._cantCarrito.HasValue ? m._cantCarrito.Value : 0),
                            Ca_minima = m.uu.Ca_minima,
                            Id_marca = m.uu.Id_marca,
                            Id_categoria = m.uu.Id_categoria,

                            Id_estado = m.uu.Id_estado,

                            Nombre_categoria = m.categoria.Categoria,
                            Nombre_marca = m.marca_carro.Marca,

                            Estado = m.estadoitem.Estado_item





                        }


                        ).ToList();

            }
        }
        public List<UEncapInventario> ConsultarInventarioPrecioCombinado(string valores, int marca, int categor)
        {
            //Metodo de Slip
            string[] split = valores.Split(',');
            int can1 = Convert.ToInt32(split[0]);
            int can2 = Convert.ToInt32(split[1]);


            using (var db = new Mapeo())
            {
                return (from uu in db.inventario.Where(x => x.Precio >= can1 && x.Precio <= can2 &&
                        x.Id_marca == marca && x.Id_categoria == categor)
                        join marca_carro in db.marca_carro on uu.Id_marca equals marca_carro.Id
                        join categoria in db.categoria on uu.Id_categoria equals categoria.Id
                        join estadoitem in db.estado_item on uu.Id_estado equals estadoitem.Id

                        let _cantCarrito = (from ss in db.carrito where ss.Producto_id == uu.Id select ss.Cantidad).Sum()

                        select new
                        {
                            uu,
                            marca_carro,
                            categoria,
                            estadoitem,

                            _cantCarrito

                        }).ToList().Select(m => new UEncapInventario

                        {
                            Id = m.uu.Id,
                            Imagen = m.uu.Imagen,
                            Titulo = m.uu.Titulo,
                            Precio = m.uu.Precio,
                            Referencia = m.uu.Referencia,
                            Ca_actual = m.uu.Ca_actual - (m._cantCarrito.HasValue ? m._cantCarrito.Value : 0),
                            Ca_minima = m.uu.Ca_minima,
                            Id_marca = m.uu.Id_marca,
                            Id_categoria = m.uu.Id_categoria,

                            Id_estado = m.uu.Id_estado,

                            Nombre_categoria = m.categoria.Categoria,
                            Nombre_marca = m.marca_carro.Marca,

                            Estado = m.estadoitem.Estado_item





                        }


                        ).ToList();

            }
        }
        public List<UEncapCategoria> ColsultarCategoria2()
        {
            using (var db = new Mapeo())
            {
                return db.categoria.OrderBy(x => x.Id >= 1).ToList();
            }
        }
        public List<UEncapMarca> ColsultarMarca()
        {
            using (var db = new Mapeo())
            {
                return db.marca_carro.OrderBy(x => x.Id).ToList();
            }
        }
        public UEncapUsuario verificarIdentificacion(UEncapUsuario verificar)
        {
            using (var db = new Mapeo())
            {
                return db.usuario.Where(x => x.Correo.Equals(verificar.Correo) || x.Identificacion.Equals(verificar.Identificacion)).FirstOrDefault();
            }
        }
        public void InsertarCliente(UEncapUsuario cliente)
        {
            using (var db = new Mapeo())
            {
                db.usuario.Add(cliente);
                db.SaveChanges();
            }
        }
        public void InsertarEmpleado(UEncapUsuario empleado)
        {
            using (var db = new Mapeo())
            {
                db.usuario.Add(empleado);
                db.SaveChanges();
            }
        }
        //CAMBIO TODOS LOS ESTADOS DEL PRODUCTO CUANDO DAN FACTURAR EN EL CARRITO
        public void ActualizarCarritoEstado(UEncapCarrito carrito)
        {
            using (var db = new Mapeo())
            {

                var carritoedit = db.carrito.Where(x => x.User_id == carrito.User_id).ToList();
                foreach (var car in carritoedit)
                {
                    car.Estadocar = carrito.Estadocar;
                }
                db.SaveChanges();
            }
        }
        //METODO PARA OBTENER TODOS LOS ELEMENTOS DEL CARRITO 
        public List<UEncapCarrito> ObtenerCarritoxUsuario(int usu)
        {
            using (var db = new Mapeo())
            {
                return (from carrito in db.carrito.Where(x => x.User_id == usu)
                        join iven in db.inventario on carrito.Producto_id equals iven.Id
                        select new
                        {
                            carrito,
                            iven
                        }).ToList().Select(m => new UEncapCarrito
                        {
                            Id_Carrito = m.carrito.Id_Carrito,
                            User_id = m.carrito.User_id,
                            Producto_id = m.carrito.Producto_id,
                            Cantidad = m.carrito.Cantidad,
                            Fecha = m.carrito.Fecha,
                            Precio = m.carrito.Precio,
                            Total = m.carrito.Total,
                            Nom_producto = m.iven.Titulo,
                            Cant_Actual = (m.iven.Ca_actual - m.carrito.Cantidad).Value,
                            Id_pedido = m.carrito.Id_pedido
                        }).ToList();
            }
        }
        //METODO PARA INSERTAR PEDIDO
        public int InsertarPedido(UEncapPedido pedido)
        {
            using (var db = new Mapeo())
            {
                db.pedidos.Add(pedido);
                db.SaveChanges();
            }
            return pedido.Id;
        }
        //MODIFICAR ID DEL PEDIDO EN CARRITO
        public void ActualizarIdpedidoCarrito(UEncapCarrito carrito)
        {
            using (var db = new Mapeo())
            {

                var carritoedit = db.carrito.Where(x => x.User_id == carrito.User_id).ToList();
                foreach (var car in carritoedit)
                {
                    car.Id_pedido = carrito.Id_pedido;
                }
                db.SaveChanges();
            }
        }

        //METODO PARA INSERTAR PRODUCTOS AL MOMENTO DE VENTA
        public void InsertarProductos(UEncapProducto_pedido producto)
        {
            using (var db = new Mapeo())
            {
                db.productos.Add(producto);
                db.SaveChanges();
            }
        }
        //ACTUALIZAR  CANTIDAD DEL PRODUCTO EN EL INVENTARIO 
        public void ActualizarCantidadInventario(UEncapInventario producto)
        {
            using (var db = new Mapeo())
            {
                UEncapInventario inventarioedit = db.inventario.Where(x => x.Id == producto.Id).SingleOrDefault();
                inventarioedit.Ca_actual = inventarioedit.Ca_actual - producto.Ca_actual;

                db.SaveChanges();
            }
        }
        //METODO PARA BORRAR EN CARRITO LUEGO DE HACER FACTURACION
        public void limpiarCarrito(int userid)
        {
            using (var db = new Mapeo())
            {
                List<UEncapCarrito> productos = db.carrito.Where(x => x.User_id == userid).ToList();

                foreach (var pro in productos)
                {
                    db.Entry(pro).State = EntityState.Deleted;
                }

                db.SaveChanges();
            }
        }
        public void ActualizarValorpedido(UEncapPedido pedido)
        {
            using (var db = new Mapeo())
            {

                UEncapPedido pedidoedit = db.pedidos.Where(x => x.Id == pedido.Id).SingleOrDefault();
                pedidoedit.Total = pedido.Total;

                db.SaveChanges();
            }
        }
    }
}
