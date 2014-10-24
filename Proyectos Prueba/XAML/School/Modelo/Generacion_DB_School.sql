/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     11/10/2014 12:19:41 a.m.                     */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('REGISTRATION') and o.name = 'FK_REGISTRA_REFERENCE_SUBJECT')
alter table REGISTRATION
   drop constraint FK_REGISTRA_REFERENCE_SUBJECT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('REGISTRATION') and o.name = 'FK_REGISTRA_REFERENCE_STUDENT')
alter table REGISTRATION
   drop constraint FK_REGISTRA_REFERENCE_STUDENT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('REGISTRATION')
            and   type = 'U')
   drop table REGISTRATION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('STUDENT')
            and   type = 'U')
   drop table STUDENT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SUBJECT')
            and   type = 'U')
   drop table SUBJECT
go

/*==============================================================*/
/* Table: REGISTRATION                                          */
/*==============================================================*/
create table REGISTRATION (
   REGISTRATIONID       int                  identity,
   SUBJECTID            int                  null,
   STUDENTID            int                  null,
   GRADE                varchar(2)           null,
   STATUS               varchar(1)           null,
   constraint PK_REGISTRATION primary key (REGISTRATIONID)
)
go

/*==============================================================*/
/* Table: STUDENT                                               */
/*==============================================================*/
create table STUDENT (
   STUDENTID            int                  identity,
   FIRSTNAME            varchar(50)          not null,
   LASTNAME             varchar(50)          not null,
   GENDER               varchar(1)           not null,
   GPA                  float                null,
   constraint PK_STUDENT primary key (STUDENTID)
)
go

/*==============================================================*/
/* Table: SUBJECT                                               */
/*==============================================================*/
create table SUBJECT (
   SUBJECTID            int                  identity,
   SUBJECTNAME          varchar(20)          not null,
   SUBJECTDESCRIPTION   varchar(50)          not null,
   SUBJECTCREDITS       float                not null,
   constraint PK_SUBJECT primary key (SUBJECTID)
)
go

alter table REGISTRATION
   add constraint FK_REGISTRA_REFERENCE_SUBJECT foreign key (SUBJECTID)
      references SUBJECT (SUBJECTID)
go

alter table REGISTRATION
   add constraint FK_REGISTRA_REFERENCE_STUDENT foreign key (STUDENTID)
      references STUDENT (STUDENTID)
go

