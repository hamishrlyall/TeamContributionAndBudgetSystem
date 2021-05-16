create procedure sp_createRolePermission(@roleid int, @permissionid int)
as
insert into RolePermission(roleid,Permissionid) values(@roleid,@permissionid)
return 0