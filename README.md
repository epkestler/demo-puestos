# demo-puestos
Caso práctico árbol de jerarquía

## Definición

Se necesita desarrollar una aplicación que permita mostrar un árbol de jerarquía en base a una tabla 
que contiene información de las plazas/empleados de una empresa. Tomar en cuenta que la tabla es 
recursiva. Adicional también debe de permitir insertar, modificar y eliminar registros de dicha tabla.  

### 📊 Tabla de ejemplo

| Id  | Nombre        | IdPuesto     | NombrePuesto   | IdJefe |
|-----|---------------|--------------|----------------|--------|
| 1   | Juan Pérez    | 1            | Gerente        | NULL   |
| 2   | Ana Gómez     | 2            | Subgerente     | 1      |
| 3   | Carlos Ruiz   | 3            | Supervisor     | 2      |
| 4   | Marta López   | 4            | Analista       | 3      |
| 5   | Pedro Torres  | 4            | Analista       | 3      |


### Jerarquía de Empleados (Ejemplo)

- **1 – Gerente – Pedro**
  - **2 – Sub Gerente – Pablo**
    - **3 – Supervisor – Juan**
  - **4 – Sub Gerente – José**
    - **5 – Supervisor – Carlos**
    - **6 – Supervisor – Diego**

La aplicación debe de consistir en las siguientes capas:

### 🔹 Frontend
Realizarla en ASP.NET con MVC, contendrá las páginas donde se muestra el árbol y 
también donde se realicen las opciones de inserción, modificación, eliminación

### 🔹 Backend
Web service REST que se consumirá desde el Frontend para operaciones de base de 
datos y lógica de negocio 

### 🔹 Store Procedures en SQL Server
Que sean consumidos desde el Backend para obtener la estructura del árbol de jerarquía, y también las operaciones CRUD necesarias.


## Alcances

### ✅ Requisitos Incluidos

- Gestión completa de empleados (alta, edición, eliminación).
- Visualización jerárquica de empleados en forma de árbol estructurado.
- Interfaz visual funcional con diseño visual básico.
- Validaciones de dominio en formularios y endpoint de backend.

### ❌ Aspectos Fuera de Alcance (Mejoras a realizar)

- Implementación de login, control de roles ni autorización sobre recursos.
- Implementación de HTTPS forzado, JWT, ni cifrado de datos sensibles en tránsito o en reposo.
- Implementación gestión de identidades o sesiones persistentes.
- Implementación UX/UI: No incluye animaciones, microinteracciones, ni diseño centrado en usuario.


## 🏗️ Arquitectura del Sistema

La aplicación está diseñada utilizando una arquitectura por capas. 

### 🔹 Estructura de capas:

1. **Frontend (ASP.NET Core MVC)** [ApiEmpleado] (WebEmpleados/)
   - Responsable de la interfaz de usuario.
   - Contiene vistas para mostrar la jerarquía en forma de árbol y en tabla.
   - Permite realizar operaciones de CRUD sobre empleados.
   - Se conecta con la API mediante llamadas HTTP. **configurar url en appsetting.json**

2. **Backend (API REST ASP.NET Core 8)** [ApiEmpleado](ApiEmpleados/)
   - Construido [Api basada en controladores](https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-8.0)
   - Capas
     - Contoller/ : Reúne gestion de endpoint y sus respuestas
     - Services/ : Lógica de Negocio
     - Model/ : Clases para las entidades (mapear tablas y resultados de consultas) 
   - Expone endpoints que manejan la lógica de negocio (Implementa Swagger).
   | Método | Ruta                  | Descripción                        | Parámetros                  |
   |--------|-----------------------|----------------------------------|-----------------------------|
   | GET    | `/api/empleado/lista` | Obtiene lista completa de empleados | Query params opcionales para filtros |
   | GET    | `/api/empleado/{id}`  | Obtiene detalle de un empleado por Id | `id` (int)                  |
   | POST   | `/api/empleado`       | Crea un nuevo empleado             | Cuerpo (JSON) con datos del empleado |
   | PUT    | `/api/empleado/{id}`  | Actualiza un empleado existente    | `id` (int), cuerpo JSON con datos actualizados |
   | DELETE | `/api/empleado/{id}`  | Elimina un empleado por Id         | `id` (int)                  |
   |--------|-----------------------|----------------------------------|-----------------------------|
   | GET    | `/api/puesto/lista`        | Obtiene lista completa de puestos |                             |
   
   - Se comunica con la base de datos mediante Dapper y procedimientos almacenados. **configurar cadena de conexion en appsetting.json**
   - Implementa [Middleware](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-8.0#exception-handler-lambda) para manejo de excepciones 

3. **Base de Datos (SQL Server)**
   - Contiene las tablas
     - `Empleado` `Puesto`.
   - Procedimientos almacenados para CRUD de las tablas.
   (/images/er.png)
   ***Ejecutar en orden del numeral los [script](script)***

### 🖼️ Diagrama de arquitectura

![Diagrama de Arquitectura](/images/arquitectura.png)

