using BusinessLayer.IServices;
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using BusinessLayer.Utilities;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepositoy _repo;
        private readonly IUserRepository _userRepository;
        private readonly IPsychiatristRepository _psychiatristRepository;

        public BookingService(IBookingRepositoy repo, IUserRepository userRepository, IPsychiatristRepository psychiatristRepository)
        {
            _repo = repo;
            _userRepository = userRepository;
            _psychiatristRepository = psychiatristRepository;
        }
        public async Task<BaseResponseModel> BookAppointmentAsync(int userId, BookingRequestModel request)
        {
            var user = await _userRepository.GetUserById(userId);
            var psychiatrist = await _psychiatristRepository.GetPsychiatristById(request.PsychiatristId);

            if (user == null || psychiatrist == null)
            {
                return new BaseResponseModel
                {
                    Code = 404,
                    Message = "User or Psychiatrist not found",
                };
            }

            var booking = new Booking
            {
                BookingDate = DateTime.UtcNow,
                AppointmentDate = request.AppointmentDate,
                Status = "Pending",
                UserId = userId,
                PsychiatristId = request.PsychiatristId,
                User = user,
                Psychiatrist = psychiatrist
            };

            await _repo.AddBooking(booking);

            return new BaseResponseModel
            {
                Code = 200,
                Message = "Appointment booked successfully",
            };
        }
    }
}
