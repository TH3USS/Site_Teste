# SiteTeste

-- Criação do banco de dados
CREATE DATABASE CadastroPessoasDB;
GO

-- Usar o banco de dados criado
USE CadastroPessoasDB;
GO

-- Criação da tabela Pessoa
CREATE TABLE Pessoa (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Telefone NVARCHAR(15) NOT NULL,
    CPF CHAR(11) NOT NULL UNIQUE
);

-- Criação da tabela Endereco
CREATE TABLE Endereco (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PessoaId INT NOT NULL,
    Logradouro NVARCHAR(200) NOT NULL,
    CEP CHAR(8) NOT NULL,
    Cidade NVARCHAR(100) NOT NULL,
    Estado CHAR(2) NOT NULL,
    CONSTRAINT FK_Enderecos_Pessoa FOREIGN KEY (PessoaId) REFERENCES Pessoa(Id) ON DELETE CASCADE
);
