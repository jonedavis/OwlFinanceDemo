namespace OwlFinance.Helpers
{
	public class EnvironmentConstants
	{
		public static readonly string Auth0Login = "owl-finance.auth0.com";
		public static readonly string Auth0Password = "seYyeCbOOdvMKwl9n9TdjtjQp1lBYIHM";
		public static readonly string Auth0Secret = "h6SCPOvyu0TbPXT6c7Kx3nIVRVrL4aJ5BGiF7nFqsP2YqSfsQW1wYkOpTWxEi6yS";
		public static readonly string AzureNotificationHub = "Owlfinance";
		public static readonly string AzureNamespace = "OwlFinance";
		public static readonly string AzurePushAccess = "kaxHgOOgbn9cY0lyc/r1NQ/PvrvReE5FXSop47eUp0M=";
		public static readonly string AzureListenAccessEndPoint = "sb://" + EnvironmentConstants.AzureNotificationHub + ".servicebus.windows.net/";
	}
}