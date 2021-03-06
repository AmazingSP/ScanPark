USE [ScanParkDb]
GO
SET IDENTITY_INSERT [dbo].[CarParks] ON 

INSERT [dbo].[CarParks] ([CarParkId], [CarkParkName], [City], [Street], [ZipCode]) VALUES (1, N'Parkhaus Ludwigsplatz', N'Karlsruhe', N'Amalienstraße 10', N'76131')
INSERT [dbo].[CarParks] ([CarParkId], [CarkParkName], [City], [Street], [ZipCode]) VALUES (2, N'EWG-Parkhaus', N'Karlsruhe', N'Bürgerstraße 10', N'76131')
INSERT [dbo].[CarParks] ([CarParkId], [CarkParkName], [City], [Street], [ZipCode]) VALUES (3, N'Parkhaus Stehanplatz', N'Karlsruhe', N'Amalienstraße 33', N'76131')
INSERT [dbo].[CarParks] ([CarParkId], [CarkParkName], [City], [Street], [ZipCode]) VALUES (4, N'Parkhaus Herrenstraße / Zirkel', N'Karlsruhe', N'Herrenstraße 9', N'76131')
INSERT [dbo].[CarParks] ([CarParkId], [CarkParkName], [City], [Street], [ZipCode]) VALUES (5, N'Parkhaus Marktplatz', N'Karlsruhe', N'Kreuzstraße 13A', N'76131')
SET IDENTITY_INSERT [dbo].[CarParks] OFF
