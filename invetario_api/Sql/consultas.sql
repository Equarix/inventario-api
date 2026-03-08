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
