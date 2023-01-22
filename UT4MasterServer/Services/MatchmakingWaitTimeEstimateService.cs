using UT4MasterServer.Models.Requests;

namespace UT4MasterServer.Services;

public class MatchmakingWaitTimeEstimateService
{
	private Dictionary<string, List<(DateTime DeleteTime, double WaitTime)>> estimates;
	private static readonly TimeSpan RelevantReportTimeDuration = TimeSpan.FromMinutes(1);

	public MatchmakingWaitTimeEstimateService()
	{
		estimates = new Dictionary<string, List<(DateTime, double)>>();
	}

	public void AddWaitTime(string mode, double seconds)
	{
		if (!estimates.ContainsKey(mode))
			estimates.Add(mode, new List<(DateTime, double)>());
		estimates[mode].Add((DateTime.UtcNow + RelevantReportTimeDuration, seconds));
	}

	public List<WaitTimeEsitmate> GetWaitTimes()
	{
		Clean();

		List<WaitTimeEsitmate> waitTimes = new List<WaitTimeEsitmate>();
		foreach (var estimate in estimates)
		{
			if (estimate.Value.Count <= 0)
				continue;

			var estimatedModeWait = estimate.Value.Average(x => x.WaitTime);
			waitTimes.Add(new WaitTimeEsitmate(estimate.Key, estimatedModeWait, estimate.Value.Count));
		}

		return waitTimes;
	}

	private void Clean()
	{
		var now = DateTime.UtcNow;
		List<string> modesToRemove = new List<string>();
		foreach (var mode in estimates)
		{
			mode.Value.RemoveAll(x => now > x.DeleteTime);
			if (mode.Value.Count <= 0)
				modesToRemove.Add(mode.Key);
		}

		foreach (var mode in modesToRemove)
		{
			estimates.Remove(mode);
		}
	}
}
