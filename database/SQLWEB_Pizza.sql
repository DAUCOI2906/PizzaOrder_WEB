create database PizzaOrderWebApp

use PizzaOrderWebApp

create table Category (
	CategoryID int IDENTITY(1,1) primary key,
	CategoryName nvarchar(50) not null,
	CategoryDescription varchar(200)
)

create table Food (
	FoodID varchar(10) primary key,
	FoodName nvarchar(50) not null,
	Ingredients nvarchar(MAX) not null,
	CategoryID int not null foreign key references Category(CategoryID),
	ImageString varchar(100)
)

create table Dish (
	FoodID varchar(10) foreign key references Food(FoodID),
	Size varchar(10),
	Price float not null,
	CONSTRAINT PK_Dish primary key (FoodID, Size)
)

create table Orders (
	OrderID int identity(1,1) primary key,
	OrderDate datetime not null,
	ShippedDate datetime not null,
	CustomerName nvarchar(100) not null,
	CustomerAddress nvarchar(MAX) not null,
	Phone varchar(20) not null,
	Note nvarchar(MAX) not null
)

create table OrderDetail (
	OrderID int foreign key references Orders(OrderID),
	FoodID varchar(10),
	Size varchar(10),
	Discount float not null,
	Quantity int not null,
	CONSTRAINT PK_Order primary key (OrderID, FoodID, Size),
	CONSTRAINT FK_Order foreign key (FoodID, Size) references Dish (FoodID, Size)
)

create table Contact (
	ContactName nvarchar(200) not null,
	Email varchar(50) primary key,
	Phone varchar(20),
	ContactMessage nvarchar(MAX) not null
)

create table Account (
	[User] nchar(50) primary key,
	[Password] nchar(50) not null
)

create table Accountuser(
	CustomerName nvarchar(100) primary key,
	Email varchar(50),
	Password varchar(50),
	Address varchar(50),
	Phone varchar(10),
	Dateofbirth datetime
)
/*insert data*/
use PizzaOrderWebApp

insert into [Account] values ('hangdt@gmail.com', '123456')

insert into Category(CategoryName, CategoryDescription) 
values ('Asian Pizza', 'Pizza Size S: 20cm/ Size M: 24cm/ Size L: 30cm')

insert into Category(CategoryName, CategoryDescription) 
values ('European Pizza', 'Pizza Size S: 20cm/ Size M: 24cm/ Size L: 30cm')

insert into Food values ('P01', 'Beefy Pizza', 
'Grilled beef, corn, BBQ sauce, mozzarella cheese', 1, 'beefy-pizza.jpg')

insert into Dish values ('P01', 'S', 4.5)
insert into Dish values ('P01', 'M', 5.5)
insert into Dish values ('P01', 'L', 7.5)

insert into Food values ('P02', 'Salami Pizza', 
'Salami sausage, onion, tomato sauce, mozzarella cheese', 1, 'salami-pizza.jpeg')

insert into Dish values ('P02', 'S', 5.0)
insert into Dish values ('P02', 'M', 5.5)
insert into Dish values ('P02', 'L', 7.5)

insert into Food values ('P03', 'BBQ Chicken Pizza', 
'Grilled chicken, BBQ sauce, mushroom, onion, spicy tomato cream, mozzarella cheese', 1, 'bbq-chicken-pizza.jpeg')

insert into Dish values ('P03', 'S', 5.5)
insert into Dish values ('P03', 'M', 6.5)
insert into Dish values ('P03', 'L', 7.5)

insert into Food values ('P04', 'Veggie Pizza', 
'Mushrooms, corn, pineapple, Da Lat peppers, onion, fresh tomato, tomato sauce, mozzarella cheese',
 1, 'veggie-pizza.jpg')

insert into Dish values ('P04', 'S', 5.0)
insert into Dish values ('P04', 'M', 5.5)
insert into Dish values ('P04', 'L', 6.0)

insert into Food values ('P05', 'Seafood Pizza', 
'Squid, shrimp, bell peppers, onion, fresh tomato, spicy tomato cream, mozzarella cheese',
 1, 'seafood-pizza.jpg')

insert into Dish values ('P05', 'S', 5.5)
insert into Dish values ('P05', 'M', 6.0)
insert into Dish values ('P05', 'L', 7.0)

insert into Food values ('P06', 'Meat Lovers Pizza', 
'Sausage, ham, onion, tomato sauce, mozzarella cheese',
 1, 'meat-lovers-pizza.png')

insert into Dish values ('P06', 'S', 5.5)
insert into Dish values ('P06', 'M', 6.0)
insert into Dish values ('P06', 'L', 7.0)

insert into Food values ('P07', 'Chicken Curry Pizza', 
'Grilled chicken, onion, mushroom, curry sauce, mozzarella cheese',
 1, 'chicken-curry-pizza.jpg')

insert into Dish values ('P07', 'S', 6.5)
insert into Dish values ('P07', 'M', 7.0)
insert into Dish values ('P07', 'L', 7.5)

/*truy van*/
select * from Contact