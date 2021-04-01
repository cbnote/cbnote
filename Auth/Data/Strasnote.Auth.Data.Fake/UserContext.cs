﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;
using Strasnote.Data.Fake;
using Strasnote.Logging;

namespace Strasnote.Auth.Data.Fake
{
	/// <inheritdoc cref="IUserContext"/>
	public sealed class UserContext : DbContext<UserEntity>, IUserContext
	{
		public UserContext(ILog<UserContext> Log) : base(Log) { }

		protected override object GetFakeModelForCreate() =>
			IdentityResult.Success;

		protected override object GetFakeModelForRetrieve(long id) =>
			new UserEntity { Id = id };

		public Task<TModel> RetrieveByEmailAsync<TModel>(string email)
		{
			Log.Information("Retrieve user with email: {Email}", email);

			var user = new UserEntity
			{
				Email = email,
				Id = 1,
				LockoutEnabled = true,
				LockoutEnd = null,
				PasswordHash = "AQAAAAEAACcQAAAAEHn2EWqFQl3+BRPBdGgLPWifuv1xysXz1zQxwq+bRCPUvwNoHk0fowP9wxp83hXJoA==",
				UserName = email
			};

			return ConvertToModel<TModel>(user);
		}

		public Task<TModel> RetrieveByUsernameAsync<TModel>(string name) =>
			RetrieveByEmailAsync<TModel>(name);

		protected override object GetFakeModelForUpdate()
		{
			throw new System.NotImplementedException();
		}
	}
}