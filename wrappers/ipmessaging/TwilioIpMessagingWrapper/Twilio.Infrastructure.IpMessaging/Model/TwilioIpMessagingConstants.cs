namespace Twilio.Infrastructure.IpMessaging.Model
{
    public class TwilioIpMessagingConstants
    {
        public static readonly string InstanceSid = "";
        public static readonly string AccountSid = "";
        public static readonly string AccountAuthToken = "";
        public class Uris
        {
            public static readonly string BaseApiUri = "https://ip-messaging.twilio.com/v1/";

            public static readonly string GetChannels = "Services/{0}/Channels";
            public static readonly string GetChannel = "Services/{0}/Channels/{1}";
            public static readonly string GetChannelMembers = "Services/{0}/Channels/{1}/Members";
            public static readonly string GetChannelMessages = "Services/{0}/Channels/{1}/Messages";
        }
    }
}
