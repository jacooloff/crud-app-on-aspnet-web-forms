using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LegalPersonsProject.Models
{
    public class Repository
    {
        protected SqlConnection connectionString = new 
            SqlConnection(ConfigurationManager.ConnectionStrings["projectEntities"].ConnectionString);
        //used to secure from sql injection
        protected string[] authorisedTables = { "JuridicalPersons", "PhysicalPersons" };
        protected bool VerifyTableName(string tableName)
        {
            
            string[] formatedAuthorisedTableNames = { };
            authorisedTables.ForEach(t => formatedAuthorisedTableNames.Append(t.ToLowerInvariant()));
            string formatedTableName = tableName.Trim().ToLowerInvariant();
            bool isAuthorised = formatedAuthorisedTableNames.Contains(formatedTableName);
            if (isAuthorised)
            {
                return isAuthorised;
            }
            else
            {
                throw new ArgumentException("Table Name contains an invalid table name.");
            }
        }

        protected DataSet CreateCommand(string queryString, string tableName)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(queryString, this.connectionString);
            DataSet dataTable = new DataSet();
            adapter.Fill(dataTable, tableName);
            return dataTable;
        }
       
        protected void NonQueryTransaction(SqlCommand command)
        {
            this.connectionString.Open();
            SqlTransaction transaction = this.connectionString.BeginTransaction();
            try
            {
                command.Transaction = transaction;
                command.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (SqlException e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                this.connectionString.Close();
            }
        }

        
    }
}