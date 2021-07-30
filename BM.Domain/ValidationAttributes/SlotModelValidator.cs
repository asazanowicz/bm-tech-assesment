using System;
using System.Linq;
using BM.DataAccess.DbContexts;
using BM.Domain.Models;
using BM.Domain.Resources;
using FluentValidation;

namespace BM.Domain.ValidationAttributes
{
    /// <summary>
    /// Slot model validator
    /// </summary>
    public class SlotModelValidator : AbstractValidator<SlotModel>
    {
        protected readonly BMContext _context;

        public SlotModelValidator(BMContext context)
        {
            _context = context;
            RuleFor(x => x.SlotId).Must(this.BeUnique).WithMessage(String.Format(Validation.BeUnique, typeof(SlotModel).Name));
            RuleFor(x => x.DateStart).Must(this.BeValidSlot).WithMessage(Validation.BeValidSlot);
        }

        /// <summary>
        /// The slot must be unique
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool BeUnique(SlotModel model, Guid id)
        {
            return _context.Slots.FirstOrDefault(x => x.SlotId == id) == null;
        }

        /// <summary>
        /// The slot must be in working day
        /// The start date must be earlier the en date
        /// THe slot must start with the beginning of hour
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="dateStart"></param>
        /// <returns></returns>
        private bool BeValidSlot(SlotModel slot, DateTime dateStart)
        {
            return slot.DateStart < slot.DateEnd 
                && TimeSpan.Parse(Validation.WorkingDayStart) < slot.DateEnd.TimeOfDay
                && TimeSpan.Parse(Validation.WorkingDayEnd) > slot.DateStart.TimeOfDay
                && (slot.DateStart.Minute != 0 || slot.DateEnd.Minute != 0);
        }

    }
}
