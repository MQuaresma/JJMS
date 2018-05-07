USE [master]
GO

/****** Object:  Database [jjmsdb]    Script Date: 05/05/2018 17:29:06 ******/
CREATE DATABASE [jjmsdb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'jjmsdb', FILENAME = N'/var/opt/mssql/data/jjmsdb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'jjmsdb_log', FILENAME = N'/var/opt/mssql/data/jjmsdb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

ALTER DATABASE [jjmsdb] SET COMPATIBILITY_LEVEL = 140
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [jjmsdb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [jjmsdb] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [jjmsdb] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [jjmsdb] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [jjmsdb] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [jjmsdb] SET ARITHABORT OFF 
GO

ALTER DATABASE [jjmsdb] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [jjmsdb] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [jjmsdb] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [jjmsdb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [jjmsdb] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [jjmsdb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [jjmsdb] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [jjmsdb] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [jjmsdb] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [jjmsdb] SET  DISABLE_BROKER 
GO

ALTER DATABASE [jjmsdb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [jjmsdb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [jjmsdb] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [jjmsdb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [jjmsdb] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [jjmsdb] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [jjmsdb] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [jjmsdb] SET RECOVERY FULL 
GO

ALTER DATABASE [jjmsdb] SET  MULTI_USER 
GO

ALTER DATABASE [jjmsdb] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [jjmsdb] SET DB_CHAINING OFF 
GO

ALTER DATABASE [jjmsdb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [jjmsdb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [jjmsdb] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [jjmsdb] SET QUERY_STORE = OFF
GO

USE [jjmsdb]
GO

ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO

ALTER DATABASE [jjmsdb] SET  READ_WRITE 
GO

USE jjmsdb
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'jjmsdb')      
     EXEC (N'CREATE SCHEMA jjmsdb')                                   
 GO                                                               

USE jjmsdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'cartaocredito'  AND sc.name = N'jjmsdb'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'cartaocredito'  AND sc.name = N'jjmsdb'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [jjmsdb].[cartaocredito]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[jjmsdb].[cartaocredito]
(
   [num] int  NOT NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0052: string literal was converted to NUMERIC literal
   */

   [mes] int  NOT NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0052: string literal was converted to NUMERIC literal
   */

   [ano] int  NOT NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0052: string literal was converted to NUMERIC literal
   */

   [cvv] int  NOT NULL,
   [pais] nvarchar(20)  NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'jjmsdb.cartaocredito',
        N'SCHEMA', N'jjmsdb',
        N'TABLE', N'cartaocredito'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE jjmsdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'cliente'  AND sc.name = N'jjmsdb'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'cliente'  AND sc.name = N'jjmsdb'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [jjmsdb].[cliente]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[jjmsdb].[cliente]
(
   [id] int  NOT NULL,
   [morada] nvarchar(50)  NOT NULL,
   [telefone] nvarchar(15)  NOT NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0052: string literal was converted to NUMERIC literal
   */

   [bloqueado] smallint  NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'jjmsdb.cliente',
        N'SCHEMA', N'jjmsdb',
        N'TABLE', N'cliente'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE jjmsdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'encomenda'  AND sc.name = N'jjmsdb'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'encomenda'  AND sc.name = N'jjmsdb'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [jjmsdb].[encomenda]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[jjmsdb].[encomenda]
(
   [id] int  NOT NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0052: string literal was converted to NUMERIC literal
   */

   [estado] int  NOT NULL,
   [destino] nvarchar(50)  NOT NULL,
   [fatura] varbinary(max)  NULL,
   [dia] date  NOT NULL,
   [hora] time  NOT NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0052: string literal was converted to NUMERIC literal
   */

   [custo] float(24)  NOT NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0052: string literal was converted to NUMERIC literal
   */

   [avaliacao] int  NULL,
   [CartaoCredito] int  NOT NULL,
   [Cliente] int  NOT NULL,
   [idFornecedor] int  NOT NULL,
   [idFuncionario] int  NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'jjmsdb.encomenda',
        N'SCHEMA', N'jjmsdb',
        N'TABLE', N'encomenda'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE jjmsdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'fornecedor'  AND sc.name = N'jjmsdb'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'fornecedor'  AND sc.name = N'jjmsdb'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [jjmsdb].[fornecedor]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[jjmsdb].[fornecedor]
(
   [id] int  NOT NULL,
   [nome] nvarchar(25)  NOT NULL,
   [morada] nvarchar(50)  NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'jjmsdb.fornecedor',
        N'SCHEMA', N'jjmsdb',
        N'TABLE', N'fornecedor'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE jjmsdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'funcionario'  AND sc.name = N'jjmsdb'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'funcionario'  AND sc.name = N'jjmsdb'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [jjmsdb].[funcionario]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[jjmsdb].[funcionario]
(
   [id] int  NOT NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0052: string literal was converted to NUMERIC literal
   */

   [zonaTrabalho] int  NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0052: string literal was converted to NUMERIC literal
   */

   [nroEnc] int  NOT NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0052: string literal was converted to NUMERIC literal
   */

   [avaliacao] float(24)  NOT NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0052: string literal was converted to NUMERIC literal
   */

   [numAvaliacoes] int  NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'jjmsdb.funcionario',
        N'SCHEMA', N'jjmsdb',
        N'TABLE', N'funcionario'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE jjmsdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'utilizador'  AND sc.name = N'jjmsdb'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'utilizador'  AND sc.name = N'jjmsdb'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [jjmsdb].[utilizador]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[jjmsdb].[utilizador]
(
   [id] int  NOT NULL,
   [email] nvarchar(30)  NOT NULL,
   [password] nvarchar(35)  NOT NULL,
   [nome] nvarchar(25)  NOT NULL,
   [idCliente] int  NULL,
   [idFuncionario] int  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'jjmsdb.utilizador',
        N'SCHEMA', N'jjmsdb',
        N'TABLE', N'utilizador'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE jjmsdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_cartaocredito_num'  AND sc.name = N'jjmsdb'  AND type in (N'PK'))
ALTER TABLE [jjmsdb].[cartaocredito] DROP CONSTRAINT [PK_cartaocredito_num]
 GO



ALTER TABLE [jjmsdb].[cartaocredito]
 ADD CONSTRAINT [PK_cartaocredito_num]
   PRIMARY KEY
   CLUSTERED ([num] ASC)

GO


USE jjmsdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_cliente_id'  AND sc.name = N'jjmsdb'  AND type in (N'PK'))
ALTER TABLE [jjmsdb].[cliente] DROP CONSTRAINT [PK_cliente_id]
 GO



ALTER TABLE [jjmsdb].[cliente]
 ADD CONSTRAINT [PK_cliente_id]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE jjmsdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_encomenda_id'  AND sc.name = N'jjmsdb'  AND type in (N'PK'))
ALTER TABLE [jjmsdb].[encomenda] DROP CONSTRAINT [PK_encomenda_id]
 GO



ALTER TABLE [jjmsdb].[encomenda]
 ADD CONSTRAINT [PK_encomenda_id]
   PRIMARY KEY
   CLUSTERED ([id] ASC, [CartaoCredito] ASC, [Cliente] ASC, [idFornecedor] ASC, [idFuncionario] ASC)

GO


USE jjmsdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_fornecedor_id'  AND sc.name = N'jjmsdb'  AND type in (N'PK'))
ALTER TABLE [jjmsdb].[fornecedor] DROP CONSTRAINT [PK_fornecedor_id]
 GO



ALTER TABLE [jjmsdb].[fornecedor]
 ADD CONSTRAINT [PK_fornecedor_id]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE jjmsdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_funcionario_id'  AND sc.name = N'jjmsdb'  AND type in (N'PK'))
ALTER TABLE [jjmsdb].[funcionario] DROP CONSTRAINT [PK_funcionario_id]
 GO



ALTER TABLE [jjmsdb].[funcionario]
 ADD CONSTRAINT [PK_funcionario_id]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE jjmsdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_utilizador_id'  AND sc.name = N'jjmsdb'  AND type in (N'PK'))
ALTER TABLE [jjmsdb].[utilizador] DROP CONSTRAINT [PK_utilizador_id]
 GO



ALTER TABLE [jjmsdb].[utilizador]
 ADD CONSTRAINT [PK_utilizador_id]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE jjmsdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'utilizador$idCliente'  AND sc.name = N'jjmsdb'  AND type in (N'UQ'))
ALTER TABLE [jjmsdb].[utilizador] DROP CONSTRAINT [utilizador$idCliente]
 GO



ALTER TABLE [jjmsdb].[utilizador]
 ADD CONSTRAINT [utilizador$idCliente]
 UNIQUE 
   NONCLUSTERED ([idCliente] ASC, [idFuncionario] ASC)

GO


USE jjmsdb
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'encomenda'  AND sc.name = N'jjmsdb'  AND si.name = N'fk_Encomenda_CartaoCredito_idx' AND so.type in (N'U'))
   DROP INDEX [fk_Encomenda_CartaoCredito_idx] ON [jjmsdb].[encomenda] 
GO
CREATE NONCLUSTERED INDEX [fk_Encomenda_CartaoCredito_idx] ON [jjmsdb].[encomenda]
(
   [CartaoCredito] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE jjmsdb
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'encomenda'  AND sc.name = N'jjmsdb'  AND si.name = N'fk_Encomenda_Cliente1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_Encomenda_Cliente1_idx] ON [jjmsdb].[encomenda] 
GO
CREATE NONCLUSTERED INDEX [fk_Encomenda_Cliente1_idx] ON [jjmsdb].[encomenda]
(
   [Cliente] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE jjmsdb
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'encomenda'  AND sc.name = N'jjmsdb'  AND si.name = N'fk_Encomenda_Fornecedor1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_Encomenda_Fornecedor1_idx] ON [jjmsdb].[encomenda] 
GO
CREATE NONCLUSTERED INDEX [fk_Encomenda_Fornecedor1_idx] ON [jjmsdb].[encomenda]
(
   [idFornecedor] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE jjmsdb
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'encomenda'  AND sc.name = N'jjmsdb'  AND si.name = N'fk_Encomenda_Funcionario1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_Encomenda_Funcionario1_idx] ON [jjmsdb].[encomenda] 
GO
CREATE NONCLUSTERED INDEX [fk_Encomenda_Funcionario1_idx] ON [jjmsdb].[encomenda]
(
   [idFuncionario] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE jjmsdb
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'utilizador'  AND sc.name = N'jjmsdb'  AND si.name = N'fk_Utilizador_Cliente1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_Utilizador_Cliente1_idx] ON [jjmsdb].[utilizador] 
GO
CREATE NONCLUSTERED INDEX [fk_Utilizador_Cliente1_idx] ON [jjmsdb].[utilizador]
(
   [idCliente] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE jjmsdb
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'utilizador'  AND sc.name = N'jjmsdb'  AND si.name = N'fk_Utilizador_Funcionario1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_Utilizador_Funcionario1_idx] ON [jjmsdb].[utilizador] 
GO
CREATE NONCLUSTERED INDEX [fk_Utilizador_Funcionario1_idx] ON [jjmsdb].[utilizador]
(
   [idFuncionario] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE jjmsdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'encomenda$fk_Encomenda_CartaoCredito'  AND sc.name = N'jjmsdb'  AND type in (N'F'))
ALTER TABLE [jjmsdb].[encomenda] DROP CONSTRAINT [encomenda$fk_Encomenda_CartaoCredito]
 GO



ALTER TABLE [jjmsdb].[encomenda]
 ADD CONSTRAINT [encomenda$fk_Encomenda_CartaoCredito]
 FOREIGN KEY 
   ([CartaoCredito])
 REFERENCES 
   [jjmsdb].[jjmsdb].[cartaocredito]     ([num])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'encomenda$fk_Encomenda_Cliente1'  AND sc.name = N'jjmsdb'  AND type in (N'F'))
ALTER TABLE [jjmsdb].[encomenda] DROP CONSTRAINT [encomenda$fk_Encomenda_Cliente1]
 GO



ALTER TABLE [jjmsdb].[encomenda]
 ADD CONSTRAINT [encomenda$fk_Encomenda_Cliente1]
 FOREIGN KEY 
   ([Cliente])
 REFERENCES 
   [jjmsdb].[jjmsdb].[cliente]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'encomenda$fk_Encomenda_Fornecedor1'  AND sc.name = N'jjmsdb'  AND type in (N'F'))
ALTER TABLE [jjmsdb].[encomenda] DROP CONSTRAINT [encomenda$fk_Encomenda_Fornecedor1]
 GO



ALTER TABLE [jjmsdb].[encomenda]
 ADD CONSTRAINT [encomenda$fk_Encomenda_Fornecedor1]
 FOREIGN KEY 
   ([idFornecedor])
 REFERENCES 
   [jjmsdb].[jjmsdb].[fornecedor]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'encomenda$fk_Encomenda_Funcionario1'  AND sc.name = N'jjmsdb'  AND type in (N'F'))
ALTER TABLE [jjmsdb].[encomenda] DROP CONSTRAINT [encomenda$fk_Encomenda_Funcionario1]
 GO



ALTER TABLE [jjmsdb].[encomenda]
 ADD CONSTRAINT [encomenda$fk_Encomenda_Funcionario1]
 FOREIGN KEY 
   ([idFuncionario])
 REFERENCES 
   [jjmsdb].[jjmsdb].[funcionario]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE jjmsdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'utilizador$fk_Utilizador_Cliente1'  AND sc.name = N'jjmsdb'  AND type in (N'F'))
ALTER TABLE [jjmsdb].[utilizador] DROP CONSTRAINT [utilizador$fk_Utilizador_Cliente1]
 GO



ALTER TABLE [jjmsdb].[utilizador]
 ADD CONSTRAINT [utilizador$fk_Utilizador_Cliente1]
 FOREIGN KEY 
   ([idCliente])
 REFERENCES 
   [jjmsdb].[jjmsdb].[cliente]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'utilizador$fk_Utilizador_Funcionario1'  AND sc.name = N'jjmsdb'  AND type in (N'F'))
ALTER TABLE [jjmsdb].[utilizador] DROP CONSTRAINT [utilizador$fk_Utilizador_Funcionario1]
 GO



ALTER TABLE [jjmsdb].[utilizador]
 ADD CONSTRAINT [utilizador$fk_Utilizador_Funcionario1]
 FOREIGN KEY 
   ([idFuncionario])
 REFERENCES 
   [jjmsdb].[jjmsdb].[funcionario]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE jjmsdb
GO
ALTER TABLE  [jjmsdb].[cartaocredito]
 ADD DEFAULT 1 FOR [mes]
GO

ALTER TABLE  [jjmsdb].[cartaocredito]
 ADD DEFAULT 1 FOR [ano]
GO

ALTER TABLE  [jjmsdb].[cartaocredito]
 ADD DEFAULT 0 FOR [cvv]
GO

ALTER TABLE  [jjmsdb].[cartaocredito]
 ADD DEFAULT N'""' FOR [pais]
GO


USE jjmsdb
GO
ALTER TABLE  [jjmsdb].[cliente]
 ADD DEFAULT N'""' FOR [morada]
GO

ALTER TABLE  [jjmsdb].[cliente]
 ADD DEFAULT N'0' FOR [telefone]
GO

ALTER TABLE  [jjmsdb].[cliente]
 ADD DEFAULT 0 FOR [bloqueado]
GO


USE jjmsdb
GO
ALTER TABLE  [jjmsdb].[encomenda]
 ADD DEFAULT 1 FOR [estado]
GO

ALTER TABLE  [jjmsdb].[encomenda]
 ADD DEFAULT N'""' FOR [destino]
GO

ALTER TABLE  [jjmsdb].[encomenda]
 ADD DEFAULT '2001-01-01' FOR [dia]
GO

ALTER TABLE  [jjmsdb].[encomenda]
 ADD DEFAULT '00:00:00' FOR [hora]
GO

ALTER TABLE  [jjmsdb].[encomenda]
 ADD DEFAULT 0 FOR [custo]
GO

ALTER TABLE  [jjmsdb].[encomenda]
 ADD DEFAULT 0 FOR [avaliacao]
GO


USE jjmsdb
GO
ALTER TABLE  [jjmsdb].[fornecedor]
 ADD DEFAULT N'""' FOR [nome]
GO

ALTER TABLE  [jjmsdb].[fornecedor]
 ADD DEFAULT N'""' FOR [morada]
GO


USE jjmsdb
GO
ALTER TABLE  [jjmsdb].[funcionario]
 ADD DEFAULT 0 FOR [zonaTrabalho]
GO

ALTER TABLE  [jjmsdb].[funcionario]
 ADD DEFAULT 0 FOR [nroEnc]
GO

ALTER TABLE  [jjmsdb].[funcionario]
 ADD DEFAULT 0 FOR [avaliacao]
GO

ALTER TABLE  [jjmsdb].[funcionario]
 ADD DEFAULT 0 FOR [numAvaliacoes]
GO


USE jjmsdb
GO
ALTER TABLE  [jjmsdb].[utilizador]
 ADD DEFAULT N'""' FOR [email]
GO

ALTER TABLE  [jjmsdb].[utilizador]
 ADD DEFAULT N'""' FOR [password]
GO

ALTER TABLE  [jjmsdb].[utilizador]
 ADD DEFAULT N'""' FOR [nome]
GO

ALTER TABLE  [jjmsdb].[utilizador]
 ADD DEFAULT NULL FOR [idCliente]
GO

ALTER TABLE  [jjmsdb].[utilizador]
 ADD DEFAULT NULL FOR [idFuncionario]
GO

