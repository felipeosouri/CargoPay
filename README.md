# 🚀 CargoPay API

API REST desarrollada en ASP.NET Core (.NET 8) que permite gestionar tarjetas, realizar pagos, generar fees dinámicos, y autenticarse mediante JWT.

---

## ✅ Requisitos

- [Visual Studio 2022](https://visualstudio.microsoft.com/)
- [.NET 8 o superior](https://dotnet.microsoft.com/en-us/download)
- [SQL Server Express o equivalente](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Postman](https://www.postman.com/) o [Thunder Client](https://www.thunderclient.com/)
- Git

---

## 📦 Clonar el repositorio

```bash
git clone https://github.com/felipeosouri/CargoPay.git
cd CargoPay

---

## ⚙️ Configuración inicial

### 1. Configura `appsettings.json` en el proyecto `CargoPay.Api`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR\\SQLEXPRESS;Database=CargoPayDb;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Jwt": {
    "Key": "ThisIsASuperSecureJwtKey1234567890"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

> 🔐 Asegúrate de usar una clave JWT de **al menos 32 caracteres** y reemplaza `TU_SERVIDOR` por el nombre de tu instancia SQL.

---

## 🛠 Migraciones y base de datos

1. Abre la solución en Visual Studio (`CargoPay.sln`)
2. Abre la **Package Manager Console** y ejecuta:

```powershell
Update-Database -Project CargoPay.Infrastructure
```

Esto aplicará las migraciones y creará la base de datos `CargoPayDb`.

---

## ▶️ Ejecutar la API

- Presiona `F5` o ejecuta `CargoPay.Api` desde Visual Studio.
- Swagger se abrirá en:  
  `http://localhost:5095/swagger`

---

## 🔐 Usuario inicial (seeder)

La base de datos se inicializa automáticamente con el siguiente usuario:

```json
{
  "username": "admin",
  "password": "admin"
}
```

---

## 🧪 Probar la API desde Postman

### 1. Obtener el token JWT

- **POST** `http://localhost:5095/api/auth/login`
- Body → JSON (raw):
```json
{
  "username": "admin",
  "password": "admin"
}
```

> 🔁 Copia el valor de `"token"` que retorna la respuesta.

---

### 2. Usar el token para consumir los endpoints protegidos

#### ✅ Crear tarjeta

- **POST** `http://localhost:5095/api/card/create`  
- Headers:
  - `Authorization: Bearer TU_TOKEN`

#### ✅ Consultar balance

- **GET** `http://localhost:5095/api/card/balance/{cardNumber}`  
- Headers:
  - `Authorization: Bearer TU_TOKEN`

#### ✅ Realizar pago

- **POST** `http://localhost:5095/api/card/pay`  
- Headers:
  - `Authorization: Bearer TU_TOKEN`
- Body:
```json
{
  "cardNumber": "YOUR_CARD_NUMBER",
  "amount": YOUR_AMOUNT
}
```

#### ✅ Ver fee actual

- **GET** `http://localhost:5095/api/fee/current`  
- Headers:
  - `Authorization: Bearer TU_TOKEN`

---

## 📦 Librerías utilizadas

- `Microsoft.AspNetCore.Identity` – Hash de contraseñas
- `FluentValidation` – Validaciones automáticas
- `Serilog` – Logging estructurado en consola y archivos
- `Entity Framework Core` – ORM para SQL Server
- `Swashbuckle` – Documentación Swagger
- `JWT Bearer Auth` – Seguridad basada en tokens

---

## 👨‍💻 Estructura del proyecto

```
CargoPay.Api/              --> API principal (controladores, config)
CargoPay.Application/      --> Servicios, interfaces, validaciones
CargoPay.Infrastructure/   --> EF DbContext, migraciones, seeders
CargoPay.Domain/           --> Entidades base del dominio
```
---

## 📮 Contacto

Desarrollado por [Juan Felipe Osorio Uribe].  
Para soporte técnico: [jfosorio@outlook.com]
