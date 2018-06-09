-- MySQL Script generated by MySQL Workbench
-- sáb 05 mai 2018 15:29:36 WEST
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema jjmsdb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema jjmsdb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `jjmsdb` DEFAULT CHARACTER SET utf8 ;
USE `jjmsdb` ;

-- -----------------------------------------------------
-- Table `jjmsdb`.`Cliente`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `jjmsdb`.`Cliente` (
  `id` INT NOT NULL,
  `morada` VARCHAR(50) NOT NULL DEFAULT '\"\"',
  `telefone` VARCHAR(15) NOT NULL DEFAULT 0,
  `bloqueado` TINYINT NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `jjmsdb`.`Funcionario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `jjmsdb`.`Funcionario` (
  `id` INT NOT NULL,
  `zonaTrabalho` INT NULL DEFAULT 0,
  `nroEnc` INT NOT NULL DEFAULT 0,
  `avaliacao` FLOAT NOT NULL DEFAULT 0,
  `numAvaliacoes` INT NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `jjmsdb`.`Utilizador`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `jjmsdb`.`Utilizador` (
  `id` INT NOT NULL,
  `email` VARCHAR(30) NOT NULL DEFAULT '\"\"',
  `password` VARCHAR(35) NOT NULL DEFAULT '\"\"',
  `nome` VARCHAR(25) NOT NULL DEFAULT '\"\"',
  `idCliente` INT NULL,
  `idFuncionario` INT NULL,
  PRIMARY KEY (`id`, `idCliente`, `idFuncionario`),
  INDEX `fk_Utilizador_Cliente1_idx` (`idCliente` ASC),
  INDEX `fk_Utilizador_Funcionario1_idx` (`idFuncionario` ASC),
  CONSTRAINT `fk_Utilizador_Cliente1`
    FOREIGN KEY (`idCliente`)
    REFERENCES `jjmsdb`.`Cliente` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Utilizador_Funcionario1`
    FOREIGN KEY (`idFuncionario`)
    REFERENCES `jjmsdb`.`Funcionario` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `jjmsdb`.`Fornecedor`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `jjmsdb`.`Fornecedor` (
  `id` INT NOT NULL,
  `nome` VARCHAR(25) NOT NULL DEFAULT '\"\"',
  `morada` VARCHAR(50) NOT NULL DEFAULT '\"\"',
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `jjmsdb`.`CartaoCredito`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `jjmsdb`.`CartaoCredito` (
  `num` INT NOT NULL,
  `mes` INT NOT NULL DEFAULT 1,
  `ano` INT NOT NULL DEFAULT 1,
  `cvv` INT NOT NULL DEFAULT 0,
  `pais` VARCHAR(20) NOT NULL DEFAULT '\"\"',
  PRIMARY KEY (`num`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `jjmsdb`.`Encomenda`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `jjmsdb`.`Encomenda` (
  `id` INT NOT NULL,
  `estado` INT NOT NULL DEFAULT 1,
  `destino` VARCHAR(50) NOT NULL DEFAULT '\"\"',
  `fatura` LONGBLOB NULL,
  `dia` DATE NOT NULL DEFAULT 01-01-01,
  `hora` TIME NOT NULL DEFAULT 00:00:00,
  `custo` FLOAT NOT NULL DEFAULT 0,
  `avaliacao` INT NULL DEFAULT 0,
  `CartaoCredito` INT NOT NULL,
  `Cliente` INT NOT NULL,
  `idFornecedor` INT NOT NULL,
  `idFuncionario` INT NOT NULL,
  PRIMARY KEY (`id`, `CartaoCredito`, `Cliente`, `idFornecedor`, `idFuncionario`),
  INDEX `fk_Encomenda_CartaoCredito_idx` (`CartaoCredito` ASC),
  INDEX `fk_Encomenda_Cliente1_idx` (`Cliente` ASC),
  INDEX `fk_Encomenda_Fornecedor1_idx` (`idFornecedor` ASC),
  INDEX `fk_Encomenda_Funcionario1_idx` (`idFuncionario` ASC),
  CONSTRAINT `fk_Encomenda_CartaoCredito`
    FOREIGN KEY (`CartaoCredito`)
    REFERENCES `jjmsdb`.`CartaoCredito` (`num`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Encomenda_Cliente1`
    FOREIGN KEY (`Cliente`)
    REFERENCES `jjmsdb`.`Cliente` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Encomenda_Fornecedor1`
    FOREIGN KEY (`idFornecedor`)
    REFERENCES `jjmsdb`.`Fornecedor` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Encomenda_Funcionario1`
    FOREIGN KEY (`idFuncionario`)
    REFERENCES `jjmsdb`.`Funcionario` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
