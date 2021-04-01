﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Data;
using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Fake
{
	public sealed class DbClient : ISqlClient
	{
		public string ConnectionString =>
			string.Empty;

		public ISqlQueries Queries =>
			throw new System.NotImplementedException();

		public IDbTables Tables =>
			throw new System.NotImplementedException();

		public IDbConnection Connect() =>
			new DbConnection();

		public bool MigrateTo(long? version) =>
			true;
	}
}
