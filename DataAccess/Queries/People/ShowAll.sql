--SELECT p.Id,
--	   CONCAT(p.firstname, ' ', p.lastname) AS Name,
--	   CONCAT(addr.addrline1, ' ', addr.addrline2, ' ', addr.zipcode) AS Address,
--	   CONCAT(c.number, ' ', c.ext, ' ', c.email) AS ContactInfo
--FROM person AS p
--INNER JOIN ContactInfo AS c ON (p.Id = c.FK_Person)
--INNER JOIN address AS addr ON (p.Id = addr.FK_Person)
--;

--DELETE FROM ContactInfo;
--DELETE FROM Address;
--delete from person;

--SELECT * FROM person AS p
--INNER JOIN ContactInfo AS c ON (p.Id = c.FK_Person)
--INNER JOIN address AS addr ON (p.Id = addr.FK_Person);

SELECT * FROM person;