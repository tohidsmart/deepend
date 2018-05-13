CREATE PROCEDURE [dbo].[GetCheque]
	@Id int 

AS
BEGIN

SET NOCOUNT ON
	SELECT Id,PersonName,Amount,dbo.Cheques.DateTime FROM  Cheques 
	WHERE Id =@Id
END
