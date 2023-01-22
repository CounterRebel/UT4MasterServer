using System.Text.Json.Serialization;

namespace UT4MasterServer.Models.Requests;

public sealed class WaitTimeEsitmate
{
	[JsonPropertyName("ratingType")]
	public string RatingType { get; set; }

	[JsonPropertyName("averageWaitTimeSecs")]
	public double WaitTimeSeconds { get; set; }

	[JsonPropertyName("numSamples")]
	public int SampleCount { get; set; }

	public WaitTimeEsitmate(string ratingType, double seconds, int sampleCount)
	{
		RatingType = ratingType;
		WaitTimeSeconds = seconds;
		SampleCount = sampleCount;
	}
}
