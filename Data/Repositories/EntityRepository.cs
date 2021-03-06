﻿using DrinkAndGo.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DrinkAndGo.Data.Repositories
{
    public class EntityRepository<TEntity, TKeyType> : IDisposable, IRepository<TEntity, TKeyType> where TEntity : class where TKeyType : struct
    {
        protected readonly DrinkDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public EntityRepository(DrinkDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public async virtual Task<TEntity> Add(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            Save();
            return entity;
        }

        /// <summary>
        /// This is how we can use Store proc in EF.
        /// </summary>
        /// <param name="command">usp_InsertDrink @p0, @p1 </param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Add(string command, SqlParameter[] param)
        {
            return Context.Database.ExecuteSqlCommand(command, param);
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public TEntity Get(TKeyType id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public void Remove(TEntity entity)
        {
            Context.Remove(entity);
            Save();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.RemoveRange(entities);
            Save();
        }

        public async void Save()
        {
            await Context.SaveChangesAsync();
        }

        public Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.SingleOrDefaultAsync(predicate);
        }

        public void Update(TEntity entity)
        {
            Context.Update(entity);
            Save();
        }
    }
}
