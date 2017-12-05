﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace GeexBlog
{
    public class SQLiteConnector : IConnection, IDisposable
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();



        public SQLiteConnection Connect()
        {
            string test = System.AppDomain.CurrentDomain.BaseDirectory;

            string connectionString = @"Data Source="+ System.AppDomain.CurrentDomain.BaseDirectory + "/App_Data/geexblog.db;Version=3;New=False;Compress=True;";

            return sql_con = new SQLiteConnection(connectionString);

        }

        public void Disconnect(SQLiteConnection Connection)
        {
            Connection.Close();
        }



        public void ExecuteQuery(string SqlQuery)
        {
            sql_con = Connect();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = SqlQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }


        public DataTable LoadData(string SQL)
        {
            sql_con = Connect();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = SQL;
            DB = new SQLiteDataAdapter(CommandText, sql_con);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            sql_con.Close();

            return DT;
        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }


    public interface IConnection
    {

        SQLiteConnection Connect();
        void Disconnect(SQLiteConnection Connection);

        void ExecuteQuery(string SqlQuery);


        DataTable LoadData(string SQL);

    }

}