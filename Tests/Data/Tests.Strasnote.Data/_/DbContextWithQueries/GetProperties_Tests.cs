﻿// Copyright (c) Strasnote
// Licensed under https://strasnote.com/licence

using Xunit;

namespace Strasnote.Data.DbContextWithQueries_Tests
{
	public class GetProperties_Tests
	{
		[Fact]
		public void Gets_Matching_Properties()
		{
			// Arrange
			var (context, _, _, _, _) = DbContextWithQueries.GetContext();

			// Act
			var result = context.GetProperties<WithSomeMatchingProperties>();

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(nameof(TestEntity.Bar), x),
				x => Assert.Equal(nameof(TestEntity.Foo), x)
			);
		}

		[Fact]
		public void Ignores_Properties_With_IgnoreAttribute()
		{
			// Arrange
			var (context, _, _, _, _) = DbContextWithQueries.GetContext();

			// Act
			var result = context.GetProperties<WithIgnoreAttribute>();

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(nameof(TestEntity.Foo), x)
			);
		}

		[Fact]
		public void Ignores_Properties_With_Same_Name_But_Different_Type()
		{
			// Arrange
			var (context, _, _, _, _) = DbContextWithQueries.GetContext();

			// Act
			var result = context.GetProperties<WithSameNameButDifferentType>();

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(nameof(TestEntity.Bar), x)
			);
		}

		[Fact]
		public void Returns_SelectAll_If_No_Matching_Properties()
		{
			// Arrange
			var (context, _, _, _, _) = DbContextWithQueries.GetContext();

			// Act
			var result = context.GetProperties<NoMatchingProperties>();

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(context.QueriesTest.SelectAll, x)
			);
		}

		[Fact]
		public void Uses_Cache_To_Return_Properties()
		{
			// Arrange
			var (context, _, _, _, _) = DbContextWithQueries.GetContext();

			// Act
			context.GetProperties<ForCacheTest>();
			context.GetProperties<ForCacheTest>();
			context.GetProperties<ForCacheTest>();
			context.GetProperties<ForCacheTest>();
			context.GetProperties<ForCacheTest>();

			// Assert
			Assert.Equal(1, context.CacheTest);
		}

		public sealed record WithSomeMatchingProperties(string Foo, int Bar, int DoNotMap);

		public sealed record WithIgnoreAttribute(string Foo, int AlwaysIgnore)
		{
			[Ignore]
			public bool Bar { get; set; }
		}

		public sealed record WithSameNameButDifferentType(int Foo, int Bar);

		public sealed record NoMatchingProperties(string FooDifferent, bool BarDifferent);

		public sealed record ForCacheTest;
	}
}