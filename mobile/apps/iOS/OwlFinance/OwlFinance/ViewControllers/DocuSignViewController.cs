using System;
using Autofac;
using Foundation;
using Twilio.OwlFinance.Domain.Interfaces.Services;
using UIKit;

namespace OwlFinance.ViewControllers
{
    public partial class DocuSignViewController : UIViewController
    {
		private static ISignalRService SignalRService => Application.Container.Resolve<ISignalRService>();
		public static bool IsVisible { get; private set; }

		public string DocuSignUrl { get; set; }
        
		public DocuSignViewController (IntPtr handle) 
			: base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			IsVisible = true;
			DoneSigningButton.Clicked += DoneSigningButton_Clicked;

			if (!string.IsNullOrWhiteSpace(DocuSignUrl))
			{
				SignatureWebView.LoadRequest(new NSUrlRequest(new NSUrl(DocuSignUrl)));
			}
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
			IsVisible = false;
		}

		private async void DoneSigningButton_Clicked(object sender, EventArgs e)
		{
			await SignalRService.SendAsync("SIGNED");
			
			DismissModalViewController(true);
		}

		private void CloseButton_TouchUpInside(object sender, EventArgs e)
		{
			DismissModalViewController(true);
		}
	}
}