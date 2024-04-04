use [passin]
Create database [passin]

--drop table [Event]
Create table Event (
    [Id] int IDENTITY,
    [Title] Varchar(150),
    [Details] Text,
    [Slug] Varchar(150),
    [MaximumAttendees] int,

    Constraint [PK_EventId] Primary KEY (Id)
);

--drop table Attendees
create table Attendees (
    [Id] Int IDENTITY,
    [Name] VARCHAR(150),
    [Email] Varchar(150),
    [EventId] int,
    [CreatedAt] Date,

    Constraint [PK_Attendees_Id] Primary Key (Id),
    Constraint [FK_Event_Id] Foreign Key (EventId) references [Event](Id)
);

Create table CheckIns (

);

select * from [Event]