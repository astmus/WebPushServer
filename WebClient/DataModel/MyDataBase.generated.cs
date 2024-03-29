﻿//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

#pragma warning disable 1573, 1591

using System;
using System.Linq;

using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Mapping;

namespace DataModels
{
	/// <summary>
	/// Database       : logs
	/// Data Source    : tcp://localhost:5432
	/// Server Version : 16.1
	/// </summary>
	public partial class LogsDB : LinqToDB.Data.DataConnection, ILogsDB
	{
		public ITable<Log> Logs { get { return this.GetTable<Log>(); } }
		public ITable<Push> Pushes { get { return this.GetTable<Push>(); } }

		partial void InitMappingSchema()
		{
		}

		public LogsDB()
		{
			InitDataContext();
			InitMappingSchema();
		}

		public LogsDB(string configuration)
			: base(configuration)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public LogsDB(DataOptions options)
			: base(options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public LogsDB(DataOptions<LogsDB> options)
			: base(options.Options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		partial void InitDataContext();
		partial void InitMappingSchema();
	}

	[Table(Schema = "public", Name = "log")]
	public partial class Log
	{
		[Column, Nullable] public string Message { get; set; } // text
		[Column, Nullable] public string MessageTemplate { get; set; } // text
		[Column, Nullable] public int? Level { get; set; } // integer
		[Column, Nullable] public DateTimeOffset? Timestamp { get; set; } // timestamp (6) with time zone
		[Column, Nullable] public string Exception { get; set; } // text
		[Column, Nullable] public string LogEvent { get; set; } // jsonb
	}

	[Table(Schema = "public", Name = "push")]
	public partial class Push
	{
		[Column("user_id"), PrimaryKey, Identity] public int UserId { get; set; } // integer
		[Column("username"), NotNull] public string Username { get; set; } // character varying(50)
		[Column("token"), NotNull] public string Token { get; set; } // character varying(50)
	}

	public static partial class TableExtensions
	{
		public static Push Find(this ITable<Push> table, int UserId)
		{
			return table.FirstOrDefault(t =>
				t.UserId == UserId);
		}
	}
}
