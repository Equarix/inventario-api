"""
seed_inventariodb.py
====================
Genera datos de prueba ALEATORIOS con Faker para InventarioDB (SQL Server).
Cada ejecución produce datos diferentes y completamente consistentes entre tablas.

Instalación de dependencias:
    pip install pyodbc faker

Configuración de conexión:
    Ajusta las variables en la sección CONFIG antes de ejecutar.
"""

import random
import hashlib
from datetime import datetime, timedelta
from faker import Faker
import pyodbc

fake = Faker("es_ES")   # Locale peruano para datos realistas
Faker.seed()            # Seed aleatorio en cada ejecución

# ──────────────────────────────────────────────────────────────
# CONFIG  ← Ajusta aquí tu cadena de conexión
# ──────────────────────────────────────────────────────────────
SERVER           = r"localhost"
DATABASE         = "InventarioDB"
USERNAME         = "usersql"
PASSWORD         = "123"
USE_WINDOWS_AUTH = False   # True = autenticación Windows, False = SQL Auth
# ──────────────────────────────────────────────────────────────

# Cantidad de registros a generar (ajusta según necesites)
NUM_USERS         = 5
NUM_IMAGES        = 10
NUM_UNITS         = 6
NUM_CATEGORIES    = 8
NUM_STORES        = 4
NUM_PROVIDERS     = 8
NUM_CLIENTS       = 100
NUM_PAYMETHODS    = 6
NUM_PRODUCTS      = 300
NUM_BOXES         = 6
NUM_ENTRY_ORDERS  = 12
NUM_DEPART_ORDERS = 10
NUM_SALES         = 1500


# ─── Helpers ──────────────────────────────────────────────────

def get_connection():
    if USE_WINDOWS_AUTH:
        conn_str = (
            f"DRIVER={{ODBC Driver 17 for SQL Server}};"
            f"SERVER={SERVER};DATABASE={DATABASE};Trusted_Connection=yes;"
        )
    else:
        conn_str = (
            f"DRIVER={{ODBC Driver 17 for SQL Server}};"
            f"SERVER={SERVER};DATABASE={DATABASE};"
            f"UID={USERNAME};PWD={PASSWORD};"
        )
    return pyodbc.connect(conn_str)


def hash_pwd(plain: str) -> str:
    return hashlib.sha256(plain.encode()).hexdigest()


def rand_date(days_back: int = 180) -> datetime:
    return fake.date_time_between(start_date=f"-{days_back}d", end_date="now")


def insert_many(cursor, table: str, pk: str, columns: list, rows: list) -> list:
    """Inserta filas y devuelve lista de IDs generados (IDENTITY)."""
    col_list     = ", ".join([f"[{c}]" for c in columns])
    placeholders = ", ".join(["?"] * len(columns))
    sql = (
        f"INSERT INTO [{table}] ({col_list}) "
        f"OUTPUT INSERTED.[{pk}] "
        f"VALUES ({placeholders})"
    )
    ids = []
    for row in rows:
        cursor.execute(sql, row)
        ids.append(cursor.fetchone()[0])
    return ids


# ─── Secciones de seed ────────────────────────────────────────

def seed_users(cursor):
    print("  → Users")
    roles = [1, 2, 3]
    rows = [("admin@empresa.com", hash_pwd("Admin123!"), "Admin", "Sistema", 1, 1)]
    for _ in range(NUM_USERS - 1):
        rows.append((
            fake.unique.email(),
            hash_pwd(fake.password(length=10)),
            fake.first_name(),
            fake.last_name(),
            random.choice(roles),
            random.choice([1, 1, 1, 0]),
        ))
    return insert_many(cursor, "Users", "userId",
        ["email", "password", "firstName", "lastName", "role", "status"], rows)


def seed_images(cursor):
    print("  → Images")
    exts = ["jpg", "png", "webp"]
    rows = [
        (
            f"https://cdn.empresa.com/productos/{fake.uuid4()}.{random.choice(exts)}",
            rand_date(365),
            fake.slug(),
        )
        for _ in range(NUM_IMAGES)
    ]
    return insert_many(cursor, "Images", "imageId",
        ["imageUrl", "createdAt", "imageName"], rows)


def seed_units(cursor):
    print("  → Units")
    catalog = [
        ("Unidad", "Pieza individual"),
        ("Caja", "Caja de productos"),
        ("Docena", "12 unidades"),
        ("Kilogramo", "Peso en kg"),
        ("Litro", "Volumen en litros"),
        ("Pack", "Paquete múltiple"),
        ("Metro", "Longitud en metros"),
        ("Rollo", "Rollo de material"),
    ]
    sample = random.sample(catalog, k=min(NUM_UNITS, len(catalog)))
    rows = [(n, d, 1) for n, d in sample]
    return insert_many(cursor, "Units", "unitId",
        ["name", "description", "status"], rows)


def seed_categories(cursor):
    print("  → Categories")
    catalog = [
        ("Electrónica", "Dispositivos electrónicos"),
        ("Computadoras", "Equipos de cómputo"),
        ("Periféricos", "Accesorios para PC"),
        ("Almacenamiento", "Discos y memorias"),
        ("Oficina", "Artículos de oficina"),
        ("Audio y Video", "Equipos A/V"),
        ("Redes", "Equipos de conectividad"),
        ("Impresión", "Impresoras y consumibles"),
        ("Seguridad", "Cámaras y control de acceso"),
        ("Energía", "UPS y reguladores"),
    ]
    sample = random.sample(catalog, k=min(NUM_CATEGORIES, len(catalog)))
    rows = [(n, d, 1) for n, d in sample]
    return insert_many(cursor, "Categories", "categoryId",
        ["name", "description", "status"], rows)


def seed_stores(cursor, user_ids):
    print("  → Stores")
    types = ["Principal", "Tienda", "Deposito", "Punto de venta"]
    rows = [
        (
            fake.company() + " Store",
            fake.bothify("ALM-###"),
            fake.address().replace("\n", ", ")[:100],
            fake.numerify("#########")[:9],
            random.randint(200, 5000),
            1,
            random.choice(types),
            random.choice(user_ids),
            rand_date(365),
            fake.sentence(nb_words=6),
        )
        for _ in range(NUM_STORES)
    ]
    return insert_many(cursor, "Stores", "storeId",
        ["name", "code", "address", "phone", "maxCapacity",
         "status", "type", "userId", "createdAt", "observations"], rows)


def seed_storeusers(cursor, store_ids, user_ids):
    print("  → Storeusers")
    pairs = set()
    for s in store_ids:
        pairs.add((s, random.choice(user_ids)))
    for _ in range(len(store_ids) * 2):
        pairs.add((random.choice(store_ids), random.choice(user_ids)))
    rows = [(s, u, rand_date(365), 1) for s, u in pairs]
    return insert_many(cursor, "Storeusers", "StoreUserId",
        ["StoreId", "UserId", "CreatedAt", "status"], rows)


def seed_providers(cursor):
    print("  → Providers")
    rows = [
        (
            fake.bothify("PROV-###"),
            fake.company(),
            fake.company_suffix() + " " + fake.last_name(),
            "RUC",
            fake.numerify("20#########"),
            fake.address().replace("\n", ", ")[:80],
            fake.numerify("01#######"),
            fake.company_email(),
            fake.name(),
            fake.numerify("9########"),
            random.randint(1, 3),
            random.choice(["PEN", "USD"]),
            random.randint(3, 30),
            1,
            rand_date(365),
        )
        for _ in range(NUM_PROVIDERS)
    ]
    return insert_many(cursor, "Providers", "providerId",
        ["code", "companyName", "publicName", "typeDocument", "documentNumber",
         "address", "phone", "email", "mainContact", "contactPhone",
         "payCondition", "typeMoney", "daysDelivery", "status", "createdAt"], rows)


def seed_clients(cursor):
    print("  → Clients")
    rows = [(1, "Cliente General", "DNI", "00000000", "000000000", "sin@email.com", 1, rand_date(365))]
    for _ in range(NUM_CLIENTS - 1):
        ctype = random.choice([1, 2])
        if ctype == 1:
            name, doc_type, doc_num = fake.name(), "DNI", fake.numerify("########")
        else:
            name, doc_type, doc_num = fake.company(), "RUC", fake.numerify("20#########")
        rows.append((
            ctype, name, doc_type, doc_num,
            fake.numerify("9########"),
            fake.email(),
            random.choice([1, 1, 1, 0]),
            rand_date(365),
        ))
    return insert_many(cursor, "Clients", "clientId",
        ["clientType", "name", "typeDocument", "documentNumber",
         "phone", "email", "status", "createdAt"], rows)


def seed_paymethods(cursor):
    print("  → Paymethods")
    catalog = [
        ("Efectivo", 1, 0),
        ("Tarjeta de Crédito", 1, 0),
        ("Tarjeta de Débito", 1, 0),
        ("Transferencia Bancaria", 1, 0),
        ("Yape", 1, 0),
        ("Plin", 1, 0),
        ("Cheque", 1, 0),
        ("Crédito 30 días", 1, 0),
    ]
    sample = random.sample(catalog, k=min(NUM_PAYMETHODS, len(catalog)))
    return insert_many(cursor, "Paymethods", "paymethodId",
        ["name", "status", "turned"],
        [(n, s, t) for n, s, t in sample])


def seed_products(cursor, category_ids, unit_ids, image_ids):
    print("  → Products")
    product_names = [
        "Laptop", "Monitor", "Mouse", "Teclado", "Auriculares", "Webcam",
        "Disco SSD", "Memoria RAM", "CPU", "Tablet", "Impresora", "Scanner",
        "Router", "Switch", "Proyector", "UPS", "Cámara IP", "Micrófono",
        "Parlante", "Hub USB", "Cable HDMI", "Pad Mouse", "Silla Ergonómica",
        "Escritorio", "Regulador", "Servidor", "NAS", "Access Point", "Smart TV",
    ]
    brands = [
        "HP", "Dell", "Lenovo", "Samsung", "Logitech", "Asus",
        "Acer", "Sony", "LG", "Epson", "TP-Link", "Cisco",
        "Kingston", "Seagate", "Western Digital",
    ]
    rows = []
    used_codes: set = set()
    for _ in range(NUM_PRODUCTS):
        code = fake.bothify("??-#####").upper()
        while code in used_codes:
            code = fake.bothify("??-#####").upper()
        used_codes.add(code)
        buy  = round(random.uniform(30.0, 3000.0), 2)
        sell = round(buy * random.uniform(1.2, 1.8), 2)
        rows.append((
            fake.bothify("INT-####"),
            code,
            f"{random.choice(product_names)} {random.choice(brands)} {fake.bothify('??-###')}",
            fake.sentence(nb_words=10),
            random.choice(category_ids),
            random.choice(unit_ids),
            buy, sell,
            random.randint(3, 20),
            1,
            random.choice(image_ids),
        ))
    return insert_many(cursor, "Products", "productId",
        ["codeInternal", "code", "name", "description", "categoryId",
         "unitId", "priceBuy", "priceSell", "minStock", "status", "imageId"], rows)


def seed_product_store(cursor, product_ids, store_ids):
    print("  → Product_Store")
    rows = []
    for p in product_ids:
        for s in store_ids:
            cost  = round(random.uniform(30.0, 2000.0), 2)
            stock = random.randint(0, 200)
            rows.append((
                p, s, stock, cost,
                rand_date(365),
                cost,
                stock + random.randint(50, 200),
                random.randint(2, 20),
                1,
            ))
    return insert_many(cursor, "Product_Store", "productStoreId",
        ["productId", "storeId", "actualStock", "avgCost", "createdAt",
         "lastCost", "maxStock", "minStock", "status"], rows)


def seed_configs(cursor):
    print("  → Configs")
    return insert_many(cursor, "Configs", "configId",
        ["enterpriseName", "contactEmail", "ruc", "address",
         "phone", "logoUrl", "localCurrency", "createdAt"],
        [(
            fake.company(),
            fake.company_email(),
            fake.numerify("20#########"),
            fake.address().replace("\n", ", ")[:100],
            fake.numerify("01#######"),
            f"https://cdn.empresa.com/logos/{fake.uuid4()}.png",
            "PEN",
            rand_date(365),
        )])


def seed_boxes(cursor, user_ids):
    print("  → Boxs")
    rows = []
    for i in range(NUM_BOXES):
        open_d  = rand_date(60)
        is_open = 1 if i == 0 else 0
        close_d = (open_d + timedelta(hours=random.randint(6, 10))) if not is_open else None
        opener  = random.choice(user_ids)
        closer  = random.choice(user_ids) if not is_open else None
        rows.append((
            open_d, close_d,
            round(random.uniform(200.0, 2000.0), 2),
            round(random.uniform(2000.0, 15000.0), 2) if not is_open else None,
            opener, closer, opener, is_open,
        ))
    return insert_many(cursor, "Boxs", "boxId",
        ["dateOpening", "dateClosing", "amountOpening", "amountClosing",
         "userOpeningId", "userClosingId", "userActualId", "isOpen"], rows)


def seed_boxmoves(cursor, box_ids, user_ids, paymethod_ids):
    print("  → Boxmoves")
    rows = []
    for box in box_ids:
        for _ in range(random.randint(3, 8)):
            rows.append((
                box,
                round(random.uniform(10.0, 2000.0), 2),
                rand_date(30),
                random.choice(user_ids),
                random.choice(paymethod_ids),
            ))
    return insert_many(cursor, "Boxmoves", "boxMoveId",
        ["boxId", "quantity", "dateMove", "userId", "paymentMethodId"], rows)


def seed_entry_orders(cursor, provider_ids, store_ids):
    print("  → EntryOrders")
    rows = [
        (
            random.choice(provider_ids),
            random.choice(store_ids),
            rand_date(90), rand_date(90),
            random.randint(1, 3),
            random.choice(["PEN", "USD"]),
            random.randint(1, 3),
            round(random.choice([0.0, 0.18]), 2),
            random.randint(1, 3),
            fake.sentence(nb_words=8),
        )
        for _ in range(NUM_ENTRY_ORDERS)
    ]
    return insert_many(cursor, "EntryOrders", "entryOrderId",
        ["providerId", "storeId", "entryDate", "createdAt", "entryOrderType",
         "typeMoney", "payCondition", "tax", "entryOrderStatus", "observation"], rows)


def seed_entry_order_details(cursor, entry_order_ids, product_ids):
    print("  → EntryOrderDetails")
    rows = []
    for eo in entry_order_ids:
        for p in random.sample(product_ids, k=random.randint(2, 5)):
            rows.append((
                eo, p,
                random.randint(5, 100),
                round(random.uniform(30.0, 2000.0), 2),
                random.randint(1, 3),
                rand_date(90),
            ))
    return insert_many(cursor, "EntryOrderDetails", "entryOrderDetailId",
        ["entryOrderId", "productId", "quantity", "unitPrice",
         "entryOrderDetailStatus", "createdAt"], rows)


def seed_departure_orders(cursor, client_ids):
    print("  → Departureorders")
    motives = [
        "Venta a cliente", "Traslado entre almacenes", "Devolución a proveedor",
        "Merma o pérdida", "Muestra gratuita", "Ajuste de inventario",
    ]
    rows = [
        (
            random.randint(1, 3),
            random.choice(client_ids),
            rand_date(90), rand_date(90),
            random.choice(motives),
            random.randint(1, 3),
            round(random.choice([0.0, 0.18]), 2),
            fake.sentence(nb_words=6),
            fake.bothify("DOC-####-????").upper(),
        )
        for _ in range(NUM_DEPART_ORDERS)
    ]
    return insert_many(cursor, "Departureorders", "departureorderId",
        ["departureType", "clientId", "departureDate", "createdAt",
         "motive", "status", "tax", "observations", "documentReference"], rows)


def seed_departure_order_details(cursor, departure_order_ids, product_ids, store_ids):
    print("  → DepartureOrderDetails")
    unit_types = ["UND", "CAJA", "PACK", "KG"]
    rows = []
    for do in departure_order_ids:
        for p in random.sample(product_ids, k=random.randint(1, 4)):
            qty = random.randint(1, 20)
            rows.append((
                do, p,
                random.choice(store_ids),
                random.choice(unit_types),
                qty, qty,
                round(random.uniform(50.0, 3000.0), 2),
                fake.bothify("LOTE-####-??").upper(),
                random.randint(1, 3),
                rand_date(90),
            ))
    return insert_many(cursor, "DepartureOrderDetails", "DepartureOrderDetailId",
        ["DepartureOrderId", "ProductId", "storeId", "unitType",
         "quantity", "departedQuantity", "unitPrice", "lote",
         "status", "createdAt"], rows)


def seed_sales(cursor, client_ids, user_ids, store_ids):
    print("  → Sales")
    rows = [
        (
            random.choice(client_ids),
            random.choice(user_ids),
            round(random.uniform(50.0, 8000.0), 2),
            fake.sentence(nb_words=5),
            rand_date(90),
            1,
            random.choice(store_ids),
            random.randint(1, 2),
            random.randint(1, 2),
        )
        for _ in range(NUM_SALES)
    ]
    return insert_many(cursor, "Sales", "saleId",
        ["clientId", "userId", "total", "observations", "createdAt",
         "status", "storeId", "typeDocument", "typeMoney"], rows)


def seed_sale_details(cursor, conn, sale_ids, product_ids):
    print("  → SaleDetails")
    cur2 = conn.cursor()
    rows = []
    for sale in sale_ids:
        for p in random.sample(product_ids, k=random.randint(1, 5)):
            cur2.execute("SELECT [name], [priceSell] FROM [Products] WHERE [productId]=?", p)
            row = cur2.fetchone()
            rows.append((sale, p, row[0], random.randint(1, 10), round(float(row[1]), 2)))
    return insert_many(cursor, "SaleDetails", "saleDetailId",
        ["saleId", "productId", "productName", "quantity", "priceSell"], rows)


def seed_sale_methods(cursor, conn, sale_ids, paymethod_ids):
    print("  → SaleMethods")
    cur2 = conn.cursor()
    pm_names = {}
    for pm in paymethod_ids:
        cur2.execute("SELECT [name] FROM [Paymethods] WHERE [paymethodId]=?", pm)
        pm_names[pm] = cur2.fetchone()[0]
    rows = []
    for sale in sale_ids:
        cur2.execute("SELECT [total] FROM [Sales] WHERE [saleId]=?", sale)
        total = float(cur2.fetchone()[0])
        pm    = random.choice(paymethod_ids)
        rows.append((pm_names[pm], sale, round(total, 2), pm))
    return insert_many(cursor, "SaleMethods", "saleMethodId",
        ["methodPayment", "saleId", "amount", "payMethodId"], rows)


# ─── Main ─────────────────────────────────────────────────────

def seed_all():
    conn   = get_connection()
    cursor = conn.cursor()

    print("\n🌱  Seed aleatorio con Faker — InventarioDB\n")
    try:
        user_ids      = seed_users(cursor);           conn.commit()
        image_ids     = seed_images(cursor);          conn.commit()
        unit_ids      = seed_units(cursor);           conn.commit()
        category_ids  = seed_categories(cursor);      conn.commit()
        store_ids     = seed_stores(cursor, user_ids);           conn.commit()
        seed_storeusers(cursor, store_ids, user_ids);            conn.commit()
        provider_ids  = seed_providers(cursor);       conn.commit()
        client_ids    = seed_clients(cursor);         conn.commit()
        paymethod_ids = seed_paymethods(cursor);      conn.commit()
        product_ids   = seed_products(cursor, category_ids, unit_ids, image_ids); conn.commit()
        seed_product_store(cursor, product_ids, store_ids);      conn.commit()
        seed_configs(cursor);                                     conn.commit()
        box_ids       = seed_boxes(cursor, user_ids);            conn.commit()
        seed_boxmoves(cursor, box_ids, user_ids, paymethod_ids); conn.commit()
        entry_ids     = seed_entry_orders(cursor, provider_ids, store_ids); conn.commit()
        seed_entry_order_details(cursor, entry_ids, product_ids);           conn.commit()
        dep_ids       = seed_departure_orders(cursor, client_ids);          conn.commit()
        seed_departure_order_details(cursor, dep_ids, product_ids, store_ids); conn.commit()
        sale_ids      = seed_sales(cursor, client_ids, user_ids, store_ids);    conn.commit()
        seed_sale_details(cursor, conn, sale_ids, product_ids);                 conn.commit()
        seed_sale_methods(cursor, conn, sale_ids, paymethod_ids);               conn.commit()

        print("\n✅  Seed completado exitosamente!\n")
        print(f"  {'Tabla':<30} {'Registros':>10}")
        print("  " + "─" * 42)
        for t in [
            "Users", "Images", "Units", "Categories", "Stores", "Storeusers",
            "Providers", "Clients", "Paymethods", "Products", "Product_Store",
            "Configs", "Boxs", "Boxmoves", "EntryOrders", "EntryOrderDetails",
            "Departureorders", "DepartureOrderDetails", "Sales", "SaleDetails", "SaleMethods",
        ]:
            cursor.execute(f"SELECT COUNT(*) FROM [{t}]")
            print(f"  {t:<30} {cursor.fetchone()[0]:>10}")
        print()

    except Exception as e:
        conn.rollback()
        print(f"\n❌  Error durante el seed: {e}")
        raise
    finally:
        conn.close()


if __name__ == "__main__":
    seed_all()
