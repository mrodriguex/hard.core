using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace HARD.CORE.OBJ
{
    public enum EnumPerfil
    {
        Administrador = 1,
        Usuario = 2
    }

    public enum EnumTipoUsuario
    {
        Local = 1,
        Red = 2
    }
    public enum EnumCorreo
    {
        RecuperarContrasena = 1,
        AvisoRecuperarContrasena = 2,
        BuzonSugerencias = 3,
        IndicacionRecuperarContrasena = 4
    }
    public enum EnumConstante
    {
        TotalAvisos = 20
    }
    public enum EnumAccion
    {
        SinCambio = 0,
        Insertar = 1,
        Actualizar = 2,
        Eliminar = 3
    }
    public enum EnumRuta
    {
        Avisos = 1,
        Usuarios = 2,
        SinImagen = 3
    }

    public enum EnumEntidad
    {
        Aviso = 1,
        BitacoraAcceso = 2,
        Calidad = 3,
        Contraseña = 4,
        Directorio = 5,
        Entrega = 6,
        EquipoAplicacion = 7,
        Facturacion = 8,
        Notificacion = 9,
        PagoPendiente = 10,
        Perfil = 11,
        Precio = 12,
        Remision = 13,
        Seguridad = 14,
        Sugerencia = 15,
        Tanque = 16,
        Usuario = 17
    }
}
