/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     27/09/2014 03:46:51 p.m.                     */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('US_CAT_ESTATUS_USUARIO') and o.name = 'FK_US_CAT_E_REFERENCE_US_LOG_R')
alter table US_CAT_ESTATUS_USUARIO
   drop constraint FK_US_CAT_E_REFERENCE_US_LOG_R
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('US_CAT_NIVEL_USUARIO') and o.name = 'FK_US_CAT_N_REFERENCE_US_LOG_R')
alter table US_CAT_NIVEL_USUARIO
   drop constraint FK_US_CAT_N_REFERENCE_US_LOG_R
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('US_CAT_TIPO_USUARIO') and o.name = 'FK_US_CAT_T_REFERENCE_US_LOG_R')
alter table US_CAT_TIPO_USUARIO
   drop constraint FK_US_CAT_T_REFERENCE_US_LOG_R
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('US_HISTORIAL') and o.name = 'FK_US_HISTO_REFERENCE_US_LOG_R')
alter table US_HISTORIAL
   drop constraint FK_US_HISTO_REFERENCE_US_LOG_R
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('US_USUARIOS') and o.name = 'FK_US_USUAR_REFERENCE_US_CAT_T')
alter table US_USUARIOS
   drop constraint FK_US_USUAR_REFERENCE_US_CAT_T
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('US_USUARIOS') and o.name = 'FK_US_USUAR_REFERENCE_US_CAT_E')
alter table US_USUARIOS
   drop constraint FK_US_USUAR_REFERENCE_US_CAT_E
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('US_USUARIOS') and o.name = 'FK_US_USUAR_REFERENCE_US_CAT_N')
alter table US_USUARIOS
   drop constraint FK_US_USUAR_REFERENCE_US_CAT_N
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('US_USUARIOS') and o.name = 'FK_US_USUAR_REFERENCE_US_HISTO')
alter table US_USUARIOS
   drop constraint FK_US_USUAR_REFERENCE_US_HISTO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('US_USUARIOS') and o.name = 'FK_US_USUAR_REFERENCE_US_LOG_R')
alter table US_USUARIOS
   drop constraint FK_US_USUAR_REFERENCE_US_LOG_R
go

if exists (select 1
            from  sysobjects
           where  id = object_id('US_CAT_ESTATUS_USUARIO')
            and   type = 'U')
   drop table US_CAT_ESTATUS_USUARIO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('US_CAT_NIVEL_USUARIO')
            and   type = 'U')
   drop table US_CAT_NIVEL_USUARIO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('US_CAT_TIPO_USUARIO')
            and   type = 'U')
   drop table US_CAT_TIPO_USUARIO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('US_HISTORIAL')
            and   type = 'U')
   drop table US_HISTORIAL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('US_LOG_REGISTRO')
            and   type = 'U')
   drop table US_LOG_REGISTRO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('US_USUARIOS')
            and   type = 'U')
   drop table US_USUARIOS
go

/*==============================================================*/
/* Table: US_CAT_ESTATUS_USUARIO                                */
/*==============================================================*/
create table US_CAT_ESTATUS_USUARIO (
   ID_ESTATUS_USUARIOS  int                  identity,
   ID_LOG_REGISTRO      int                  null,
   ESTATUS_USUARIO      varchar(30)          not null,
   DESCRIPCION          varchar(150)         not null,
   constraint PK_US_CAT_ESTATUS_USUARIO primary key (ID_ESTATUS_USUARIOS)
)
go

/*==============================================================*/
/* Table: US_CAT_NIVEL_USUARIO                                  */
/*==============================================================*/
create table US_CAT_NIVEL_USUARIO (
   ID_NIVEL_USUARIO     int                  identity,
   ID_LOG_REGISTRO      int                  null,
   NIVEL_USUARIO        varchar(30)          not null,
   DESCRIPCION          varchar(150)         not null,
   constraint PK_US_CAT_NIVEL_USUARIO primary key (ID_NIVEL_USUARIO)
)
go

/*==============================================================*/
/* Table: US_CAT_TIPO_USUARIO                                   */
/*==============================================================*/
create table US_CAT_TIPO_USUARIO (
   ID_TIPO_USUARIO      int                  identity,
   ID_LOG_REGISTRO      int                  null,
   TIPO_USUARIO         varchar(30)          not null,
   DESCRIPCION          varchar(150)         null,
   constraint PK_US_CAT_TIPO_USUARIO primary key (ID_TIPO_USUARIO)
)
go

/*==============================================================*/
/* Table: US_HISTORIAL                                          */
/*==============================================================*/
create table US_HISTORIAL (
   ID_HISTORIAL         int                  identity,
   ID_LOG_REGISTRO      int                  null,
   FECHA_ULTIMA_CESION  datetime             null,
   FECHA_CAMBIOCONTRASENA_ULTIMO datetime             null,
   ULTIMA_CONTRASENA    varchar(30)          null,
   constraint PK_US_HISTORIAL primary key (ID_HISTORIAL)
)
go

/*==============================================================*/
/* Table: US_LOG_REGISTRO                                       */
/*==============================================================*/
create table US_LOG_REGISTRO (
   ID_LOG_REGISTRO      int                  identity,
   ADICIONADO_POR       varchar(30)          not null,
   FECHA_REGISTRO       datetime             not null,
   MODIFICADO_POR       varchar(30)          null,
   FECHA_MODIFICACION   datetime             null,
   ES_BORRADO           bit                  not null,
   constraint PK_US_LOG_REGISTRO primary key (ID_LOG_REGISTRO)
)
go

/*==============================================================*/
/* Table: US_USUARIOS                                           */
/*==============================================================*/
create table US_USUARIOS (
   ID_USUARIOS          int                  identity,
   ID_TIPO_USUARIO      int                  null,
   ID_ESTATUS_USUARIOS  int                  null,
   ID_NIVEL_USUARIO     int                  null,
   ID_HISTORIAL         int                  null,
   ID_LOG_REGISTRO      int                  null,
   USUARIO              varchar(50)          null,
   CONTRASENA           varchar(30)          null,
   NOMBRE_COMPLETO      varchar(50)          null,
   CORREO_ELECTRONICO   varchar(50)          null,
   constraint PK_US_USUARIOS primary key (ID_USUARIOS)
)
go

alter table US_CAT_ESTATUS_USUARIO
   add constraint FK_US_CAT_E_REFERENCE_US_LOG_R foreign key (ID_LOG_REGISTRO)
      references US_LOG_REGISTRO (ID_LOG_REGISTRO)
go

alter table US_CAT_NIVEL_USUARIO
   add constraint FK_US_CAT_N_REFERENCE_US_LOG_R foreign key (ID_LOG_REGISTRO)
      references US_LOG_REGISTRO (ID_LOG_REGISTRO)
go

alter table US_CAT_TIPO_USUARIO
   add constraint FK_US_CAT_T_REFERENCE_US_LOG_R foreign key (ID_LOG_REGISTRO)
      references US_LOG_REGISTRO (ID_LOG_REGISTRO)
go

alter table US_HISTORIAL
   add constraint FK_US_HISTO_REFERENCE_US_LOG_R foreign key (ID_LOG_REGISTRO)
      references US_LOG_REGISTRO (ID_LOG_REGISTRO)
go

alter table US_USUARIOS
   add constraint FK_US_USUAR_REFERENCE_US_CAT_T foreign key (ID_TIPO_USUARIO)
      references US_CAT_TIPO_USUARIO (ID_TIPO_USUARIO)
go

alter table US_USUARIOS
   add constraint FK_US_USUAR_REFERENCE_US_CAT_E foreign key (ID_ESTATUS_USUARIOS)
      references US_CAT_ESTATUS_USUARIO (ID_ESTATUS_USUARIOS)
go

alter table US_USUARIOS
   add constraint FK_US_USUAR_REFERENCE_US_CAT_N foreign key (ID_NIVEL_USUARIO)
      references US_CAT_NIVEL_USUARIO (ID_NIVEL_USUARIO)
go

alter table US_USUARIOS
   add constraint FK_US_USUAR_REFERENCE_US_HISTO foreign key (ID_HISTORIAL)
      references US_HISTORIAL (ID_HISTORIAL)
go

alter table US_USUARIOS
   add constraint FK_US_USUAR_REFERENCE_US_LOG_R foreign key (ID_LOG_REGISTRO)
      references US_LOG_REGISTRO (ID_LOG_REGISTRO)
go

