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

23. Se corrigieron las referencias a las funciones comunes de la clase `Funciones` en el proyecto Web. El proyecto Web ahora compila correctamente.

24. En este punto se creó la base de datos SQL Server, se ejecutaron los scripts de creación de los objetos de la base ubicados en `/DB/Scripts-ControlMantenimientoDB-SQLServer.txt`, se agregó el correcto *Connection String* en el `app.config` de la apliación Desktop y se procedió a ejecutar la misma para comprobar su correcto funcionamiento. 
    * Rápidamente se observó que aún había referencias a `System.Configuration` en la clase `AccesoDatos` que estaban causando errores ya que estaban buscando el *Connection String* en los `AppSettings` del `App.config` en lugar de hacerlo en la sección `connectionStrings` que es lo correcto. De todas formas como no es responsabilidad de la capa de acceso a datos obtener el connection string se cambió esto para usar el connection string provisto por el constructor de la clase.

25. Se comprobó el funcionamiento correcto de la aplicación desktop.

26. Se agregó el connection string al `Web.Config` de la aplicación, web, y rápidamente se encontró un error relacionado a *Unobtrusive Validation*, que se corrigió usando [esta solución](http://stackoverflow.com/a/16705149/643085)

### En este punto ámbas aplicaciones (Web y Desktop) funcionan correctamente, y con una estructura mucho mejor que al inicio, sin código duplicado, sin violaciones de la separación de capas y respetando el SRP. Se procede a [taggear](https://git-scm.com/book/en/v2/Git-Basics-Tagging) el commit como **V0.1**

#### los proyectos `ControlMantenimiento.Data.MySql` y `ControlMantenimiento.Data.Oracle` aún no compilan.

27. Se agregó el [**Paquete Nuget**](https://docs.microsoft.com/en-us/nuget/) [`MySql.Data`](https://www.nuget.org/packages/MySql.Data/) al proyecto `ControlMantenimiento.Data.MySql`

28. Se realizaron los mismos cambios que en el punto `13`, pero esta vez con la clase `AccesoDatos` del proyecto `ControlMantenimiento.Data.MySql`.

29. Se corrigieron las referencias a los namespaces del model y se quitaron las referencias hacia la capa de arriba de la misma forma que en el punto 14, pero esta vez con la clase `AccesoDatos` del proyecto `ControlMantenimiento.Data.MySql`.

30. Se agregó una referencia a la assembly `System.Data.OracleClient` del .NET Framework al proyecto `ControlMantenimiento.Data.Oracle`

31. Se realizó el paso 28, pero sobre `ControlMantenimiento.Data.Oracle`.

32. Se removieron referencias a incorrectas `System.Data.SqlClient` y se reemplazaron por las respectivas de Oracle.

33. Se realizaron los mismos cambios que en el punto `13`, pero esta vez con la clase `AccesoDatos` del proyecto `ControlMantenimiento.Data.Oracle`

#### en este punto toda la solución compila correctamente, pero el IDE muestra 24 warnings acerca de que `System.Data.OracleClient` está deprecado.

34. En este punto y antes de continuar con la abstracción de la capa de acceso a datos, resulta conveniente corregir el naming en varios lugares:
    * Se renombró el namespace `ControlMantenimiento_NetDesktop.BO` a `ControlMantenimiento.Model`
    * Se cambió el namespace de la clase `CargaCombosListas` de `ControlMantenimiento_NetDesktop.BLL` a `ControlMantenimiento.Model`
    * Se renombró el namespace `ControlMantenimiento_NetDesktop.DAL` a `ControlMantenimiento.Data`
    * Se cambió el nombre de la clase `ControlMantenimiento_NetWeb.DAL.AccesoDatos` en el proyecto `ControlMantenimiento.Data.MySql` a `ControlMantenimiento.Data.MySql.MySqlAccesoDatos`
    * Se cambió el nombre de la clase `ControlMantenimiento_NetWeb.DAL.AccesoDatos en el proyecto `ControlMantenimiento.Data.Oracle` a `ControlMantenimiento.Data.Oracle.OracleAccesoDatos`
    * Se renombraron todos los parámetros de todos los métodos de la clase `AccesoDatos` para usar el estilo *camelCase* que es el estándar en C# para este tipo de identificadores.
    * Se renombraron todos los parámetros de todos los métodos de la clase `OracleAccesoDatos` para usar el estilo *camelCase* que es el estándar en C# para este tipo de identificadores.
    * Se renombraron todos los parámetros de todos los métodos de la clase `MySqlAccesoDatos` para usar el estilo *camelCase* que es el estándar en C# para este tipo de identificadores.

35. Se cambiaron los métodos `IniciarBusqueda()` y `LiberarRecursos` y `BuscarRegistro()` de la clases de acceso a datos de `public` a `private` ya que no se usan fuera de éstas.

36. Se creó la interfaz `ControlMantenimiento.Data.IAccesoDatos` a partir de los métodos públicos de `ControlMantenimiento.Data.AccesoDatos`.

37. Al tratar de implementar la interfaz `IAccesoDatos` en la clase `MySqlAccesoDatos` se descubrió que faltaba el método `public ArrayList CargarListas(string tabla)`. Se agregó éste a partir de la definición del mismo en la clase `AccesoDatos`, reemplazando las APIs de SQL Server (`SqlConnection` y `SqlCommand`) por las de MySQL (`MySqlConnection` y `MySqlCommand`)

38. Idem punto anterior, pero con `OracleAccesoDatos`.

39. Se cambió la referencia de la clase `Controlador` a la implementación concreta `AccesoDatos` por la abstracción `IAccesoDatos`.
    * Esto reveló que la clase `Controlador` accedía a los *campos públicos* `ArlListEquipo`, `ArlListLinea`, `ArlListMarca`, y `ArlListOperarios` de la clase `AccesoDatos`, lo cual no es correcto ya que no se puede abstraer, y además viola el SRP dándole a la capa de acceso a datos la responsabilidad de persistir estado en memoria, cosa que no debería.
    * Se convierten estos campos públicos en properties momentáneamente en las 3 clases de acceso a datos. Luego será refactorizado ésto para evitar tener estado en la capa de acceso a datos.

40. Se creó la clase estática `ControlMantenimiento.Business.AccesoDatosFactory` en el proyecto `ControlMantenimiento.Business`, que permite obtener la implementación adecuada de acceso a datos a partir de los parámetros del connection string (más precisamente el `providerName`).

41. Se aplicó el uso de `AccesoDatosFactory` en los métodos `CrearControlador()` tanto en el proyecto Desktop como Web.

### En este punto ámbas aplicaciones (Web y Desktop) funcionan correctamente, y ámbas implementan una abstracción que automáticamente selecciona la implementación adecuada de *Acceso a Datos* dependiendo simplemente del parámetro `providerName` del *Connection String* en su respectivo archivo de configuración.

42. A partir de este punto se realiza una limpieza de código antes de seguir haciendo mejoras en la estructura / arquitectura del proyecto.
    * Se cambian todas las propiedades de las clases de `ControlMantenimiento.Model` a auto-properties.
    * Se eliminan constructores default innecesarios en las clases de `ControlMantenimiento.Model`.
    * Se renombras todas las propiedades de las clases de `ControlMantenimiento.Model` para usar el estilo *ProperCase* que es estándar en C# para este tipo de identificadores.

