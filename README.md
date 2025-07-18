# ToDoListOnOff

ToDoListOnOff es un proyecto básico con autenticación para que cada usuario cree sus tareas y pueda marcarlas como realizadas o pendientes.

## Arquitectura y decisiones técnicas

Para el backend se tomaron las siguientes decisiones arquitectónicas:

- **Ports & Adapters (Arquitectura Hexagonal)**
- **CQRS** (Command Query Responsibility Segregation)

Además, se implementaron los siguientes patrones de diseño:

- Repository
- Mediador
- UnitOfWork
- Strategy

El proyecto utiliza Entity Framework Core como ORM para la persistencia de datos. Se aplicaron principios de Clean Code y todo el código está documentado para facilitar su comprensión y mantenimiento.

## Instalación y uso

Para instalar y ejecutar el proyecto correctamente, sigue los siguientes pasos:

1. **Validar la cadena de conexión**

   Abre el archivo `appsettings.json` ubicado en el proyecto `ToDoListOnOff.Api` y reemplaza el valor de la cadena de conexión con una válida para tu entorno. Por ejemplo:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=TU_SERVIDOR;Database=ToDoListOnOffDb;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

2. **Instalar dotnet ef**

   Asegúrate de tener instalado el CLI de Entity Framework Core. Si no lo tienes, instálalo con el siguiente comando:

   ```bash
   dotnet tool install --global dotnet-ef
   ```

3. **Ejecutar las migraciones**

   Desde la ruta de la carpeta de la API, que se llama `ToDoListOnOff.Api`, ejecuta los siguientes comandos:

   ```bash
   dotnet ef migrations add FirstMigration
   dotnet ef database update
   ```

   Esto creará la base de datos y aplicará la migración inicial.

4. **Ejecutar la aplicación**

   Una vez configurado todo lo anterior, puedes ejecutar la API con el siguiente comando:

   ```bash
   dotnet run --project ToDoListOnOff.Api
   ```

   La API estará disponible y lista para recibir peticiones.

## Estructura del proyecto

El proyecto está organizado de la siguiente manera:

```
ToDoListOnOff/
├── ToDoListOnOff.Api/            → Proyecto API (endpoints, configuración, etc.)
├── ToDoListOnOff.Application/    → Lógica de negocio (CQRS, handlers, validaciones)
├── ToDoListOnOff.Domain/         → Entidades, interfaces, contratos del dominio
├── ToDoListOnOff.Infrastructure/ → Implementaciones técnicas (repositorios, EF Core, estrategias)
```

## Requisitos

- .NET 9 o superior
- SQL Server
- Entity Framework Core CLI (`dotnet-ef`)

