﻿/****** Object:  Table [dbo].[BitacoraPagos]    Script Date: 10/11/2016 12:38:18 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[BitacoraPagos](
	[ID_ARCHIVO] [INT] NOT NULL,
	[CV_CREDITO] [VARCHAR](15) NOT NULL,
	[CV_ESTATUS_PAGO] [INT] NOT NULL,
	[NU_PAGO_MES_CORRIENTE] [INT] NOT NULL,
	[IM_PAGO_MES_CORRIENTE] [NUMERIC](10,2) NOT NULL,
	[TX_PAGO_MES_CORRIENTE] [VARCHAR](150) NOT NULL,
	[FECHA_ALTA] [DATETIME] NOT NULL,
	[FECHA] [DATETIME] NOT NULL
)

GO

SET ANSI_PADDING OFF
GO


ALTER TABLE [dbo].[BitacoraPagos]  WITH CHECK ADD  CONSTRAINT [FK_Archivos_BitacoraPagos] FOREIGN KEY(ID_ARCHIVO)
REFERENCES [dbo].[Archivos] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[BitacoraPagos] CHECK CONSTRAINT [FK_Archivos_BitacoraPagos]
GO


ALTER TABLE [dbo].[BitacoraPagos]  WITH CHECK ADD  CONSTRAINT [FK_CatEstatusPagos_BitacoraPagos] FOREIGN KEY([CV_ESTATUS_PAGO])
REFERENCES [dbo].[CatEstatusPagos] ([ID_ESTATUS])
GO

ALTER TABLE [dbo].[BitacoraPagos] CHECK CONSTRAINT [FK_CatEstatusPagos_BitacoraPagos]
GO