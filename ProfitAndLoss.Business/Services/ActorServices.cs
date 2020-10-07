using ProfitAndLoss.Business.Models;
using ProfitAndLoss.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfitAndLoss.Business.Services
{
    public interface IActorServices : IBaseRepository<Actor, int>
    {
        Actor Create(RequestCreateActorModel entity);
        IEnumerable<Actor> GetActors();
        Actor UpdateActive(int id);
        Actor Edit(Actor entity);
        bool Exist(Actor entity);
        bool Validate(Actor entity);
    }
    public class ActorServices : BaseRepository<Actor, int>, IActorServices
    {
        public ActorServices(DataContext context) : base(context)
        {

        }
        public IQueryable<Actor> Actors
        {
            get
            {
                return Entity();
            }
        }
        public void PrepareCreate(Actor entity)
        {
           
        }
        public void PrepareUpdate(Actor entity)
        {
        }
        /// <summary>
        /// Create new Actor - save to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Actor</returns>
        public Actor Create(RequestCreateActorModel model)
        {
            var entity = model.ToEntity();
            PrepareCreate(entity);
            return Add(entity);
        }
        /// <summary>
        /// GetAll Actor
        /// </summary>
        /// <param name="model"></param>
        /// <returns>IEnumerable<Actor></returns>
        public IEnumerable<Actor> GetActors()
        {
            return GetAll();
        }

        public Actor UpdateActive(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                PrepareUpdate(entity);
                Update(entity);
            }
            return entity;
        }



        public bool Exist(Actor entity)
        {
            return Actors.Any(e => e.Id != entity.Id);
        }

        public Actor Edit(Actor entity)
        {
            PrepareUpdate(entity);
            Update(entity);
            return entity;
        }

        public bool Validate(Actor entity)
        {
            return false;
        }

        public IEnumerable<Actor> GetDisplaceActors()
        {
            return Actors.OrderByDescending(e => e.FirstName);
            
        }

    }
}
