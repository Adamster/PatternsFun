// File: DbAdapterService.cs in
// PatternsFun by Serghei Adam 
// Created 20 08 2015 
// Edited 21 08 2015

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DbService
{
    public class DbAdapterService
    {
        internal static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["VehicleDBOrig"].ConnectionString;

        public static void Adapter()
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                var sqlCommandText = "SELECT * from Vehicle";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommandText, sqlConnection);

                DataTable data = new DataTable();
                dataAdapter.Fill(data);


                foreach (DataRow item in data.Rows)
                {
                    Console.WriteLine(item.ItemArray[1]);
                }


                var sqlInsert = @"INSERT Vehicle(name)  VALUES (@name)";
                var sqlInsertCommand = new SqlCommand(sqlInsert, sqlConnection);
                sqlInsertCommand.Parameters.Add("@name", SqlDbType.VarChar, 50, "Name");
                dataAdapter.InsertCommand = sqlInsertCommand;
                var row = data.NewRow();
                row["Name"] = "TestCarCSharp";
                data.Rows.Add(row);
                dataAdapter.Update(data);


                sqlConnection.Open();
                var sqlUpdate = @"UPDATE  Vehicle
                                  SET Mileage = 240
                                  WHERE   id > @id;";
                SqlCommand updateCommand = new SqlCommand(sqlUpdate, sqlConnection);
                updateCommand.Parameters.Add("@id", SqlDbType.Int);
                updateCommand.Parameters["@id"].Value = 22;

                data.Rows.Remove(row);

                dataAdapter.UpdateCommand = updateCommand;
                dataAdapter.UpdateCommand.ExecuteNonQuery();
                dataAdapter.Update(data);


                var sqlDelete = @"DELETE FROM Vehicle
                                 WHERE ID > 22";
                SqlCommand deleteCommand = new SqlCommand(sqlDelete, sqlConnection);
                dataAdapter.DeleteCommand = deleteCommand;
                dataAdapter.DeleteCommand.ExecuteNonQuery();
                dataAdapter.Update(data);
            }
        }
    }
}