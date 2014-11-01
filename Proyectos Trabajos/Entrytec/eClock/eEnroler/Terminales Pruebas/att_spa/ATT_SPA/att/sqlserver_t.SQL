CREATE TABLE CHECKEXACT (
	EXACTID INT IDENTITY(1,1) NOT NULL ,  
	USERID INT NULL DEFAULT 0,              
	CHECKTIME DATETIME NULL DEFAULT 0,      
	CHECKTYPE VARCHAR (2) NULL DEFAULT 0,   
	ISADD SMALLINT NULL DEFAULT 0,          
	YUYIN VARCHAR (25) NULL ,         
	ISMODIFY SMALLINT NULL DEFAULT 0,
	ISDELETE SMALLINT NULL DEFAULT 0,       
	INCOUNT SMALLINT NULL DEFAULT 0,        
	ISCOUNT SMALLINT NULL DEFAULT 0,        
	MODIFYBY VARCHAR (20) NULL,      
	[DATE] DATETIME NULL,             
        CONSTRAINT EXACTID PRIMARY KEY (EXACTID)
)
;

CREATE TABLE CHECKINOUT (               
	USERID INT NOT NULL ,               
	CHECKTIME DATETIME NOT NULL DEFAULT GETDATE(),       
	CHECKTYPE VARCHAR (1) NULL DEFAULT 'I',
	VERIFYCODE INT NULL DEFAULT 0,           
	SENSORID VARCHAR (5) NULL,          
        CONSTRAINT USERCHECKTIME PRIMARY KEY (USERID, CHECKTIME)
)
;

CREATE TABLE DEPARTMENTS (              
	DEPTID INT IDENTITY(1,1) NOT NULL ,    
	DEPTNAME VARCHAR (30) NULL ,             
	SUPDEPTID INT NOT NULL DEFAULT 1,                      
        CONSTRAINT DEPTID PRIMARY KEY (DEPTID)
)
;

CREATE TABLE EXCNOTES (              
	USERID INT NULL ,
	ATTDATE DATETIME NULL ,
	NOTES VARCHAR (200) NULL
)  
;

CREATE TABLE HOLIDAYS (                            
	HOLIDAYID INT IDENTITY(1,1) NOT NULL ,   
	HOLIDAYNAME VARCHAR (20) NULL ,            
	HOLIDAYYEAR SMALLINT NULL ,                
	HOLIDAYMONTH SMALLINT NULL ,               
	HOLIDAYDAY SMALLINT NULL DEFAULT 1,                 
	STARTTIME DATETIME NULL ,                  
	DURATION SMALLINT NULL ,                   
	HOLIDAYTYPE SMALLINT NULL ,                
	XINBIE VARCHAR (4) NULL ,                     
	MINZU VARCHAR (50) NULL,                       
        CONSTRAINT HOLID PRIMARY KEY (HOLIDAYID)
)
;

CREATE TABLE NUM_RUN (                              
	NUM_RUNID INT IDENTITY(1,1) NOT NULL ,    
	OLDID INT NULL DEFAULT -1,                            
	NAME VARCHAR (30) NOT NULL ,                
	STARTDATE DATETIME NULL DEFAULT '1900-1-1',                   
	ENDDATE DATETIME NULL DEFAULT '2099-12-31',                     
	CYLE SMALLINT NULL DEFAULT 1,                        
	UNITS SMALLINT NULL DEFAULT 1,                        
        CONSTRAINT NUMID PRIMARY KEY (NUM_RUNID)
)
;

CREATE TABLE NUM_RUN_DEIL (                      
	NUM_RUNID SMALLINT NOT NULL ,                
	STARTTIME DATETIME NOT NULL ,                
	ENDTIME DATETIME NULL ,                  
	SDAYS SMALLINT NOT NULL ,                    
	EDAYS SMALLINT NULL ,
        SCHCLASSID INT NULL DEFAULT -1,
        CONSTRAINT NUMID2 PRIMARY KEY (NUM_RUNID, SDAYS, STARTTIME)
)
;

CREATE TABLE SECURITYDETAILS (                 
	SECURITYDETAILID INT IDENTITY(1,1) NOT NULL ,
	USERID SMALLINT NULL ,
	DEPTID SMALLINT NULL ,
	SCHEDULE SMALLINT NULL ,
	USERINFO SMALLINT NULL ,
	ENROLLFINGERS SMALLINT NULL ,
	REPORTVIEW SMALLINT NULL ,
	REPORT VARCHAR (10) NULL,
        CONSTRAINT NAMEID2 PRIMARY KEY (SECURITYDETAILID)
)  
;

CREATE TABLE SHIFT (                           
	SHIFTID INT IDENTITY(1,1) NOT NULL ,  
	NAME VARCHAR (20) NULL ,                   
	USHIFTID INT NULL DEFAULT -1,                     
	STARTDATE DATETIME NOT NULL DEFAULT '1900-1-1',           
	ENDDATE DATETIME NULL DEFAULT '1900-12-31',                 
	RUNNUM SMALLINT NULL DEFAULT 0,                  
	SCH1 INT NULL DEFAULT 0,                         
	SCH2 INT NULL DEFAULT 0,
	SCH3 INT NULL DEFAULT 0,
	SCH4 INT NULL DEFAULT 0,
	SCH5 INT NULL DEFAULT 0,
	SCH6 INT NULL DEFAULT 0,
	SCH7 INT NULL DEFAULT 0,
	SCH8 INT NULL DEFAULT 0,
	SCH9 INT NULL DEFAULT 0,
	SCH10 INT NULL DEFAULT 0,
	SCH11 INT NULL DEFAULT 0,
	SCH12 INT NULL DEFAULT 0,
	CYCLE SMALLINT NULL DEFAULT 0,             
	UNITS SMALLINT NULL DEFAULT 0 ,             
        CONSTRAINT SHIFTS PRIMARY KEY (SHIFTID)
)  
;

CREATE TABLE TEMPLATE (                           
	TEMPLATEID INT IDENTITY(1,1) NOT NULL ,  
	USERID INT NOT NULL ,                      
	FINGERID INT NOT NULL ,                    
	TEMPLATE image NOT NULL ,                  
	TEMPLATE2 image NULL ,                  
	TEMPLATE3 image NULL ,                  
	BITMAPPICTURE image NULL ,
	BITMAPPICTURE2 image NULL ,                  
	BITMAPPICTURE3 image NULL ,                  
	BITMAPPICTURE4 image NULL ,                  
	USETYPE SMALLINT NULL ,                      
        CONSTRAINT TEMPLATED PRIMARY KEY (TEMPLATEID)
)
;

CREATE TABLE USER_OF_RUN (              
	USERID INT NOT NULL ,           
	NUM_OF_RUN_ID INT not NULL ,        
	STARTDATE DATETIME not NULL DEFAULT '1900-1-1',          
	ENDDATE DATETIME not NULL DEFAULT '2099-12-31',            
	ISNOTOF_RUN INT NULL DEFAULT 0,          
	ORDER_RUN INT NULL ,              
        CONSTRAINT USER_ST_NUM PRIMARY KEY (USERID, NUM_OF_RUN_ID, STARTDATE, ENDDATE)
)
;

CREATE TABLE USER_SPEDAY (                    
	USERID INT NOT NULL ,
	STARTSPECDAY DATETIME NOT NULL DEFAULT '1900-1-1',          
	ENDSPECDAY DATETIME NULL DEFAULT '2099-12-31',            
	DATEID SMALLINT not NULL DEFAULT -1,                
	YUANYING VARCHAR (200) NULL ,         
	[DATE] DATETIME NULL ,                  
        CONSTRAINT USER_SEP PRIMARY KEY (USERID, STARTSPECDAY, DATEID)
)
;

CREATE TABLE USER_TEMP_SCH (                  
	USERID INT not NULL ,                     
	COMETIME DATETIME not NULL ,              
	LEAVETIME DATETIME not NULL ,             
        OVERTIME INT not NULL DEFAULT 0,     
	[TYPE] SMALLINT NULL DEFAULT 0,                  
	FLAG SMALLINT NULL DEFAULT 1,
        SCHCLASSID INT NULL DEFAULT -1,
        CONSTRAINT USER_TEMP PRIMARY KEY (USERID, COMETIME, LEAVETIME)

)
;

CREATE TABLE USERINFO (                      
	USERID INT IDENTITY(1,1) NOT NULL ,   
	BADGENUMBER VARCHAR (12) NOT NULL ,         
	SSN VARCHAR (20) NULL ,                 
	NAME VARCHAR (20) NULL ,               
	GENDER VARCHAR (2) NULL ,               
	TITLE VARCHAR (20) NULL ,               
	PAGER VARCHAR (20) NULL ,              
	BIRTHDAY DATETIME NULL ,                
	HIREDDAY DATETIME NULL ,                
	STREET VARCHAR (40) NULL ,             
	CITY VARCHAR (2) NULL ,                 
	STATE VARCHAR (2) NULL ,                
	ZIP VARCHAR (12) NULL ,                 
	OPHONE VARCHAR (20) NULL ,
	FPHONE VARCHAR (20) NULL ,              
	VERIFICATIONMETHOD SMALLINT NULL ,      
	DEFAULTDEPTID SMALLINT NULL  DEFAULT 1,           
	SECURITYFLAGS SMALLINT NULL ,           
	ATT SMALLINT NOT NULL DEFAULT 1,        
	INLATE SMALLINT NOT NULL DEFAULT 1,                    
	OUTEARLY SMALLINT NOT NULL DEFAULT 1,                   
	OVERTIME SMALLINT NOT NULL DEFAULT 1,                    
	SEP SMALLINT NOT NULL DEFAULT 1,                     
	HOLIDAY SMALLINT NOT NULL DEFAULT 1,                 
	MINZU VARCHAR (8) NULL ,                
	[PASSWORD] VARCHAR (20) NULL ,          
	LUNCHDURATION SMALLINT NOT NULL DEFAULT 1, 
        MVerifyPass VARCHAR(10) NULL,
	PHOTO Image NULL,
        CONSTRAINT USERIDS PRIMARY KEY (USERID)
)
;


CREATE  UNIQUE  INDEX USERFINGER ON TEMPLATE(USERID, FINGERID)
;

CREATE  UNIQUE  INDEX HOLIDAYNAME ON HOLIDAYS(HOLIDAYNAME)
;

CREATE  INDEX DEPTNAME ON DEPARTMENTS(DEPTNAME)
;

CREATE  UNIQUE  INDEX EXCNOTE ON EXCNOTES(USERID, ATTDATE)
;

CREATE  UNIQUE  INDEX BADGENUMBER ON USERINFO(BADGENUMBER)
;

INSERT INTO DEPARTMENTS (DEPTNAME, SUPDEPTID) VALUES('總公司',0)
;

Create Table LeaveClass(
  LeaveId INT Identity(1,1) not null primary key,
  LeaveName VARCHAR(20) not null,                
  MinUnit float not null default 1,              
  Unit smallint not null default 1,              
  RemaindProc smallint not null default 1,     
  RemaindCount smallint not null default 1,    
  ReportSymbol varchar(4) not null default '-',
  Deduct float not null default 0,             
  Color int not null default 0,
  Classify SMALLINT NOT null default 0) 
;

Create Table LeaveClass1(
  LeaveId INT Identity(999,1) not null primary key,
  LeaveName VARCHAR(20) not null,
  MinUnit float not null default 1,
  Unit smallint not null default 0,
  RemaindProc smallint not null default 2,
  RemaindCount smallint not null default 1,
  ReportSymbol varchar(4) not null default '-',
  Deduct float not null default 0,
  LeaveType SMALLINT not null default 0,
  Color int not null default 0,
  Classify SMALLINT not null default 0,
  Calc text null)
;

CREATE TABLE SchClass(
  schClassid INT identity(1,1) NOT NULL PRIMARY KEY,
  schName VARCHAR(20) NOT null,
  StartTime datetime NOT NULL,
  EndTime datetime NOT NULL,
  LateMinutes int null,
  EarlyMinutes int null,
  CheckIn int null default 1,
  CheckOut int null default 1,
  CheckInTime1 datetime NULL,
  CheckInTime2 datetime NULL,
  CheckOutTime1 datetime NULL,
  CheckOutTime2 datetime NULL,
  Color Int NULL default 16715535,
  AutoBind SMALLINT NULL DEFAULT 1)
;


Create Table AttParam(
  PARANAME VARCHAR (20) NOT NULL Primary key,
  PARATYPE VARCHAR (2) NULL ,
  PARAVALUE VARCHAR(100) NOT NULL)
;
insert into LeaveClass(LeaveName, Unit, ReportSymbol, Color) 
  values('病假', 1, 'B', 3398744);
insert into LeaveClass(LeaveName, Unit, ReportSymbol, Color) 
  values('事假', 1, 'S', 8421631);
insert into LeaveClass(LeaveName, Unit, ReportSymbol, Color) 
  values('探親假', 1, 'T', 16744576);
insert into LeaveClass1(LeaveName, MinUnit, Unit, RemaindProc,
  RemaindCount, ReportSymbol, LeaveType, Calc)
  values('公出', 0.5, 3, 1, 1, 'G', 3, 'if(AttItem(LeaveType1)=999,AttItem(LeaveTime1),0)+if(AttItem(LeaveType2)=999,AttItem(LeaveTime2),0)+if(AttItem(LeaveType3)=999,AttItem(LeaveTime3),0)+if(AttItem(LeaveType4)=999,AttItem(LeaveTime4),0)+if(AttItem(LeaveType5)=999,AttItem(LeaveTime5),0)');
insert into LeaveClass1(LeaveName, MinUnit, Unit, RemaindProc,
  RemaindCount, ReportSymbol, LeaveType)
  values('正常', 0.5, 3, 1, 0, ' ', 3);
insert into LeaveClass1(LeaveName, MinUnit, Unit, RemaindProc,
  RemaindCount, ReportSymbol, LeaveType, Calc)
  values('遲到', 10, 2, 2, 1, '>', 3, 'AttItem(minLater)');
insert into LeaveClass1(LeaveName, MinUnit, Unit, RemaindProc,
  RemaindCount, ReportSymbol, LeaveType, Calc)
  values('早退', 10, 2, 2, 1, '<', 3, 'AttItem(minEarly)');
insert into LeaveClass1(LeaveName, MinUnit, Unit, RemaindProc,
  RemaindCount, ReportSymbol, LeaveType, Calc)
  values('請假', 1, 1, 1, 1, '假', 3, 
  'if((AttItem(LeaveType1)>0) and (AttItem(LeaveType1)<999),AttItem(LeaveTime1),0)+if((AttItem(LeaveType2)>0) and (AttItem(LeaveType2)<999),AttItem(LeaveTime2),0)+if((AttItem(LeaveType3)>0) and (AttItem(LeaveType3)<999),AttItem(LeaveTime3),0)+if((AttItem(LeaveType4)>0) and (AttItem(LeaveType4)<999),AttItem(LeaveTime4),0)+if((AttItem(LeaveType5)>0) and (AttItem(LeaveType5)<999),AttItem(LeaveTime5),0)');
insert into LeaveClass1(LeaveName, MinUnit, Unit, RemaindProc,
  RemaindCount, ReportSymbol, LeaveType, Calc)
  values('曠工', 0.5, 3, 1, 0, '曠', 3, 'AttItem(MinAbsent)');
insert into LeaveClass1(LeaveName, MinUnit, Unit, RemaindProc,
  RemaindCount, ReportSymbol, LeaveType, Calc)
  values('加班', 1, 1, 1, 1, '+', 3, 'AttItem(MinOverTime)');
insert into LeaveClass1(LeaveName, MinUnit, Unit, RemaindProc,
  RemaindCount, ReportSymbol, LeaveType, Calc)
  values('節日加班', 1, 1, 0, 1, '=', 0, 'if(HolidayId(1)=1, AttItem(MinOverTime),0)');
insert into LeaveClass1(LeaveName, MinUnit, Unit, RemaindProc,
  RemaindCount, ReportSymbol, LeaveType)
  values('休息日', 0.5, 3, 2, 1, '-', 2);
insert into LeaveClass1(LeaveName, MinUnit, Unit, RemaindProc,
  RemaindCount, ReportSymbol, LeaveType, Calc)
  values('未簽到', 1, 4, 2, 1, '[', 2, 
  'If(AttItem(CheckIn)=null,If(AttItem(OnDuty)=null,0,if(((AttItem(LeaveStart1)=null) or (AttItem(LeaveStart1)>AttItem(OnDuty))) and AttItem(DutyOn),1,0)), 0)');
insert into LeaveClass1(LeaveName, MinUnit, Unit, RemaindProc,
  RemaindCount, ReportSymbol, LeaveType, Calc)
  values('未簽退', 1, 4, 2, 1, ']', 2, 
  'If(AttItem(CheckOut)=null,If(AttItem(OffDuty)=null,0,if((AttItem(LeaveEnd1)=null) or (AttItem(LeaveEnd1)<AttItem(OffDuty)),if((AttItem(LeaveEnd2)=null) or (AttItem(LeaveEnd2)<AttItem(OffDuty)),if(((AttItem(LeaveEnd3)=null) or (AttItem(LeaveEnd3)<AttItem(OffDuty))) and AttItem(DutyOff),1,0),0),0)),0)');
insert into LeaveClass1(LeaveName, MinUnit, Unit, RemaindProc,
  RemaindCount, ReportSymbol, LeaveType)
  values('離崗未簽到', 1, 4, 2, 1, '{', 6);
insert into LeaveClass1(LeaveName, MinUnit, Unit, RemaindProc,
  RemaindCount, ReportSymbol, LeaveType)
  values('離崗未簽退', 1, 4, 2, 1, '}', 6);
insert into LeaveClass1(LeaveName, MinUnit, Unit, RemaindProc,
  RemaindCount, ReportSymbol, LeaveType)
  values('離崗', 1, 1, 2, 1, 'L', 3);

insert into AttParam(ParaName,ParaValue) values('MinsEarly',5);
insert into AttParam(ParaName,ParaValue) values('MinsLate',10);
insert into AttParam(ParaName,ParaValue) values('MinsNoBreakIn',60);
insert into AttParam(ParaName,ParaValue) values('MinsNoBreakOut',60);
insert into AttParam(ParaName,ParaValue) values('MinsNoIn',60);
insert into AttParam(ParaName,ParaValue) values('MinsNoLeave',60);
insert into AttParam(ParaName,ParaValue) values('MinsNotOverTime',60);
insert into AttParam(ParaName,ParaValue) values('MinsWorkDay',420);
insert into AttParam(ParaName,ParaValue) values('NoBreakIn',1012);
insert into AttParam(ParaName,ParaValue) values('NoBreakOut',1012);
insert into AttParam(ParaName,ParaValue) values('NoIn',1001);
insert into AttParam(ParaName,ParaValue) values('NoLeave',1002);
insert into AttParam(ParaName,ParaValue) values('OutOverTime',0);
insert into AttParam(ParaName,ParaValue) values('TwoDay',0);
insert into AttParam(ParaName,ParaValue) values('CheckInColor',16777151);
insert into AttParam(ParaName,ParaValue) values('CheckOutColor',12910591);
insert into AttParam(ParaName,ParaValue) values('DBVersion',167);
