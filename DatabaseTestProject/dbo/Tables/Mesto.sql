CREATE TABLE [dbo].[Mesto] (
    [MestoId]        INT           NOT NULL,
    [Naziv]          NVARCHAR (50) NOT NULL,
    [Ptt]            INT           NOT NULL,
    [BrojStanovnika] INT           NULL,
    PRIMARY KEY CLUSTERED ([MestoId] ASC),
    CONSTRAINT [CK_Mesto_BrojStanovnika] CHECK ([BrojStanovnika]>(0)),
    CONSTRAINT [CK_Mesto_Ptt] CHECK ([Ptt]>=(11000))
);


GO
CREATE TRIGGER [TriggerVelikoSlovo]
	ON [dbo].[Mesto]
	AFTER UPDATE, INSERT
AS
BEGIN
UPDATE dbo.Mesto
SET Naziv = UPPER(LEFT(I.Naziv,1))+LOWER(SUBSTRING(I.Naziv,2,LEN(I.Naziv)))
FROM Mesto M,INSERTED I
WHERE M.MestoId = I.MestoId
END