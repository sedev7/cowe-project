using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using COWE.DomainClasses;
using COWE.DataLayer;

namespace COWE.DataLayer.Models
{ 
    public class CumulativeProbabilityDistributionRepository : ICumulativeProbabilityDistributionRepository
    {
        //CumulativeProbabilityContext context = new CumulativeProbabilityContext();
        //private CumulativeProbabilityContext context;
        private PacketCaptureContext context;
        public CumulativeProbabilityDistributionRepository(UnitOfWorkCumulativeProbability uow)
        {
            context = uow.Context;
        }

        public IQueryable<CumulativeProbabilityDistribution> All
        {
            get { return context.CumulativeProbabilityDistributions; }
        }

        public IQueryable<CumulativeProbabilityDistribution> AllIncluding(params Expression<Func<CumulativeProbabilityDistribution, object>>[] includeProperties)
        {
            IQueryable<CumulativeProbabilityDistribution> query = context.CumulativeProbabilityDistributions;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public CumulativeProbabilityDistribution Find(int id)
        {
            return context.CumulativeProbabilityDistributions.Find(id);
        }

        public void InsertOrUpdate(CumulativeProbabilityDistribution cumulativeprobabilitydistribution)
        {
            if (cumulativeprobabilitydistribution.CumulativeProbabilityDistributionId == default(int)) {
                // New entity
                context.CumulativeProbabilityDistributions.Add(cumulativeprobabilitydistribution);
            } else {
                // Existing entity
                context.Entry(cumulativeprobabilitydistribution).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var cumulativeprobabilitydistribution = context.CumulativeProbabilityDistributions.Find(id);
            context.CumulativeProbabilityDistributions.Remove(cumulativeprobabilitydistribution);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface ICumulativeProbabilityDistributionRepository : IDisposable
    {
        IQueryable<CumulativeProbabilityDistribution> All { get; }
        IQueryable<CumulativeProbabilityDistribution> AllIncluding(params Expression<Func<CumulativeProbabilityDistribution, object>>[] includeProperties);
        CumulativeProbabilityDistribution Find(int id);
        void InsertOrUpdate(CumulativeProbabilityDistribution cumulativeprobabilitydistribution);
        void Delete(int id);
        void Save();
    }
}