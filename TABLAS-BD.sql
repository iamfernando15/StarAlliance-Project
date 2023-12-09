create database StarAlliance;

use StarAlliance 

-- Modulo Registro 

create table Passenger ( ---Procedimientos listos 
ID_Passenger int identity (1,1) primary key not null,
Fst_Nombre nvarchar (30) not null,
Snd_Nombre nvarchar (30) ,
Fst_Apellido nvarchar (30) not null,
Snd_Apellido nvarchar (30),
Birthdate date not null,
Identification char (16) check (Identification like'[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]-[A-Z]')  not null,
Phone_Number char (8) check (Phone_Number like '[8|5|7|2][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
Adress nvarchar (80) not null,
Email nvarchar (30)
)
select * from Passenger 


create table Passport (---Procedimientos listos 
ID_Passport int identity (1,1) primary key not null,
ID_Passenger int foreign key references Passenger (ID_Passenger) not null, 
Passport_Type char (1) not null,
Country_Code  nvarchar (10) not null,
Passport_Num  nvarchar (9) check (Passport_Num like'[A-Z]-[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') not null,
Nationality nvarchar (30) not null,
Gender char (1) not null,
IssueDate date not null,
ExpiryDate  date not null
)




create table Visa (---Procedimientos listos
ID_Visa int identity (1,1) primary key not null,
ID_Passport int foreign key references  Passport (ID_Passport) not null,
ID_Passenger int foreign key references Passenger (ID_Passenger) not null, 
Issuing_Postname nvarchar (30) not null,
Control_Number nvarchar (30) not null,
Visa_Num  nvarchar(8) check (Visa_Num like '[A-Z]-[0-9][0-9][0-9][0-9][0-9][0-9][0-9]') not null,
Visa_Type char (1) not null,
Visa_Class nvarchar (5) not null,
Entries char (1) not null,
Annotation nvarchar (80) not null,
IssueDate date not null,
ExpiryDate  date not null

)

----DESDE ACA
create table Flight_Origin(---Procedimientos listos
ID_Origin int identity (1,1) primary key not null,
ID_Passenger int foreign key references Passenger (ID_Passenger) not null, 
Airport nvarchar (30) not null,
Country nvarchar (30) not null,
City nvarchar (30) not null,
ZipCode nvarchar (5) not null,
Airline nvarchar(30) not null,
DepartureDate datetime not null,
Departure_Time datetime not null


)




create table Flight_Stops (---Procedimientos listos
ID_FlightStops  int identity (1,1) primary key not null,
ID_Passenger int foreign key references Passenger (ID_Passenger) not null, 
Transhipment bit not null, -- transbordo
Partner_Airline bit not null,-- aerolinea amiga
Airport nvarchar(30),
StopTime datetime ,
Transhipment_Price decimal(5,2),
PA_Price decimal(5,2)

)



create table Flight_Destination(---Procedimientos listos
ID_Destination int identity (1,1) primary key not null,
ID_Passenger int foreign key references Passenger (ID_Passenger) not null, 
Airport nvarchar (30) not null,
Country nvarchar (30) not null,
City nvarchar (30) not null,
ZipCode nvarchar (5) not null,
Destiny_Price decimal(5,2) not null,
ArrivalDate date not null,
Arrival_Time time not null
)


create table Flight_Luggage(---Procedimientos listos
ID_Luggage int identity (1,1) primary key not null,
ID_Passenger int foreign key references Passenger (ID_Passenger) not null,
Luggage_Type nvarchar (30) not null,
Luggage_Weight decimal(5,2)not null ,
Extra_Luggage decimal(5,2) not null ,
Total_Extra decimal(10,2)
)
ALTER TABLE Flight_Luggage ADD Total_Extra decimal(10,2)

Create table Flight(---Procedimientos listos
ID_Flight int identity (1,1) primary key not null,
ID_Passenger int foreign key references Passenger (ID_Passenger) not null,
ID_Origin int foreign key references Flight_Origin(ID_Origin) not null,
ID_Destination int foreign key references Flight_Destination (ID_Destination) not null,
ID_FlightStops int foreign key references   Flight_Stops (ID_FlightStops) not null,
ID_Luggage int foreign key references  Flight_Luggage (ID_Luggage) not null,
TotalSeats int
)

ALTER TABLE Flight ADD TotalSeats int




create table Flight_Class(---Procedimientos listos
ID_Flight_Class int identity (1,1) primary key not null,
ID_Flight int foreign key references Flight(ID_Flight) not null,
Bussines_Class bit,
Premium_Class bit,
Tourist_Class bit,

)


create table Flight_Seats (--Procedimientos listos

ID_Seat int identity (1,1) primary key not null,
ID_Flight int foreign key references Flight(ID_Flight) not null,
Seat_Location nvarchar (50) not null,
Seat_Number nvarchar(5) not null,
Assigned_Seats int
)

insert into  Flight_Seats  values (1, 'R-Window', 'A-01', 3)


create table Det_Ticket(--Procedimientos listos
ID_Ticket int identity (1,1) primary key not null,
ID_Flight int foreign key references Flight(ID_Flight) not null,
ID_Flight_Class int foreign key references Flight_Class (ID_Flight_Class) not null,
ID_Passenger int foreign key references Passenger (ID_Passenger) not null,
ID_Seat int foreign key references Flight_Seats (ID_Seat) not null,
Bought_Ticket datetime not null,
Ticket_Status bit,
Total_Price decimal(10,2)

ALTER TABLE Det_Ticket ADD Total_Price decimal(10,2)
)


-- Modulo Administración 

create table Itinerario(--Procedimientos listos
ID_Flight int foreign key references Flight(ID_Flight) not null,
ID_Ticket  int foreign key references Det_Ticket(ID_Ticket ) not null,
    Airline nvarchar(30),
    Country_Origin nvarchar(30),
    Departure_DateTime datetime,
    Country_Destination nvarchar(30),
    Arrival_DateTime datetime,
ID_Flight_Class int foreign key references Flight_Class (ID_Flight_Class) not null,
ID_Passenger int foreign key references Passenger (ID_Passenger)

)


DROP TABLE Itinerary
ALTER TABLE Itinerario ADD ID_Passenger int foreign key references Passenger (ID_Passenger)




create table Cancellation (--Procedimientos listos
ID_Cancellation int identity (1,1) primary key not null,
ID_Ticket int foreign key references Det_Ticket (ID_Ticket) not null,
ID_Passenger int foreign key references Passenger (ID_Passenger) not null,
Cancellation_Date datetime not null, 
Reason nvarchar (300) not null,
Refund bit

)


create table USUARIO(
IdUsuario int primary key identity (1,1) not null,
Correo varchar(100) not null,
Clave varchar (500) not null
)
 

 -----ÁLGEBRA-CÁLCULO RELACIONAL 

 -------------(Operaciones Relacionales)--------------

 --Proyección (π)

SELECT Fst_Nombre, Snd_Nombre, Fst_Apellido, Snd_Apellido
FROM Passenger;




--Producto Cartesiano (x)

SELECT Passenger.Fst_Nombre, Passenger.Snd_Nombre, Passenger.Fst_Apellido, Passenger.Snd_Apellido,
       Itinerario.Airline, Itinerario.Country_Origin, Itinerario.Country_Destination
FROM Passenger, Itinerario;


 --Selección(θ)

 SELECT *
FROM Flight
WHERE ID_Origin IN (
    SELECT ID_Origin
    FROM Flight_Origin
    WHERE City = 'Managua'
);

---Cálculo de predicado 

SELECT *
FROM Det_Ticket
WHERE Bought_Ticket >= '2023-08-24' AND Bought_Ticket < DATEADD(DAY, 1, '2023-08-24');

