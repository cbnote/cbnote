﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Auth.Data;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;
using Strasnote.Util;
using Xunit;

namespace Tests.Strasnote.Auth.Data
{
	public sealed class Tests_HasPasswordAsync
	{
		private readonly IUserContext userContext = Substitute.For<IUserContext>();
		private readonly IRoleContext roleContext = Substitute.For<IRoleContext>();

		[Fact]
		public async Task HasPassword_Bool_Returns_True_When_PasswordHash_Has_A_Value()
		{
			// Arrange
			var userStore = new UserStore(userContext, roleContext);

			var userEntity = new UserEntity
			{
				PasswordHash = Rnd.Str
			};

			// Act
			var result = await userStore.HasPasswordAsync(userEntity, new CancellationToken());

			// Assert
			Assert.True(result);
		}

		[Fact]
		public async Task HasPassword_Bool_Returns_False_When_PasswordHash_Has_A_Value()
		{
			// Arrange
			var userStore = new UserStore(userContext, roleContext);

			// Act
			var result = await userStore.HasPasswordAsync(new UserEntity(), new CancellationToken());

			// Assert
			Assert.False(result);
		}
	}
}
