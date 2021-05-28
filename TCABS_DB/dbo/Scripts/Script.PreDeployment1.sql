/*
 Pre-Deployment Script Template                     
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.   
 Use SQLCMD syntax to include a file in the pre-deployment script.         
 Example:      :r .\myfile.sql                        
 Use SQLCMD syntax to reference a variable in the pre-deployment script.      
 Example:      :setvar TableName MyTable                     
               SELECT * FROM [$(TableName)]               
--------------------------------------------------------------------------------------
*/

--DROP TABLE IF EXISTS [dbo].[RolePermission];

--DROP TABLE IF EXISTS [dbo].[Permission];

--DROP TABLE IF EXISTS [dbo].[UserRole];

--DROP TABLE IF EXISTS [dbo].[Role];

--DROP TABLE IF EXISTS [dbo].[Minute]

--DROP TABLE IF EXISTS [dbo].[MeetingAgendaItem]

--DROP TABLE IF EXISTS [dbo].[MeetingAttendee]

--DROP TABLE IF EXISTS [dbo].[ProjectTask]

--DROP TABLE IF EXISTS [dbo].[Enrollment]

--DROP TABLE IF EXISTS [dbo].[MeetingComment]

--DROP TABLE IF EXISTS [dbo].[Meeting]

--DROP TABLE IF EXISTS [dbo].[ProjectOffering]

--DROP TABLE IF EXISTS [dbo].[UnitOffering]

--DROP TABLE IF EXISTS [dbo].[Unit]

--DROP TABLE IF EXISTS [dbo].[Year]

--DROP TABLE IF EXISTS [dbo].[TeachingPeriod]

--DROP TABLE IF EXISTS [dbo].[Project]

--DROP TABLE IF EXISTS [dbo].[ProjectRoleLink]

--DROP TABLE IF EXISTS [dbo].[ProjectRoleGroup]

--DROP TABLE IF EXISTS [dbo].[ProjectRole]

--DROP TABLE IF EXISTS [dbo].[ProjectTask]

--DROP TABLE IF EXISTS [dbo].[User]

--DROP TABLE IF EXISTS [dbo].[Team]



--DROP PROCEDURE IF EXISTS [dbo].[spApproveProjectTask]
--DROP PROCEDURE IF EXISTS [dbo].[spCreateProject]
--DROP PROCEDURE IF EXISTS [dbo].[spCreateProjectRole]
--DROP PROCEDURE IF EXISTS [dbo].[spCreateProjectRoleGroup]
--DROP PROCEDURE IF EXISTS [dbo].[spCreateProjectRoleLink]
--DROP PROCEDURE IF EXISTS [dbo].[spCreateProjectTask]
--DROP PROCEDURE IF EXISTS [dbo].[spCreateRolePermission]
--DROP PROCEDURE IF EXISTS [dbo].[spCreateUser]
--DROP PROCEDURE IF EXISTS [dbo].[spDeleteEnrollment]
--DROP PROCEDURE IF EXISTS [dbo].[spDeleteProject]
--DROP PROCEDURE IF EXISTS [dbo].[spDeleteProjectOffering]
--DROP PROCEDURE IF EXISTS [dbo].[spDeleteProjectRole]
--DROP PROCEDURE IF EXISTS [dbo].[spDeleteProjectRoleGroup]
--DROP PROCEDURE IF EXISTS [dbo].[spDeleteProjectRoleLink]
--DROP PROCEDURE IF EXISTS [dbo].[spDeleteRolePermission]
--DROP PROCEDURE IF EXISTS [dbo].[spDeleteTeachingPeriod]
--DROP PROCEDURE IF EXISTS [dbo].[spDeleteTeam]
--DROP PROCEDURE IF EXISTS [dbo].[spDeleteUnit]
--DROP PROCEDURE IF EXISTS [dbo].[spDeleteUnitOffering]
--DROP PROCEDURE IF EXISTS [dbo].[spDeleteUser]
--DROP PROCEDURE IF EXISTS [dbo].[spDeleteUserRole]
--DROP PROCEDURE IF EXISTS [dbo].[spDeleteYear]
--DROP PROCEDURE IF EXISTS [dbo].[spGetAllProjectRoleGroups]
--DROP PROCEDURE IF EXISTS [dbo].[spGetAllProjectRoles]
--DROP PROCEDURE IF EXISTS [dbo].[spGetAllProjects]
--DROP PROCEDURE IF EXISTS [dbo].[spGetAllProjectTasks]
--DROP PROCEDURE IF EXISTS [dbo].[spGetPermissionsFromUsername]
--DROP PROCEDURE IF EXISTS [dbo].[spGetProject]
--DROP PROCEDURE IF EXISTS [dbo].[spGetProjectForProjectOfferingId]
--DROP PROCEDURE IF EXISTS [dbo].[spGetProjectOfferingsForUserId]
--DROP PROCEDURE IF EXISTS [dbo].[spGetProjectOfferingsForUserIdOnlyStudents]
--DROP PROCEDURE IF EXISTS [dbo].[spGetProjectRole]
--DROP PROCEDURE IF EXISTS [dbo].[spGetProjectRoleGroup]
--DROP PROCEDURE IF EXISTS [dbo].[spGetProjectRoleLinksForGroup]
--DROP PROCEDURE IF EXISTS [dbo].[spGetProjectRoleLinksForRole]
--DROP PROCEDURE IF EXISTS [dbo].[spGetProjectRolesForEnrollment]
--DROP PROCEDURE IF EXISTS [dbo].[spGetProjectTask]
--DROP PROCEDURE IF EXISTS [dbo].[spInsertEnrollment]
--DROP PROCEDURE IF EXISTS [dbo].[spInsertProjectOffering]
--DROP PROCEDURE IF EXISTS [dbo].[spInsertTeachingPeriod]
--DROP PROCEDURE IF EXISTS [dbo].[spInsertTeam]
--DROP PROCEDURE IF EXISTS [dbo].[spInsertUnit]
--DROP PROCEDURE IF EXISTS [dbo].[spInsertUnitOffering]
--DROP PROCEDURE IF EXISTS [dbo].[spInsertUserRole]
--DROP PROCEDURE IF EXISTS [dbo].[spInsertYear]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectConvenors]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectEnrollmentCountForTeamIdAndUserId]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectEnrollmentCountForUnitOfferingIdAndUserId]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectEnrollmentForEnrollmentId]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectEnrollmentsForTeamId]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectEnrollmentsForUnitOfferingId]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectPermission]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectPermissions]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectProjectOfferingForProjectOfferingId]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectProjectOfferings]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectProjectOfferingsForUnitOfferingId]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectRoleForRoleId]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectRolePermissionsForRoleId]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectRoles]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectRoleWithPermissions]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectStudents]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectSupervisors]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectTeachingPeriodForName]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectTeachingPeriodForTeachingPeriodId]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectTeachingPeriods]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectTeam]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectTeamsForUnitOfferingId]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectUnitForName]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectUnitForUnitId]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectUnitOfferingCountForTeachingPeriod]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectUnitOfferingCountForUnit]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectUnitOfferingCountForYear]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectUnitOfferingForDetails]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectUnitOfferingForUnitOfferingId]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectUnitOfferings]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectUnitOfferingWithEnrollments]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectUnits]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectUserForUserId]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectUserForUsername]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectUserFromEnrollment]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectUserRole]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectUserRoleCountForUserIdAndRoleId]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectUserRoles]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectUserRolesForUserId]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectUsers]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectUserWithRoles]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectYearForYearId]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectYearForYearValue]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectYears]
--DROP PROCEDURE IF EXISTS [dbo].[spUpdateEnrollmentWithTeamId]
--DROP PROCEDURE IF EXISTS [dbo].[spUpdatePassword]
--DROP PROCEDURE IF EXISTS [dbo].[spUpdateProject]
--DROP PROCEDURE IF EXISTS [dbo].[spUpdateProjectRole]
--DROP PROCEDURE IF EXISTS [dbo].[spUpdateProjectRoleGroup]
--DROP PROCEDURE IF EXISTS [dbo].[spUpdateProjectTask]
--DROP PROCEDURE IF EXISTS [dbo].[spUpdateTeachingPeriod]
--DROP PROCEDURE IF EXISTS [dbo].[spUpdateTeam]
--DROP PROCEDURE IF EXISTS [dbo].[spUpdateUnit]
--DROP PROCEDURE IF EXISTS [dbo].[spUpdateUnitOffering]
--DROP PROCEDURE IF EXISTS [dbo].[spUpdateUser]
--DROP PROCEDURE IF EXISTS [dbo].[spSelectTeamsForProjectOfferingId]