CREATE PROCEDURE [dbo].[spGetProject]
	@ProjectId int
AS
BEGIN
	SELECT *
	FROM [dbo].[Project]
	WHERE ProjectId = @ProjectId
END;
