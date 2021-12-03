﻿/*
 *    The contents of this file are subject to the Initial
 *    Developer's Public License Version 1.0 (the "License");
 *    you may not use this file except in compliance with the
 *    License. You may obtain a copy of the License at
 *    https://github.com/FirebirdSQL/NETProvider/raw/master/license.txt.
 *
 *    Software distributed under the License is distributed on
 *    an "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either
 *    express or implied. See the License for the specific
 *    language governing rights and limitations under the License.
 *
 *    All Rights Reserved.
 */

//$Authors = Jiri Cincura (jiri@cincura.net)

using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;

namespace FirebirdSql.EntityFrameworkCore.Firebird.FunctionalTests.Query
{
	public class FromSqlQueryFbTest : FromSqlQueryTestBase<NorthwindQueryFbFixture<NoopModelCustomizer>>
	{
		public FromSqlQueryFbTest(NorthwindQueryFbFixture<NoopModelCustomizer> fixture)
			: base(fixture)
		{ }

		protected override DbParameter CreateDbParameter(string name, object value)
			=> new FbParameter
			{
				ParameterName = name,
				Value = value
			};

		[Theory]
		[MemberData(nameof(IsAsyncData))]
		public override async Task FromSql_Count_used_twice_without_parameters(bool async)
		{
			using var context = CreateContext();

			var query = context.Set<OrderQuery>()
				.FromSqlRaw(NormalizeDelimitersInRawString("SELECT 'ALFKI' AS [CustomerID] FROM RDB$DATABASE"))
				.IgnoreQueryFilters();

			var result1 = async
				? await query.CountAsync() > 0
				: query.Count() > 0;

			var result2 = async
				? await query.CountAsync() > 0
				: query.Count() > 0;
		}

		[Theory]
		[MemberData(nameof(IsAsyncData))]
		public override async Task FromSql_Count_used_twice_with_parameters(bool async)
		{
			using var context = CreateContext();

			var query = context.Set<OrderQuery>()
				.FromSqlRaw(NormalizeDelimitersInRawString("SELECT CAST({0} AS CHAR(5)) AS [CustomerID] FROM RDB$DATABASE"), "ALFKI")
				.IgnoreQueryFilters();

			var result1 = async
				? await query.CountAsync() > 0
				: query.Count() > 0;

			var result2 = async
				? await query.CountAsync() > 0
				: query.Count() > 0;
		}

		[Theory]
		[MemberData(nameof(IsAsyncData))]
		public override async Task FromSql_used_twice_without_parameters(bool async)
		{
			using var context = CreateContext();

			var query = context.Set<OrderQuery>()
				.FromSqlRaw(NormalizeDelimitersInRawString("SELECT 'ALFKI' AS [CustomerID] FROM RDB$DATABASE"))
				.IgnoreQueryFilters();

			var result1 = async
				? await query.AnyAsync()
				: query.Any();

			Assert.Equal(
				RelationalStrings.QueryFromSqlInsideExists,
				async
					? (await Assert.ThrowsAsync<InvalidOperationException>(() => query.AnyAsync())).Message
					: Assert.Throws<InvalidOperationException>(() => query.Any()).Message);
		}

		[Theory]
		[MemberData(nameof(IsAsyncData))]
		public override async Task FromSql_used_twice_with_parameters(bool async)
		{
			using var context = CreateContext();

			var query = context.Set<OrderQuery>()
				.FromSqlRaw(NormalizeDelimitersInRawString("SELECT CAST({0} AS CHAR(5)) AS [CustomerID] FROM RDB$DATABASE"), "ALFKI")
				.IgnoreQueryFilters();

			var result1 = async
				? await query.AnyAsync()
				: query.Any();

			Assert.Equal(
				RelationalStrings.QueryFromSqlInsideExists,
				async
					? (await Assert.ThrowsAsync<InvalidOperationException>(() => query.AnyAsync())).Message
					: Assert.Throws<InvalidOperationException>(() => query.Any()).Message);
		}

		[Theory(Skip = "Provider does the casting.")]
		[MemberData(nameof(IsAsyncData))]
		public override Task Bad_data_error_handling_invalid_cast(bool async)
		{
			return base.Bad_data_error_handling_invalid_cast(async);
		}

		[Theory(Skip = "Provider does the casting.")]
		[MemberData(nameof(IsAsyncData))]
		public override Task Bad_data_error_handling_invalid_cast_key(bool async)
		{
			return base.Bad_data_error_handling_invalid_cast_key(async);
		}

		[Theory(Skip = "Provider does the casting.")]
		[MemberData(nameof(IsAsyncData))]
		public override Task Bad_data_error_handling_invalid_cast_no_tracking(bool async)
		{
			return base.Bad_data_error_handling_invalid_cast_no_tracking(async);
		}

		[Theory(Skip = "Provider does the casting.")]
		[MemberData(nameof(IsAsyncData))]
		public override Task Bad_data_error_handling_invalid_cast_projection(bool async)
		{
			return base.Bad_data_error_handling_invalid_cast_projection(async);
		}

		[Theory(Skip = "Provider does the casting.")]
		[MemberData(nameof(IsAsyncData))]
		public override Task FromSqlRaw_queryable_simple_projection_composed(bool async)
		{
			return base.FromSqlRaw_queryable_simple_projection_composed(async);
		}
	}
}
