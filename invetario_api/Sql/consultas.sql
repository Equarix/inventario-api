select c.clientId, c.name, count(s.saleId) totalSales  from Clients c
left join Sales s on s.clientId = c.clientId
GROUP BY c.name, c.clientId
order by totalSales desc