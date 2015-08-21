// File: IRepository.cs in
// PatternsFun by Serghei Adam 
// Created 21 08 2015 
// Edited 21 08 2015

using Domain;

namespace Repository.Interfaces
{
    public interface IRepository
    {
        void Save<TEntity>(TEntity entity) where TEntity : Entity;
    }
}