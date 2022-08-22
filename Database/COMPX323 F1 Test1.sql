---This database contains actual data from the 2020 Formula One season---

create table country
(
    countryName varchar(20) not null,
    population int not null,
    area int not null,
    primary key(countryName)
)

create table driver
(
    pid smallint not null,
    fname varchar(30) not null,
    lname varchar(30) not null,
    dob date not null,
    numChamp int not null,
    num int not null,
    pastPos int,
    countryName varchar(30) not null,
    teamName varchar(30) not null,
    salary number not null,
    endDate date not null,
    primary key(pid),
    foreign key(countryName) references country,
    foreign key(teamName) references team
)

create table team
(
    teamName varchar(30) not null,
    numChamp smallint not null,
    primary key(teamName)
)

--Changed primary key to name and removed city
create table grandPrix
(
    name varchar(30) not null,
    laps smallint not null,
    circuitLength varchar(8) not null,
    firstHeld date not null,
    countryName varchar(20) not null,
    primary key(name) not null,
    foreign key(countryName) references country
)

create table founder
(
    pid smallint not null,
    teamName varchar(30) not null,
    fname varchar(20),
    lname varchar(20),
    dob date,
    primary key(pid, teamName),
    foreign key(teamName) references team
)

create table car
(
    carName varchar(20) not null,
    teamName varchar(30) not null,
    powerUnit varchar(20) not null,
    tyres varchar(20) not null,
    creationDate date not null,
    primary key(carName, teamName),
    foreign key(teamName) references team
)

create table participates
(
    prixName varchar(30) not null,
    pid smallint not null,
    position smallint not null,
    pdate date not null,
    primary key(prixName, pid),
    foreign key(prixName) references grandPrix,
    foreign key(pid) references driver
)

--Without Index
explain plan for select * from driver where salary = 10 --Fetched in 0.019 seconds
select PLAN_TABLE_OUTPUT from TABLE(DBMS_XPLAN.DISPLAY())
select * from driver where salary > 990 and salary < 995 -- Fetched in 0.01 econds

select * from driver where numChamp = 12 --Fetched in 0.012 seconds
select * from driver where numChamp > 12 and numChamp < 16 --Fetched in 0.01 seconds

--B-Tree Index
drop index index_numChamp
create index index_numChamp on driver(numChamp)
alter session set optimizer_index_cost_adj=10
explain plan for select /*INDEX(driver, index_numChamp)*/* from driver where numChamp = 12
select PLAN_TABLE_OUTPUT from TABLE(DBMS_XPLAN.DISPLAY())
explain plan for select /*INDEX(driver, index_numChamp)*/* from driver where numChamp > 12 and numChamp < 16
select PLAN_TABLE_OUTPUT from TABLE(DBMS_XPLAN.DISPLAY())

--Hash Index
drop cluster hash_salary including tables
create cluster hash_salary(salary number) size 128 single table hashkeys 1000
create table driver_hash
(
    pid smallint not null,
    fname varchar(30) not null,
    lname varchar(30) not null,
    dob date not null,
    numChamp int not null,
    num int not null,
    pastPos int,
    countryName varchar(30) not null,
    teamName varchar(30) not null,
    salary number not null,
    endDate date not null,
    primary key(pid),
    foreign key(countryName) references country,
    foreign key(teamName) references team
)
    cluster hash_salary(salary)

alter session set optimizer_index_cost_adj=10
explain plan for select /*+hash(hash_salary)*/* from driver_hash where salary = 10
select PLAN_TABLE_OUTPUT from TABLE(DBMS_XPLAN.DISPLAY())
explain plan for select /*+hash(hash_salary)*/* from driver_hash where salary > 990 and salary < 995
select PLAN_TABLE_OUTPUT from TABLE(DBMS_XPLAN.DISPLAY())
