using System;
using System.Collections.Generic;
using System.Linq;
using BM.DataAccess.DbContexts;
using BM.DataAccess.Entities;
using BM.Domain.Abstract;
using Microsoft.Extensions.Logging;

namespace BM.Domain.Concrete
{
    /// <summary>
    /// Slot repository
    /// </summary>
    public class SlotRepository : BaseRepository, ISlotRepository
    {
        public SlotRepository(BMContext context, ILogger<SlotRepository> logger) : base(context, logger)
        {

        }

        /// <summary>
        /// Saving availability slot
        /// </summary>
        /// <param name="slot"></param>
        public void Save(Slot slot)
        {
            if (slot == null)
            {
                _logger.LogInformation("No slots to save");
            }

            using var transaction = this._context.Database.BeginTransaction();
            try
            {
                slot.SlotId = Guid.NewGuid();
                _context.Add(slot);
                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                _logger.LogError(e.Message);
            }
        }

        /// <summary>
        /// Finding available slots for candidate and all interviewers
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="interviewerIds"></param>
        /// <returns></returns>
        public IQueryable<Slot> GetAvailability(Guid userId, IEnumerable<Guid> interviewerIds)
        {
            var userSlots = _context.Slots.Where(x => x.UserId == userId);
            var interviewerSlots = _context.Slots.Where(x => interviewerIds.Contains(x.UserId));

            var res = from iSlot in interviewerSlots
                join uSlot in userSlots on new { iSlot.DateStart, iSlot.DateEnd } equals new { uSlot.DateStart, uSlot.DateEnd }
                select iSlot;

            return res;
        }
    }
}
