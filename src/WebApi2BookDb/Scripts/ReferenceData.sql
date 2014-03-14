--if not exists(Select * from [dbo].[aspnet_SchemaVersions])
--begin
--	--insert into dbo.aspnet_SchemaVersions (Feature, CompatibleSchemaVersion, IsCurrentVersion)
--	--	values('common', 1, 1)
--	--insert into dbo.aspnet_SchemaVersions (Feature, CompatibleSchemaVersion, IsCurrentVersion)
--	--	values('membership', 1, 1)
--	--insert into dbo.aspnet_SchemaVersions (Feature, CompatibleSchemaVersion, IsCurrentVersion)
--	--	values('role manager', 1, 1)
--end

if not exists(select * from dbo.Status where Name = 'Not Started')
	insert into dbo.Status(Name, Ordinal) values('Not Started', 0);
if not exists(select * from dbo.Status where Name = 'In Progress')
	insert into dbo.Status(Name, Ordinal) values('In Progress', 1);
if not exists(select * from dbo.Status where Name = 'Completed')
	insert into dbo.Status(Name, Ordinal) values('Completed', 2);
