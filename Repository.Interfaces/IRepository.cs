﻿using Domain;

namespace Repository.Interfaces
{
    public interface IRepository
    {
      
        void Save<TEntity>(TEntity entity) where TEntity : Entity;
        void Update(long id);
        void Delete(long id);
    }
}