CREATE TABLE Countries(
Id int NOT NULL Primary key,
Name nvarchar(25) NOT NULL
)

go

CREATE TABLE Cities(
Id int NOT NULL Primary key,
Name nvarchar(25) NOT NULL,
CountryId int NOT NULL
)

go

alter table Cities
add foreign key (CountryId) references Countries([Id]);
go
--

CREATE TABLE Sections(
Id int NOT NULL Primary key,
Name nvarchar(50) NOT NULL
)

go

create TABLE Discounts(
Id int NOT NULL Primary key,
Name nvarchar(50) NOT NULL,
StartDate date NOT NULL,
FinishDate date NOT NULL,
SectionId int NOT NULL
)

go

alter table Discounts
add foreign key (SectionId) references Sections([Id]);

go

CREATE TABLE [Customers](
Id int NOT NULL Primary key,
FullName nvarchar(200) NOT NULL,
Gender bit NOT NULL,
Email CHAR(25) NOT NULL,
CityId int NOT NULL
)

CREATE TABLE CustomersSections(
CustomerId int NOT NULL,
SectionId int NOT NULL,
)

go

alter table Customers
add foreign key (CityId) references Cities([Id]);

alter table CustomersSections
add primary key (CustomerId, SectionId);
alter table CustomersSections
add foreign key (CustomerId) references Sections([Id]);
alter table CustomersSections
add foreign key (SectionId) references [Customers]([Id]);
go


insert into Countries([Id], [Name])
values
	(1, '�������'),
	(2,'������'),
	(3,'�������'),
	(4,'�����');

insert into Cities([Id],[Name], CountryId)
values
	(1,'����', '1'),
	(2,'�����', '1'),
	(3,'��������', '1'),
	(4,'���', '2'),
	(5,'���������', '2'),
	(6,'�����', '3'),
	(7,'�����', '3'),
	(8,'�������', '4'),
	(9,'�����', '4');

insert into Customers([Id],FullName, Email, Gender, CityId)
values
	(1,'������� ���� ��������', 'user1@gmail.com', '0','1'),
	(2,'������� ������ ���������', 'user2@gmail.com', '1','2'),
	(3,'������ ������� ����������','user3@gmail.com', '0','3'),
	(4,'������� ���� �������������','user4@gmail.com', '0','4'),
	(5,'���������� ��������� ���������','user5@gmail.com','0', '5'),
	(6,'�������� ����� ������������','user6@gmail.com','1', '6'),
	(7,'�������� ����� �������������','user7@gmail.com', '1','7'),
	(8,'������ ������ ����������','user8@gmail.com', '1','8'),
	(9,'���������� ���� ����������','user9@gmail.com', '0','9');


insert into Sections([Id],[Name])
values
	(1,'��������'),
	(2,'��'),
	(3,'����������'),
	(4,'���������'),
	(5,'��������'),
	(6,'�����-����');

insert into Discounts([Id], [Name], StartDate, FinishDate, SectionId)
values
	(1,'������� ASUS',GETDATE(),GETDATE(),'1'),
	(2,'�� Cobra',GETDATE(),GETDATE(),'2'),
	(3,'��������� Samsung',GETDATE(),GETDATE(),'3'),
	(4,'Iphone 7s',GETDATE(),GETDATE(),'4'),
	(5,'Airpods 1',GETDATE(),GETDATE(),'5'),
	(6,'Mi band 3',GETDATE(),GETDATE(),'6'),
	(7,'������� Lenovo',GETDATE(),GETDATE(),'1'),
	(8,'�� Bravo',GETDATE(),GETDATE(),'2'),
	(9,'��������� Phillips',GETDATE(),GETDATE(),'3'),
	(10,'Nokia 3212',GETDATE(),GETDATE(),'4'),
	(11,'�������� Hyper-X',GETDATE(),GETDATE(),'5'),
	(12,'Apple-Watch 1',GETDATE(),GETDATE(),'6');

insert into CustomersSections(CustomerId, SectionId)
values
	(7,4),
	(8,5),
	(9,6),
	(5,1),
	(5,2),
	(5,3);
