﻿using DomainService.DbService;
using DomainService.Entity;
using Infrastructure.Common;
using Infrastructure.Repository.Interfaces;

namespace Infrastructure.Repository
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<Contact> GetContacts()
        {
            return Get().Where(x => x.IsDeleted != true);
        }
    }
}
