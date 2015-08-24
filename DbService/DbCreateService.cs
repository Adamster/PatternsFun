// File: DbCreateService.cs in
// PatternsFun by Serghei Adam 
// Created 20 08 2015 
// Edited 20 08 2015

using System;
using System.CodeDom;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Utils;

namespace DbService
{
    public class DbCreateService
    {
        static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["VehicleDB"].ConnectionString;

        static readonly string ConnectionString2 = ConfigurationManager.ConnectionStrings["VehicleDBOrig"].ConnectionString;
        public static void CreateDbStrucutre()
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                Logger.AddMsgToLog("sql connection created");
#region sqlText
                //indian test code :)
                var sqlCommandText = @"CREATE TABLE Vehicle
(
    [ID]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (50) NOT NULL,
    [Owner_ID] INT null ,  
    [Mileage] FLOAT         NULL CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED ([id])
);


CREATE TABLE Car
(
    [Vehicle_ID]        INT           NOT NULL,
    [Weight]            INT           NOT NULL,
    [Fueltank]          FLOAT         NOT NULL,
    [Engine_ID]         INT           NOT NULL UNIQUE,
    [AccelerationSpeed] FLOAT         NOT NULL,
    [FuelType]          NVARCHAR (15) NOT NULL,
    [SpecialAdds]       NVARCHAR (30) NULL CONSTRAINT [PK_Car] PRIMARY KEY CLUSTERED ([Vehicle_ID])
);

CREATE TABLE ElectroCar
(
    [Vehicle_ID]        INT   NOT NULL,
    [Weight]            INT   NOT NULL,
    [ChargeLvl]         INT   NOT NULL,
    [ElectroEngine_ID]  INT   NOT NULL UNIQUE,
    [AccelerationSpeed] FLOAT NOT NULL,
    CONSTRAINT [PK_ElectroCar] PRIMARY KEY CLUSTERED ([Vehicle_ID])
);


create table Pilot
(
	[ID] int identity(1,1) not null,
	[Name] NVARCHAR(30) NOT NULL,
	[DebutDame] date NOT NULL,
	[Age] int NOT NULL,
	[Team] NVARCHAR(30),
	constraint [PK_Pilot] primary key clustered ([id])

);


CREATE TABLE SportCar
(
    [Car_ID]           INT  NOT NULL,
    [DownForePressure] INT DEFAULT 0 NOT NULL,
    CONSTRAINT [PK_SportCar] PRIMARY KEY CLUSTERED ([Car_ID])
);

CREATE TABLE Engine
(
    [ID]          INT IDENTITY (1, 1) NOT NULL,
    [HorsePowers] INT NOT NULL,
    CONSTRAINT [PK_Engine] PRIMARY KEY CLUSTERED ([ID])
);

CREATE TABLE GasolineEngine
(
    [Engine_ID]    INT  NOT NULL,
    [CylinderNumber] INT NOT NULL,
    CONSTRAINT [PK_GasolineEngine] PRIMARY KEY CLUSTERED ([Engine_ID])
);

CREATE TABLE ElectroEngine
(
    [Engine_ID]   INT NOT NULL,
    [ECE] INT NOT NULL
	CONSTRAINT [PK_ElectroEngine] PRIMARY KEY CLUSTERED ([Engine_ID])
);

ALTER TABLE [Car]
    ADD CONSTRAINT [Car.Vehicle_ID] FOREIGN KEY ([Vehicle_ID]) REFERENCES [Vehicle] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
        CONSTRAINT Chk_Weight CHECK ([Weight] > 0),
        CONSTRAINT Chk_AccelerationSpeed CHECK ([accelerationSpeed] > 0),
        CONSTRAINT [Car.Engine_ID] FOREIGN KEY ([Engine_ID]) REFERENCES [GasolineEngine] ([Engine_ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [ElectroCar]
    ADD CONSTRAINT [ElectroCar.ID] FOREIGN KEY ([Vehicle_ID]) REFERENCES [Vehicle] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
        CONSTRAINT EWeight CHECK ([Weight] > 0),
        CONSTRAINT Chk_EAccelerationSpeed CHECK ([accelerationSpeed] > 0),
        CONSTRAINT [ElectroCar.ElectroEngine_ID] FOREIGN KEY ([ElectroEngine_ID]) REFERENCES ElectroEngine ([Engine_ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [SportCar]
    ADD CONSTRAINT [SportCar.ID] FOREIGN KEY ([Car_ID]) REFERENCES [Car] ([Vehicle_ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [GasolineEngine]
    ADD CONSTRAINT [GasolineEngine.ID] FOREIGN KEY ([Engine_ID]) REFERENCES [engine] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
        CONSTRAINT Chk_CylindNum CHECK ([cylinderNumber] > 0);

ALTER TABLE ElectroEngine
    ADD CONSTRAINT [ElectroEngine.ID] FOREIGN KEY ([Engine_ID]) REFERENCES Engine ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION

Alter table vehicle 
add constraint [Vehicle.Owner_ID] foreign key  ([Owner_ID]) references pilot ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;";
	
#endregion
                using (var sqlcommand = new SqlCommand(sqlCommandText, sqlConnection))
                {
                    Logger.AddMsgToLog("sql command started");
                    sqlcommand.ExecuteNonQuery();
                    Logger.AddMsgToLog("sql command executed");
                }

            }
        }

        public static void CreateCustomTable()
        {
            using (var sqlConnection= new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                var sqlCommandText = "CREATE TABLE Vehicle1 (ID int not null   identity(1,1) primary key , name varchar(50))";
                using (var sqlCommand = new SqlCommand(sqlCommandText, sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public static void ScalarTest()
        {
            using (var sqlConn = new SqlConnection(ConnectionString2))
            {
                sqlConn.Open();
                var sqlText = "Select Count(*) from pilot";
                using (var sqlCommand = new SqlCommand(sqlText, sqlConn))
                {
                  var count =  sqlCommand.ExecuteScalar();
                    Console.WriteLine(count);
                }
            }
        }

        public  static void ReaderTest()
        {
            using (var sqlConn = new SqlConnection(ConnectionString2))
            {
                sqlConn.Open();
                var sqlText = "select * from pilot";
                using (var command = new SqlCommand(sqlText, sqlConn))
                {
                     SqlDataReader reader =  command.ExecuteReader();

                    while (reader.Read())
                    {
                        string name = (string) reader["Name"];
                        DateTime DebutTime = (DateTime) reader["DebutDate"];
                        Console.WriteLine(name+" debuted at "+ DebutTime.ToLongDateString());
                    }
                   
                }
            }
        }

        public static void ParametrQuery(int parametr)
        {
            using (var sqlConn = new SqlConnection(ConnectionString2))
            {
                sqlConn.Open();
                var sqlText = "select * from pilot where id > @ID";
                using (var sqlCommand = new SqlCommand(sqlText, sqlConn))
                {
                    sqlCommand.Parameters.Add("@ID", SqlDbType.Int);
                    sqlCommand.Parameters["@ID"].Value = parametr;

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        string name = (string)reader["Name"];
                        DateTime debutTime = (DateTime)reader["DebutDate"];
                        Console.WriteLine("id: "+(int)reader["ID"]+") "+name + " debuted at " + debutTime.ToLongDateString());
                    }
                }
            }
        }
    }
}