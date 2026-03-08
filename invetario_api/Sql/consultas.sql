select c.clientId, c.name, count(s.saleId) totalSales  from Clients c
left join Sales s on s.clientId = c.clientId
GROUP BY c.name, c.clientId
order by totalSales desc

go
create proc sale_kpi
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


select * from sales
select * from SaleDetails