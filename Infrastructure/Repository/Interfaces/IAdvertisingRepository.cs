﻿using DomainService.Entity;
using Infrastructure.Common.Interfaces;

namespace Infrastructure.Repository.Interfaces
{
    public interface IAdvertisingRepository : IGenericRepository<Advertising>
    {
        public IQueryable<Advertising> GetAdvertisings();
        public Advertising GetAdvertisingById(int advertisingId);
    }
}
