using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BM.DataAccess.Entities;
using BM.Domain.Abstract;
using BM.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BM.API.Controllers
{
    /// <summary>
    /// Calendar API controller
    /// </summary>
    [ApiController]
    [Route("api/calendar")]
    public class CalendarController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ISlotRepository _slotRepository;
        private readonly IMapper _mapper;

        public CalendarController(IUserRepository userRepository, ISlotRepository slotRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _slotRepository = slotRepository ?? throw new ArgumentNullException(nameof(slotRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get calendar slot availability
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="interviewers"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<SlotModel>> GetAvailability(Guid userId, IEnumerable<Guid> interviewers)
        {
            var user = _userRepository.GetUser(userId);
            if (user == null)
            {
                return NotFound($"User: {userId}");
            }

            var slotsAvailable = _slotRepository.GetAvailability(userId, interviewers);

            if (!slotsAvailable.Any())
            {
                return NotFound($"Slots not found");
            }

            return Ok(_mapper.Map<SlotModel>(slotsAvailable.ToList()));
        }

        /// <summary>
        /// Set calendar slot availability
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<UserModel> SetAvailability(SlotModel slot)
        {
            var user = _userRepository.GetUser(slot.User.UserId);
            if (user == null)
            {
                return NotFound($"User: {slot.User.UserId}");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(_mapper.Map<UserModel>(user));
            }

            var slotEntitie = _mapper.Map<Slot>(slot);

            _slotRepository.Save(slotEntitie);
            return Ok(_mapper.Map<UserModel>(user));
        }
    }
}