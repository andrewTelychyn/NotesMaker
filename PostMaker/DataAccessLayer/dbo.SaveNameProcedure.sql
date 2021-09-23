CREATE PROCEDURE [dbo].[SaveNameProcedure]
	@param1 nvarchar(15),
	@param2 nvarchar(15),
	@param3 date
AS
	INSERT INTO dbo.AutBookTableDb VALUES (@param1, @param2, @param3)
GO;
