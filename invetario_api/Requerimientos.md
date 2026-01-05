Perfecto, este es **un muy buen proyecto** para .NET + SQL Server y además **realista a nivel empresa**.
Te dejo los **requerimientos funcionales completos** de un **Sistema de Inventario y Ventas**, bien estructurados para que luego puedas diseñar BD, API y lógica sin improvisar.

---

# 📦 Sistema de Inventario y Ventas

## 📑 Requerimientos Funcionales

---

## 1️⃣ Gestión de Usuarios y Seguridad

### RF-01 Gestión de Usuarios

- El sistema debe permitir:

  - Crear usuarios [X]
  - Editar usuarios
  - Activar / desactivar usuarios

- Campos mínimos:

  - Nombre
  - Email
  - Usuario
  - Contraseña (encriptada)

---

### RF-02 Roles y Permisos

- El sistema debe manejar roles:

  - Administrador
  - Almacén
  - Ventas
  - Auditor (solo lectura)

- Cada rol tendrá permisos configurables:

  - Acceso a módulos
  - Acciones (crear, editar, eliminar, ver)

---

## 2️⃣ Catálogos Base

### RF-03 Gestión de Productos

- El sistema debe permitir:

  - Crear, editar, eliminar productos [X]

- Campos:

  - Código interno
  - Código de barras
  - Nombre
  - Descripción
  - Categoría
  - Unidad de medida
  - Precio de compra
  - Precio de venta
  - Stock mínimo
  - Estado (activo/inactivo)

---

### RF-04 Categorías y Unidades

- Gestión de:

  - Categorías de producto [X]
  - Unidades de medida (UND, KG, LT, etc.) [X]

---

## 3️⃣ Almacenes e Inventario

### RF-05 Gestión de Almacenes

- El sistema debe permitir:

  - Crear múltiples almacenes [X]
  - Asignar productos a almacenes [X]

| Atributo                        | Tipo / Formato | Descripción                                  |
| ------------------------------- | -------------- | -------------------------------------------- |
| **AlmacenId**                   | INT / GUID     | Identificador único del almacén              |
| **Nombre**                      | VARCHAR        | Nombre del almacén (Ej: "Almacén Central")   |
| **Código**                      | VARCHAR        | Código corto interno (Ej: "ALM-C")           |
| **Dirección**                   | VARCHAR        | Dirección física o ubicación                 |
| **Ciudad / Provincia / Región** | VARCHAR        | Para clasificar geográficamente              |
| **Teléfono / Contacto**         | VARCHAR        | Contacto responsable del almacén             |
| **Email**                       | VARCHAR        | Contacto electrónico                         |
| **Capacidad Máxima**            | DECIMAL        | Capacidad total en unidades / m³             |
| **Estado / Activo**             | BIT / BOOLEAN  | Activo o desactivado                         |
| **Tipo de Almacén**             | ENUM           | Ej: Principal, Secundario, Externo, Temporal |
| **Encargado / Responsable**     | FK a Usuario   | Persona responsable del almacén              |
| **Fecha de Creación**           | DATETIME       | Registro histórico                           |
| **Fecha de Actualización**      | DATETIME       | Última modificación                          |
| **Observaciones**               | TEXT           | Notas adicionales sobre el almacén           |

| Atributo                  | Tipo sugerido (SQL Server)      | Descripción                                |
| ------------------------- | ------------------------------- | ------------------------------------------ |
| **ProductStoreId**        | INT IDENTITY / UNIQUEIDENTIFIER | Identificador único                        |
| **ProductId**             | INT (FK)                        | Producto asociado                          |
| **StoreId**               | INT (FK)                        | Almacén asociado                           |
| **StockActual**           | DECIMAL(18,2)                   | Stock disponible actual                    |
| **StockReservado**        | DECIMAL(18,2)                   | Stock comprometido (ventas/pedidos)        |
| **StockDisponible**       | DECIMAL(18,2)                   | StockActual - StockReservado               |
| **StockMinimo**           | DECIMAL(18,2)                   | Stock mínimo permitido                     |
| **StockMaximo**           | DECIMAL(18,2)                   | Límite máximo recomendado                  |
| **CostoPromedio**         | DECIMAL(18,6)                   | Costo promedio del producto en ese almacén |
| **UltimoCosto**           | DECIMAL(18,6)                   | Último costo de compra                     |
| **Estado**                | BIT                             | Activo / Inactivo                          |
| **FechaUltimoMovimiento** | DATETIME                        | Última entrada o salida                    |
| **CreadoPor**             | INT (FK Usuario)                | Usuario que creó el registro               |
| **FechaCreacion**         | DATETIME                        | Fecha de creación                          |

---

### RF-06 Control de Stock

- El sistema debe:

  - Mantener stock por **producto y almacén**
  - Actualizar stock automáticamente con cada movimiento
  - Evitar stock negativo (configurable)

---

### RF-07 Kardex / Movimientos

- Registrar cada movimiento de inventario:

  - Entrada
  - Salida
  - Ajuste
  - Transferencia

- Cada movimiento debe guardar:

  - Fecha
  - Tipo de movimiento
  - Documento origen
  - Usuario
  - Cantidad
  - Stock anterior
  - Stock posterior

---

## 4️⃣ Órdenes de Entrada (Compras)

### RF-08 Registro de Órdenes de Entrada

- El sistema debe permitir:

  - Registrar órdenes de entrada (compras) [X]
  - Asociar proveedor [X]
  - Registrar múltiples productos por orden [X]

  | Atributo                    | Tipo de Dato (SQL Server)       | Requerido | Descripción                      |
  | --------------------------- | ------------------------------- | --------- | -------------------------------- |
  | **CompraId**                | INT IDENTITY / UNIQUEIDENTIFIER | Sí        | Identificador único de la compra |
  | **ProveedorId**             | INT                             | Sí        | Proveedor asociado               |
  | **AlmacenId**               | INT                             | Sí        | Almacén destino                  |
  | **FechaCompra**             | DATETIME                        | Sí        | Fecha del documento              |
  | **FechaRegistro**           | DATETIME                        | Sí        | Fecha de registro en sistema     |
  | **TipoDocumento**           | VARCHAR(20)                     | Sí        | Factura, Boleta, Guía            |
  | **Moneda**                  | VARCHAR(10)                     | Sí        | Moneda de la compra              |
  | **CondicionPago**           | VARCHAR(50)                     | Sí        | Contado / Crédito                |
  | **Impuesto**                | DECIMAL(18,2)                   | Sí        | IGV / IVA                        |
  | **Total**                   | DECIMAL(18,2)                   | Sí        | Total del documento              |
  | **EstadoCompra**            | VARCHAR(20)                     | Sí        | Pendiente, Confirmada, Anulada   |
  | **Observaciones**           | VARCHAR(MAX)                    | No        | Comentarios                      |
  | **FechaActualizacion**      | DATETIME                        | No        | Auditoría                        |
  | **CreadoPorUsuarioId**      | INT                             | Sí        | Usuario creador                  |
  | **ActualizadoPorUsuarioId** | INT                             | No        | Usuario editor                   |

### Detalle de la compra

| Atributo                    | Tipo de Dato (SQL Server)       | Requerido | Descripción                     |
| --------------------------- | ------------------------------- | --------- | ------------------------------- |
| **CompraDetalleId**         | INT IDENTITY / UNIQUEIDENTIFIER | Sí        | Identificador único del detalle |
| **CompraId**                | INT                             | Sí        | Referencia a la compra          |
| **ProductoId**              | INT                             | Sí        | Producto comprado               |
| **Cantidad**                | DECIMAL(18,4)                   | Sí        | Cantidad comprada               |
| **CostoUnitario**           | DECIMAL(18,4)                   | Sí        | Precio de compra unitario       |
| **Subtotal**                | DECIMAL(18,2)                   | Sí        | Cantidad × CostoUnitario        |
| **Impuesto**                | DECIMAL(18,2)                   | Sí        | IGV / IVA del ítem              |
| **Total**                   | DECIMAL(18,2)                   | Sí        | Total del ítem                  |
| **EstadoDetalle**           | VARCHAR(20)                     | Sí        | Pendiente, Recibido, Anulado    |
| **FechaCreacion**           | DATETIME                        | Sí        | Auditoría                       |
| **FechaActualizacion**      | DATETIME                        | No        | Auditoría                       |
| **CreadoPorUsuarioId**      | INT                             | Sí        | Usuario creador                 |
| **ActualizadoPorUsuarioId** | INT                             | No        | Usuario editor                  |

---

### RF-09 Confirmación de Entrada

- Al confirmar una orden:

  - Se incrementa el stock [X]
  - Se generan movimientos de inventario
  - La orden queda en estado **Confirmada** [X]

- Estados:

  - Pendiente
  - Confirmada
  - Anulada

---

## 5️⃣ Órdenes de Salida (Despachos)

### RF-10 Registro de Órdenes de Salida

- Permitir registrar salidas por:

  - Venta
  - Consumo interno
  - Merma

  | Atributo                    | Tipo de Dato (SQL Server)       | Requerido | Descripción                                 |
  | --------------------------- | ------------------------------- | --------- | ------------------------------------------- |
  | **OrdenSalidaId**           | INT IDENTITY / UNIQUEIDENTIFIER | Sí        | Identificador único                         |
  | **NumeroOrdenSalida**       | VARCHAR(30)                     | Sí        | Correlativo del documento                   |
  | **TipoSalida**              | VARCHAR(30)                     | Sí        | Venta, ConsumoInterno, Merma, Transferencia |
  | **ClienteId**               | INT                             | No        | Cliente (si es venta)                       |
  | **FechaSalida**             | DATETIME                        | Sí        | Fecha del movimiento                        |
  | **FechaRegistro**           | DATETIME                        | Sí        | Fecha en sistema                            |
  | **MotivoSalida**            | VARCHAR(150)                    | No        | Motivo o referencia                         |
  | **DocumentoReferencia**     | VARCHAR(50)                     | No        | Factura, pedido, OT                         |
  | **EstadoOrdenSalida**       | VARCHAR(20)                     | Sí        | Pendiente, Confirmada, Anulada              |
  | **Subtotal**                | DECIMAL(18,2)                   | No        | Subtotal (si aplica)                        |
  | **Impuesto**                | DECIMAL(18,2)                   | No        | IGV / IVA                                   |
  | **Total**                   | DECIMAL(18,2)                   | No        | Total                                       |
  | **Observaciones**           | VARCHAR(MAX)                    | No        | Comentarios                                 |
  | **Activo**                  | BIT                             | Sí        | Registro activo                             |
  | **CreadoPorUsuarioId**      | INT                             | Sí        | Usuario creador                             |
  | **ActualizadoPorUsuarioId** | INT                             | No        | Usuario editor                              |

  - Detalle de Venta

    | Atributo                    | Tipo de Dato (SQL Server)       | Requerido | Descripción                    |
    | --------------------------- | ------------------------------- | --------- | ------------------------------ |
    | **OrdenSalidaDetalleId**    | INT IDENTITY / UNIQUEIDENTIFIER | Sí        | Identificador único            |
    | **OrdenSalidaId**           | INT                             | Sí        | Cabecera de la orden           |
    | **AlmacenId**               | INT                             | Sí        | Almacén origen del producto    |
    | **ProductoId**              | INT                             | Sí        | Producto despachado            |
    | **UnidadMedida**            | VARCHAR(20)                     | Sí        | UND, KG, LT                    |
    | **CantidadSolicitada**      | DECIMAL(18,4)                   | Sí        | Cantidad solicitada            |
    | **CantidadDespachada**      | DECIMAL(18,4)                   | No        | Cantidad real despachada       |
    | **CostoUnitario**           | DECIMAL(18,4)                   | No        | Costo para valorización        |
    | **PrecioUnitario**          | DECIMAL(18,4)                   | No        | Precio de venta (si aplica)    |
    | **Subtotal**                | DECIMAL(18,2)                   | No        | Cantidad × Precio              |
    | **Impuesto**                | DECIMAL(18,2)                   | No        | IGV / IVA                      |
    | **Total**                   | DECIMAL(18,2)                   | No        | Total del ítem                 |
    | **Lote**                    | VARCHAR(50)                     | No        | Lote del producto              |
    | **FechaVencimiento**        | DATE                            | No        | Fecha de vencimiento           |
    | **EstadoDetalle**           | VARCHAR(20)                     | Sí        | Pendiente, Despachado, Anulado |
    | **Observaciones**           | VARCHAR(MAX)                    | No        | Comentarios                    |
    | **Activo**                  | BIT                             | Sí        | Registro activo                |
    | **FechaCreacion**           | DATETIME                        | Sí        | Auditoría                      |
    | **FechaActualizacion**      | DATETIME                        | No        | Auditoría                      |
    | **CreadoPorUsuarioId**      | INT                             | Sí        | Usuario creador                |
    | **ActualizadoPorUsuarioId** | INT                             | No        | Usuario editor                 |

---

### RF-11 Validación de Stock

- Antes de confirmar una salida:

  - Validar stock disponible
  - Bloquear si no hay stock suficiente

---

### RF-12 Confirmación de Salida

- Al confirmar:

  - Se descuenta stock
  - Se registra movimiento de inventario
  - Cambia estado de la orden

---

## 6️⃣ Ventas y Facturación

### RF-13 Registro de Ventas

- El sistema debe permitir:

  - Crear ventas
  - Asociar cliente
  - Registrar detalle de productos

- Cálculo automático:

  - Subtotal
  - Impuestos
  - Total

---

### RF-14 Impacto en Inventario

- Cada venta confirmada:

  - Genera una orden de salida
  - Actualiza stock
  - Registra kardex

---

## 7️⃣ Clientes y Proveedores

### RF-15 Gestión de Clientes

- Crear, editar, desactivar clientes [X]
- Datos básicos (nombre, documento, contacto) [X]

| Atributo                    | Tipo de Dato (SQL Server)       | Requerido | Descripción                     |
| --------------------------- | ------------------------------- | --------- | ------------------------------- |
| **ClienteId**               | INT IDENTITY / UNIQUEIDENTIFIER | Sí        | Identificador único del cliente |
| **TipoCliente**             | VARCHAR(30)                     | Sí        | Natural / Empresa               |
| **NombreComercial**         | VARCHAR(150)                    | No        | Nombre comercial                |
| **TipoDocumento**           | VARCHAR(20)                     | Sí        | DNI, RUC, Pasaporte             |
| **NumeroDocumento**         | VARCHAR(20)                     | Sí        | Documento fiscal                |
| **Telefono**                | VARCHAR(30)                     | No        | Teléfono principal              |
| **Email**                   | VARCHAR(150)                    | No        | Correo principal                |
| **Activo**                  | BIT                             | Sí        | Cliente activo/inactivo         |
| **FechaCreacion**           | DATETIME                        | Sí        | Fecha de registro               |
| **FechaActualizacion**      | DATETIME                        | No        | Última modificación             |
| **CreadoPorUsuarioId**      | INT                             | Sí        | Usuario creador                 |
| **ActualizadoPorUsuarioId** | INT                             | No        | Usuario editor                  |

---

### RF-16 Gestión de Proveedores

- Crear, editar, desactivar proveedores [X]

| Atributo                    | Tipo de Dato (SQL Server)       | Requerido | Descripción                       |
| --------------------------- | ------------------------------- | --------- | --------------------------------- |
| **ProveedorId**             | INT IDENTITY / UNIQUEIDENTIFIER | Sí        | Identificador único del proveedor |
| **CodigoProveedor**         | VARCHAR(20)                     | Sí        | Código interno del proveedor      |
| **RazonSocial**             | VARCHAR(150)                    | Sí        | Nombre legal del proveedor        |
| **NombreComercial**         | VARCHAR(150)                    | No        | Nombre comercial                  |
| **TipoDocumento**           | VARCHAR(20)                     | Sí        | RUC, DNI, Pasaporte               |
| **NumeroDocumento**         | VARCHAR(20)                     | Sí        | Número de documento fiscal        |
| **Telefono**                | VARCHAR(30)                     | No        | Teléfono principal                |
| **Email**                   | VARCHAR(150)                    | No        | Correo principal                  |
| **ContactoPrincipal**       | VARCHAR(150)                    | No        | Persona de contacto               |
| **TelefonoContacto**        | VARCHAR(30)                     | No        | Teléfono del contacto             |
| **CondicionPago**           | VARCHAR(50)                     | Sí        | Contado, Crédito 30/60 días       |
| **Moneda**                  | VARCHAR(10)                     | Sí        | Moneda de operación               |
| **PlazoEntregaDias**        | INT                             | No        | Días promedio de entrega          |
| **Activo**                  | BIT                             | Sí        | Proveedor activo/inactivo         |
| **Observaciones**           | VARCHAR(MAX)                    | No        | Notas internas                    |
| **FechaCreacion**           | DATETIME                        | Sí        | Fecha de creación                 |
| **FechaActualizacion**      | DATETIME                        | No        | Última modificación               |
| **CreadoPorUsuarioId**      | INT                             | Sí        | Usuario que creó el registro      |
| **ActualizadoPorUsuarioId** | INT                             | No        | Usuario que actualizó             |

---

## 8️⃣ Reportes

### RF-17 Reporte de Inventario

- Mostrar:

  - Stock actual por producto y almacén
  - Productos bajo stock mínimo

---

### RF-18 Reporte de Kardex

- Mostrar movimientos por:

  - Producto
  - Rango de fechas
  - Tipo de movimiento

---

### RF-19 Reporte de Ventas

- Ventas por:

  - Fecha
  - Cliente
  - Producto

- Totales y acumulados

---

## 9️⃣ Auditoría y Trazabilidad

### RF-20 Auditoría

- Registrar:

  - Usuario
  - Fecha
  - Acción
  - Entidad afectada

---

### RF-21 Historial de Cambios

- Guardar cambios de:

  - Precios
  - Stock manual
  - Estados de órdenes

---

## 🔟 Reglas de Negocio Clave

### RN-01

- No se puede eliminar un producto con movimientos

### RN-02

- No se puede confirmar una orden ya confirmada

### RN-03

- El stock no puede ser negativo (configurable)

### RN-04

- Toda modificación de stock debe generar kardex

---

## 🎯 Extras (Nivel Senior)

- FIFO / Promedio ponderado
- Transferencias entre almacenes
- Importación masiva (Excel)
- Jobs de cierre mensual
- Cache de consultas frecuentes

---

## 🚀 Siguiente paso recomendado

Puedo ayudarte a:
1️⃣ Diseñar el **modelo de base de datos (SQL Server)**
2️⃣ Crear la **arquitectura en .NET (Clean Architecture)**
3️⃣ Definir **endpoints REST reales**
4️⃣ Escribir **queries avanzadas (Kardex, FIFO, acumulados)**

👉 Dime **con qué quieres continuar** y lo armamos como proyecto real de producción 💪
