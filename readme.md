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

10. Se crearon los proyectos `ControlMantenimiento.Data.MySql` y `ControlMantenimiento.Data.Oracle` y se movió el código de acceso a datos respectivo a estos.

11. Se creó el proyecto `ControlMantenimiento.Business` y se movió el código de lógica de negocio (anteriormente llamado `BLL` y duplicado en los proyectos Web y Desktop).
   * La interfaz `IControlador` que se encontraba duplicada tenía definiciones exactamente iguales, excepto por el método `CargarListas()`, el cuál ahora tiene 2 sobrecargas.
   * La clases `Mensajes` tenía definiciones idénticas, excepto por la propiedad `Mensaje28` que se fue agregada.
   * La clases `Funciones` tenían definiciones idénticas, excepto por los campos estáticos y el método `LimpiarForma()`, cuya implementación es específica para desktop y web. Se unificaron los métodos duplicados en la clase `ControlMantenimiento.Business.Funciones` y se mantuvieron las diferencias en los respectivos proyectos.
   * La clases `Controlador` tenían definiciones idénticas, excepto por el método `CargarListas()` que ahora tiene 2 sobrecargas.
   
12. Se unificó el acceso a datos en la clase `AccesoDatos` en el proyecto `ControlMantenimiento.Data` eliminando el código duplicado que existía en los proyectos Web y Desktop.
    * En este punto, el proyecto se encuentra correctamente separado por capas (además de no tener código duplicado), donde cada capa reside en un proyecto separado, con referencias hacia las capas "de abajo":
        * El proyecto `ControlMantenimiento.Model` contiene el modelo de datos, y no requiere referencias a ningun otro proyecto.
        * El proyecto `ControlMantenimiento.Data` contiene el acceso a datos, y hace uso de las entidades definidas en `ControlMantenimiento.Model`.
        * El proyecto `ControlMantenimiento.Business` contiene la lógica de negocio y funciones comunes, y hace uso tanto de `ControlMantenimiento.Model` como de `ControlMantenimiento.Data`.
        * El proyecto `ControlMantenimiento.Web` contiene la aplicación Web, y hace uso de la capa de negocio, el model y la capa de datos.
        * El proyecto `ControlMantenimiento-NetDesktop` contiene la aplicación desktop, y hace uso de la capa de negocio, el model y la capa de datos.
    * A pesar de todo esto, el proyecto está en un estado no compilable, y aún sufre las violaciones de la separación en capas que se encontraron en el punto 7.

13. Se cambiaron todos los métodos estáticos de la clase `AccesoDatos` por métodos de instancia, y se quitó las referencias directas a `ConfigurationManager` en favor de obtener el Connection String mediante un parámetro en el constructor de la clase. Esto quita las dependencias de la capa de datos a la configuración de la aplicación, que es una violación de SRP.

14. Se quitaron todas las referencias desde `AccesoDatos` hacia la capa de arriba (`Business`), para mantener una separación de capas correcta y eliminar el problema detectado en el punto 7.

15. Se modificó la clase `Controlador` para que reciba por parámetro en el constructor una instancia de `AccesoDatos` y use esta instancia en lugar de invocar los métodos de forma estática.

16. Se introdujo el parámetro `double usuarioConectado` en el constructor de la clase `Controlador` para guardarlo en un campo de instancia y pasarlo a `AccesoDatos` cuando sea necesario.

17. Se creó un método estático (factory) en la clase `Funciones` del proyecto Desktop para unificar la creación del `Controlador` pasándo la instancia de `AccesoDatos` con el *Connection String* apropiado.

18. Se convirtió la clase `Mensajes` del proyecto `ControlMantenimiento.Business` en `public` para que pueda ser accedida desde los demás proyectos.

19. Se corrigieron todas las referencias a `Funciones` en el proyecto desktop. El proyecto desktop ahora compila correctamente.

20. Se implementó un método estático igual al del punto 17 en el proyecto Web se usó el mismo para instanciar la clase `Controlador` en los casos necesarios.

21. Se eliminaron las referencias a namespaces inexistentes en el proyecto Web.

22. Se corrigieron las referencias a la clase `Mensajes` en el proyecto Web.