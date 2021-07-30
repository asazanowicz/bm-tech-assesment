using System;
using System.Collections.Generic;
using System.Linq;
using BM.DataAccess.Entities;

namespace BM.Domain.Abstract
{
    public interface ISlotRepository
    {
        void Save(Slot slots);

        IQueryable<Slot> GetAvailability(Guid userId, IEnumerable<Guid> interviewerIds);
    }
}
