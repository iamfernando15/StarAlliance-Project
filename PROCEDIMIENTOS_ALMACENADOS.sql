-- Procedimiento Almacenado para LOGIN
use StarAlliance
create proc sp_RegistrarUsuario(

@Correo varchar (100),
@Clave varchar (500),
@Registrado bit output,
@Mensaje varchar (100) output
)
as
begin

if(not exists(select * from USUARIO where Correo = @Correo))

begin 

insert into USUARIO (Correo, Clave) values(@Correo, @Clave)
set @Registrado = 1
set @Mensaje = 'usuario registrado'

end 

else
begin
 set @Registrado = 0
  set @Mensaje = 'correo ya existe'
end

end




create proc sp_ValidarUsuario(
@Correo varchar (100),
@Clave varchar (500)

)
as
begin

if(exists(select * from USUARIO where Correo = @Correo and Clave = @Clave))
select IdUsuario from USUARIO where Correo = @Correo and Clave = @Clave

else 

select '0'


end


declare @registrado bit, @mensaje varchar(100)

exec sp_RegistrarUsuario 'jade08@gmial.com', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', @registrado output, @mensaje output
exec sp_RegistrarUsuario 'ezequiel00@gmial.com', '921443c5e72aac9f10321d52f095edd5ed04ab8deeca8cd0eb425ad46c135c14', @registrado output, @mensaje output


select @registrado
select @mensaje


SELECT * FROM USUARIO

exec sp_ValidarUsuario 'jade08@gmial.com', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9'
exec sp_ValidarUsuario 'ezequiel00@gmial.com', '921443c5e72aac9f10321d52f095edd5ed04ab8deeca8cd0eb425ad46c135c14'

------------------------------------------------------------------------------------------------------------------------------------------



--Procedimiento de inserción en la tabla "Passenger" LISTO

CREATE PROCEDURE RegistrarPasajero
    @Fst_Nombre nvarchar(30),
    @Snd_Nombre nvarchar(30),
    @Fst_Apellido nvarchar(30),
    @Snd_Apellido nvarchar(30),
    @Birthdate date,
    @Identification char(16),
    @Phone_Number char(8),
    @Adress nvarchar(80),
    @Email nvarchar(30)
AS
BEGIN
    INSERT INTO Passenger (Fst_Nombre, Snd_Nombre, Fst_Apellido, Snd_Apellido, Birthdate, Identification, Phone_Number, Adress, Email)
    VALUES (@Fst_Nombre, @Snd_Nombre, @Fst_Apellido, @Snd_Apellido, @Birthdate, @Identification, @Phone_Number, @Adress, @Email)
END



---Actualiza Passenger 
CREATE PROCEDURE UpdatePassenger
    @ID_Passenger int,
    @Phone_Number char(8),
    @Adress nvarchar(80),
    @Email nvarchar(30)
AS
BEGIN
    UPDATE Passenger
    SET
        Phone_Number = @Phone_Number, Adress = @Adress, Email = @Email
    WHERE ID_Passenger = @ID_Passenger;
END;


--Dar de baja pasajero 
CREATE PROCEDURE Bajar_Passenger
    @ID_Passenger int
AS
BEGIN
    DELETE FROM Passenger
    WHERE ID_Passenger = @ID_Passenger;
END;






--Procedimiento de inserción en la tabla "Passport" LISTO

CREATE PROCEDURE RegistrarPasaporte
    @ID_Passenger int,
    @Passport_Type char(1),
    @Country_Code nvarchar(10),
    @Passport_Num nvarchar(9),
    @Nationality nvarchar(30),
    @Gender char(1),
    @IssueDate date,
    @ExpiryDate date
AS
BEGIN
    INSERT INTO Passport (ID_Passenger, Passport_Type, Country_Code, Passport_Num, Nationality, Gender, IssueDate, ExpiryDate)
    VALUES (@ID_Passenger, @Passport_Type, @Country_Code, @Passport_Num, @Nationality, @Gender, @IssueDate, @ExpiryDate)
END





--- ASCTUALIZAR PASAPORTE 
CREATE PROCEDURE sp_UpdatePassport
    @ID_Passport int,
    @ID_Passenger int,
    @Passport_Type char(1),
    @Country_Code nvarchar(10),
    @Passport_Num nvarchar(9),
    @Nationality nvarchar(30),
    @Gender char(1),
    @IssueDate date,
    @ExpiryDate date
AS
BEGIN
    UPDATE Passport
    SET ID_Passenger = @ID_Passenger,
        Passport_Type = @Passport_Type,
        Country_Code = @Country_Code,
        Passport_Num = @Passport_Num,
        Nationality = @Nationality,
        Gender = @Gender,
        IssueDate = @IssueDate,
        ExpiryDate = @ExpiryDate
    WHERE ID_Passport = @ID_Passport;
END;

---BAJAR PASAPORTE 

CREATE PROCEDURE BAja_Passport
    @ID_Passport int
AS
BEGIN
    DELETE FROM Passport
    WHERE ID_Passport = @ID_Passport;
END;


--Procedimiento de inserción en la tabla "Visa"

CREATE PROCEDURE RegistrarVisa
    @ID_Passport int,
    @ID_Passenger int,
    @Issuing_Postname nvarchar(30),
    @Control_Number nvarchar(30),
    @Visa_Num nvarchar(8),
    @Visa_Type char(1),
    @Visa_Class nvarchar(5),
    @Entries char(1),
    @Annotation nvarchar(80),
    @IssueDate date,
    @ExpiryDate date
AS
BEGIN
    INSERT INTO Visa (ID_Passport, ID_Passenger, Issuing_Postname, Control_Number, Visa_Num, Visa_Type, Visa_Class, Entries, Annotation, IssueDate, ExpiryDate)
    VALUES (@ID_Passport, @ID_Passenger, @Issuing_Postname, @Control_Number, @Visa_Num, @Visa_Type, @Visa_Class, @Entries, @Annotation, @IssueDate, @ExpiryDate)
END

---- modifi visa 
CREATE PROCEDURE sp_UpdateVisa
    @ID_Visa int,
    @ID_Passport int,
    @ID_Passenger int,
    @Issuing_Postname nvarchar(30),
    @Control_Number nvarchar(30),
    @Visa_Num nvarchar(8),
    @Visa_Type char(1),
    @Visa_Class nvarchar(5),
    @Entries char(1),
    @Annotation nvarchar(80),
    @IssueDate date,
    @ExpiryDate date
AS
BEGIN
    UPDATE Visa
    SET ID_Passport = @ID_Passport,
        ID_Passenger = @ID_Passenger,
        Issuing_Postname = @Issuing_Postname,
        Control_Number = @Control_Number,
        Visa_Num = @Visa_Num,
        Visa_Type = @Visa_Type,
        Visa_Class = @Visa_Class,
        Entries = @Entries,
        Annotation = @Annotation,
        IssueDate = @IssueDate,
        ExpiryDate = @ExpiryDate
    WHERE ID_Visa = @ID_Visa;
END;


---baja visa 
CREATE PROCEDURE Baja_Visa
    @ID_Visa int
AS
BEGIN
    DELETE FROM Visa
    WHERE ID_Visa = @ID_Visa;
END;


----------------INSERCION VUELO DE ORIGEN -------------LISTO
CREATE PROCEDURE InsertFlightOrigin
    @ID_Passenger int,
    @Airport nvarchar(30),
    @Country nvarchar(30),
    @City nvarchar(30),
    @ZipCode nvarchar(5),
    @Airline nvarchar(30),
    @DepartureDate datetime,
    @Departure_Time datetime
AS
BEGIN
    INSERT INTO Flight_Origin (ID_Passenger, Airport, Country, City, ZipCode, Airline, DepartureDate, Departure_Time)
    VALUES (@ID_Passenger, @Airport, @Country, @City, @ZipCode, @Airline, @DepartureDate, @Departure_Time);
END;



------------ACTUALIXZAR VUELO ORIGEN 
CREATE PROCEDURE UpdateFlightOrigin
    @ID_Origin int,
    @Airport nvarchar(30),
    @Airline nvarchar(30)
AS
BEGIN
    UPDATE Flight_Origin
    SET Airport = @Airport, Airline = @Airline
    WHERE ID_Origin = @ID_Origin;
END;


---BAJA VUELO ORIGEN
CREATE PROCEDURE BAJA_FlightOrigin
    @ID_Origin int
AS
BEGIN
    DELETE FROM Flight_Origin
    WHERE ID_Origin = @ID_Origin;
END;




-----------INSERSION ESCALAS ------LISTOOO
CREATE PROCEDURE InsertFlightStop
    @ID_Passenger int,
    @Transhipment bit,
    @Partner_Airline bit,
    @Airport nvarchar(30),
    @StopTime datetime,
    @Transhipment_Price decimal(5,2),
    @PA_Price decimal(5,2)
AS
BEGIN
    INSERT INTO Flight_Stops (ID_Passenger, Transhipment, Partner_Airline, Airport, StopTime, Transhipment_Price, PA_Price)
    VALUES (@ID_Passenger, @Transhipment, @Partner_Airline, @Airport, @StopTime, @Transhipment_Price, @PA_Price);
END;


-----ACTUAIZAR ESCALAS 
CREATE PROCEDURE UpdateFlightStop
    @ID_FlightStops int,
    @Transhipment bit,
    @Partner_Airline bit,
    @Airport nvarchar(30),
    @StopTime datetime,
    @Transhipment_Price decimal(5,2),
    @PA_Price decimal(5,2)
AS
BEGIN
    UPDATE Flight_Stops
    SET Transhipment = @Transhipment, Partner_Airline = @Partner_Airline, Airport = @Airport,
        StopTime = @StopTime, Transhipment_Price = @Transhipment_Price, PA_Price = @PA_Price
    WHERE ID_FlightStops = @ID_FlightStops;
END;



-----ELIMINAR 

CREATE PROCEDURE DeleteFlightStop
    @ID_FlightStops int
AS
BEGIN
    DELETE FROM Flight_Stops
    WHERE ID_FlightStops = @ID_FlightStops;
END;





---------INSERTA DESTINO -------Listoo
CREATE PROCEDURE InsertFlightDestination
    @ID_Passenger int,
    @Airport nvarchar(30),
    @Country nvarchar(30),
    @City nvarchar(30),
    @ZipCode nvarchar(5),
    @Destiny_Price decimal(5,2),
    @ArrivalDate date,
    @Arrival_Time time
AS
BEGIN
    INSERT INTO Flight_Destination (ID_Passenger, Airport, Country, City, ZipCode, Destiny_Price, ArrivalDate, Arrival_Time)
    VALUES (@ID_Passenger, @Airport, @Country, @City, @ZipCode, @Destiny_Price, @ArrivalDate, @Arrival_Time);
END;



----ACTUALIZA DESTINO 

CREATE PROCEDURE UpdateFlightDestination
    @ID_Destination int,
    @Airport nvarchar(30),
    @Country nvarchar(30),
    @City nvarchar(30),
    @ZipCode nvarchar(5),
    @Destiny_Price decimal(5,2),
    @ArrivalDate date,
    @Arrival_Time time
AS
BEGIN
    UPDATE Flight_Destination
    SET Airport = @Airport, Country = @Country, City = @City, ZipCode = @ZipCode,
        Destiny_Price = @Destiny_Price, ArrivalDate = @ArrivalDate, Arrival_Time = @Arrival_Time
    WHERE ID_Destination = @ID_Destination;
END;


----BAJA O ELIMINA DESTINO 
CREATE PROCEDURE Baj_DeltFlightDestination
    @ID_Destination int
AS
BEGIN
    DELETE FROM Flight_Destination
    WHERE ID_Destination = @ID_Destination;
END;




------insertar maleta ------------listooo
CREATE PROCEDURE InsertFlightLuggage
    @ID_Passenger int,
    @Luggage_Type nvarchar(30),
    @Luggage_Weight decimal(5,2),
    @Extra_Luggage decimal(5,2),
    @Total_Extra decimal(10,2)
AS
BEGIN
    INSERT INTO Flight_Luggage (ID_Passenger, Luggage_Type, Luggage_Weight, Extra_Luggage, Total_Extra)
    VALUES (@ID_Passenger, @Luggage_Type, @Luggage_Weight, @Extra_Luggage, @Total_Extra);
END;



---actualiza maleta
CREATE PROCEDURE UpdateFlightLuggage
    @ID_Luggage int,
    @Luggage_Type nvarchar(30),
    @Luggage_Weight decimal(5,2),
    @Extra_Luggage decimal(5,2),
    @Total_Extra decimal(10,2)
AS
BEGIN
    UPDATE Flight_Luggage
    SET Luggage_Type = @Luggage_Type, Luggage_Weight = @Luggage_Weight,
        Extra_Luggage = @Extra_Luggage, Total_Extra = @Total_Extra
    WHERE ID_Luggage = @ID_Luggage;
END;


---emlimina o baja maleta 

CREATE PROCEDURE Del_BjaFlightLuggage
    @ID_Luggage int
AS
BEGIN
    DELETE FROM Flight_Luggage
    WHERE ID_Luggage = @ID_Luggage;
END;



-----INSERTAR VUELO -listooo

CREATE PROCEDURE sp_InsertFlight
    @ID_Passenger int,
    @ID_Origin int,
    @ID_Destination int,
    @ID_FlightStops int,
    @ID_Luggage int,
    @TotalSeats int
AS
BEGIN
    INSERT INTO Flight (ID_Passenger, ID_Origin, ID_Destination, ID_FlightStops, ID_Luggage, TotalSeats)
    VALUES (@ID_Passenger, @ID_Origin, @ID_Destination, @ID_FlightStops, @ID_Luggage, @TotalSeats);
END;

----ACTUALIZAR VUELO 

CREATE PROCEDURE sp_UpdateFlight
    @ID_Flight int,
    @ID_Passenger int,
    @ID_Origin int,
    @ID_Destination int,
    @ID_FlightStops int,
    @ID_Luggage int,
    @TotalSeats int
AS
BEGIN
    UPDATE Flight
    SET ID_Passenger = @ID_Passenger,
        ID_Origin = @ID_Origin,
        ID_Destination = @ID_Destination,
        ID_FlightStops = @ID_FlightStops,
        ID_Luggage = @ID_Luggage,
        TotalSeats = @TotalSeats
    WHERE ID_Flight = @ID_Flight;
END;

----ELIMIAR O BAJAR VUELO 
CREATE PROCEDURE Del_BajFlight
    @ID_Flight int
AS
BEGIN
    DELETE FROM Flight
    WHERE ID_Flight = @ID_Flight;
END;



----insertar clase --------------listooo
CREATE PROCEDURE sp_InsertFlightClass
    @ID_Flight int,
    @Bussines_Class bit,
    @Premium_Class bit,
    @Tourist_Class bit
AS
BEGIN
    INSERT INTO Flight_Class (ID_Flight, Bussines_Class, Premium_Class, Tourist_Class)
    VALUES (@ID_Flight, @Bussines_Class, @Premium_Class, @Tourist_Class);
END;

---actualizar clase

CREATE PROCEDURE sp_UpdateFlightClass
    @ID_Flight_Class int,
    @ID_Flight int,
    @Bussines_Class bit,
    @Premium_Class bit,
    @Tourist_Class bit
AS
BEGIN
    UPDATE Flight_Class
    SET ID_Flight = @ID_Flight,
        Bussines_Class = @Bussines_Class,
        Premium_Class = @Premium_Class,
        Tourist_Class = @Tourist_Class
    WHERE ID_Flight_Class = @ID_Flight_Class;
END;

--bajar o eliminar 
CREATE PROCEDURE sp_DeleteFlightClass
    @ID_Flight_Class int
AS
BEGIN
    DELETE FROM Flight_Class
    WHERE ID_Flight_Class = @ID_Flight_Class;
END;


---INSERTAR ASIENTO ---listoooo

CREATE PROCEDURE sp_InsertFlightSeat
    @ID_Flight int,
    @Seat_Location nvarchar(50),
    @Seat_Number nvarchar(5),
    @Assigned_Seats int
AS
BEGIN
    INSERT INTO Flight_Seats (ID_Flight, Seat_Location, Seat_Number, Assigned_Seats)
    VALUES (@ID_Flight, @Seat_Location, @Seat_Number, @Assigned_Seats);
END;

---ACTUALIZAR ASIENTO

CREATE PROCEDURE sp_UpdateFlightSeat
    @ID_Seat int,
    @ID_Flight int,
    @Seat_Location nvarchar(50),
    @Seat_Number nvarchar(5),
    @Assigned_Seats int
AS
BEGIN
    UPDATE Flight_Seats
    SET ID_Flight = @ID_Flight,
        Seat_Location = @Seat_Location,
        Seat_Number = @Seat_Number,
        Assigned_Seats = @Assigned_Seats
    WHERE ID_Seat = @ID_Seat;
END;


---ELIMINAR O BAJAR ASIENTO
CREATE PROCEDURE sp_DeleteFlightSeat
    @ID_Seat int
AS
BEGIN
    DELETE FROM Flight_Seats
    WHERE ID_Seat = @ID_Seat;
END;


--Procedimiento de inserción en la tabla "Ticket" ----listooo

CREATE PROCEDURE RegistrarBoleto
 
    @ID_Flight int,
    @ID_Flight_Class int,
    @ID_Passenger int,
    @ID_Seat int,
    @Bought_Ticket datetime,
    @Ticket_Status bit,
    @Total_Price decimal(10,2)
AS
BEGIN
    INSERT INTO Det_Ticket (ID_Flight, ID_Flight_Class, ID_Passenger, ID_Seat, Bought_Ticket, Ticket_Status, Total_Price)
    VALUES (@ID_Flight, @ID_Flight_Class, @ID_Passenger, @ID_Seat, @Bought_Ticket, @Ticket_Status, @Total_Price);
END;



-----ACTUALIZAR TICKET 
CREATE PROCEDURE sp_UpdateDetTicket
    @ID_Ticket int,
    @ID_Flight int,
    @ID_Flight_Class int,
    @ID_Passenger int,
    @ID_Seat int,
    @Bought_Ticket datetime,
    @Ticket_Status bit,
    @Total_Price decimal(10,2)
AS
BEGIN
    UPDATE Det_Ticket
    SET ID_Flight = @ID_Flight,
        ID_Flight_Class = @ID_Flight_Class,
        ID_Passenger = @ID_Passenger,
        ID_Seat = @ID_Seat,
        Bought_Ticket = @Bought_Ticket,
        Ticket_Status = @Ticket_Status,
        Total_Price = @Total_Price
    WHERE ID_Ticket = @ID_Ticket;
END;

---EMILINAR O BAJAR 
CREATE PROCEDURE sp_DeleteDetTicket
    @ID_Ticket int
AS
BEGIN
    DELETE FROM Det_Ticket
    WHERE ID_Ticket = @ID_Ticket;
END;



----INSERTAR ITINERARIO -----LISTOOO

  ---Llenar la info del itinario cada que se agrega info en las tablas relacionadas 

CREATE PROCEDURE LlenarItinerario
    @ID_Flight int,
    @ID_Ticket int,
    @Airline nvarchar(30),
    @Country_Origin nvarchar(30),
    @Departure_DateTime datetime,
    @Country_Destination nvarchar(30),
    @Arrival_DateTime datetime,
    @ID_Flight_Class int
AS
BEGIN
    INSERT INTO Itinerario (
        ID_Flight,
        ID_Ticket,
        Airline,
        Country_Origin,
        Departure_DateTime,
        Country_Destination,
        Arrival_DateTime,
        ID_Flight_Class
    )
    VALUES (
        @ID_Flight,
        @ID_Ticket,
        @Airline,
        @Country_Origin,
        @Departure_DateTime,
        @Country_Destination,
        @Arrival_DateTime,
        @ID_Flight_Class
    );
END;


-- Trigger para actualizar el itinerario automáticamente
CREATE TRIGGER ActualizaItinerario
ON Itinerario
AFTER INSERT
AS
BEGIN
    DECLARE @ID_Passenger int, @ID_Flight int, @ID_Ticket int, @Airline nvarchar(30), @Country_Origin nvarchar(30);
    DECLARE @Departure_DateTime datetime, @Country_Destination nvarchar(30), @Arrival_DateTime datetime, @ID_Flight_Class int;

    SELECT @ID_Passenger = fo.ID_Passenger,
           @ID_Flight = f.ID_Flight,
           @ID_Ticket = dt.ID_Ticket,
           @Airline = fo.Airline,
           @Country_Origin = fo.Country,
           @Country_Destination = fd.Country,
           @Departure_DateTime = fo.DepartureDate,
           @Arrival_DateTime = fd.ArrivalDate,
           @ID_Flight_Class = dt.ID_Flight_Class
    FROM Flight_Origin fo
    JOIN Flight_Destination fd ON fo.ID_Passenger = fd.ID_Passenger
    JOIN Det_Ticket dt ON fo.ID_Passenger = dt.ID_Passenger
    JOIN Flight f ON fo.ID_Passenger = f.ID_Passenger
 
    INSERT INTO Itinerario (
        ID_Flight,
        ID_Ticket,
        Airline,
        Country_Origin,
        Departure_DateTime,
        Country_Destination,
        Arrival_DateTime,
        ID_Flight_Class
    )
    VALUES (
        @ID_Flight,
        @ID_Ticket,
        @Airline,
        @Country_Origin,
        @Departure_DateTime,
        @Country_Destination,
        @Arrival_DateTime,
        @ID_Flight_Class
    );
END;

--ELIMINAR-BAJA ITINERARIO 
CREATE PROCEDURE sp_DeleteItinerary
    @ID_Itinerary int
AS
BEGIN
    DELETE FROM Itinerario
    WHERE ID_Itinerary = @ID_Itinerary;
END;



--Procedimiento de inserción en la tabla "Cancellation"LISTOOO

CREATE PROCEDURE RegistrarCancelacion
    @ID_Ticket int,
    @ID_Passenger int,
    @Cancellation_Date datetime,
    @Reason nvarchar(300),
	@Refund bit
AS
BEGIN
    INSERT INTO Cancellation (ID_Ticket, ID_Passenger, Cancellation_Date,  Reason, Refund)
    VALUES (@ID_Ticket, @ID_Passenger, @Cancellation_Date, @Reason,@Refund )
END


---ACTUALIZAR-MODIF CANCELACION
CREATE PROCEDURE sp_UpdateCancellation
    @ID_Cancellation int,
    @ID_Ticket int,
    @ID_Passenger int,
    @Cancellation_Date datetime,
    @Reason nvarchar(300),
    @Refund bit
AS
BEGIN
    UPDATE Cancellation
    SET ID_Ticket = @ID_Ticket,
        ID_Passenger = @ID_Passenger,
        Cancellation_Date = @Cancellation_Date,
        Reason = @Reason,
        Refund = @Refund
    WHERE ID_Cancellation = @ID_Cancellation;
END;


---EMILINAR CANCELACION
CREATE PROCEDURE sp_DeleteCancellation
    @ID_Cancellation int
AS
BEGIN
    DELETE FROM Cancellation
    WHERE ID_Cancellation = @ID_Cancellation;
END;



--------Procedimiento Almacenado para verificar boleto activo---------------- LISTO

CREATE PROCEDURE sp_Verify_Boleto 
    @ID_Passenger int, 
    @Mensaje varchar (100) output
AS 
BEGIN 
    IF EXISTS (SELECT * FROM Det_Ticket WHERE ID_Passenger=@ID_Passenger AND Ticket_Status=1)
    BEGIN 
        SET @Mensaje = 'El boleto del pasajero aún está vigente'
    END 
    ELSE 
    BEGIN 
        SET @Mensaje ='El boleto del pasajero ha expirado'
    END 
END


DROP PROCEDURE sp_Verify_Boleto




--------Procedimiento Almacenado para Registrar el Número de Asientos de un Vuelo: listo

CREATE PROCEDURE RegistrarNumeroAsientos
    @ID_Flight int,
    @TotalSeats int
AS
BEGIN
    UPDATE Flight
    SET @TotalSeats = @TotalSeats
    WHERE ID_Flight = @ID_Flight;
END


select * from Flight

---Procedimiento para Actualizar el Número de Asientos Reservados y Emitir un Mensaje: LISTO

CREATE PROCEDURE ActualizarAsientosReservados
    @ID_Flight int -- ID del nuevo vuelo
AS
BEGIN
    DECLARE @TotalSeats int, @Assigned_Seats int,@Assigned_SeatsEnt int;

   

    -- Obtener el total de asientos disponibles en el vuelo
    SELECT @TotalSeats = TotalSeats FROM Flight WHERE ID_Flight = @ID_Flight;

    -- Obtener la cantidad de asientos ya asignados para ese vuelo
    SELECT @Assigned_Seats = COUNT(*) FROM Flight_Seats WHERE ID_Flight = @ID_Flight;
	select @Assigned_SeatsEnt =Assigned_Seats FROM FLIGHT_SEATS WHERE ID_FLIGHT = @ID_FLIGHT;

    -- Calcular la cantidad de asientos disponibles después de la reserva
    DECLARE @RemainingSeats int;
    SET @RemainingSeats = @TotalSeats - @Assigned_SeatsEnt;

    -- Actualizar la cantidad de asientos disponibles en la tabla Flight
    UPDATE Flight
    SET @TotalSeats = @RemainingSeats
    WHERE ID_Flight = @ID_Flight;

    -- Imprimir mensaje dependiendo de la disponibilidad de asientos
    IF @RemainingSeats = 0
    BEGIN
        PRINT 'Todos los asientos para este vuelo ya han sido reservados.';
    END
    ELSE
    BEGIN
        PRINT 'Asientos reservados exitosamente. Quedan ' + CAST(@RemainingSeats AS nvarchar(10)) + ' asientos disponibles.';
    END;
END;

EXEC ActualizarAsientosReservados 1

SELECT * FROM Flight

DROP PROCEDURE  ActualizarAsientosReservados




-- Trigger para asegurarse de que la fecha de emisión de un pasaporte sea anterior a la fecha de expiración  LISTO
CREATE TRIGGER ValidPassp
ON Passport
AFTER INSERT, UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE IssueDate >= ExpiryDate
    )
    BEGIN
        RAISERROR ('La fecha de emisión debe ser anterior a la fecha de vencimiento.', 16, 1);
        ROLLBACK;
    END
END



	  --------REEMBOLSO-------------LISTO
	  CREATE PROCEDURE CalculatePassengerRefund
    @ID_Passenger int,
    @Cancellation_Reason nvarchar(300),
    @RefundAmount decimal(10, 2) OUTPUT
AS
BEGIN
    DECLARE @IsAirlineIssue bit;
    DECLARE @DestinationDistance int;
    
    -- Verificar si la situación es de la aerolínea 
    SET @IsAirlineIssue = CASE
                             WHEN @Cancellation_Reason = 'Situación de la aerolínea' THEN 1
                             ELSE 0
                         END;
    
    -- Puedes definir @DestinationDistance directamente
    SET @DestinationDistance = 0; -- Cambia esto a la distancia deseada
    
    -- Calcular reembolso si la situación es de la aerolínea
    IF @IsAirlineIssue = 1
    BEGIN
        IF @DestinationDistance <= 1500 --KM
            SET @RefundAmount = 250.00;
        ELSE IF @DestinationDistance <= 3500 --KM
            SET @RefundAmount = 400.00;
        ELSE
            SET @RefundAmount = 600.00;
    END
    ELSE
    BEGIN
        SET @RefundAmount = 0.00;
    END;
END;


DROP PROCEDURE CalculatePassengerRefund



-----calcular precios de reservacion, tomar en cuenta si hay escala o no


create function Total_luggagePrice
(@id_luggage int)
returns int
as
 begin
 declare @total_luggage decimal(10,2)
declare @Extra_Luggage decimal(5,2)
 set @Extra_Luggage = (select Extra_Luggage from flight_luggage where id_luggage=@id_luggage)

 select @total_luggage = 50* @Extra_luggage
 return @total_luggage
 end

--Adaptar al cambio 
CREATE PROCEDURE FACTURA_TICKET --------------Listo
@ID_Ticket int,
@id_class int,
@Id_destination int,
@ID_STOP Int,
@id_luggage int
as 

   --tabla luggage 
   -- tabla flight_class
   declare @bussines_class bit
   declare @premium_class bit
   declare @tourist_class bit
   declare @total_price decimal(10,2)
 

   select @premium_class =  Premium_Class from Flight_Class where @id_class = ID_Flight_Class
   select @bussines_class =  Bussines_Class from Flight_Class where @id_class = ID_Flight_Class
   select @tourist_class =  Tourist_Class from Flight_Class where @id_class = ID_Flight_Class
   -- tabla flight_destination

   declare @destiny_price decimal(5,2)

   select @Destiny_price = Destiny_price from Flight_Destination where @Id_destination = ID_Destination

   -- tabla flight_stops
   declare @transhipment bit
   declare @transhipment_price decimal(5,2)
   declare @parnet_airline bit
   declare @PA_price decimal(5,2)

   select @transhipment = transhipment from Flight_Stops where @ID_STOP = ID_FlightStops
   select @transhipment_price = Transhipment_Price from Flight_Stops where @ID_STOP = ID_FlightStops
   select @parnet_airline = Partner_Airline from Flight_Stops where @ID_STOP = ID_FlightStops
   select @PA_price = PA_Price from Flight_Stops where @ID_STOP = ID_FlightStops

   -- inicio del procedimiento
  if (@tourist_class=1) -- presio clase turista
    begin
   if(@transhipment=1)
    begin 
      if(@parnet_airline=1)
       begin

           select @total_price = 500.00 + @destiny_price + @transhipment_price + @PA_price +dbo.Total_luggagePrice(@id_luggage)
       update Det_Ticket set Total_Price = @total_price where @ID_Ticket = ID_Ticket
       end
    else--precio total sin aerolinea amiga
          select @total_price = 500.00 + @destiny_price + @transhipment_price+ dbo.Total_luggagePrice(@id_luggage)
         update Det_Ticket set Total_Price = @total_price where @ID_Ticket = ID_Ticket
   end
    else --precio total sin transbordes
         select @total_price = 500.00 + @destiny_price + dbo.Total_luggagePrice(@id_luggage)

    update Det_Ticket set Total_Price = @total_price where @ID_Ticket = ID_Ticket
   end
  else --precio clase empresarial

   if (@bussines_class =1)
    begin
   if(@transhipment=1)
    begin 
      if(@parnet_airline=1)
       begin
           select @total_price = 1100.00 + @destiny_price + @transhipment_price + @PA_price + dbo.Total_luggagePrice(@id_luggage)
       update Det_Ticket set Total_Price = @total_price where @ID_Ticket = ID_Ticket
    
       end
    else--precio total sin aerolinea amiga
          select @total_price = 1100.00  + @destiny_price + @transhipment_price + dbo.Total_luggagePrice(@id_luggage)
         update Det_Ticket set Total_Price = @total_price where @ID_Ticket = ID_Ticket
   end
    else --precio total sin transbordes
         select @total_price = 1100.00 + @destiny_price + dbo.Total_luggagePrice(@id_luggage)
    update Det_Ticket set Total_Price = @total_price where @ID_Ticket = ID_Ticket
   end
  else

  if (@premium_class =1) --precio clase premium
    begin
   if(@transhipment=1)
    begin 
      if(@parnet_airline=1)
       begin
           select @total_price = 1900.00 + @destiny_price + @transhipment_price + @PA_price + dbo.Total_luggagePrice(@id_luggage)
       update Det_Ticket set Total_Price = @total_price where @ID_Ticket = ID_Ticket
       end
    else--precio total sin aerolinea amiga
          select @total_price = 1900.00 + @destiny_price + @transhipment_price + dbo.Total_luggagePrice(@id_luggage)
         update Det_Ticket set Total_Price = @total_price where @ID_Ticket = ID_Ticket
   end
    else --precio total sin transbordes
         select @total_price = 1900.00 + @destiny_price + dbo.


Total_luggagePrice(@id_luggage)
     update Det_Ticket set Total_Price = @total_price where @ID_Ticket = ID_Ticket
   end
 

 select * from Det_Ticket
  select * from Flight_Class
   select * from Flight_Destination
  select * from Flight_Stops
  select * from Flight_Luggage










 