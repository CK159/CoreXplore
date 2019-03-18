--Fill in new fields for existing records

UPDATE [dbo].AspNetUsers
SET
	ConcurrencyStamp = NEWID()
WHERE ConcurrencyStamp IS NULL

UPDATE [dbo].AspNetUsers
SET
	NormalizedEmail = UPPER(Email)
WHERE NormalizedEmail IS NULL

UPDATE [dbo].AspNetUsers
SET
	NormalizedUserName = UPPER(UserName)
WHERE NormalizedUserName IS NULL