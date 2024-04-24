using IntegratedSystems.Domain.Identity_Models;
using IntegratedSystems.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Delete(IntegratedSystemsUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Users.Remove(entity);
            context.SaveChanges();
        }

        public IntegratedSystemsUser Get(string id)
        {
            var strguid = id.ToString();
            return context.Users.FirstOrDefault(s => s.Id == strguid);
        }

        public IEnumerable<IntegratedSystemsUser> GetAll()
        {
            return context.Users.AsEnumerable();
        }

        public void Insert(IntegratedSystemsUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Users.Add(entity);
            context.SaveChanges();
        }

        public void Update(IntegratedSystemsUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Users.Update(entity);
            context.SaveChanges();
        }
    }
}
