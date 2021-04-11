CREATE PROCEDURE PROC_INSERT_OSOBA

 @OsobaId as INTEGER,
 @MaticniBroj VARCHAR(13),
 @Ime NVARCHAR(50), 
 @Prezime NVARCHAR(50),
 @Visina INT = null, 
 @Tezina INTEGER = null,
 @BojaOciju INTEGER = null,
 @Telefon VARCHAR(20) = null,
 @Email VARCHAR(50),
 @DatumRodjenja DATE,
 @Adresa NVARCHAR(50) = null,
 @Napomena NVARCHAR(50) = null,
 @MestoId INTEGER
 
AS  
	IF exists(SELECT 1 FROM Mesto WHERE MestoId = @MestoId)
BEGIN  
 INSERT INTO Osoba VALUES (@OsobaId, @MaticniBroj, @Ime, @Prezime, @Visina, @Tezina, @BojaOciju, @Telefon, @Email, @DatumRodjenja, @Adresa, @Napomena, @MestoId)  
END