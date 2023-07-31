using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using TaskPlannerMetrum.Model.Base;
using TaskPlannerMetrum.Model.Context;
using System.Linq;
using TaskPlannerMetrum.Data.VO;
using TaskPlannerMetrum.Model;
using TaskPlannerMetrum.Data.Converter.Implementations;

namespace TaskPlannerMetrum.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private MSSQLContext _context;

        private readonly UserConverter _converter;

        private DbSet<T> dataset;
        public GenericRepository(MSSQLContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }

        public List<T> FindAll()
        {
            
            return  dataset.ToList();
            
        }

        public T FindByID(int id)
        {
            return dataset.SingleOrDefault(p => p.Id.Equals(id));
        }

        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private UserVO ConvertUser(TeamUsers User)
        {
            UserVO user = new UserVO();
            user.CreationDate =  User.CreationDate;
            user.UserName = User.UserName;
            user.UserEmail = User.UserEmail;
            user.RefreshTokenExpiryTime = User.RefreshTokenExpiryTime;
            user.PhoneNumber = User.PhoneNumber;
            user.UserName = User.UserName;
            user.DepartmentId = User.DepartmentId;
            user.WorkspaceID= User.WorkspaceID;
            user.RefreshToken = User.RefreshToken;
            user.Id= User.Id;
            user.DepartamentName = User.DepartamentName;
            user.Password= User.Password;
            user.RefreshTokenExpiryTime = User.RefreshTokenExpiryTime;
            return user;
        }
        




        public T Update(T item)
        {
            var result = dataset.SingleOrDefault(p => p.Id.Equals(item.Id));
            
            
            
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return null;
            }
        }
        

        public void Delete(int id)
        {
            var result = dataset.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    dataset.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Exists(int id)
        {
            return dataset.Any(p => p.Id.Equals(id));
        }
    }
}
