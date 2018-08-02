SELECT * FROM Person AS p
LEFT JOIN ContactInfo AS c ON p.ID = c.FK_Person
LEFT JOIN address AS a ON p.ID = a.FK_Person
;

