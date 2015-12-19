/*MySQL - create tables*/
create database notebase;
use notebase;

create table Notes(note_id int primary key auto_increment, title varchar(50) not null,
body varchar(65000) not null, created datetime not null, updated datetime);

create table Tag(tag_id int primary key auto_increment, text varchar(25) not null);

create table Attachment(attach_id int primary key auto_increment,
attachment varbinary(65000) not null, filename varchar(100) not null);

create table Customers(cust_id int primary key auto_increment, firstname varchar(50) not null,
lastname varchar(50) not null, username varchar(50) not null, hashedpassword varchar(50) not null,
salt varchar(50) not null, enccardnum varchar(50) not null, enckey varchar(60) not null, iv varchar(50) not null);

create table NoteTags(note_id int not null, tag_id int not null, primary key(note_id, tag_id),
foreign key(note_id) references Notes(note_id) on delete cascade,
foreign key(tag_id) references Tag(tag_id) on delete cascade);

create table AttachedNotes(note_id int not null, attach_id int not null, primary key(note_id, attach_id),
foreign key(note_id) references Notes(note_id) on delete cascade,
foreign key(attach_id) references Attachment(attach_id));
