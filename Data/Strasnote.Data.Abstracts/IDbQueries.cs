﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Collections.Generic;

namespace Strasnote.Data.Abstracts
{
	public interface IDbQueries
	{
		/// <summary>
		/// String to select all columns
		/// </summary>
		string SelectAll { get; }

		/// <summary>
		/// Return Create query
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns</param>
		string GetCreateQuery(string table, List<string> columns);

		/// <summary>
		/// Return Retrieve (by ID) query
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns to select</param>
		/// <param name="idColumn">ID column</param>
		/// <param name="id">ID value</param>
		string GetRetrieveQuery(string table, List<string> columns, string idColumn, long id);

		/// <summary>
		/// Return Retrieve query using all predicates (performs an AND query)
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns to select</param>
		/// <param name="predicates">List of predicates (uses AND)</param>
		(string query, Dictionary<string, object> param) GetRetrieveQuery(
			string table, List<string> columns, List<(string column, SearchOperator op, object value)> predicates
		);

		/// <summary>
		/// Return Update query
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns to update</param>
		/// <param name="idColumn">ID column</param>
		/// <param name="id">ID value</param>
		string GetUpdateQuery(string table, List<string> columns, string idColumn, long id);

		/// <summary>
		/// Return Delete query
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="idColumn">ID column</param>
		/// <param name="id">ID value</param>
		string GetDeleteQuery(string table, string idColumn, long id);
	}
}
