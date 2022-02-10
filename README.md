# Backend_Prueba.FullStack.F2X.RecaudosFrontend
1. Para instalar los paquetes de angular usar la versión CLI 13.2.2
2. En el archivo src/app/services/consumo-recaudos.service.ts editar la variable: private baseUrl = 'http://localhost:59450'; 
   Cambiar el valor de la URL por la URL del API desarrollada en el backend
3. Cuando se ejecute el proyecto de frontend se visualizará una barra superior, en la cual hay dos opciones: la primera es para los recaudos (guardar y consultar)
   y la segunda es para el reporte mensual tabulado. En la vista de Recaudos hay un botón para guardar todos los recaudos por una fecha ingresada en una caja de texto,
   posterior a esto hay un botón de consulta de todos los recaudos que están en la base de datos

Backend
1. Establecer como proyecto principal: Prueba.FullStack.F2X.Recaudos.Api
2. Al ejecutar la solución, en el navegador se abrirá una página de bienvenida: Home, ir a la opcion del menu superior API
   Allí se puede ver las APIs desarrolladas: Seguridad y Recaudos
3. Consumo API Seguridad:
   Para la seguridad se desarrollo un método para generar token propio de esta API (es otro metodo diferente al entregado en el api de la URL http://190.145.81.67:5200), 
   datos para hacer el llamado:

   Method: POST 
   URL: http://host:puerto/api/Seguridad/GenerarToken
   Body JSON:
   {
   "UserName": "user",
   "Password": "1234"
   }

4. Consumo API Recaudos:
   
   Este método guarda los recaudos obtenidos por fecha en el recurso: http://190.145.81.67:5200/
   Method: POST
   URL: http://host:puerto/api/Recaudos/Guardar
   Agregar en el header la siguiente configuración:
   KEY: Token  VALUE: Es el token devuelto en el recurso: api/Seguridad/GenerarToken
   Body JSON:
   {
    "FechaConsulta": "2022-02-09"
   }

   Este método obtiene todos los recaudos registrados en la base de de datos
   Method: GET 
   URL: http://host:puerto/api/Recaudos/Recaudos
   Agregar en el header la siguiente configuración:
   KEY: Token  VALUE: Es el token devuelto en el recurso: api/Seguridad/GenerarToken

   Este método obtiene el reporte mensual tabulado
   Method: GET 
   URL: http://host:puerto/api/Recaudos/ReporteMensual
   Agregar en el header la siguiente configuración:
   KEY: Token  VALUE: Es el token devuelto en el recurso: api/Seguridad/GenerarToken

