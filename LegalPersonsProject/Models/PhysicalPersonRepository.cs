using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using Microsoft.Ajax.Utilities;

namespace LegalPersonsProject.Models
{
    public class PhysicalPersonRepository : Repository
    {
        
        private string _tableName = "PhysicalPersons";
        public DataTable GetAll(string sortColumns, int startRowIndex, int maximumRows)
        {
            try
            {
                VerifySortColumns(sortColumns);
                string query = "SELECT * FROM " + this._tableName;
                if (sortColumns.Trim() == "")
                {
                    query += " ORDER BY Lastname";
                }
                else
                {
                    query += " ORDER BY " + sortColumns;
                }
                DataSet physicalPersonsDataSet = CreateCommand(query, this._tableName);
                return physicalPersonsDataSet.Tables[_tableName];
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool VerifySortColumns(string sortColumns)
        {
            if (sortColumns.ToLowerInvariant().EndsWith(" desc"))
                sortColumns = sortColumns.Substring(0, sortColumns.Length - 5);
            string[] authorisedColumns = {"", "id", "name", "secondname", "lastname",
               "binoriin", "createdat", "updatedat", "createdby", "updatedby"};
            string[] columnNames = sortColumns.Split(',');
            string[] formatedColumnNames = { };
            columnNames.ForEach(c => formatedColumnNames.Append(c.Trim().ToLowerInvariant()));
            bool isAuthorised = formatedColumnNames.All(c => authorisedColumns.Contains(c));
            if (isAuthorised)
            {
                return isAuthorised;
            }
            else
            {
                throw new ArgumentException("Sort column contains an invalid column name.");
            }
        }

        public DataTable GetById(Guid id)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                string query = "SELECT * FROM " + this._tableName + " WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, this.connectionString);
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                command.Parameters["@Id"].Value = id;
                adapter.SelectCommand = command;
                DataSet physicalPersonsDataSet = new DataSet();
                adapter.Fill(physicalPersonsDataSet, this._tableName);
                return physicalPersonsDataSet.Tables[_tableName];
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable FindByBinOrIin(long binOrIin)
        {
            //long parsedBinOrIin = long.Parse(binOrIin);
            SqlDataAdapter adapter = new SqlDataAdapter();
            string queryString = "SELECT * FROM " + this._tableName + " WHERE CAST(BINorIIN as varchar) LIKE '%' + CAST(@binOrIin as varchar) + '%'";
            SqlCommand command = new SqlCommand(queryString, this.connectionString);
            command.Parameters.Add("@binOrIin", SqlDbType.BigInt);
            command.Parameters["@binOrIin"].Value = binOrIin;
            adapter.SelectCommand = command;
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, this._tableName);
            return dataSet.Tables[this._tableName];
        }

        public void InsertPhysicalPerson(string name,
                                string secondName,
                                string lastName,
                                string binOrIin,
                                string createdBy)
        {
            try
            {
                PhysicalPerson physicalPerson = new PhysicalPerson(
                name, secondName, lastName, binOrIin, createdBy);
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand("INSERT INTO " + this._tableName + "(Id, Name, " +
                    "Secondname, Lastname, BINorIIN, CreatedAt, UpdatedAt, " +
                    "CreatedBy, UpdatedBy) VALUES (@Id, @Name, @Secondname, " +
                    "@Lastname, @BINorIIN, @CreatedAt, @UpdatedAt, " +
                    "@CreatedBy, @UpdatedBy)", this.connectionString);
                //Parameters
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                command.Parameters.Add("@Name", SqlDbType.VarChar, 255);
                command.Parameters.Add("@Secondname", SqlDbType.VarChar, 255);
                command.Parameters.Add("@Lastname", SqlDbType.VarChar, 255);
                command.Parameters.Add("@BINorIIN", SqlDbType.BigInt);
                command.Parameters.Add("@CreatedAt", SqlDbType.DateTime);
                command.Parameters.Add("@UpdatedAt", SqlDbType.DateTime);
                command.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 255);
                command.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 255);
                //Values
                command.Parameters["@Id"].Value = physicalPerson.Id;
                command.Parameters["@Name"].Value = physicalPerson.Name;
                command.Parameters["@Secondname"].Value = physicalPerson.Secondname;
                command.Parameters["@Lastname"].Value = physicalPerson.Lastname;
                command.Parameters["@BINorIIN"].Value = physicalPerson.BINorIIN;
                command.Parameters["@CreatedAt"].Value = physicalPerson.CreatedAt;
                command.Parameters["@UpdatedAt"].Value = physicalPerson.UpdatedAt;
                command.Parameters["@CreatedBy"].Value = physicalPerson.CreatedBy;
                command.Parameters["@UpdatedBy"].Value = physicalPerson.UpdatedBy;
                base.NonQueryTransaction(command);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void UpdatePhysicalPerson(Guid id,
                                string name,
                                string secondName,
                                string lastName,
                                string binOrIin,
                                string createdAt,
                                string updatedAt,
                                string createdBy,
                                string updatedBy)
        {
            try
            {
                PhysicalPerson physicalPerson = new PhysicalPerson(id,
                name, secondName, lastName, binOrIin,
                createdAt, updatedAt, createdBy, updatedBy);
                SqlCommand command = new SqlCommand("UPDATE " + this._tableName + " SET Name = @Name, " +
                    "Secondname = @Secondname, Lastname = @Lastname, BINorIIN = @BINorIIN, " +
                    "CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt, CreatedBy = @CreatedBy," +
                    "UpdatedBy = @UpdatedBy WHERE Id = @Id", this.connectionString);
                //Parameters
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                command.Parameters.Add("@Name", SqlDbType.VarChar, 255);
                command.Parameters.Add("@Secondname", SqlDbType.VarChar, 255);
                command.Parameters.Add("@Lastname", SqlDbType.VarChar, 255);
                command.Parameters.Add("@BINorIIN", SqlDbType.BigInt);
                command.Parameters.Add("@CreatedAt", SqlDbType.DateTime);
                command.Parameters.Add("@UpdatedAt", SqlDbType.DateTime);
                command.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 255);
                command.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 255);
                //Values
                command.Parameters["@Id"].Value = physicalPerson.Id;
                command.Parameters["@Name"].Value = physicalPerson.Name;
                command.Parameters["@Secondname"].Value = physicalPerson.Secondname;
                command.Parameters["@Lastname"].Value = physicalPerson.Lastname;
                command.Parameters["@BINorIIN"].Value = physicalPerson.BINorIIN;
                command.Parameters["@CreatedAt"].Value = physicalPerson.CreatedAt;
                command.Parameters["@UpdatedAt"].Value = physicalPerson.UpdatedAt;
                command.Parameters["@CreatedBy"].Value = physicalPerson.CreatedBy;
                command.Parameters["@UpdatedBy"].Value = physicalPerson.UpdatedBy;
                base.NonQueryTransaction(command);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void DeletePhysicalPerson(Guid Id)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand("DELETE FROM " + this._tableName + " WHERE Id = @Id", this.connectionString);
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                command.Parameters["@Id"].Value = Id;
                adapter.DeleteCommand = command;
                base.NonQueryTransaction(command);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}