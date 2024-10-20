using AutoMapper;
using BusinessLayer.IServices;
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using BusinessLayer.Utilities;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _configuration;
        private readonly IAppointmentRepository _repo;
        private readonly IMapper _mapper;

        public VnPayService(IConfiguration configuration, IAppointmentRepository repo, IMapper mapper)
        {
            _configuration = configuration;
            _repo = repo;
            _mapper = mapper;
        }
        public string CreatePaymentUrl(PaymentInformationModel model, HttpContext context)
        {
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnPayLibrary();
            var urlCallBack = _configuration["PaymentCallBack:ReturnUrl"];

            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            pay.AddRequestData("vnp_Amount", ((int)model.Amount * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
            pay.AddRequestData("vnp_OrderInfo", $"{model.AppointmentId} {model.OrderDescription} {model.Amount}");
            pay.AddRequestData("vnp_OrderType", model.OrderType);
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
            pay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl =
                pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

            return paymentUrl;
        }

        public PaymentResponseModel PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnPayLibrary();

            var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);

            return response;
        }

        public async Task<BaseResponseModel<PaymentResponseModel>> AddPaymentAsync(PaymentResponseModel request)
        {
            var payment = _mapper.Map<Payment>(request);

            var existedAppointment = await _repo.GetById((int)payment.AppointmentId);
            if (existedAppointment == null)
            {
                return new BaseResponseModel<PaymentResponseModel>
                {
                    Code = 404,
                    Message = "Appointment not exists",
                    Data = null
                };
            }
            

            try
            {
                await _repo.AddPaymentAsync(payment);

                existedAppointment.PaymentId = payment.Id;

                if (request.Success=true)
                {
                    existedAppointment.Status = "Paid";
                }

                await _repo.UpdateAsync(existedAppointment);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<PaymentResponseModel>
                {
                    Code = 500,
                    Message = ex.Message,
                    Data = null
                };
            }

            return new BaseResponseModel<PaymentResponseModel>
            {
                Code = 200,
                Message = "Payment Created Success",
                Data = request
            };

        }
    }
}
