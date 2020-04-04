using System.Linq;
using IS4UI.Backend.Data;
using IS4UI.Backend.Data.Entities;
using HotChocolate;
using HotChocolate.Resolvers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class DeviceCodeResolvers
{
   public IQueryable<DeviceCode> GetDeviceCodes([Service] ApplicationDbContext db)
   {
      return db.DeviceCodes;
   }

   public async Task<DeviceCode> GetDeviceCode([Service] ApplicationDbContext db, IResolverContext context, string userCode)
   {
       var dataLoader = context.BatchDataLoader<string, DeviceCode>(nameof(GetDeviceCode), async userCodes => await db.DeviceCodes.Where(c => userCodes.Contains(c.UserCode)).ToDictionaryAsync(x => x.UserCode, x => x));
       return await dataLoader.LoadAsync(userCode, context.RequestAborted);
   }
}


