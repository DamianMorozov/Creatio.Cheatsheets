-- ------------------------------------------------------------------------------------------------------------------------
-- �������� ������������ ������ 7.16.
-- https://community.terrasoft.ru/questions/staraya-konfiguraciya
-- 1. ����������� ����� �� ..\Terrasoft.WebApp\Compatibility\OldUI\ � ..\Terrasoft.WebApp\bin\
-- Terrasoft.UI.OldConfiguration.dll, Terrasoft.UI.OldConfiguration.xml, Terrasoft.UI.WebControls.dll, Terrasoft.UI.WebControls.xml
-- 2. ���������SQL-script (�������� ��������� ��������� OldUI)
-- 3. �������������� ������������
-- ------------------------------------------------------------------------------------------------------------------------
declare @deleteSetting bit = 0  -- ������� ���������
-- ------------------------------------------------------------------------------------------------------------------------
set nocount on
-- ������� ���������.
if (@deleteSetting = 1) begin
	declare @idSearch uniqueidentifier = (select [SysSettingsValue].[Id] 
	from [SysSettings] 
	inner join [SysSettingsValue] on [SysSettings].[Id]=[SysSettingsValue].[SysSettingsId]
	where [SysSettings].[Code] = 'OldUI')
	delete from [SysSettingsValue] where [Id] = @idSearch
	delete from [SysSettings] where [Name] = 'OldUI'
	print N'[-] ��������� �������� ��������� "OldUI"'
end
-- ------------------------------------------------------------------------------------------------------------------------
-- ������� ���������.
declare @code varchar(max) = 'OldUI'
declare @value bit = 1
declare @id uniqueidentifier
 if not exists (select [Id] from [SysSettings] where [Code] = @code)
begin
	insert into [SysSettings] ([Name], [Code], [ValueTypeName], [IsCacheable]) values (@code, @code, 'boolean', 1)
end
set @id = (select [Id] from [SysSettings] where [Code] = @code)
if not exists (select [Id] from [SysSettingsValue] where [SysSettingsId] = @id)
begin
	insert into [SysSettingsValue] ([SysSettingsId], [SysAdminUnitId], [BooleanValue], [Position]) 
	values (@id, 'a29a3ba5-4b0d-de11-9a51-005056c00008', 1, 2147483647)
	print N'[+] ��������� �������� ��������� "OldUI"'
end
else
begin
	update [SysSettingsValue] set [BooleanValue] = @value where [SysSettingsId] = @id
	print N'[*] ��������� ���������� ��������� "OldUI"'
end
-- ------------------------------------------------------------------------------------------------------------------------
-- �������� ���������.
select 
	 [SysSettings].[Name] [SysSettings_Name]
	,[SysSettings].[Code] [SysSettings_Code]
	,[SysSettings].[ValueTypeName] [SysSettings_ValueTypeName]
	,[SysSettings].[IsCacheable] [SysSettings_IsCacheable]
	,[SysAdminUnit].[Name] [SysAdminUnit_Name]
	,[SysSettingsValue].[Id] [SysSettingsValue_Id]
	,[SysSettingsValue].[BooleanValue] [SysSettingsValue_BooleanValue]
	,[SysSettingsValue].[Position] [SysSettingsValue_Position]
from [SysSettings]
inner join [SysSettingsValue] on [SysSettings].[Id]=[SysSettingsValue].[SysSettingsId]
inner join [SysAdminUnit] on [SysSettingsValue].[SysAdminUnitId]=[SysAdminUnit].[Id]
where [SysSettings].[Name] = 'OldUI'
print N'[*] ��������� ����� ��������� "OldUI"'
-- ------------------------------------------------------------------------------------------------------------------------
set nocount off