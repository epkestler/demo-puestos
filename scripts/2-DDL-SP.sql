USE [Organizacion]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Esteban Palacios
-- Create date: 2025-08-01
-- Description:	
-- =============================================
CREATE PROCEDURE procEmpleadoInsert
	@iCodigo VARCHAR(20),
    @iIdPuesto INT,
    @iNombre VARCHAR(100),
    @iIdJefe INT = NULL
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Empleado (Codigo, IdPuesto, Nombre, IdJefe)
    VALUES (@iCodigo, @iIdPuesto, @iNombre, @iIdJefe);
END
GO


USE [Organizacion]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Esteban Palacios
-- Create date: 2025-08-01
-- Description:	
-- =============================================
CREATE PROCEDURE procEmpleadoId
	@iId VARCHAR(20) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT e.*, 
           p.Nombre AS NombrePuesto,
           jefe.Nombre AS NombreJefe
    FROM Empleado e
    JOIN Puesto p ON e.IdPuesto = p.Id
    LEFT JOIN Empleado jefe ON e.IdJefe = jefe.Id
    WHERE e.Id = @iId 

END
GO


USE [Organizacion]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Esteban Palacios
-- Create date: 2025-08-01
-- Description:	
-- =============================================
CREATE PROCEDURE procEmpleadoUpdate
	@iId INT,
    @iCodigo VARCHAR(20),
    @iIdPuesto INT,
    @iNombre VARCHAR(100),
    @iIdJefe INT = NULL
AS
BEGIN
	SET NOCOUNT ON;


	UPDATE Empleado
    SET Codigo = @iCodigo,
        IdPuesto = @iIdPuesto,
        Nombre = @iNombre,
        IdJefe = @iIdJefe
    WHERE Id = @iId;

END
GO


USE [Organizacion]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Esteban Palacios
-- Create date: 2025-08-01
-- Description:	
-- =============================================
CREATE PROCEDURE procEmpleadoDelete
    @iId INT
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Empleado SET IdJefe = NULL WHERE IdJefe = @iId
	DELETE FROM Empleado WHERE Id = @iId;
	--TODO: bitacora, si aplica
END
GO


USE [Organizacion]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Esteban Palacios
-- Create date: 2025-08-01
-- Description:	
-- =============================================
CREATE PROCEDURE procEmpleadoBusqueda
	@iId VARCHAR(20) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT e.*, 
           p.Nombre AS NombrePuesto,
           jefe.Nombre AS NombreJefe
    FROM Empleado e
    JOIN Puesto p ON e.IdPuesto = p.Id
    LEFT JOIN Empleado jefe ON e.IdJefe = jefe.Id
    

END
GO



USE [Organizacion]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Esteban Palacios
-- Create date: 2025-08-01
-- Description:	Catalogo de puestos
-- =============================================
CREATE PROCEDURE procPuestosId
	@iId VARCHAR(20) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id,
           Nombre,
		   NivelJerarquia
	FROM Puesto
	WHERE Id = @iId

END
GO

USE [Organizacion]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Esteban Palacios
-- Create date: 2025-08-01
-- Description:	Catalogo de puestos
-- =============================================
CREATE PROCEDURE procPuestosLista
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id,
           Nombre,
		   NivelJerarquia
	FROM Puesto

END
GO


exec procPuestosLista
exec [dbo].[procEmpleadoId] @iId =2