namespace Twilio.Infrastructure.IpMessaging.Model
{
    public class TwilioIpMessagingConstants
    {
#if DEBUG
		public static readonly string InstanceSid = "";
		public static readonly string AccountSid = "";
		public static readonly string AccountAuthToken = "";
#else
        public static readonly string InstanceSid = "";
        public static readonly string AccountSid = "";
        public static readonly string AccountAuthToken = "";
#endif
		public class Uris
        {
            public static readonly string BaseApiUri = "";

            public static readonly string GetChannels = "Services/{0}/Channels";
            public static readonly string GetChannel = "Services/{0}/Channels/{1}";
            public static readonly string GetChannelMembers = "Services/{0}/Channels/{1}/Members";
            public static readonly string GetChannelMessages = "Services/{0}/Channels/{1}/Messages";
        }
    }
}