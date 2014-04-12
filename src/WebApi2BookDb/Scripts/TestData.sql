
declare @statusId int,
	@taskId int,
	@userId int

if not exists (select * from [User] where Username = 'jbob')
	INSERT into [dbo].[User] ([Firstname], [Lastname], [Username]) 
		VALUES (N'Jim', N'Bob', N'jbob')

if not exists (select * from [User] where Username = 'jdoe')
	INSERT into [dbo].[User] ([Firstname], [Lastname], [Username]) 
		VALUES (N'Jane', N'Doe', N'jdoe')

if not exists(select * from dbo.Task where Subject = 'Test Task')
begin
	select top 1 @statusId = StatusId from Status;
	select top 1 @userId = UserId from [User];

	insert into dbo.Task(Subject, StartDate, StatusId, CreatedDate, CreatedUserId)
		values('Test Task', getdate(), @statusId, getdate(), @userId);

	set @taskId = SCOPE_IDENTITY();
	
	INSERT [dbo].[TaskUser] ([TaskId], [UserId]) 
		VALUES (@taskId, @userId)
end





