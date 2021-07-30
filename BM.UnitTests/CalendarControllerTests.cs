using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BM.API.Controllers;
using BM.DataAccess.Entities;
using BM.Domain.Abstract;
using BM.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BM.UnitTests
{
    public class CalendarControllerTests
    {
        private readonly CalendarController _calendarController;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ISlotRepository> _slotRepository;
        private readonly Mock<IMapper> _mapper;

        public CalendarControllerTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _slotRepository = new Mock<ISlotRepository>();
            _mapper = new Mock<IMapper>();
            _mapper.Setup(x => x.Map<SlotModel>(It.IsAny<Slot>()))
                .Returns((Slot source) =>
                    new SlotModel { SlotId = source.SlotId, DateStart = source.DateStart, DateEnd = source.DateEnd }
                );

            _calendarController = new CalendarController(_userRepository.Object, _slotRepository.Object, _mapper.Object);
        }

        [Fact]
        public void GetAvailability_IfResultIsOk_Test()
        {
            //Setup
            var user = new User
            {
                UserId = new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                AvailableSlots = new List<Slot>
                {
                    new Slot
                    {
                        SlotId = new Guid("40ff5488-fdab-45b5-bc3a-14302d59869a"),
                        DateStart = DateTime.Now,
                        DateEnd = DateTime.Now.AddHours(1)
                    }
                }
            };
            _userRepository.Setup(x => x.GetUser(user.UserId)).Returns(user);
            _slotRepository.Setup(x => x.GetAvailability(user.UserId, new List<Guid>())).Returns(user.AvailableSlots.AsQueryable());

            //Act
            var okResult = _calendarController.GetAvailability(user.UserId, new List<Guid>());
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        /// <summary>
        /// TODO: to be implemented
        /// </summary>
        [Fact]
        public void SetAvailabilityTest1()
        {
            throw new NotImplementedException();
        }
    }
}
