﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System;
using System.Threading.Tasks;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Abstracts;
using Strasnote.Data.Entities.Auth;
using Strasnote.Data.Fake;
using Strasnote.Logging;

namespace Strasnote.Auth.Data.Fake
{
	/// <inheritdoc cref="IRefreshTokenRepository"/>
	public sealed class RefreshTokenRepository : SqlRepository<RefreshTokenEntity>, IRefreshTokenRepository
	{
		public RefreshTokenRepository(ILog<RefreshTokenRepository> log) : base(log) { }

		public new Task CreateAsync(RefreshTokenEntity entity)
		{
			LogOperation(Operation.Create, "{Token}", entity);
			return Task.FromResult(0L);
		}

		public Task<RefreshTokenEntity> RetrieveForUserAsync(long userId, string refreshToken)
		{
			LogOperation("RetrieveForUser", "{UserId} {Token}", userId, refreshToken);
			return Task.FromResult(new RefreshTokenEntity("token", DateTime.Now.AddDays(1), 1));
		}

		public Task<bool> DeleteByUserIdAsync(long userId)
		{
			LogOperation(Operation.Delete, "{UserId}", userId);
			return Task.FromResult(true);
		}

		protected override object GetFakeModelForCreate() => throw new NotImplementedException();
		protected override object GetFakeModelForRetrieve(long id) => throw new NotImplementedException();
		protected override object GetFakeModelForUpdate() => throw new NotImplementedException();
	}
}
