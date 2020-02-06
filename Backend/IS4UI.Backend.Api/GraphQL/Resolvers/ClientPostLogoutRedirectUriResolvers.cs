using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using HotChocolate;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;


public class ClientPostLogoutRedirectUriResolvers
{

    private readonly ApplicationDbContext db;

    public ClientPostLogoutRedirectUriResolvers(ApplicationDbContext db)
    {
        this.db = db;
    }

    public List<ClientPostLogoutRedirectUri> GetClientPostLogoutRedirectUris()
    {
        return db.ClientPostLogoutRedirectUris.ToList();
    }

    public List<ClientPostLogoutRedirectUri> GetClientPostLogoutRedirectUrisByClientId(int clientId)
    {
        return db.ClientPostLogoutRedirectUris.Where(x => x.ClientId == clientId).ToList();
    }

    public List<ClientPostLogoutRedirectUri> GetClientPostLogoutRedirectUrisByClientIdFromParent([Parent]Client client)
    {
        return db.ClientPostLogoutRedirectUris.Where(x => x.ClientId == client.Id).ToList();
    }

    public ClientPostLogoutRedirectUri GetClientPostLogoutRedirectUri(int id)
    {
        return db.ClientPostLogoutRedirectUris.SingleOrDefault(x => x.Id == id);
    }

}