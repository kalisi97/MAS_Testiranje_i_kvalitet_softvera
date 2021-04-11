CREATE VIEW [dbo].[PunoletneOsobe]
	AS SELECT o.OsobaId as id, o.MaticniBroj as mBroj,
    o.Ime as ime,
	o.Prezime as prezime,
	o.Visina as visina,
	o.Tezina as tezina,
	o.BojaOciju as boja_ociju,
	o.Telefon as kontakt,
	o.Email as email,
    o.DatumRodjenja as datum_rodj,
	o.Adresa as adresa,
	o.Napomena as napomena,
	m.MestoId as mesto_Id,
	m.Naziv as naziv_mesta
	FROM [dbo].[Osoba] as o INNER JOIN [dbo].[Mesto] as m 
	on o.MestoId = m.MestoId where DATEADD(YEAR,18,o.DatumRodjenja) < SYSDATETIME();