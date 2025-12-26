# Order Lookup POC

Sistema de consulta de pedidos para atención al cliente. Aplicación full-stack con Next.js y .NET.

## Tech Stack

| Frontend | Backend |
|----------|---------|
| Next.js 14 | .NET 8 |
| TypeScript | Entity Framework Core |
| Material UI | SQLite |
| Axios | JWT Authentication |

## Estructura del Proyecto

```
├── backend/
│   └── OrderLookup.API/    # API REST .NET 8
├── frontend/               # App Next.js
└── README.md
```

## Instalación y Ejecución

### Backend

```bash
cd backend/OrderLookup.API
dotnet restore
dotnet run
```
API disponible en `http://localhost:5000`

### Frontend

```bash
cd frontend
npm install
npm run dev
```
App disponible en `http://localhost:3000`

## Credenciales de Prueba

| Usuario | Contraseña | Rol |
|---------|------------|-----|
| admin | admin123 | Administrador |
| agent1 | agent123 | Atención al Cliente |
| agent2 | agent123 | Atención al Cliente |

## Pedidos de Ejemplo

- `ORD-2024-78542` - Pedido VIP entregado (múltiples productos)
- `ORD-2024-79123` - Pedido mayorista en proceso
- `ORD-2024-79456` - Pedido con devolución (producto defectuoso)

## API Endpoints

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| POST | `/api/auth/login` | Iniciar sesión |
| GET | `/api/orders/{orderNumber}` | Obtener detalle de pedido |

## Características

- Autenticación JWT
- Búsqueda de pedidos por número
- Vista detallada con:
  - Información del cliente
  - Productos del pedido
  - Historial de estados
  - Información de envío
  - Detalles de pago
  - Notas de atención al cliente

## Licencia

Apache 2.0
