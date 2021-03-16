﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Microsoft.AspNetCore.Identity;
using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Entities.Auth
{
	/// <inheritdoc cref="IdentityRole{TKey}"/>
	public class UserRoleEntity : IdentityUserRole<long>, IEntity
	{
		/// <inheritdoc/>
		public long Id =>
			UserRoleId;

		/// <summary>
		/// User Role ID
		/// </summary>
		public long UserRoleId { get; init; }
	}
}