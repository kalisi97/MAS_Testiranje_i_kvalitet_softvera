CREATE TABLE [dbo].[Osoba] (
    [OsobaId]       INT           NOT NULL,
    [MaticniBroj]   VARCHAR (13)  NOT NULL,
    [Ime]           NVARCHAR (50) NOT NULL,
    [Prezime]       NVARCHAR (50) NOT NULL,
    [Visina]        INT           NULL,
    [Tezina]        INT           NULL,
    [BojaOciju]     INT           NULL,
    [Telefon]       VARCHAR (20)  NULL,
    [Email]         VARCHAR (50)  NOT NULL,
    [DatumRodjenja] DATE          NULL,
    [Adresa]        NVARCHAR (50) NULL,
    [Napomena]      NVARCHAR (50) NULL,
    [MestoId]       INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([OsobaId] ASC),
    CONSTRAINT [CK_Osoba_Email] CHECK ([Email] like '%.rs'),
    CONSTRAINT [CK_Osoba_Tezina] CHECK ([Tezina]<(250)),
    CONSTRAINT [CK_Osoba_Visina] CHECK ([Visina]>(35)),
    CONSTRAINT [FK_Osoba_Mesto] FOREIGN KEY ([MestoId]) REFERENCES [dbo].[Mesto] ([MestoId]),
    UNIQUE NONCLUSTERED ([MaticniBroj] ASC)
);


GO
CREATE TRIGGER [TriggerImePrezime]
	ON [dbo].[Osoba]
	AFTER UPDATE, INSERT
AS
BEGIN
UPDATE dbo.Osoba
SET Ime = UPPER(LEFT(I.Ime,1))+LOWER(SUBSTRING(I.Ime,2,LEN(I.Ime))),
Prezime = UPPER(LEFT(I.Prezime,1))+LOWER(SUBSTRING(I.Prezime,2,LEN(I.Prezime)))
FROM Osoba O,INSERTED I
WHERE O.OsobaId = I.OsobaId
END