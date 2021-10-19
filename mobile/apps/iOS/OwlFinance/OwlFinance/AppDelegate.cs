using System;
using Autofac;
using Foundation;
using HockeyApp.iOS;
using OwlFinance.Helpers;
using OwlFinance.ViewControllers;
using Twilio.OwlFinance.Domain.Interfaces;
using UIKit;
using Xamarin.SWRevealViewController;

namespace OwlFinance
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window { get; set; }
		public ISettings Settings => Application.Container.Resolve<ISettings>();
		public UINavigationController NavController { get; private set; }
		public UIStoryboard MainStoryboard
		{
			get { return UIStoryboard.FromName("Main", NSBundle.MainBundle); }
		}

		// Creates an instance of viewControllerName from storyboard
		public UIViewController GetViewController(UIStoryboard storyboard, string viewControllerName)
		{
			return storyboard.InstantiateViewController(viewControllerName);
		}
				
		// Sets the RootViewController of the Apps main window with an option for animation.
		public void SetRootViewController(UIViewController rootViewController, bool animate)
		{
			if (animate)
			{
				var transitionType = UIViewAnimationOptions.TransitionFlipFromRight;
				NavController = new UINavigationController(rootViewController);
				NavController.NavigationBar.BarStyle = UIBarStyle.Default;
				Window.RootViewController = NavController;

				UIView.Transition(
					Window,
					0.5,
					transitionType,
					() => Window.RootViewController = rootViewController,
					null);
			}
			else
			{
				NavController = new UINavigationController(rootViewController);
				Window.RootViewController = NavController;
			}
		}

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			UIApplication.SharedApplication.IdleTimerDisabled = true;

			var manager = BITHockeyManager.SharedHockeyManager;
			// Configure it to use our APP_ID
			manager.Configure("df393da6b2f245378904e99fcf642518");

			// Start the manager
			manager.StartManager();

			if (!Settings.IsAuthenticated)
			{
				// User needs to log in, so show the Login View Controlller
				var loginController = GetViewController(MainStoryboard, "LoginController") as LoginViewController;
				SetRootViewController(loginController, false);
			}

			SetupGlobalAppearances();

			return true;
		}

		static void SetupGlobalAppearances()
		{
			// NavigationBar
			UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;
			UINavigationBar.Appearance.BackgroundColor = Colors.PureBlueColor;
			UINavigationBar.Appearance.BarTintColor = Colors.PureBlueColor;
			UINavigationBar.Appearance.TintColor = Colors.WhiteColor;
			UINavigationBar.Appearance.SetTitleTextAttributes(
				new UITextAttributes 
				{ 
					Font = UIFont.FromName("Lato-Regular", 18f), 
					TextColor = Colors.WhiteColor 
				});
			// NavigationBar Buttons 
			UIBarButtonItem.Appearance.SetTitleTextAttributes(
				new UITextAttributes 
				{ 
					Font = UIFont.FromName("Lato-Bold", 18f), 
					TextColor = Colors.WhiteColor 
				}, UIControlState.Normal);

			// TabBar
			UITabBarItem.Appearance.SetTitleTextAttributes(
				new UITextAttributes 
				{ 
					Font = UIFont.FromName("Lato-Bold", 18f) 
				}, UIControlState.Normal);
		}

		void AuthController_OnLoginSuccess(object sender, EventArgs e)
		{
			var dashboardController = GetViewController(MainStoryboard, "DashboardController");
			SetRootViewController(dashboardController, true);
		}

		public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
			var rurl = new Rivets.AppLinkUrl(url.ToString());
			var id = string.Empty;

			if (rurl.InputQueryParameters.ContainsKey("id"))
			{
				id = rurl.InputQueryParameters["id"];
			}

			if (rurl.InputUrl.Host.Equals("transactions") && !string.IsNullOrEmpty(id))
			{
				var transactionDetailViewController = GetViewController(MainStoryboard, "DashboardController") as DashboardViewController;
				transactionDetailViewController.SelectedTransactionId = Convert.ToInt32(id);
				var frontNavigationController = new UINavigationController(transactionDetailViewController);

				var rearViewController = GetViewController(MainStoryboard, "MenuController") as MenuViewController;
				var mainRevealController = new SWRevealViewController();

				mainRevealController.RearViewController = rearViewController;
				mainRevealController.FrontViewController = frontNavigationController;
				Window.RootViewController = mainRevealController;
				Window.MakeKeyAndVisible();

				return true;
			}

			NavController.PopToRootViewController(true);

			return true;
		}
	}
}
