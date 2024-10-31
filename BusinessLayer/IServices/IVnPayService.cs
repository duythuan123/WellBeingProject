
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.IServices
{
    public interface IVnPayService
    {
        Task<string> CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        Task<PaymentResponseModel> PaymentExecute(IQueryCollection collections);
        Task<BaseResponseModel<PaymentResponseModel>> AddPaymentAsync(PaymentResponseModel request);
    }
}