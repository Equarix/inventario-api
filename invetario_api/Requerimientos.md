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
  - Asignar productos a almacenes

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

  - Registrar órdenes de entrada (compras)
  - Asociar proveedor
  - Registrar múltiples productos por orden

---

### RF-09 Confirmación de Entrada

- Al confirmar una orden:

  - Se incrementa el stock
  - Se generan movimientos de inventario
  - La orden queda en estado **Confirmada**

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

- Crear, editar, desactivar clientes
- Datos básicos (nombre, documento, contacto)

---

### RF-16 Gestión de Proveedores

- Crear, editar, desactivar proveedores

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
