USE [ideapod_HealthHack]
GO
/****** Object:  Table [ideapod_healthhack].[Mutation_cDNA_test]    Script Date: 27/10/2013 4:03:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [ideapod_healthhack].[Mutation_cDNA_test](
	[Patient_ID] [varchar](50) NOT NULL,
	[Mutation_File_ID] [int] IDENTITY(1,1) NOT NULL,
	[Gene] [varchar](20) NULL,
	[Gene_Sequence_Locator] [varchar](50) NULL,
	[cDNA_reference] [varchar](20) NULL,
	[MCPosition] [int] NULL,
	[PreviousCodon] [char](3) NULL,
	[MutatedCodon] [char](3) NULL,
	[NextCodon] [char](3) NULL,
	[WT] [char](1) NULL,
	[Mut] [char](1) NULL,
	[WA_] [char](1) NULL,
	[_CG] [char](1) NULL,
	[CG_] [char](1) NULL,
	[WRC_] [char](1) NULL,
	[_GYW] [char](1) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
