CREATE TABLE Films (
 ID integer not null PRIMARY key AUTOINCREMENT,
 Title text not null,
 Category text not null,
 PYear integer not null,
 FLength integer not null,
 Priority integer not null,
 UNIQUE(Title)
);
