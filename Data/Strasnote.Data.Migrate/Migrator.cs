﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Config;
using Strasnote.Data.Entities.Auth;
using Strasnote.Logging;

namespace Strasnote.Data.Migrate
{
	/// <summary>
	/// Migrates and inserts test data into the database using the specified database client
	/// </summary>
	public sealed class Migrator : IMigrator
	{
		private readonly MigrateConfig migrateConfig;

		private readonly UserConfig userConfig;

		private readonly IDbClient client;

		private readonly ILog log;

		private readonly IUserRepository user;

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="services">IServiceProvider</param>
		public Migrator(IServiceProvider services)
		{
			// Get config
			migrateConfig = services.GetRequiredService<IOptions<MigrateConfig>>().Value;
			userConfig = services.GetRequiredService<IOptions<UserConfig>>().Value;

			// Get other services
			client = services.GetRequiredService<IDbClient>();
			log = services.GetRequiredService<ILog<Migrator>>();

			// Get repositories
			user = services.GetRequiredService<IUserRepository>();
		}

		/// <inheritdoc/>
		public Task ExecuteAsync()
		{
			if (migrateConfig.NukeOnStartup)
			{
				return Nuke();
			}
			else if (migrateConfig.MigrateToLatestOnStartup)
			{
				client.MigrateToLatest();
			}

			return Task.CompletedTask;
		}

		/// <summary>
		/// Wipe and reinstall the database and test data
		/// </summary>
		private Task Nuke()
		{
			// Destroy database (i.e. migrate to version 0)
			log.Information("Nuking database.");
			client.Nuke();

			// Migrate to the latest version of the database
			MigrateToLatest();

			// Insert test data
			return InsertTestData();
		}

		/// <summary>
		/// Migrate to the latest version of the database
		/// </summary>
		private void MigrateToLatest()
		{
			log.Information("Migrating database to the latest version.");
			client.MigrateToLatest();
		}

		/// <summary>
		/// Insert test data
		/// </summary>
		private async Task InsertTestData()
		{
			log.Information("Inserting test data.");

			// Insert default user
			await DefaultUser.InsertAsync(user, userConfig).ConfigureAwait(false);
		}
	}
}
