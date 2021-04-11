CREATE PROCEDURE PROC_INSERT_MESTO

 @MestoId as INTEGER,
 @Naziv NVARCHAR(50),
 @Ptt INTEGER, 
 @BrojStanovnika INT = null

AS  
BEGIN  

INSERT INTO Mesto
VALUES (@MestoId, @Naziv, @Ptt, @BrojStanovnika)  

END