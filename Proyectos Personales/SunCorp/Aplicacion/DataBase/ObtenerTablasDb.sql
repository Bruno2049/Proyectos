SELECT --*
'' + COLUMN_NAME + ','
FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PRODUCTOS' --AND TABLE_TYPE='BASE TABLE' 
--SELECT * FROM PRODUCTOS