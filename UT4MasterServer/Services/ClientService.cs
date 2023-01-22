using Microsoft.AspNetCore.DataProtection;
using MongoDB.Driver;
using System.Net.Sockets;
using UT4MasterServer.Models;
using UT4MasterServer.Other;

namespace UT4MasterServer.Services;

public class ClientService
{
	private readonly IMongoCollection<Client> collection;

	public ClientService(DatabaseContext dbContext)
	{
		collection = dbContext.Database.GetCollection<Client>("clients");
	}

	public async Task<bool?> UpdateAsync(Client client)
	{
		var options = new ReplaceOptions() { IsUpsert = true };
		var result = await collection.ReplaceOneAsync(x => x.ID == client.ID, client, options);
		if (!result.IsAcknowledged)
			return null;
		return result.ModifiedCount == 1;
	}

	public async Task<Client?> GetAsync(EpicID id)
	{
		var options = new FindOptions<Client>() { Limit = 1 };
		var cursor = await collection.FindAsync(x => x.ID == id, options);
		return await cursor.SingleOrDefaultAsync();
	}

	public async Task<Client?> GetAsync(EpicID id, string secret)
	{
		var options = new FindOptions<Client>() { Limit = 1 };
		var cursor = await collection.FindAsync(x => x.ID == id && x.Secret == secret, options);
		return await cursor.SingleOrDefaultAsync();
	}

	public async Task<List<Client>> ListAsync()
	{
		var options = new FindOptions<Client>() { Limit = 1 };
		var cursor = await collection.FindAsync(x => true, options);
		return await cursor.ToListAsync();
	}
}