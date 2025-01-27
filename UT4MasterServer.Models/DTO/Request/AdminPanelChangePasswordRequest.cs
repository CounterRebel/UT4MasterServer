﻿using System.Text.Json.Serialization;

namespace UT4MasterServer.Models.DTO.Request;

public class AdminPanelChangePasswordRequest
{
	[JsonPropertyName("newPassword")]
	public string NewPassword { get; set; } = string.Empty;

	[JsonPropertyName("iAmSure")]
	public bool? IAmSure { get; set; } = null;
}
