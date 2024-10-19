
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.IServices
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);

        Task AddPaymentAsync(PaymentResponseModel request);
    }
}