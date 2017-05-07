using System;

namespace ControlMantenimiento_NetWeb.BLL
{
    public class Mensajes // Coleccion de mensajes lanzados por el sistema
    {   
        public static string MensajeAplicacion = "SIMI - SISTEMA DE MANTENIMIENTO INDUSTRIAL";
        public static string MensajeGraba = "El registro ha sido grabado satisfactoriamente";
        public static string MensajeActualiza = "El registro ha sido modificado satisfactoriamente";
        public static string MensajeBorrado = "El registro ha sido eliminado satisfactoriamente";
        public static string MensajeConfirmarBorrado = "¿ Esta seguro que desea eliminar este registro ?";
        public static string MensajeCampoRequerido = "Error, campos requeridos ingresar informacion";
        public static string MensajeErrorBD = "Se ha producido un error accesando la base de datos";
        public static string Mensaje2 = "Error, la clave es incorrecta, intente de nuevo";
        public static string Mensaje3 = "Error, esta no es su clave actual, reportese  con el administrador";
        public static string Mensaje4 = "Error, debe ingresar una clave diferente de la anterior";
        public static string Mensaje5 = "Error, esta no es la misma clave digitada en clave nueva";
        public static string Mensaje6 = "Error, primera cifra no puede ser 0";
        public static string Mensaje7 = "Error, esta serie ya esta asignada a otro equipo";
        public static string Mensaje8 = "Error, este nombre ya esta asignado";
        public static string Mensaje9 = "Error, este registro no se puede eliminar ya que esta relacionado en equipos";
        public static string Mensaje10 = "Error, ya existe una programacion para este operario en esta fecha";
        public static string Mensaje11 = "No existen Marcas disponibles para relacionar al equipo";
        public static string Mensaje12 = "No existen Lineas disponibles para relacionar al equipo";
        public static string Mensaje13 = "No existen Operarios disponibles para relacionar al mantenimiento";
        public static string Mensaje14 = "No existen Equipos disponibles para relacionar al mantenimiento";
        public static string Mensaje15 = "Error, documentos no pueden ser menores de 6 digitos";
        public static string Mensaje16 = "Error, correo con formato errado";
        public static string Mensaje17 = "Error, la longitud del telefono debe tener 7 o 10 dígitos";
        public static string Mensaje18 = "Error, no se encontro la foto referenciada en la ruta";
        public static string Mensaje19 = "Error, el formato de la imagen no es valido";
        public static string Mensaje20 = "Error, este operario no se puede eliminar ya que tiene programaciones pendientes";
        public static string Mensaje21 = "Error, por favor ingrese una clave que contenga al menos 6 digitos";
        public static string Mensaje22 = "Error, este equipo no se puede eliminar ya que tiene programacion pendiente por ejecutar";
        public static string Mensaje23 = "Error, existen caracteres numericos en este campo";
        public static string Mensaje24 = "Error, No se encontro la foto referenciada en la ruta";
        public static string Mensaje25 = "Error, caracteres no numericos en este campo";
        public static string Mensaje26 = "Error, este registro ya existe";
        public static string Mensaje27 = "Error, fecha no puede ser menor que la actual";



        public Mensajes()
        {
        }

    }
}
