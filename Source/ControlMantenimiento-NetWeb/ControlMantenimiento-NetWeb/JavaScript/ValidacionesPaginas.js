function VerificarAcceso()
{ 
   document.frmAcceso.txtDocumento.value = document.frmAcceso.txtDocumento.value.replace(/^\s+|\s+$/g,"");
   if (document.frmAcceso.txtDocumento.value == "")
   {    
       document.frmAcceso.txtMensajeError.value= 'Documento es requerido';	
       document.frmAcceso.txtDocumento.focus();
	   return false;
   }
   if (document.frmAcceso.txtDocumento.value.length < 6) 
   { 	
       document.frmAcceso.txtMensajeError.value= 'Documento debe ser mayor de 6 digitos';	
       document.frmAcceso.txtDocumento.focus();             
       return false;
   }	
   document.frmAcceso.txtClave.value = document.frmAcceso.txtClave.value.replace(/^\s+|\s+$/g,"");
   if (document.frmAcceso.txtClave.value == "")
   {    
       document.frmAcceso.txtMensajeError.value= 'Clave es requerido';	
       document.frmAcceso.txtClave.focus();
	   return false;
   }     
   if (document.frmAcceso.txtClave.value.length < 6)
   {    
       document.frmAcceso.txtClave.focus();
	   return false;
   }      

   return true;
}	

function VerificarOperarios()
{ 
   document.frmOperarios.txtDocumento.value = document.frmOperarios.txtDocumento.value.replace(/^\s+|\s+$/g,"");
   if (document.frmOperarios.txtDocumento.value == "")
   {    
       document.frmOperarios.txtMensajeError.value= 'Documento es requerido';	
       document.frmOperarios.txtDocumento.focus();
	   return false;
   }
   if (document.frmOperarios.txtDocumento.value.length < 6) 
   { 	
       document.frmOperarios.txtMensajeError.value= 'Documento debe ser mayor de 6 digitos';	
       document.frmOperarios.txtDocumento.focus();             
       return false;
   }	
   if (document.frmOperarios.txtDocumento.value.substring(0,1)==0) 
   { 	
       document.frmOperarios.txtMensajeError.value= 'Error, primera cifra no puede ser 0';	
       document.frmOperarios.txtDocumento.focus();             
       return false;
   }   
   document.frmOperarios.txtNombres.value = document.frmOperarios.txtNombres.value.replace(/^\s+|\s+$/g,"");
   if ( document.frmOperarios.txtNombres.value == "")
   {    
        document.frmOperarios.txtMensajeError.value= 'Nombres es requerido';
        document.frmOperarios.txtNombres.focus();
	    return false;
   }
   document.frmOperarios.txtApellidos.value = document.frmOperarios.txtApellidos.value.replace(/^\s+|\s+$/g,"");
   if ( document.frmOperarios.txtApellidos.value == "")
   {    
        document.frmOperarios.txtMensajeError.value= 'Apellidos es requerido';	
        document.frmOperarios.txtApellidos.focus();
	    return false;
   }
   document.frmOperarios.txtTelefono.value = document.frmOperarios.txtTelefono.value.replace(/^\s+|\s+$/g,"");
   if ( document.frmOperarios.txtTelefono.value == "")
   {    
        document.frmOperarios.txtMensajeError.value= 'Telefono es requerido';	
        document.frmOperarios.txtTelefono.focus();
	    return false;
   }
   if ((document.frmOperarios.txtTelefono.value.length != 7)  && (document.frmOperarios.txtTelefono.value.length != 10))
   { // Si es un teléfono celular debe ser de 10 dígitos	
       document.frmOperarios.txtMensajeError.value= 'Por favor debe ingresar entre 7 y 10 digitos para el telefono';
       document.frmOperarios.txtTelefono.focus();             
       return false;
   }
   if (document.frmOperarios.txtTelefono.value.substring(0,1)==0) 
   { 	
       document.frmOperarios.txtMensajeError.value= 'Error, primera cifra no puede ser 0';	
       document.frmOperarios.txtTelefono.focus();             
       return false;
   }  
   document.frmOperarios.txtCorreo.value = document.frmOperarios.txtCorreo.value.replace(/^\s+|\s+$/g,"");
   if (document.frmOperarios.txtCorreo.value.length > 0)
   {
    if( !(/\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/.test(document.frmOperarios.txtCorreo.value)) )                        
     {
       document.frmOperarios.txtMensajeError.value= 'Formato de correo errado';
       document.frmOperarios.txtCorreo.focus();   
       return false;    
     }   
   }   
   document.frmOperarios.txtClave.value = document.frmOperarios.txtClave.value.replace(/^\s+|\s+$/g,"");
   if ( document.frmOperarios.txtClave.value == "")
   {    
        document.frmOperarios.txtMensajeError.value= 'Clave es requerido';	
        document.frmOperarios.txtClave.focus();
	    return false;
   }
   if ( document.frmOperarios.txtClave.value.length < 6)
   {    
        document.frmOperarios.txtMensajeError.value= 'Error, por favor ingrese una clave que contenga al menos 6 digitos';	
        document.frmOperarios.txtClave.focus();
	    return false;
   }

   return true;
}

function VerificarListaValores()
{ 
   document.frmListaValores.txtNombre.value = document.frmListaValores.txtNombre.value.replace(/^\s+|\s+$/g,"");
   if (document.frmListaValores.txtNombre.value == "")
   {    
       document.frmListaValores.txtMensajeError.value= 'Nombre es requerido';	
       document.frmListaValores.txtNombre.focus();
	   return false;
   }
   return true;
}	

function VerificarCambioClave()
{ 
   document.frmCambioClave.txtClave.value = document.frmCambioClave.txtClave.value.replace(/^\s+|\s+$/g,"");
   if (document.frmCambioClave.txtClave.value == "")
   {    
       document.frmCambioClave.txtMensajeError.value= 'Clave es requerido';	
       document.frmCambioClave.txtClave.focus();
	   return false;
   }
   if (document.frmCambioClave.txtClave.value.length < 6) 
   { 	
       document.frmCambioClave.txtMensajeError.value= 'Clave debe ser mayor de 6 digitos';	
       document.frmCambioClave.txtClave.focus();             
       return false;
   }
   document.frmCambioClave.txtClaveNueva.value = document.frmCambioClave.txtClaveNueva.value.replace(/^\s+|\s+$/g,"");
   if (document.frmCambioClave.txtClaveNueva.value == "")
   {    
       document.frmCambioClave.txtMensajeError.value= 'Clave Nueva es requerido';	
       document.frmCambioClave.txtClaveNueva.focus();
	   return false;
   }
   if (document.frmCambioClave.txtClaveNueva.value.length < 6) 
   { 	
       document.frmCambioClave.txtMensajeError.value= 'Clave debe ser mayor de 6 digitos';	
       document.frmCambioClave.txtClaveNueva.focus();             
       return false;
   }
   if (document.frmCambioClave.txtClave.value == document.frmCambioClave.txtClaveNueva.value)
   {    
       document.frmCambioClave.txtMensajeError.value= 'La clave debe ser diferente de la anterior';	
       document.frmCambioClave.txtClaveNueva.focus();
	   return false;
   }
   document.frmCambioClave.txtConfirmar.value = document.frmCambioClave.txtConfirmar.value.replace(/^\s+|\s+$/g,"");
   if (document.frmCambioClave.txtConfirmar.value == "")
   {    
       document.frmCambioClave.txtMensajeError.value= 'Debe confirmar la clave';	
       document.frmCambioClave.txtConfirmar.focus();
	   return false;
   }
   if (document.frmCambioClave.txtConfirmar.value.length < 6) 
   { 	
       document.frmCambioClave.txtMensajeError.value= 'Clave debe ser mayor de 6 digitos';	
       document.frmCambioClave.txtConfirmar.focus();             
       return false;
   }
   if (document.frmCambioClave.txtConfirmar.value != document.frmCambioClave.txtClaveNueva.value)
   {    
       document.frmCambioClave.txtMensajeError.value= 'La clave es diferente';	
       document.frmCambioClave.txtConfirmar.focus();
	   return false;
   }
   return true;
}	

function VerificarBusquedas()
{ 
   var indice = document.frmBusquedas.lstSeleccion.selectedIndex;
   document.frmBusquedas.txtBuscar.value = document.frmBusquedas.txtBuscar.value.replace(/^\s+|\s+$/g,"");
   if ((document.frmBusquedas.txtBuscar.value == "") && ( indice == -1))
   {    
      document.frmBusquedas.txtMensajeError.value= 'Por favor seleccione o ingrese el dato a buscar';	
      document.frmBusquedas.txtBuscar.focus();
	  return false;
   }     
   
   return true;
}

function VerificarEquipos()
{ 
   document.frmEquipos.txtNombre.value = document.frmEquipos.txtNombre.value.replace(/^\s+|\s+$/g,"");
   if (document.frmEquipos.txtNombre.value == "")
   {    
      document.frmEquipos.txtMensajeError.value= 'Nombre es requerido';	
      document.frmEquipos.txtNombre.focus();
	  return false;
   }     
   document.frmEquipos.txtSerie.value = document.frmEquipos.txtSerie.value.replace(/^\s+|\s+$/g,"");
   if (document.frmEquipos.txtSerie.value == "")
   {    
      document.frmEquipos.txtMensajeError.value= 'Serie es requerido';	
      document.frmEquipos.txtSerie.focus();
	  return false;
   }
   return true;
}

function ConfirmarBorrado()
{
 if (confirm("¿ Esta seguro que desea eliminar este registro ?")) 
 {
   return true;
 }
 else
 {
  return false;
 }
}