﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Strasnote.Auth.Data;
using Strasnote.Data.Entities.Auth;
using Strasnote.Logging;

namespace Strasnote.Auth.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : Controller
	{
		private readonly UserStore users;

		private readonly ILog<UserController> log;

		public UserController(UserStore users, ILog<UserController> log) =>
			(this.users, this.log) = (users, log);

		[HttpPost]
		public async Task<IdentityResult> Create(UserEntity user)
		{
			log.Trace("Creating user: {@User}", user);
			return await users.CreateAsync(user, new());
		}

		[HttpGet]
		[Route("id/{id}")]
		public async Task<UserEntity> GetById(string id)
		{
			log.Trace("Get user with ID: {Id}", id);
			return await users.FindByIdAsync(id, new());
		}

		[HttpGet]
		[Route("name/{username}")]
		public async Task<UserEntity> GetByName(string username)
		{
			log.Trace("Get user with name: {Username}", username);
			return await users.FindByNameAsync(username, new());
		}

		[HttpPut]
		public async Task<IdentityResult> Update(UserEntity user)
		{
			log.Trace("Updating user: {@User}", user);
			return await users.UpdateAsync(user, new());
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IdentityResult> Delete(string id)
		{
			log.Trace("Deleting user with ID: {Id}", id);
			var user = await users.FindByIdAsync(id, new());
			return await users.DeleteAsync(user, new());
		}
	}
}
