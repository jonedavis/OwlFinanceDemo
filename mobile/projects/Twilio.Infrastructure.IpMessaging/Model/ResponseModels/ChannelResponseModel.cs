// ReSharper disable InconsistentNaming
namespace Twilio.Infrastructure.IpMessaging.Model.ResponseModels
{
    public class ChannelResponseModel
    {
        public string sid { get; set; }
        public string account_sid { get; set; }
        public string service_sid { get; set; }
        public string friendly_name { get; set; }
        public string unique_name { get; set; }
        public string attributes { get; set; }
        public string type { get; set; }
        public string date_created { get; set; }
        public string date_updated { get; set; }
        public string created_by { get; set; }
        public string url { get; set; }
        public Links links { get; set; }
    }
}
