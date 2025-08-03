--Restriccion de alcances 
---unacamente se valida

USE [master]
GO

CREATE DATABASE [Organizacion]
GO

----Creación de catalogo de puestos
USE [Organizacion] 
GO

CREATE TABLE Puesto (
    Id INT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    NivelJerarquia INT NOT NULL
);

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Tabla que define los diferentes tipos de puestos y su jerarquía organizacional.',
    @level0type = N'SCHEMA', @level0name = 'dbo',
    @level1type = N'TABLE',  @level1name = 'Puesto';

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Identificador del puesto.',
    @level0type = N'SCHEMA', @level0name = 'dbo',
    @level1type = N'TABLE',  @level1name = 'Puesto',
    @level2type = N'COLUMN', @level2name = 'Id';

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Nombre del puesto, por ejemplo: Gerente, Sub Gerente, Supervisor, Operador, entre otros.',
    @level0type = N'SCHEMA', @level0name = 'dbo',
    @level1type = N'TABLE',  @level1name = 'Puesto',
    @level2type = N'COLUMN', @level2name = 'Nombre';

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Nivel jerárquico del puesto. Valores menores representan mayor jerarquía, con el objetivo de validar presedencia de los puestos',
    @level0type = N'SCHEMA', @level0name = 'dbo',
    @level1type = N'TABLE',  @level1name = 'Puesto',
    @level2type = N'COLUMN', @level2name = 'NivelJerarquia';

GO


--Creación de la table de empleados
USE [Organizacion] 
GO

CREATE TABLE Empleado (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(20) NULL,
    IdPuesto INT NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    IdJefe INT NULL,
    FOREIGN KEY (IdPuesto) REFERENCES Puesto(Id),
    FOREIGN KEY (IdJefe) REFERENCES Empleado(Id) ON DELETE SET NULL
);


EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Tabla que almacena los empleados de la empresa.',
    @level0type = N'SCHEMA',  @level0name = 'dbo',
    @level1type = N'TABLE',   @level1name = 'Empleado';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Identificador interno único del empleado.',
    @level0type = N'SCHEMA',  @level0name = 'dbo',
    @level1type = N'TABLE',   @level1name = 'Empleado',
    @level2type = N'COLUMN',  @level2name = 'Id';


EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Código del empleado asignado por RRHH.',
    @level0type = N'SCHEMA',  @level0name = 'dbo',
    @level1type = N'TABLE',   @level1name = 'Empleado',
    @level2type = N'COLUMN',  @level2name = 'Codigo';

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Referencia al puesto que ocupa el empleado ver tabla Puesto.',
    @level0type = N'SCHEMA',  @level0name = 'dbo',
    @level1type = N'TABLE',   @level1name = 'Empleado',
    @level2type = N'COLUMN',  @level2name = 'IdPuesto';

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Nombre completo del empleado.',
    @level0type = N'SCHEMA',  @level0name = 'dbo',
    @level1type = N'TABLE',   @level1name = 'Empleado',
    @level2type = N'COLUMN',  @level2name = 'Nombre';

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'Referencia al jefe inmediato del empleado.',
    @level0type = N'SCHEMA',  @level0name = 'dbo',
    @level1type = N'TABLE',   @level1name = 'Empleado',
    @level2type = N'COLUMN',  @level2name = 'IdJefe';

