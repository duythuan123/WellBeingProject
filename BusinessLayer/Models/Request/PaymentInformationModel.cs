using System.ComponentModel;

namespace BusinessLayer.Models.Request
{
    public class PaymentInformationModel
    {
        [DefaultValue("Appointment")]
        public string OrderType { get; set; }
        public double Amount { get; set; }
        public string OrderDescription { get; set; }
        public string AppointmentId { get; set; }
    }
}
