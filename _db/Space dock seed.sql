USE SpaceDockDatabase

DELETE FROM cargo_card;
DELETE FROM commodity;
DELETE FROM commodity_type;
DELETE FROM docking_certificate;
DELETE FROM docks;
DELETE FROM faction;
DELETE FROM pilot;
DELETE FROM pilot_competencies;
DELETE FROM prices;
DELETE FROM ship;
DELETE FROM ship_class;

INSERT INTO faction (Name, Policy)
VALUES ('The Wardens','To protect'),('Xa-al Empire','To build an Empire'),('The United Kingdom of Earth and Northern Mars','Expansion of earth-based humans'),
('Free Merchant Society','To evolve trade among society'),('Legion of Hope','To share the hope of our loving god satan'),('New Frontier','To expand and solidify the border worlds'),
('Purity Initiative','To preserve purity of form'),('Sirius Sovereignty','To liberate once great sirius system');

INSERT INTO ship_class(Name, Designation)
VALUES ('Firefly','Freighter'),('Victory','Flagship'),('Se-ekh','Shuttle'),('Crusader','Battleship'),('Snowpiercer','Freighter'),('Hierophant','Battleship'),
('Stormwing','Interceptor'),('Dromedary','Freighter'),('Discovery','Worldship'),('Colonization','Worldship'),('Assault','Worldship'),('Aegis','Worldship'),
('Apostle','Battleship'),('A-laar','Worldship'),('Abundance','Transport');

INSERT INTO docks(Name, Location, Capacity)
VALUES ('Queen Elizabeth 2nd-s Drydock','Mars', 4),('London Spaceport','Earth', 20),('Worldship Dock','Mars', 7),('Freeport 5','Omega-41', 4),
('Tau system jumpgate construction site','Pluto', 6);

INSERT INTO commodity_type(Category)
VALUES ('Construction material'),('Nourishments'),('Energy container');

INSERT INTO commodity(Name, Category)
VALUES ('Aluminum_alloys',(SELECT ID FROM commodity_type WHERE commodity_type.category = 'Construction material')),
('Heat_resistant_alloys',(SELECT ID FROM commodity_type WHERE commodity_type.category = 'Construction material')),
('Basic_rations',(SELECT ID FROM commodity_type WHERE commodity_type.category = 'Nourishments')),
('Luxurious_rations',(SELECT ID FROM commodity_type WHERE commodity_type.category = 'Nourishments')),
('Plasma_cell',(SELECT ID FROM commodity_type WHERE commodity_type.category = 'Energy container')),
('Liquid_manacyte',(SELECT ID FROM commodity_type WHERE commodity_type.category = 'Energy container'))
;

INSERT INTO ship (Name, CargoSpace, ClassID )
VALUES ('HMS Victory',4000,(SELECT ID FROM ship_class WHERE ship_class.Name = 'Victory')),
('Serenity',512,(SELECT ID FROM ship_class WHERE ship_class.Name = 'Firefly')),
('Groth-iir',12,(SELECT ID FROM ship_class WHERE ship_class.Name = 'Se-ekh')),
('North Star',500,(SELECT ID FROM ship_class WHERE ship_class.Name = 'Crusader')),
('Sleipnir',460,(SELECT ID FROM ship_class WHERE ship_class.Name = 'Snowpiercer')),
('New Dawn',420,(SELECT ID FROM ship_class WHERE ship_class.Name = 'Hierophant')),
('Ratatoskr',460,(SELECT ID FROM ship_class WHERE ship_class.Name = 'Snowpiercer')),
('Summer Gale',7,(SELECT ID FROM ship_class WHERE ship_class.Name = 'Stormwing')),
('Swan song',512,(SELECT ID FROM ship_class WHERE ship_class.Name = 'Firefly')),
('Prometheus',50000000,(SELECT ID FROM ship_class WHERE ship_class.Name = 'Discovery')),
('Emerald Wind',100000000,(SELECT ID FROM ship_class WHERE ship_class.Name = 'Colonization')),
('Galaxy Lance',10000000,(SELECT ID FROM ship_class WHERE ship_class.Name = 'Assault')),
('Aegis of Worlds',10000000,(SELECT ID FROM ship_class WHERE ship_class.Name = 'Aegis')),
('Saviour',280,(SELECT ID FROM ship_class WHERE ship_class.Name = 'Apostle')),
('Thro-laar',1000000,(SELECT ID FROM ship_class WHERE ship_class.Name = 'A-laar')),
('Greh-laar',1000000,(SELECT ID FROM ship_class WHERE ship_class.Name = 'A-laar')),
('Khre-laar',1000000,(SELECT ID FROM ship_class WHERE ship_class.Name = 'A-laar')),
('Bruhmhilde',3060,(SELECT ID FROM ship_class WHERE ship_class.Name = 'Abundance'));

INSERT INTO pilot(Name, Birthday, OnHandCredits, ShipID, FactionID)
VALUES ('Malcolm Reynolds','35840920',1000000,(SELECT ID FROM ship WHERE ship.Name = 'Serenity'), (SELECT ID FROM faction WHERE faction.Name = 'Free Merchant Society')),
('Scott Blackburn','35721116',1000000,(SELECT ID FROM ship WHERE ship.Name = 'HMS Victory'), (SELECT ID FROM faction WHERE faction.Name = 'The United Kingdom of Earth and Northern Mars')),
('Khru-uhk','33560127',10000,(SELECT ID FROM ship WHERE ship.Name = 'Groth-iir'), (SELECT ID FROM faction WHERE faction.Name = 'Xa-al Empire')),
('Nakata Shizu','35980512',90000,(SELECT ID FROM ship WHERE ship.Name = 'North Star'), (SELECT ID FROM faction WHERE faction.Name = 'Legion of Hope')),
('Frida Knutsdottir','35871223',680000,(SELECT ID FROM ship WHERE ship.Name = 'Sleipnir'), (SELECT ID FROM faction WHERE faction.Name = 'New Frontier')),
('Aaditya Nehru','35900706',1000000,(SELECT ID FROM ship WHERE ship.Name = 'New Dawn'), (SELECT ID FROM faction WHERE faction.Name = 'Purity Initiative')),
('Thormund Freysson','35790514',1125000,(SELECT ID FROM ship WHERE ship.Name = 'Ratatoskr'), (SELECT ID FROM faction WHERE faction.Name = 'New Frontier')),
('Reinhardt Schneider','35640725',9050,(SELECT ID FROM ship WHERE ship.Name = 'Summer Gale'), (SELECT ID FROM faction WHERE faction.Name = 'Sirius Sovereignty')),
('Willbur Higgins','35810814',2000000,(SELECT ID FROM ship WHERE ship.Name = 'Swan song'), (SELECT ID FROM faction WHERE faction.Name = 'Independent')),
('Shiroe','19900610',1005000000,(SELECT ID FROM ship WHERE ship.Name = 'Prometheus'), (SELECT ID FROM faction WHERE faction.Name = 'The Wardens')),
('Leiria Lirien','19950312',1200000000,(SELECT ID FROM ship WHERE ship.Name = 'Emerald Wind'), (SELECT ID FROM faction WHERE faction.Name = 'The Wardens')),
('Artanis','33520101',150000000,(SELECT ID FROM ship WHERE ship.Name = 'Galaxy Lance'), (SELECT ID FROM faction WHERE faction.Name = 'The Wardens')),
('Sheppard','20890712',150000000,(SELECT ID FROM ship WHERE ship.Name = 'Aegis of Worlds'), (SELECT ID FROM faction WHERE faction.Name = 'The Wardens')),
('Jawaharlal Nehru','35900706',15000000,(SELECT ID FROM ship WHERE ship.Name = 'Saviour'), (SELECT ID FROM faction WHERE faction.Name = 'Purity Initiative')),
('Rha-khir','34290709',10000000,(SELECT ID FROM ship WHERE ship.Name = 'Thro-laar'), (SELECT ID FROM faction WHERE faction.Name = 'Xa-al Empire')),
('Skhre-ton','33911024',10000000,(SELECT ID FROM ship WHERE ship.Name = 'Greh-laar'), (SELECT ID FROM faction WHERE faction.Name = 'Xa-al Empire')),
('Gkon-tar','34851117',10000000,(SELECT ID FROM ship WHERE ship.Name = 'Khre-laar'), (SELECT ID FROM faction WHERE faction.Name = 'Xa-al Empire')),
('Nadja Neumann','35861006',800000,(SELECT ID FROM ship WHERE ship.Name = 'Bruhmhilde'), (SELECT ID FROM faction WHERE faction.Name = 'Sirius Sovereignty'));

INSERT INTO pilot_competencies( PilotID, Ship_classID)
VALUES ((SELECT ID FROM pilot WHERE pilot.Name = 'Malcolm Reynolds'),(SELECT ID FROM ship_class WHERE ship_class.Name = 'Firefly')),
((SELECT ID FROM pilot WHERE pilot.Name = 'Scott Blackburn'),(SELECT ID FROM ship_class WHERE ship_class.Name = 'Victory')),
((SELECT ID FROM pilot WHERE pilot.Name = 'Khru-uhk'),(SELECT ID FROM ship_class WHERE ship_class.Name = 'Se-ekh')),
((SELECT ID FROM pilot WHERE pilot.Name = 'Frida Knutsdottir'),(SELECT ID FROM ship_class WHERE ship_class.Name = 'Snowpiercer')),
((SELECT ID FROM pilot WHERE pilot.Name = 'Aaditya Nehru'),(SELECT ID FROM ship_class WHERE ship_class.Name = 'Hierophant')),
((SELECT ID FROM pilot WHERE pilot.Name = 'Thormund Freysson'),(SELECT ID FROM ship_class WHERE ship_class.Name = 'Snowpiercer')),
((SELECT ID FROM pilot WHERE pilot.Name = 'Reinhardt Schneider'),(SELECT ID FROM ship_class WHERE ship_class.Name = 'Stormwing')),
((SELECT ID FROM pilot WHERE pilot.Name = 'Willbur Higgins'),(SELECT ID FROM ship_class WHERE ship_class.Name = 'Firefly')),
((SELECT ID FROM pilot WHERE pilot.Name = 'Shiroe'),(SELECT ID FROM ship_class WHERE ship_class.Name = 'Discovery')),
((SELECT ID FROM pilot WHERE pilot.Name = 'Leiria Lirien'),(SELECT ID FROM ship_class WHERE ship_class.Name = 'Colonization')),
((SELECT ID FROM pilot WHERE pilot.Name = 'Artanis'),(SELECT ID FROM ship_class WHERE ship_class.Name = 'Assault')),
((SELECT ID FROM pilot WHERE pilot.Name = 'Sheppard'),(SELECT ID FROM ship_class WHERE ship_class.Name = 'Aegis')),
((SELECT ID FROM pilot WHERE pilot.Name = 'Jawaharlal Nehru'),(SELECT ID FROM ship_class WHERE ship_class.Name = 'Apostle')),
((SELECT ID FROM pilot WHERE pilot.Name = 'Rha-khir'),(SELECT ID FROM ship_class WHERE ship_class.Name = 'A-laar')),
((SELECT ID FROM pilot WHERE pilot.Name = 'Skhre-ton'),(SELECT ID FROM ship_class WHERE ship_class.Name = 'A-laar')),
((SELECT ID FROM pilot WHERE pilot.Name = 'Gkon-tar'),(SELECT ID FROM ship_class WHERE ship_class.Name = 'A-laar')),
((SELECT ID FROM pilot WHERE pilot.Name = 'Nadja Neumann'),(SELECT ID FROM ship_class WHERE ship_class.Name = 'Abundance')),
((SELECT ID FROM pilot WHERE pilot.Name = 'Leiria Lirien'),(SELECT ID FROM ship_class WHERE ship_class.Name = 'Aegis')),
((SELECT ID FROM pilot WHERE pilot.Name = 'Jawaharlal Nehru'),(SELECT ID FROM ship_class WHERE ship_class.Name = 'Hierophant')),
((SELECT ID FROM pilot WHERE pilot.Name = 'Aaditya Nehru'),(SELECT ID FROM ship_class WHERE ship_class.Name = 'Apostle'));

INSERT INTO prices(CommodityID, DockID, UnitPrice)
VALUES ((SELECT ID FROM commodity WHERE commodity.Name = 'Aluminum_alloys'),(SELECT ID FROM docks WHERE docks.Name = 'Queen Elizabeth 2nd-s Drydock'),500),
((SELECT ID FROM commodity WHERE commodity.Name = 'Heat_resistant_alloys'),(SELECT ID FROM docks WHERE docks.Name = 'Queen Elizabeth 2nd-s Drydock'),1000),
((SELECT ID FROM commodity WHERE commodity.Name = 'Basic_rations'),(SELECT ID FROM docks WHERE docks.Name = 'Queen Elizabeth 2nd-s Drydock'),200),
((SELECT ID FROM commodity WHERE commodity.Name = 'Luxurious_rations'),(SELECT ID FROM docks WHERE docks.Name = 'Queen Elizabeth 2nd-s Drydock'),1500),
((SELECT ID FROM commodity WHERE commodity.Name = 'Plasma_cell'),(SELECT ID FROM docks WHERE docks.Name = 'Queen Elizabeth 2nd-s Drydock'),5000),
((SELECT ID FROM commodity WHERE commodity.Name = 'Aluminum_alloys'),(SELECT ID FROM docks WHERE docks.Name = 'London Spaceport'),750),
((SELECT ID FROM commodity WHERE commodity.Name = 'Heat_resistant_alloys'),(SELECT ID FROM docks WHERE docks.Name = 'London Spaceport'),10000),
((SELECT ID FROM commodity WHERE commodity.Name = 'Basic_rations'),(SELECT ID FROM docks WHERE docks.Name = 'London Spaceport'),10),
((SELECT ID FROM commodity WHERE commodity.Name = 'Luxurious_rations'),(SELECT ID FROM docks WHERE docks.Name = 'London Spaceport'),750),
((SELECT ID FROM commodity WHERE commodity.Name = 'Plasma_cell'),(SELECT ID FROM docks WHERE docks.Name = 'London Spaceport'),3000),
((SELECT ID FROM commodity WHERE commodity.Name = 'Liquid_manacyte'),(SELECT ID FROM docks WHERE docks.Name = 'London Spaceport'),1000000),
((SELECT ID FROM commodity WHERE commodity.Name = 'Liquid_manacyte'),(SELECT ID FROM docks WHERE docks.Name = 'Worldship Dock'),2000000),
((SELECT ID FROM commodity WHERE commodity.Name = 'Plasma_cell'),(SELECT ID FROM docks WHERE docks.Name = 'Freeport 5'),5000),
((SELECT ID FROM commodity WHERE commodity.Name = 'Basic_rations'),(SELECT ID FROM docks WHERE docks.Name = 'Freeport 5'),300),
((SELECT ID FROM commodity WHERE commodity.Name = 'Aluminum_alloys'),(SELECT ID FROM docks WHERE docks.Name = 'Tau system jumpgate construction site'),1000),
((SELECT ID FROM commodity WHERE commodity.Name = 'Heat_resistant_alloys'),(SELECT ID FROM docks WHERE docks.Name = 'Tau system jumpgate construction site'),1700),
((SELECT ID FROM commodity WHERE commodity.Name = 'Basic_rations'),(SELECT ID FROM docks WHERE docks.Name = 'Tau system jumpgate construction site'),200);

INSERT INTO cargo_card(ShipID, CommodityID, NumberOfUnits)
VALUES ((SELECT ID FROM ship WHERE ship.Name = 'Serenity'), (SELECT ID FROM commodity WHERE commodity.Name = 'Plasma_cell'),326),
((SELECT ID FROM ship WHERE ship.Name = 'Groth-iir'), (SELECT ID FROM commodity WHERE commodity.Name = 'Plasma_cell'),2),
((SELECT ID FROM ship WHERE ship.Name = 'North Star'), (SELECT ID FROM commodity WHERE commodity.Name = 'Basic_rations'),400),
((SELECT ID FROM ship WHERE ship.Name = 'Sleipnir'), (SELECT ID FROM commodity WHERE commodity.Name = 'Luxurious_rations'),50),
((SELECT ID FROM ship WHERE ship.Name = 'New Dawn'), (SELECT ID FROM commodity WHERE commodity.Name = 'Aluminum_alloys'),75),
((SELECT ID FROM ship WHERE ship.Name = 'New Dawn'), (SELECT ID FROM commodity WHERE commodity.Name = 'Heat_resistant_alloys'),50),
((SELECT ID FROM ship WHERE ship.Name = 'Ratatoskr'), (SELECT ID FROM commodity WHERE commodity.Name = 'Aluminum_alloys'),400),
((SELECT ID FROM ship WHERE ship.Name = 'Summer Gale'), (SELECT ID FROM commodity WHERE commodity.Name = 'Plasma_cell'),5),
((SELECT ID FROM ship WHERE ship.Name = 'Swan song'), (SELECT ID FROM commodity WHERE commodity.Name = 'Plasma_cell'),25),
((SELECT ID FROM ship WHERE ship.Name = 'Prometheus'), (SELECT ID FROM commodity WHERE commodity.Name = 'Liquid_manacyte'),500),
((SELECT ID FROM ship WHERE ship.Name = 'Emerald Wind'), (SELECT ID FROM commodity WHERE commodity.Name = 'Liquid_manacyte'),1000),
((SELECT ID FROM ship WHERE ship.Name = 'Galaxy Lance'), (SELECT ID FROM commodity WHERE commodity.Name = 'Liquid_manacyte'),5000),
((SELECT ID FROM ship WHERE ship.Name = 'Aegis of Worlds'), (SELECT ID FROM commodity WHERE commodity.Name = 'Liquid_manacyte'),10000),
((SELECT ID FROM ship WHERE ship.Name = 'Saviour'), (SELECT ID FROM commodity WHERE commodity.Name = 'Heat_resistant_alloys'),200),
((SELECT ID FROM ship WHERE ship.Name = 'Saviour'), (SELECT ID FROM commodity WHERE commodity.Name = 'Plasma_cell'),6),
((SELECT ID FROM ship WHERE ship.Name = 'Thro-laar'), (SELECT ID FROM commodity WHERE commodity.Name = 'Aluminum_alloys'),250000),
((SELECT ID FROM ship WHERE ship.Name = 'Thro-laar'), (SELECT ID FROM commodity WHERE commodity.Name = 'Heat_resistant_alloys'),250000),
((SELECT ID FROM ship WHERE ship.Name = 'Greh-laar'), (SELECT ID FROM commodity WHERE commodity.Name = 'Plasma_cell'),300000),
((SELECT ID FROM ship WHERE ship.Name = 'Khre-laar'), (SELECT ID FROM commodity WHERE commodity.Name = 'Basic_rations'),800000),
((SELECT ID FROM ship WHERE ship.Name = 'Bruhmhilde'), (SELECT ID FROM commodity WHERE commodity.Name = 'Heat_resistant_alloys'),2000);

INSERT INTO docking_certificate(ShipID, DockID, ValidFrom)
VALUES ((SELECT ID FROM ship WHERE ship.Name = 'Serenity'), (SELECT ID FROM docks WHERE docks.Name = 'Freeport 5'),'36300915'),
((SELECT ID FROM ship WHERE ship.Name = 'HMS Victory'), (SELECT ID FROM docks WHERE docks.Name = 'London Spaceport'),'36300803'),
((SELECT ID FROM ship WHERE ship.Name = 'Groth-iir'), (SELECT ID FROM docks WHERE docks.Name = 'London Spaceport'),'36300921'),
((SELECT ID FROM ship WHERE ship.Name = 'North Star'), (SELECT ID FROM docks WHERE docks.Name = 'London Spaceport'),'36300921'),
((SELECT ID FROM ship WHERE ship.Name = 'Sleipnir'), (SELECT ID FROM docks WHERE docks.Name = 'Queen Elizabeth 2nd-s Drydock'),'36300827'),
((SELECT ID FROM ship WHERE ship.Name = 'New Dawn'), (SELECT ID FROM docks WHERE docks.Name = 'London Spaceport'),'36300222'),
((SELECT ID FROM ship WHERE ship.Name = 'Ratatoskr'), (SELECT ID FROM docks WHERE docks.Name = 'Tau system jumpgate construction site'),'36300918'),
((SELECT ID FROM ship WHERE ship.Name = 'Summer Gale'), (SELECT ID FROM docks WHERE docks.Name = 'Queen Elizabeth 2nd-s Drydock'),'36300921'),
((SELECT ID FROM ship WHERE ship.Name = 'Swan song'), (SELECT ID FROM docks WHERE docks.Name = 'Freeport 5'),'36300428'),
((SELECT ID FROM ship WHERE ship.Name = 'Prometheus'), (SELECT ID FROM docks WHERE docks.Name = 'Worldship Dock'),'36300921'),
((SELECT ID FROM ship WHERE ship.Name = 'Emerald Wind'), (SELECT ID FROM docks WHERE docks.Name = 'Worldship Dock'),'36200921'),
((SELECT ID FROM ship WHERE ship.Name = 'Galaxy Lance'), (SELECT ID FROM docks WHERE docks.Name = 'Worldship Dock'),'36260220'),
((SELECT ID FROM ship WHERE ship.Name = 'Aegis of Worlds'), (SELECT ID FROM docks WHERE docks.Name = 'Worldship Dock'),'36250714'),
((SELECT ID FROM ship WHERE ship.Name = 'Saviour'), (SELECT ID FROM docks WHERE docks.Name = 'Tau system jumpgate construction site'),'36300220'),
((SELECT ID FROM ship WHERE ship.Name = 'Thro-laar'), (SELECT ID FROM docks WHERE docks.Name = 'Worldship Dock'),'36300921'),
((SELECT ID FROM ship WHERE ship.Name = 'Greh-laar'), (SELECT ID FROM docks WHERE docks.Name = 'Worldship Dock'),'36300921'),
((SELECT ID FROM ship WHERE ship.Name = 'Khre-laar'), (SELECT ID FROM docks WHERE docks.Name = 'Worldship Dock'),'36300921'),
((SELECT ID FROM ship WHERE ship.Name = 'Bruhmhilde'), (SELECT ID FROM docks WHERE docks.Name = 'Tau system jumpgate construction site'),'36300921');