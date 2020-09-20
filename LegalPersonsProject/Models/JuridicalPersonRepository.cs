using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Ajax.Utilities;
using System.Diagnostics;

namespace LegalPersonsProject.Models
{
   public class JuridicalPersonRepository : Repository
    {
        protected readonly string _tableName = "JuridicalPersons";
        protected readonly string _physicalPersonsTableName = "PhysicalPersons";
        protected readonly string _juridicalPersonContactsTableName = "JuridicalPersonContacts";
        public DataTable GetAll(string sortColumns, int startRowIndex, int maximumRows)
        {
            try
            {
                VerifySortColumns(sortColumns);
                string query = "SELECT * FROM " + this._tableName;
                if (sortColumns.Trim() == "")
                {
                    query += " ORDER BY Name";
                }
                else
                {
                    query += " ORDER BY " + sortColumns;
                }
                DataSet juridicalPersonsDataSet = CreateCommand(query, this._tableName);
                return juridicalPersonsDataSet.Tables[_tableName];
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
            string[] authorisedColumns = {"", "id", "name",
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
                DataSet juridicalPersonsDataSet = new DataSet();
                adapter.Fill(juridicalPersonsDataSet, this._tableName);
                return juridicalPersonsDataSet.Tables[_tableName];
            }
            catch (Exception e)
            {

                throw e;
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

        public DataSet GetContactsById(Guid id)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            string query = "SELECT c.PhysicalPersonId, p.Name, p.Secondname, p.Lastname, " +
                " p.BINorIIN, p.CreatedAt, p.UpdatedAt, p.CreatedBy, p.UpdatedBy, c.Position FROM " +
                this._physicalPersonsTableName + " p " + 
                " JOIN " + this._juridicalPersonContactsTableName + " c " + " ON " +
                " c.PhysicalPersonId = p.Id " + " JOIN " + this._tableName + " j " +
                " ON  j.Id = c.JuridicalPersonId " + " WHERE j.Id = @id ";
            SqlCommand command = new SqlCommand(query, this.connectionString);
            command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
            command.Parameters["@Id"].Value = id;
            adapter.SelectCommand = command;
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            return dataSet;
        }

        public void InsertJuridicalPerson(
                                        string name,
                                        string binOrIin,
                                        string createdBy)
        {
            try
            {
                JuridicalPerson juridicalPerson = new JuridicalPerson(
                name, binOrIin, createdBy);
                SqlCommand command = new SqlCommand("INSERT INTO " + this._tableName + "(Id, Name, " +
                    " BINorIIN, CreatedAt, UpdatedAt, " +
                    "CreatedBy, UpdatedBy) VALUES (@Id, @Name, " +
                    " @BINorIIN, @CreatedAt, @UpdatedAt, " +
                    "@CreatedBy, @UpdatedBy) ", this.connectionString);
                //    +
                //   "INSERT INTO JuridicalPersonContacts(JuridicalPersonId, PhysicalPersonId, " +
                //   "Position) VALUES (@Id, (SELECT Id FROM PhysicalPersons WHERE Id=@contactId), " +
                //   "@contactPosition);"
                //Parameters
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                command.Parameters.Add("@Name", SqlDbType.VarChar, 255);
                
                command.Parameters.Add("@BINorIIN", SqlDbType.BigInt);
                command.Parameters.Add("@CreatedAt", SqlDbType.DateTime);
                command.Parameters.Add("@UpdatedAt", SqlDbType.DateTime);
                command.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 255);
                command.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 255);
                //command.Parameters.Add("@contactId", SqlDbType.UniqueIdentifier);
                //command.Parameters.Add("@contactPosition", SqlDbType.VarChar, 255);
                //Values
                command.Parameters["@Id"].Value = juridicalPerson.Id;
                command.Parameters["@Name"].Value = juridicalPerson.Name;
                command.Parameters["@BINorIIN"].Value = juridicalPerson.BINorIIN;
                command.Parameters["@CreatedAt"].Value = juridicalPerson.CreatedAt;
                command.Parameters["@UpdatedAt"].Value = juridicalPerson.UpdatedAt;
                command.Parameters["@CreatedBy"].Value = juridicalPerson.CreatedBy;
                command.Parameters["@UpdatedBy"].Value = juridicalPerson.UpdatedBy;
                //command.Parameters["@contactId"].Value = contactId;
                //command.Parameters["@contactPosition"].Value = contactPosition;
                base.NonQueryTransaction(command);
            }
            catch (Exception e)
            {

                throw e;
            }
            

        }

        public void UpdateJuridicalPerson(Guid id,
                                string name,
                                string binOrIin,
                                string createdAt,
                                string updatedAt,
                                string createdBy,
                                string updatedBy)
        {
            try
            {
                JuridicalPerson juridicalPerson = new JuridicalPerson(id,
                name, binOrIin,
                createdAt, updatedAt, createdBy, updatedBy);
                SqlCommand command = new SqlCommand("UPDATE " + this._tableName + " SET Name = @Name, " +
                    " BINorIIN = @BINorIIN, " +
                    "CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt, CreatedBy = @CreatedBy," +
                    "UpdatedBy = @UpdatedBy WHERE Id = @Id", this.connectionString);
                //Parameters
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                command.Parameters.Add("@Name", SqlDbType.VarChar, 255);
                command.Parameters.Add("@BINorIIN", SqlDbType.BigInt);
                command.Parameters.Add("@CreatedAt", SqlDbType.DateTime);
                command.Parameters.Add("@UpdatedAt", SqlDbType.DateTime);
                command.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 255);
                command.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 255);
                //Values
                command.Parameters["@Id"].Value = juridicalPerson.Id;
                command.Parameters["@Name"].Value = juridicalPerson.Name;
                command.Parameters["@BINorIIN"].Value = juridicalPerson.BINorIIN;
                command.Parameters["@CreatedAt"].Value = juridicalPerson.CreatedAt;
                command.Parameters["@UpdatedAt"].Value = juridicalPerson.UpdatedAt;
                command.Parameters["@CreatedBy"].Value = juridicalPerson.CreatedBy;
                command.Parameters["@UpdatedBy"].Value = juridicalPerson.UpdatedBy;
                base.NonQueryTransaction(command);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteJuridicalPerson(Guid Id)
        {
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM " + this._tableName + " WHERE Id = @Id", this.connectionString);
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                command.Parameters["@Id"].Value = Id;
                base.NonQueryTransaction(command);
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        public void InsertContact(Guid juridicalPersonId, Guid physicalPersonId, string position)
        {
            string queryString = "INSERT INTO " + this._juridicalPersonContactsTableName +
                " VALUES (@juridicalPersonId, @physicalPersonId, @position)";
            SqlCommand command = new SqlCommand(queryString, this.connectionString);
            command.Parameters.AddWithValue("@juridicalPersonId", juridicalPersonId);
            command.Parameters.AddWithValue("@physicalPersonId", physicalPersonId);
            command.Parameters.AddWithValue("@position", position);
            base.NonQueryTransaction(command);
        }

        public void UpdateContact(Guid juridicalPersonId, Guid physicalPersonId, string position)
        {
            string queryString = "UPDATE "+ this._juridicalPersonContactsTableName + " SET " + 
                " JuridicalPersonId = @juridicalPersonId, PhysicalPersonId = @physicalPersonId," +
                " Position = @position WHERE JuridicalPersonId = @juridicalPersonId AND " +
                " PhysicalPersonId = @physicalPersonId ";
            SqlCommand command = new SqlCommand(queryString, this.connectionString);
            command.Parameters.AddWithValue("@juridicalPersonId", juridicalPersonId);
            command.Parameters.AddWithValue("@physicalPersonId", physicalPersonId);
            command.Parameters.AddWithValue("@position", position);
            base.NonQueryTransaction(command);
        }

        public void DeleteContact(Guid juridicalPersonId, Guid physicalPersonId)
        {
            string queryString = "DELETE FROM " + this._juridicalPersonContactsTableName +
                " WHERE JuridicalPersonId = @juridicalPersonId AND PhysicalPersonId = @physicalPersonId";
            SqlCommand command = new SqlCommand(queryString, this.connectionString);
            command.Parameters.AddWithValue("@physicalPersonId", physicalPersonId);
            command.Parameters.AddWithValue("@juridicalPersonId", juridicalPersonId);
            base.NonQueryTransaction(command);
        }
    }
}