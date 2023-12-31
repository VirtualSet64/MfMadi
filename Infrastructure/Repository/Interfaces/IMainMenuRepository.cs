﻿using DomainService.Entity;
using Infrastructure.Common.Interfaces;

namespace Infrastructure.Repository.Interfaces
{
    public interface IMainMenuRepository : IGenericRepository<MainMenu>
    {
        public IQueryable<MainMenu> GetMainMenu();
        public MainMenu GetMainMenuById(int mainMenuId);
    }
}
