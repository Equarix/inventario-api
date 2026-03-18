select c.clientId, c.name, count(s.saleId) totalSales  from Clients c
left join Sales s on s.clientId = c.clientId
GROUP BY c.name, c.clientId
order by totalSales desc

go
create or alter proc sale_kpi
    @storeId int
as
BEGIN

    IF @storeId = 0
    BEGIN
        select count(saleId) as total_venta,
        ROUND(sum(total), 2) as total_ingresos,
        ROUND(AVG(total), 2) as promedio_ingresos,
        (select count(saleId) from Sales where createdAt = GETDATE()) as ventas_hoy
        from Sales
    END
    ELSE
        select count(saleId) as total_venta,
            ROUND(sum(total), 2) as total_ingresos,
            ROUND(AVG(total), 2) as promedio_ingresos,
            (select count(saleId) from Sales where createdAt = GETDATE()) as ventas_hoy
            from Sales
            where storeId = @storeId
end     



GO
create or alter proc products_kpi
AS
BEGIN
    select COUNT(*) as total_products,
    COUNT(CASE  WHEN [status] = 1 THEN 1 END) as active_products,
    ROUND(AVG(priceSell), 2) as average_price
    from Products 
end


GO
CREATE OR ALTER proc salesTrendData 
AS
BEGIN
    SET LANGUAGE Spanish;
    SET DATEFIRST 1; 

    WITH Ultimos7Dias AS (
        SELECT 
            CAST(DATEADD(DAY, -6, CAST(GETDATE() AS DATE)) AS DATE) AS Fecha
        UNION ALL
        SELECT DATEADD(DAY, 1, Fecha)
        FROM Ultimos7Dias
        WHERE Fecha < CAST(GETDATE() AS DATE)
    )
    SELECT 
        UPPER(LEFT(DATENAME(WEEKDAY, d.Fecha), 3)) AS name,
        ISNULL(COUNT(s.saleId), 0) AS sales,
        ISNULL(ROUND(SUM(s.total), 2), 0) AS revenue
    FROM Ultimos7Dias d
    LEFT JOIN Sales s 
        ON CAST(s.createdAt AS DATE) = d.Fecha
    GROUP BY d.Fecha
    ORDER BY d.Fecha
END



GO
CREATE OR ALTER proc getTopProducts
as
BEGIN
    SELECT TOP 5
        p.name as [name],
        SUM(sd.quantity) as [value]
        from Products p
        LEFT JOIN SaleDetails sd on sd.productId = p.productId 
        GROUP BY p.name, p.productId, sd.productId
        ORDER BY [value] DESC
END

GO
CREATE OR ALTER proc getKpiInventory
AS
BEGIN
    SELECT 
        ROUND(SUM(p.priceSell), 2) as total_inventory_value,
        COUNT(CASE WHEN ps.actualStock = 0 THEN 1 END) as out_of_stock_products,
        (select count(*) from EntryOrders where entryOrderStatus = 0) as entry_order_pending,
        (select count(*) from Sales WHERE createdAt >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)) as sales_this_month
    FROM
    Products p
    LEFT JOIN Product_Store ps on ps.productId = p.productId
END


GO
CREATE OR ALTER proc getCriticalProducts
AS
BEGIN
    SELECT 
        p.name as [name],
        ps.actualStock as stock,
        ps.minStock as min,
        CASE 
            WHEN ps.actualStock <= (ps.minStock * 0.4) THEN 'Crítico'
            ELSE 'Bajo'
        END AS [status],
        s.name as store
    from
    Products p
    LEFT JOIN Product_Store ps on ps.productId = p.productId
    INNER JOIN Stores s on s.storeId = ps.storeId
    WHERE ps.actualStock <= ps.minStock
END

GO
CREATE OR ALTER PROCEDURE sp_Top5CategoriasPorPrecioSell
AS
BEGIN
    SET NOCOUNT ON;

    -- Colores estáticos asignados por ranking
    DECLARE @Colores TABLE (rn INT, color NVARCHAR(20));
    INSERT INTO @Colores VALUES
        (1, '#3B82F6'),
        (2, '#A855F7'),
        (3, '#F59E0B'),
        (4, '#10B981'),
        (5, '#EF4444');

    WITH TotalesPorCategoria AS (
        SELECT
            c.categoryId,
            c.name                     AS categoria,
            SUM(p.priceSell)           AS totalCategoria
        FROM dbo.Products p
        INNER JOIN dbo.Categories c ON c.categoryId = p.categoryId
        WHERE c.status = 1
        GROUP BY c.categoryId, c.name
    ),
    Top5 AS (
        SELECT TOP 5
            categoryId,
            categoria,
            totalCategoria,
            ROW_NUMBER() OVER (ORDER BY totalCategoria DESC) AS rn
        FROM TotalesPorCategoria
        ORDER BY totalCategoria DESC
    ),
    GranTotal AS (
        SELECT SUM(totalCategoria) AS total FROM Top5
    )
    SELECT
        t.rn                                                        AS posicion,
        t.categoria                                                 AS name,
        CAST(
            ROUND((t.totalCategoria / g.total) * 100, 2)
        AS DECIMAL(5,2))                                            AS value,
        col.color                                                   AS color,
        CAST(ROUND(t.totalCategoria, 2) AS DECIMAL(18,2))          AS totalPriceSell
    FROM Top5 t
    CROSS JOIN GranTotal g
    INNER JOIN @Colores col ON col.rn = t.rn
    ORDER BY t.rn;
END
GO

exec sp_Top5CategoriasPorPrecioSell