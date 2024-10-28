using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoiFish_Core.Domain.Content;
using KoiFish_Core.SeedWorks;

namespace KoiFish_Core.Repositories
{
    public interface IKoiFishColorRepository :  IRepositoryBase<FishColor, Guid>
    {
         Task<int> SaveChangeASync();
    }
}