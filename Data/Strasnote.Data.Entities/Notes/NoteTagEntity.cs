﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Strasnote.Data.Abstracts;

namespace Strasnote.Data.Entities.Notes
{
	/// <summary>
	/// Note Tag entity
	/// </summary>
	public sealed record NoteTagEntity : IEntity
	{
		/// <inheritdoc/>
		[Ignore]
		public long Id
		{
			get => NoteTagId;
			init => NoteTagId = value;
		}

		/// <summary>
		/// Note Tag ID
		/// </summary>
		public long NoteTagId { get; init; }

		#region Relationships

		/// <summary>
		/// Note ID
		/// </summary>
		public long NoteId { get; init; }

		/// <summary>
		/// Tag ID
		/// </summary>
		public long TagId { get; init; }

		#endregion
	}
}
