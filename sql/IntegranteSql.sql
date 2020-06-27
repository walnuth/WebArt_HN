IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Integrantes] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(200) NOT NULL,
    [Unidade] varchar(200) NOT NULL,
    [Matricula] varchar(50) NOT NULL,
    [FuncaoBordo] varchar(150) NOT NULL,
    [FuncaoContrato] varchar(150) NOT NULL,
    [Empresa] varchar(150) NOT NULL,
    [Endereco] varchar(350) NOT NULL,
    [Telefone] varchar(150) NOT NULL,
    [Email] varchar(150) NOT NULL,
    [ImgFoto] nvarchar(max) NOT NULL,
    [ImgSign] nvarchar(max) NOT NULL,
    [Nacionalidade] varchar(150) NOT NULL,
    [Admin] varchar(50) NOT NULL,
    [Ativo] varchar(50) NOT NULL,
    [Admissao] varchar(150) NOT NULL,
    [DoB] varchar(150) NOT NULL,
    CONSTRAINT [PK_Integrantes] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200626172010_Integrantes', N'3.1.5');

GO

