CREATE PROCEDURE [dbo].[GetCheques]
	
AS
BEGIN
SET NOCOUNT ON
	SELECT Id,Amount,PersonName,DateTime FROM Cheques
END 

