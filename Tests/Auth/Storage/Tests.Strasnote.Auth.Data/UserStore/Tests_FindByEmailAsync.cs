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
	public sealed class Tests_FindByEmailAsync
	{
		private readonly IUserContext userContext = Substitute.For<IUserContext>();
		private readonly IRoleContext roleContext = Substitute.For<IRoleContext>();

		public Tests_FindByEmailAsync()
		{
			userContext.RetrieveByEmailAsync<UserEntity>(Arg.Any<string>())
				.Returns(new UserEntity());
		}

		[Fact]
		public async Task UserEntity_Returned_On_Successful_Call()
		{
			// Arrange
			var userStore = new UserStore(userContext, roleContext);

			// Act
			var result = await userStore.FindByEmailAsync(Rnd.Str, new CancellationToken());

			// Assert
			Assert.IsAssignableFrom<UserEntity>(result);
		}

		[Fact]
		public async Task UserContext_RetrieveByEmailAsync_Is_Called_Once()
		{
			// Arrange
			var userStore = new UserStore(userContext, roleContext);

			// Act
			await userStore.FindByEmailAsync(Rnd.Str, new CancellationToken());

			// Assert
			await userContext.Received(1).RetrieveByEmailAsync<UserEntity>(Arg.Any<string>());
		}
	}
}