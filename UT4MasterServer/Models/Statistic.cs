﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using UT4MasterServer.Enums;
using UT4MasterServer.Other;

namespace UT4MasterServer.Models;

public sealed class Statistic
{
	[BsonId]
	[BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
	public string ID { get; set; } = default!;

	[BsonElement("accountId")]
	public EpicID AccountID { get; set; } = default!;

	[BsonElement("createdAt")]
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	[BsonElement("modifiedAt")]
	[BsonIgnoreIfNull]
	public DateTime? ModifiedAt { get; set; }

	[BsonElement("type")]
	public StatisticType Type { get; set; } = StatisticType.Unknown;

	[BsonElement("value")]
	public float Value { get; set; } = 0;

	[BsonElement("window")]
	public StatisticWindow Window { get; set; } = StatisticWindow.Daily;

	[BsonElement("ownerType")]
	public OwnerType OwnerType { get; set; } = OwnerType.Default;
}