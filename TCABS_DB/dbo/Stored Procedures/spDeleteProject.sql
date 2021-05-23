CREATE PROCEDURE [dbo].[spDeleteProject]
   @ProjectId int
AS
BEGIN
   DELETE FROM [dbo].[Project]
   WHERE ProjectId = @ProjectId
END;
