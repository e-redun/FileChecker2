SELECT 
	--fk.name AS ForeignKey,
	OBJECT_NAME(fk.parent_object_id) AS ChildTable
  , COL_NAME(fkc.parent_object_id, fkc.parent_column_id) AS ChildColumn
  , OBJECT_NAME (fk.referenced_object_id) AS ParentTable
  , COL_NAME(fkc.referenced_object_id, fkc.referenced_column_id) AS ParentColumn
FROM sys.foreign_keys AS fk
INNER JOIN sys.foreign_key_columns AS fkc
	ON fk.OBJECT_ID = fkc.constraint_object_id