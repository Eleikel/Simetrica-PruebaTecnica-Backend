

# - Preview de la Prueba Tecnica de Simetrica Consulting (.NET 8) By:  [Victor Esleiker Diaz Santana](https://www.linkedin.com/in/esleiker-diaz-34a636237/)

## ● ¿Como Autenticarse?

![How-to-Authenticate](https://github.com/Eleikel/Simetrica-PruebaTecnica-Backend/assets/80076300/0a24fad9-0b2b-4d16-9cb8-0068697bdb7b)


## ● Agregar nueva Tarea

![Add-task](https://github.com/Eleikel/Simetrica-PruebaTecnica-Backend/assets/80076300/55ea1fb4-5f0c-4d6f-9aa1-f25c91de0bad)


## ● Obteniendo la tarea registrada

![Get-Task](https://github.com/Eleikel/Simetrica-PruebaTecnica-Backend/assets/80076300/48a1eed8-6482-4182-a8ac-2805eddabea1)




# - Configuración el API (.NET 8)

Este README proporciona instrucciones paso a paso para configurar correctamente un proyecto ASP.NET Core 6 API.

## Paso 1: Cambiar el nombre del servidor en la cadena de conexión

1. Abre el archivo `appsettings.json` en el proyecto.
2. Busca la sección `"ConnectionStrings"` y dentro de ella, el campo `"DefaultConnection"`.
3. Modifica el valor del campo `"Server"` con el nombre del servidor al que deseas conectarte.

## Paso 2: Configuración de las migraciones

1. Abre una terminal en la raíz del proyecto.
2. Ejecuta el siguiente comando para crear las migraciones iniciales:

   ```bash
   dotnet ef migrations add InitialCreate
3. Una vez que las migraciones se han creado correctamente, aplica las migraciones a la base de datos con el siguiente comando:
   
   ```bash
   dotnet ef database update

## Paso 3: Ejecutar el proyecto

1. Asegúrate de que estás en el directorio raíz del proyecto.
2. Ejecuta el siguiente comando para iniciar la aplicación:
   
   ```bash
   dotnet run

Esto compilará y ejecutará la aplicación ASP.NET Core 8 API.




---




# - Como Ejecutar Las Pruebas Unitarias

A continuacion se estara dando las instrucciones de cómo configurar y ejecutar pruebas unitarias utilizando xUnit en un proyecto .NET 8.


## Desde Visual Studio

1. Abre la solución del proyecto en Visual Studio.

2. Ve a Test > Run All Tests o `presiona Ctrl + R, A` para ejecutar todas las pruebas.

![How-to-run-test](https://github.com/Eleikel/Simetrica-PruebaTecnica-Backend/assets/80076300/0a1dd07d-73b0-4b3f-8f13-0cce5445be37)

   
