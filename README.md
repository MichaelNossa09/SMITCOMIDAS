# SMITCOMIDAS

Sistema de Gestión de Pedidos de Comida para Empresas e Instituciones

## 📋 Descripción

SMITCOMIDAS es una solución integral de gestión de pedidos de comida corporativa que permite a las empresas administrar eficientemente el servicio de alimentación para sus empleados. El sistema facilita la creación y gestión de menús por parte de proveedores, el registro de pedidos por empleados con control de cuotas mensuales, y el seguimiento completo del ciclo de vida de las órdenes.

## 🎯 Características Principales

- ✅ **Gestión de Menús** - Los proveedores pueden crear y administrar menús con disponibilidad por día y tipo de comida
- ✅ **Sistema de Pedidos** - Empleados realizan pedidos con seguimiento de estados en tiempo real
- ✅ **Control de Cuotas** - Límite de pedidos mensuales por empleado
- ✅ **Multi-tenant** - Soporte para múltiples compañías y centros de costo
- ✅ **Roles y Permisos** - Sistema de autorización basado en roles (Admin, Administrativo, Operativo, Proveedor)
- ✅ **Aplicación Móvil** - App multiplataforma para Android, iOS y Windows
- ✅ **API RESTful** - Backend escalable con autenticación JWT
- ✅ **UI Adaptativa** - Interfaz responsiva optimizada para web y móvil

## 🛠️ Tecnologías

### Frontend
- **.NET 9.0**
- **Blazor** (Server e Híbrido)
- **Razor Components**
- **Bootstrap 5**

### Backend
- **ASP.NET Core 9.0** Web API
- **Entity Framework Core 9.0**
- **ASP.NET Core Identity**
- **JWT Authentication**

### Base de Datos
- **SQL Server**
- **Entity Framework Core Migrations**

### Mobile
- **.NET MAUI** (Multi-platform App UI)
- **Blazor WebView**
- Soporta: Android, iOS, Windows, MacCatalyst

## 📁 Estructura del Proyecto

```
SMITCOMIDAS/
├── src/SMITCOMIDAS/
│   ├── SMITCOMIDAS.Web/          # Backend API + Frontend Web
│   │   ├── Controllers/           # Controladores REST API
│   │   ├── Data/                  # DbContext y migraciones
│   │   ├── Services/              # Lógica de negocio
│   │   ├── Migrations/            # Migraciones EF Core
│   │   └── wwwroot/               # Archivos estáticos
│   │
│   ├── SMITCOMIDAS/               # Aplicación móvil MAUI
│   │   ├── Platforms/             # Código específico por plataforma
│   │   ├── Resources/             # Iconos, splash, fuentes
│   │   └── MauiProgram.cs         # Configuración MAUI
│   │
│   └── SMITCOMIDAS.Shared/        # Código compartido
│       ├── Models/                # Entidades y DTOs
│       ├── Services/              # Interfaces de servicios
│       ├── Pages/                 # Páginas Blazor
│       ├── Components/            # Componentes UI reutilizables
│       └── Layout/                # Layouts de la aplicación
│
└── SMITCOMIDAS.sln                # Archivo de solución
```

## 🗄️ Modelo de Datos

### Entidades Principales

- **ApplicationUser** - Usuarios del sistema (basado en ASP.NET Identity)
- **Persona** - Perfiles de empleados con información personal y cuotas
- **Proveedor** - Proveedores de servicios de comida
- **Compania** - Empresas/organizaciones
- **CentroCosto** - Centros de costo de las compañías
- **Menu** - Menús con fechas de vigencia y estados
- **ElementoMenu** - Platillos/items individuales de los menús
- **DisponibilidadElemento** - Disponibilidad de items por día y tipo de comida
- **Pedido** - Órdenes de comida con workflow de estados
- **DetallePedido** - Detalles de items en cada pedido

### Estados de Pedidos

El sistema maneja un workflow completo de estados:

```
Pendiente → Confirmado → En Preparación → Listo → Entregado → Recibido
```

## 🚀 Instalación y Configuración

### Prerrequisitos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/sql-server) (Express o superior)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/)
- Para desarrollo móvil: Android SDK, Xcode (macOS)

### Pasos de Instalación

1. **Clonar el repositorio**
   ```bash
   git clone <repository-url>
   cd SMITCOMIDAS
   ```

2. **Configurar la base de datos**

   Editar `src/SMITCOMIDAS/SMITCOMIDAS.Web/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=TU_SERVIDOR;Database=SMITCOMIDAS;Trusted_Connection=True;TrustServerCertificate=True"
     }
   }
   ```

3. **Aplicar migraciones**
   ```bash
   cd src/SMITCOMIDAS/SMITCOMIDAS.Web
   dotnet ef database update
   ```

4. **Ejecutar el proyecto web**
   ```bash
   dotnet run --project src/SMITCOMIDAS/SMITCOMIDAS.Web
   ```

5. **Ejecutar la aplicación móvil (opcional)**
   ```bash
   cd src/SMITCOMIDAS/SMITCOMIDAS
   dotnet build -t:Run -f net9.0-android
   ```

## 🔐 Autenticación

El sistema utiliza JWT (JSON Web Tokens) para autenticación.

### Usuario Administrador por Defecto

```
Email: admin@smitco.com.co
Password: Admin123!
```

### Roles Disponibles

- **Admin** - Acceso completo al sistema
- **Administrativo** - Gestión administrativa
- **Operativo** - Operaciones del día a día
- **Proveedor** - Gestión de menús y pedidos de proveedores

## 📡 API Endpoints

### Autenticación
- `POST /api/auth/login` - Iniciar sesión
- `POST /api/auth/register` - Registrar nuevo usuario

### Pedidos
- `GET /api/pedidos` - Listar todos los pedidos (Admin)
- `GET /api/pedidos/usuario` - Pedidos del usuario actual
- `GET /api/pedidos/{id}` - Detalle de un pedido
- `POST /api/pedidos` - Crear nuevo pedido
- `PUT /api/pedidos/{id}` - Actualizar pedido
- `PATCH /api/pedidos/{id}/estado` - Cambiar estado del pedido

### Menús
- `GET /api/menus` - Listar menús
- `GET /api/menus/{id}` - Detalle de menú
- `POST /api/menus` - Crear menú
- `PUT /api/menus/{id}` - Actualizar menú
- `DELETE /api/menus/{id}` - Eliminar menú

### Otros Endpoints
- `/api/personas` - Gestión de empleados
- `/api/proveedores` - Gestión de proveedores
- `/api/companias` - Gestión de compañías
- `/api/centroscosto` - Gestión de centros de costo
- `/api/elementosmenu` - Gestión de elementos de menú
- `/api/roles` - Gestión de roles

## 🏗️ Arquitectura

El proyecto sigue los principios de **Arquitectura Limpia (Clean Architecture)** con separación de capas:

1. **Capa de Presentación** - Blazor pages y components (Shared)
2. **Capa API** - REST controllers (Web)
3. **Capa de Negocio** - Services (Web & Shared)
4. **Capa de Acceso a Datos** - EF Core DbContext (Web)
5. **Capa de Dominio** - Entities y DTOs (Shared)

### Patrones Implementados

- **Repository Pattern** - Via EF Core DbContext
- **Service Layer Pattern** - Separación interfaz/implementación
- **DTO Pattern** - Separación de DTOs y entidades
- **Dependency Injection** - A lo largo de toda la aplicación
- **Authentication & Authorization** - JWT + role-based access

## 🧪 Testing

> ⚠️ **Nota**: Actualmente el proyecto no tiene implementado un framework de testing. Se recomienda agregar:
> - Pruebas unitarias con xUnit o NUnit
> - Pruebas de integración para los API endpoints
> - Pruebas E2E para la UI

## 📱 Aplicación Móvil

La aplicación móvil está construida con .NET MAUI y comparte el código UI con la versión web mediante Blazor WebView.

### Configuración del API para Móvil

Por defecto, la app móvil está configurada para conectarse a:
```
http://10.0.2.2:5000/ (Android Emulator)
```

Para producción, actualizar en `MauiProgram.cs`.

## 🤝 Contribución

Si deseas contribuir al proyecto:

1. Fork el repositorio
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto es privado y confidencial.

## 👥 Contacto

Para preguntas o soporte, contactar al equipo de desarrollo.

---

**Desarrollado con .NET 9.0 y Blazor**
