﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Strasnote.Auth.Data;
using Strasnote.Auth.Data.Abstracts;
using Strasnote.Data.Entities.Auth;
using Xunit;

namespace Tests.Strasnote.Auth.Data
{
	public sealed class Tests_DeleteAsync
	{
		private readonly IUserRepository userRepository = Substitute.For<IUserRepository>();

		[Fact]
		public async Task UserRepository_DeleteAsync_Is_Called_Once()
		{
			// Arrange
			var userStore = new UserStore(userRepository);

			var userEntity = new UserEntity();

			// Act
			await userStore.DeleteAsync(userEntity, new CancellationToken());

			// Assert
			await userRepository.Received(1).DeleteAsync(Arg.Any<long>());
		}

		[Fact]
		public async Task IdentityResult_Success_Returned_When_Delete_Success()
		{
			// Arrange
			var userStore = new UserStore(userRepository);

			var userEntity = new UserEntity();

			userRepository.DeleteAsync(Arg.Any<long>()).Returns(true);

			// Act
			var result = await userStore.DeleteAsync(userEntity, new CancellationToken());

			// Assert
			Assert.True(result.Succeeded);
		}
	}
}