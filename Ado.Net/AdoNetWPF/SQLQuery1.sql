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
	(1, 'Украина'),
	(2,'Италия'),
	(3,'Франция'),
	(4,'Китай');

insert into Cities([Id],[Name], CountryId)
values
	(1,'Киев', '1'),
	(2,'Львов', '1'),
	(3,'Николаев', '1'),
	(4,'Рим', '2'),
	(5,'Флоренция', '2'),
	(6,'Париж', '3'),
	(7,'Ницца', '3'),
	(8,'Гановер', '4'),
	(9,'Пекин', '4');

insert into Customers([Id],FullName, Email, Gender, CityId)
values
	(1,'Евсеева Юлия Артёмовна', 'user1@gmail.com', '0','1'),
	(2,'Голубев Кирилл Матвеевич', 'user2@gmail.com', '1','2'),
	(3,'Орлова Евгения Артемьевна','user3@gmail.com', '0','3'),
	(4,'Егорова Вера Александровна','user4@gmail.com', '0','4'),
	(5,'Плотникова Екатерина Тимуровна','user5@gmail.com','0', '5'),
	(6,'Высоцкий Тимур Вячеславович','user6@gmail.com','1', '6'),
	(7,'Леонтьев Тимур Александрович','user7@gmail.com', '1','7'),
	(8,'Иванов Андрей Михайлович','user8@gmail.com', '1','8'),
	(9,'Никольская Вера Николаевна','user9@gmail.com', '0','9');


insert into Sections([Id],[Name])
values
	(1,'Ноутбуки'),
	(2,'ПК'),
	(3,'Телевизоры'),
	(4,'Смартфоны'),
	(5,'Наушники'),
	(6,'Смарт-часы');

insert into Discounts([Id], [Name], StartDate, FinishDate, SectionId)
values
	(1,'Ноутбук ASUS',GETDATE(),GETDATE(),'1'),
	(2,'ПК Cobra',GETDATE(),GETDATE(),'2'),
	(3,'Телевизор Samsung',GETDATE(),GETDATE(),'3'),
	(4,'Iphone 7s',GETDATE(),GETDATE(),'4'),
	(5,'Airpods 1',GETDATE(),GETDATE(),'5'),
	(6,'Mi band 3',GETDATE(),GETDATE(),'6'),
	(7,'Ноутбук Lenovo',GETDATE(),GETDATE(),'1'),
	(8,'ПК Bravo',GETDATE(),GETDATE(),'2'),
	(9,'Телевизор Phillips',GETDATE(),GETDATE(),'3'),
	(10,'Nokia 3212',GETDATE(),GETDATE(),'4'),
	(11,'Наушники Hyper-X',GETDATE(),GETDATE(),'5'),
	(12,'Apple-Watch 1',GETDATE(),GETDATE(),'6');

insert into CustomersSections(CustomerId, SectionId)
values
	(7,4),
	(8,5),
	(9,6),
	(5,1),
	(5,2),
	(5,3);
