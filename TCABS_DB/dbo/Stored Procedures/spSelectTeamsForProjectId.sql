CREATE PROCEDURE [dbo].[spSelectTeamsForProjectId]
   @projectId int
AS
BEGIN
   SELECT * FROM [dbo].[Team]
   WHERE ProjectId = @projectId
END;
