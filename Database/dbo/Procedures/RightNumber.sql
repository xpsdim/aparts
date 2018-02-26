CREATE FUNCTION [dbo].[RightNumber]
(
@number varchar(256)
)
RETURNS INT
AS
BEGIN
	declare @reversed varchar(50) = REVERSE(@number);
	return cast(REVERSE(SUBSTRING(@reversed, 1, PATINDEX('%[^0-9]%', @reversed)-1)) as int);
END
