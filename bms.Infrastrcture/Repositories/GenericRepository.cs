﻿using bms.Application.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using bms.Infrastructure.DbContexts;

namespace bms.Infrastructure.Repositories
{
        public class GenericRepository<T, Tid>(
        BookDbContext context)
        : IGenericRepository<T, Tid>
        where T : class
        {
            public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
            {
                await context.Set<T>().AddAsync(entity, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return entity;
            }

            public async Task DeleteAsync(Tid id, CancellationToken cancellationToken = default)
            {
                var entity = await GetByIdAsync(id, cancellationToken);
                if (entity != null)
                {
                    context.Set<T>().Remove(entity);
                    await context.SaveChangesAsync(cancellationToken); 
                }
            }

            public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
            {
                return await context.Set<T>().AnyAsync(predicate, cancellationToken);
            }

            public async Task<T?> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
            {
                await context.Set<T>().AddRangeAsync(entities, cancellationToken);
                await context.SaveChangesAsync(cancellationToken); 
                return entities.FirstOrDefault();
            }

            public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
            {
                return await context.Set<T>().ToListAsync(cancellationToken);
            }

            public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
            {
                return await context.Set<T>().Where(predicate).ToListAsync(cancellationToken);
            }

            public async Task<T?> GetByIdAsync(Tid id, CancellationToken cancellationToken = default)
            {
                return await context.Set<T>().FindAsync([id], cancellationToken);
            }

            public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
            {
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync(cancellationToken);
            }

            public async Task<T?> GetByIdWithQueryAsync(Func<IQueryable<T>, IQueryable<T>> queryBuilder, CancellationToken cancellationToken = default)
            {
                var query = queryBuilder(context.Set<T>());

                return await query.FirstOrDefaultAsync(cancellationToken);
            }

            public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
            {
                return await context.Set<T>().AnyAsync(predicate, cancellationToken);
            }

            public async Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
            {
                return await context.Set<T>().CountAsync(predicate, cancellationToken);
            }


            public async Task<T?> GetByIdWithIncludeAsync(Tid id, params Expression<Func<T, object>>[] includes)
            {
                var query = context.Set<T>().AsQueryable();

                // Apply includes
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                // Get entity's primary key property name
                var entityType = context.Model.FindEntityType(typeof(T));
                var primaryKey = entityType?.FindPrimaryKey();
                var pkProperty = primaryKey?.Properties.FirstOrDefault();

                if (pkProperty == null)
                {
                    throw new InvalidOperationException($"Entity {typeof(T).Name} has no primary key defined");
                }

                // Build predicate expression
                var parameter = Expression.Parameter(typeof(T), "e");
                var property = Expression.Property(parameter, pkProperty.Name);
                var equals = Expression.Equal(
                    property,
                    Expression.Constant(id)
                );
                var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);

                return await query.FirstOrDefaultAsync(lambda);
            }
        }
}
