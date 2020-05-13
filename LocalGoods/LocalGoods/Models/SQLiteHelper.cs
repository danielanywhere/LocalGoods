//	SQLiteHelper.cs
//
//	Copyright (c). 2020 Daniel Patterson, MCSD (danielanywhere)
//	Released for public access under the MIT License.
//	http://www.opensource.org/licenses/mit-license.php
//
//	Static helper functionality for SQLite databases.
//	System.Data.Sqlite variation.
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

using System.Data.SQLite;
using System.Web.Hosting;

namespace LocalGoods
{
	public class SQLHelper
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		private static string mCommonName =
			HostingEnvironment.MapPath("~/LocalGoodsData.sqlite3");
		private static string mConnectionName =
			$"Data Source={mCommonName};Version=3;";
		private static object mLocker = new object();
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* BracketKeyword																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Bracket the column name if it is a keyword.
		/// </summary>
		/// <param name="value">
		/// The value to inspect.
		/// </param>
		/// <returns>
		/// The caller's column name, bracketed if it is a keyword, or unbracketed
		/// if not.
		/// </returns>
		public static string BracketKeyword(string value)
		{
			string result = value;

			if(IsKeyword(value))
			{
				result = $"[{value}]";
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ConnectionString																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the default Connection String.
		/// </summary>
		public static string ConnectionString
		{
			get { return mConnectionName; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ConvertColumnTypes																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert object column types to natural data types.
		/// </summary>
		/// <param name="table">
		/// Reference to the table having columns to be converted.
		/// </param>
		public static void ConvertColumnTypes(DataTable table)
		{
			ColumnMetadataItem meta = null;

			foreach(DataColumn column in table.Columns)
			{
				meta = WebApiApplication.ColumnDefinitions[column.ColumnName];
				if(meta != null)
				{
					column.MaxLength = -1;
					switch(meta.DbType)
					{
						case DbType.DateTime:
							column.DataType = typeof(DateTime);
							break;
						case DbType.Double:
							column.DataType = typeof(double);
							break;
						case DbType.Guid:
							column.DataType = typeof(Guid);
							break;
						case DbType.Int32:
							column.DataType = typeof(int);
							break;
						case DbType.String:
							column.DataType = typeof(string);
							break;
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ConvertTableFromObjectToTypes																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert a table full of object-type cells to cells of base data types.
		/// </summary>
		/// <param name="table">
		/// Reference to the generic object table to be converted.
		/// </param>
		/// <returns>
		/// Reference to a newly created general types table.
		/// </returns>
		public static DataTable ConvertTableFromObjectToTypes(DataTable table)
		{
			DataTable result = new DataTable();

			if(table != null && (table.Rows.Count > 0 || table.Columns.Count > 0))
			{
				result = table.Clone();
				ConvertColumnTypes(result);
				foreach(DataRow row in table.Rows)
				{
					result.ImportRow(row);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetDbSize																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the field size associated with the caller's value.
		/// </summary>
		/// <param name="value">
		/// Value to inspect.
		/// </param>
		/// <returns>
		/// Size for use with Command Parameters.
		/// </returns>
		public static int GetDbSize(object value)
		{
			int rv = 0;

			if (value is String)
			{
				rv = ((String)value).Length;
			}
			return rv;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetDbType																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the DbType associated with the caller's value.
		/// </summary>
		/// <param name="value">
		/// Value to inspect.
		/// </param>
		/// <returns>
		/// DbType loosely associated with the value.
		/// </returns>
		public static DbType GetDbType(object value)
		{
			DbType rv = DbType.Int32;

			if(value is Boolean)
			{
				rv = DbType.Int32;
			}
			else if(value is Byte)
			{
				rv = DbType.Int32;
			}
			else if (value is Char)
			{
				rv = DbType.String;
			}
			else if (value is DataSet)
			{
				rv = DbType.String;
			}
			else if (value is DateTime)
			{
				rv = DbType.DateTime;
			}
			else if (value is Decimal)
			{
				rv = DbType.Double;
			}
			else if(value is Enum)
			{
				rv = DbType.Int32;
			}
			else if(value is Guid)
			{
				rv = DbType.Guid;
			}
			else if(value is Int16)
			{
				rv = DbType.Int32;
			}
			else if(value is Int32)
			{
				rv = DbType.Int32;
			}
			else if(value is Int64)
			{
				rv = DbType.Int32;
			}
			else if(value is Object)
			{
				rv = DbType.String;
			}
			else if (value is Single)
			{
				rv = DbType.Double;
			}
			else if(value is String)
			{
				rv = DbType.String;
			}
			else if (value is StringBuilder)
			{
				rv = DbType.String;
			}
			return rv;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetScalar																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a single value.
		/// </summary>
		/// <param name="sql">
		/// SQL SELECT Query, or other query returning rows.
		/// </param>
		/// <returns>
		/// Populated Data Table.
		/// </returns>
		public static int GetScalar(string sql)
		{
			SQLiteCommand cmd;								//	Working Command.
			SQLiteConnection conn;						//	Working Connection.
			int rv = 0;												//	Return Value.

			//	Create the connection.
			conn = new SQLiteConnection(ConnectionString);
			conn.Open();
			cmd = new SQLiteCommand(sql, conn);

			try
			{
				rv = Convert.ToInt32(cmd.ExecuteScalar());
			}
			catch(Exception ex)
			{
				Trace.WriteLine("Error: " + ex.Message + "\r\n" + sql,
					"SQLiteHelper.GetScalar");
			}
			cmd.Dispose();
			conn.Close();
			conn.Dispose();
			return rv;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetScalarBool																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a single value.
		/// </summary>
		/// <param name="sql">
		/// SQL SELECT Query, or other query returning rows.
		/// </param>
		/// <param name="defaultValue">
		/// Default Value if no results are found. If no default value is
		/// supplied, then false is assumed.
		/// </param>
		/// <returns>
		/// Boolean Value.
		/// </returns>
		public static bool GetScalarBool(string sql, bool defaultValue = false)
		{
			SQLiteCommand cmd;								//	Working Command.
			SQLiteConnection conn;						//	Working Connection.
			bool rv = defaultValue;						//	Return Value.

			//	Create the connection.
			conn = new SQLiteConnection(ConnectionString);
			conn.Open();
			cmd = new SQLiteCommand(sql, conn);

			try
			{
				rv = Convert.ToBoolean(cmd.ExecuteScalar());
			}
			catch(Exception ex)
			{
				Trace.WriteLine("Error: " + ex.Message + "\r\n" + sql,
					"SQLiteHelper.GetScalar");
			}
			cmd.Dispose();
			conn.Close();
			conn.Dispose();
			return rv;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetScalarInt																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a single value.
		/// </summary>
		/// <param name="sql">
		/// SQL SELECT Query, or other query returning rows.
		/// </param>
		/// <returns>
		/// Populated Data Table.
		/// </returns>
		public static int GetScalarInt(string sql)
		{
			SQLiteCommand cmd;								//	Working Command.
			SQLiteConnection conn;						//	Working Connection.
			int rv = 0;												//	Return Value.

			//	Create the connection.
			conn = new SQLiteConnection(ConnectionString);
			conn.Open();
			cmd = new SQLiteCommand(sql, conn);

			try
			{
				rv = Convert.ToInt32(cmd.ExecuteScalar());
			}
			catch(Exception ex)
			{
				Trace.WriteLine("Error: " + ex.Message + "\r\n" + sql,
					"SQLiteHelper.GetScalar");
			}
			cmd.Dispose();
			conn.Close();
			conn.Dispose();
			return rv;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a single value.
		/// </summary>
		/// <param name="tableName">
		/// Name of the Table to search.
		/// </param>
		/// <param name="displayColumn">
		/// Name of the Column to display.
		/// </param>
		/// <param name="keyColumn">
		/// Name of the Column to match.
		/// </param>
		/// <param name="keyValue">
		/// Value to match.
		/// </param>
		/// <returns>
		/// Populated Data Table.
		/// </returns>
		public static int GetScalarInt(string tableName,
			string displayColumn, string keyColumn, object keyValue)
		{
			SQLiteCommand cmd;								//	Working Command.
			SQLiteConnection conn;						//	Working Connection.
			int rv = 0;												//	Return Value.
			string sq = "";										//	SQL Command Text.

			sq = "SELECT " + 
				tableName + "." + displayColumn + " " +
				"FROM " + tableName + " " +
				"WHERE " +
				tableName + "." + keyColumn + " = " + ToSql(keyValue);
			//	Create the connection.
			conn = new SQLiteConnection(ConnectionString);
			conn.Open();
			cmd = new SQLiteCommand(sq, conn);

			try
			{
				rv = Convert.ToInt32(cmd.ExecuteScalar());
			}
			catch(Exception ex)
			{
				Trace.WriteLine("Error: " + ex.Message + "\r\n" + sq,
					"SQLiteHelper.GetScalarInt");
			}
			cmd.Dispose();
			conn.Close();
			conn.Dispose();
			return rv;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetScalarString																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a single value.
		/// </summary>
		/// <param name="sql">
		/// SQL SELECT Query, or other query returning rows.
		/// </param>
		/// <returns>
		/// Populated Data Table.
		/// </returns>
		public static string GetScalarString(string sql)
		{
			SQLiteCommand cmd;								//	Working Command.
			SQLiteConnection conn;						//	Working Connection.
			string rv = "";										//	Return Value.

			//	Create the connection.
			conn = new SQLiteConnection(ConnectionString);
			conn.Open();
			cmd = new SQLiteCommand(sql, conn);

			try
			{
				rv = Convert.ToString(cmd.ExecuteScalar());
			}
			catch(Exception ex)
			{
				Trace.WriteLine("Error: " + ex.Message + "\r\n" + sql,
					"SQLiteHelper.GetScalar");
			}
			cmd.Dispose();
			conn.Close();
			conn.Dispose();
			return rv;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetTable																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a populated DataTable containing the results of the query.
		/// </summary>
		/// <param name="sql">
		/// SQL SELECT Query, or other query returning rows.
		/// </param>
		/// <returns>
		/// Populated Data Table.
		/// </returns>
		public static DataTable GetTable(string sql)
		{
			SQLiteCommand cmd;								//	Working Command.
			SQLiteConnection conn;						//	Working Connection.
			SQLiteDataReader dr;							//	Working Data Reader.
			DataTable dt = new DataTable();   //	Working Data Table.
			DataTable result = new DataTable();

			//	Create the connection.
			conn = new SQLiteConnection(ConnectionString);
			conn.Open();

			cmd = new SQLiteCommand(sql, conn);
			try
			{
				dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
				dt.Load(dr);
				dr.Close();
				dr.Dispose();
				result = ConvertTableFromObjectToTypes(dt);
				result.Constraints.Clear();
				foreach(DataColumn column in result.Columns)
				{
					column.AllowDBNull = true;
				}
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"Error in GetTable: {ex.Message}");
			}
			cmd.Dispose();
			conn.Close();
			conn.Dispose();
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	InsertRecord																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Insert a record into the database.
		/// </summary>
		/// <param name="tableName">
		/// Name of the destination table.
		/// </param>
		/// <param name="columnNames">
		/// Names of the columns.
		/// </param>
		/// <param name="columnValues">
		/// Names of the values.
		/// </param>
		/// <returns>
		/// Value indicating whether the operation was a success.
		/// </returns>
		public static bool InsertRecord(string tableName,
			List<string> columnNames, List<object> columnValues)
		{
			return InsertRecord(tableName,
				columnNames.ToArray(), columnValues.ToArray());
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Insert a record into the database.
		/// </summary>
		/// <param name="tableName">
		/// Name of the destination table.
		/// </param>
		/// <param name="columnNames">
		/// Names of Columns.
		/// </param>
		/// <param name="columnValues">
		/// Values to insert, aligned with column names.
		/// </param>
		/// <returns>
		/// Value indicating whether the operation was a success.
		/// </returns>
		/// <remarks>
		/// In this version, all values are expected to be presented in their
		/// target formatting.
		/// </remarks>
		public static bool InsertRecord(string tableName,
			string[] columnNames, object[] columnValues)
		{
			int cc = columnNames.Length;    //	Column Count.
			int cp = 0;                     //	Column Position.
			SQLiteConnection conn;          //	Working Connection.
			SQLiteCommand cmd;              //	Working Command.
			bool rv = false;                //	Return Value.
			StringBuilder sc = new StringBuilder();       //	SQL Column Names.
			StringBuilder sq = new StringBuilder();       //	SQL Command Text.
			StringBuilder sv = new StringBuilder();       //	SQL Values.

			//	Create the connection.
			conn = new SQLiteConnection(ConnectionString);
			conn.Open();

			for(cp = 0; cp < cc; cp++)
			{
				if(sc.Length > 0)
				{
					sc.Append(",");
					sv.Append(",");
				}
				sc.Append(columnNames[cp]);
				sv.Append(columnValues[cp]);
			}
			if(sc.Length > 0)
			{
				sq.Append("INSERT INTO ");
				sq.Append(tableName);
				sq.Append(" (");
				sq.Append(sc.ToString());
				sq.Append(") VALUES (");
				sq.Append(sv.ToString());
				sq.Append(");");
			}
			cmd = new SQLiteCommand(sq.ToString(), conn);
			rv = (cmd.ExecuteNonQuery() != 0);
			cmd.Dispose();
			conn.Close();
			conn.Dispose();

			return rv;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsKeyword																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the caller's value is a keyword.
		/// </summary>
		/// <param name="value">
		/// The value to inspect.
		/// </param>
		/// <returns>
		/// True if the caller's value is a system keyword. Otherwise, false.
		/// </returns>
		public static bool IsKeyword(string value)
		{
			string kw = "";
			bool result = false;
			string tl = "";

			if(value?.Length > 0)
			{
				kw = ResourceMain.rwSQLDB;
				tl = value.ToLower();
				//	By knowing the first and last items on the delimited list, we can
				//	assure that all other entries are surrounded by commas without
				//	splitting the string.
				if(tl == "username" || tl == "password" || kw.IndexOf($",{tl},") > -1)
				{
					result = true;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	RecordExists																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified record exists.
		/// </summary>
		/// <param name="tableName">
		/// Name of the Table to check.
		/// </param>
		/// <param name="keyColumn">
		/// Name of the Column to test.
		/// </param>
		/// <param name="keyValue">
		/// Value to test.
		/// </param>
		/// <returns>
		/// True, if the specified record was found. Otherwise, false.
		/// </returns>
		/// <remarks>
		/// This version runs all values through ToSql.
		/// </remarks>
		public static bool RecordExists(string tableName,
			string keyColumn, object keyValue)
		{
			SQLiteConnection conn;					//	Working Connection.
			SQLiteCommand cmd;							//	Working Command.
			SQLiteDataReader rdr = null;		//	Working Reader.
			bool rv = false;								//	Return Value.
			StringBuilder sq = new StringBuilder();				//	SQL Command Text.

			//	Create the connection.
			conn = new SQLiteConnection(ConnectionString);
			conn.Open();

			sq.Append("SELECT ");
			sq.Append(keyColumn);
			sq.Append(" FROM ");
			sq.Append(tableName);
			sq.Append(" WHERE ");
			sq.Append(keyColumn);
			sq.Append(" = ");
			sq.Append(ToSql(keyValue));
			cmd = new SQLiteCommand(sq.ToString(), conn);
			rdr = cmd.ExecuteReader();
			rv = (rdr.HasRows);
			rdr.Close();
			rdr.Dispose();
			cmd.Dispose();
			conn.Close();
			conn.Dispose();

			return rv;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	RemoveColumns																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Remove one or more columns from the provided table.
		/// </summary>
		/// <param name="table">
		/// Reference to the data table containing the unwanted columns.
		/// </param>
		/// <param name="columnNames">
		/// Names of the columns to remove.
		/// </param>
		public static void RemoveColumns(DataTable table, string[] columnNames)
		{
			if(table != null)
			{
				foreach(string columnName in columnNames)
				{
					if(table.Columns.Contains(columnName))
					{
						table.Columns.Remove(columnName);
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ToSql																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Inspect the caller's value type, and return a formatted SQL value
		/// assignment.
		/// </summary>
		/// <param name="value">
		/// Value to query in SQL.
		/// </param>
		/// <returns>
		/// String value, formatted for use in SQL value assignment.
		/// </returns>
		public static string ToSql(object value)
		{
			string rv = "NULL";

			if(value != null)
			{
				rv = value.ToString();
				if(value is String)
				{
					rv = "'" + rv.Replace("'", "''") + "'";
				}
				else if(value is DateTime)
				{
					rv =
						"'" + ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
				}
				else if(value is Guid)
				{
					rv = "'" + ((Guid)value).ToString("D").ToUpper() + "'";
				}
			}
			return rv;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Update																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the database from values in the caller-specified table.
		/// </summary>
		/// <param name="table">
		/// DataTable to send.
		/// </param>
		/// <returns>
		/// Number of rows affected.
		/// </returns>
		/// <remarks>
		/// In this version, all values are set through ToSql conversion.
		/// </remarks>
		public static int Update(DataTable table)
		{
			string[] ca = new string[0];			//	Column Names array.
			int cc = 0;												//	Column Count.
			SQLiteCommand cmd;								//	Working Command.
			SQLiteConnection conn;						//	Working Connection.
			int cp = 0;												//	Column Position.
			DataRow dr = null;                //	Working Row.
			string kn = "";                   //	Key name.
			string kv = "";										//	Key value.
			int rc = 0;												//	Row Count.
			int rp = 0;												//	Row Position.
			int rv = 0;												//	Return Value.
			List<string> sb = new List<string>();
			string sq = "";										//	SQL Command Text.
			string tn = "";                   //	Table Name.
			string vl = "";										//	Working value.

			if(table != null && table.Columns.Count > 1 && table.Rows.Count > 0)
			{
				//	Create the connection.
				kn = tn = table.Columns[0].ColumnName;
				if(IsKeyword(kn))
				{
					kn = $"[{kn}]";
				}
				if(tn.EndsWith("ID"))
				{
					tn = tn.Substring(0, tn.Length - 2);
				}
				else if(tn.EndsWith("Ticket"))
				{
					tn = tn.Substring(0, tn.Length - 6);
				}

				cc = table.Columns.Count;
				rc = table.Rows.Count;
				lock(mLocker)
				{
					conn = new SQLiteConnection(ConnectionString);
					conn.Open();
					for(rp = 0; rp < rc; rp ++)
					{
						//	Each row.
						dr = table.Rows[rp];
						sb.Clear();
						kv = ToSql(table.Rows[rp][0]);
						for(cp = 1; cp < cc; cp++)
						{
							vl = ToSql(dr[cp]);
							sb.Add($"{BracketKeyword(table.Columns[cp].ColumnName)} = {vl}");
						}
						sq = $"UPDATE {tn} SET {string.Join(",", sb)} WHERE {kn} = {kv}";
						cmd = new SQLiteCommand(sq, conn);
						try
						{
							rv += cmd.ExecuteNonQuery();
						}
						catch(Exception ex)
						{
							Trace.WriteLine("Error: " + ex.Message + "\r\n" + sq,
								"SQLHelper.Update(DataTable)");
						}
						cmd.Dispose();
					}
					conn.Close();
					conn.Dispose();
				}
			}
			return rv;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Update the database with prepared SQL.
		/// </summary>
		/// <param name="sql">
		/// SQL UPDATE Query, or other query affecting rows.
		/// </param>
		/// <returns>
		/// Number of rows affected.
		/// </returns>
		public static int Update(string sql)
		{
			SQLiteCommand cmd;								//	Working Command.
			SQLiteConnection conn;						//	Working Connection.
			int rv = 0;												//	Return Value.

			//	Create the connection.
			conn = new SQLiteConnection(ConnectionString);
			conn.Open();
			cmd = new SQLiteCommand(sql, conn);

			try
			{
				rv = cmd.ExecuteNonQuery();
			}
			catch(Exception ex)
			{
				Trace.WriteLine("Error: " + ex.Message + "\r\n" + sql,
					"SQLiteHelper.Update");
			}
			cmd.Dispose();
			conn.Close();
			conn.Dispose();
			return rv;
		}
		//*-----------------------------------------------------------------------*

	}
}
