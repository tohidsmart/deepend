CREATE PROCEDURE [dbo].[AddCheque]
	@PersonName nvarchar(50),
	@Amount decimal(18,2),
	@DateTime datetime
AS
BEGIN
 SET NOCOUNT ON
	INSERT INTO Cheques(PersonName,Amount,dbo.DateTime) 
	OUTPUT Inserted.Id
	VALUES(@PersonName,@Amount,@DateTime)
	
END
