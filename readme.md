# SIMI - Sistema de Mantenimiento Industrial

Este proyecto es un rework del proyecto [SIMI](http://www.lawebdelprogramador.com/codigo/C-sharp/3653-Acceso-Datos-C-Sharp.html) encontrado en [lawebdelprogramador.com](http://lawebdelprogramador.com).

Describiré en este documento los pasos que fui dando para adaptar el proyecto a las prácticas correctas usadas actualmente en proyectos profesionales en .NET

1. Primero y principal luego de descargar el proyecto lo primero que se hizo fue meterlo bajo control de código, creando un repositorio Git que luego sería subido a GitHub.

2. Se realizó modificaciones en la estructura de directorio del proyecto:
   * Se creó una carpeta Source/ y poniendo todo el código fuente dentro.
   * Se creó una carpeta DB/ y se colocó en esta los scripts de inicialización de la base de datos.
   * Se creó una carpeta Doc/ y se colocó dentro todos los documentos de ayuda del proyecto.

3. Se actualizó el proyecto `ControlMantenimiento-NetDesktop` a la versió actual del .NET Framework: 4.6.2

4. Se creó una solución nueva (`ControlMantenimiento.sln`) que contendrá tanto la versión desktop como la web, así también como los módulos comunes de acceso a datos, lógica de negocio, etc.

5. Se movió el model (anteriormente llamado `BO` y ubicado dentro de una carpeta del proyecto desktop) a un proyecto aparte: `ControlMantenimiento.Model`.

6. También se movió a `ControlMantenimiento.Model` la clase `ControlMantenimiento_NetDesktop.BLL.CargaCombosListas`, debido a que la misma se referencia desde el código de acceso a datos.

7. Se movió el acceso a datos (anteriormente ubicado dentro de una carpeta `DAL` en el proyecto desktop) a un proyecto aparte: `ControlMantenimiento.Data`.
   * Esto dejó al descubierto una violación en la separación de capas: la clase `ControlMantenimiento_NetDesktop.DAL.AccesoDatos` hace referencia al campo estático `ControlMantenimiento_NetDesktop.BLL.Funciones.UsuarioConectado`. 
   * Debido a este error, el proyecto no compila en este punto del tiempo.

8. Se agregó el proyecto web a la solución bajo el nombre `ControlMantenimiento.Web`.

9. Se eliminaron las clases duplicadas de `BO` del proyecto `ControlMantenimiento.Web`.

