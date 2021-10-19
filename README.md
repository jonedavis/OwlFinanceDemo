# Twilio's Owl Finance Demo
### Learn how to build a production quality contact center

Owl Finance is a fully functioning, fictional, banking contact center. Customers can send text messages, create traditional phone calls, in app voice calls, and send in app chat messages through the contact center. All channels of communication are intelligently routed to an agent with the appropriate skills to handle any request. Take a look at the following video for a high level explanation:

[![Owl Finance Demo](https://github.com/jonedavis/OwlFinance/blob/master/images/youtube.png?raw=true)](https://youtu.be/pSVdTt7zJh8)

The following technologies and frameworks are used in Owl Finance:

1. Twilio Services
    1. TaskRouter (Vendor-agnostic API for intelligent task routing)
    2. Programmable SMS (Send notifications of fraudulent transactions)
    3. Programmable Chat (Text based chat service for agent and customer)
    4. Programmable Phone Number (Agent assigned phone number for WebRTC calls)
    5. Programmable Video (In app voice calls using the audio channel of video sdk)
2. Microsoft Azure (Web, Server, and Blob Storage Hosting)
3. Xamarin.iOS (Mobile Client)
    * [Twilio IP Messaging NuGet](https://www.nuget.org/packages/Twilio.IPMessaging.Xamarin/) 
    * [Twilio Rooms SDK for In App Audio NuGet](https://www.nuget.org/packages/Twilio.Rooms.Xamarin/1.0.0-beta3-2)
4. ASP.NET (Web and Server APIs)
5. Auth0 (Authentication Service)
6. AngularJS (Web Client)
7. DocuSign (Push a real-time dynamically generated document from the agent to customer in app)
8. DarkSky (Get weather by zipcode)
9. Zipcode API (Determine geographical location based off zipcode)

## Services and API Keys 
All API keys are stored in `api/website/TwilioOwlFinanceApi/web.config.` 
**Note:** Your API Secrets will only be shown once. Make sure to save them in a secure location when you generate an API key pair.

 Services / Credentials | Description
---------- | -----------
Twilio Account SID | Your main Twilio account identifier - [find it on your dashboard.](https://www.twilio.com/user/account/video)
Twilio Video Configuration SID | Adds video capability to the access token - [generate one here.](https://www.twilio.com/user/account/video/profiles)
Twilio API Key | Used to authenticate - [generate one here.](https://www.twilio.com/user/account/messaging/dev-tools/api-keys)
Twilio API Secret | Used to authenticate - [get one here.](https://www.twilio.com/user/account/messaging/dev-tools/api-keys)
Twilio Phone Number | Used to initiate PSTN call from Agent to Customer - [get one here.](https://www.twilio.com/console/phone-numbers/search)
Twilio Chat Service Instance | SID	Like a database for your Chat data - [generate one here.](https://www.twilio.com/console/chat/services)
Twilio TaskRouter | Used to create cases. Need: WorkspaceSID, OfflineActivitySID, IdleActivitySID, BusyActivitySID, ReservedActivitySID. See **Helpful Links** below for more information. - [generate them here.](https://www.twilio.com/console/taskrouter/dashboard)
HockeyApp | Used for beta app distribution. Email sent when accounts created. (Optional) - [signup here.](https://www.hockeyapp.net/)
Microsoft Azure | Cloud computing and hosting - [signup here.](https://azure.microsoft.com/)
Azure Blog Storage | To store Gravatar images. See **Helpful Links** below for more information.
Auth0 | Login provider - [signup here.](https://auth0.com/)
DocuSign | To send certified documents to Customers - [signup here.](https://www.docusign.com/developer-center)
ZipCode | Zipcode API to determine location - [signup here.](https://www.zipcodeapi.com)
DarkSky | Get Weather based off zipcode - [signup here.](https://api.darksky.net)
Ngrok | You will need 2 ngrok tunnels. - [download here.](https://ngrok.com/download)

**Note:** You will need 2 ngrok tunnels to run Owl Finance locally. Set ngrok tunnels in `web/website/web.config`

## Mobile Service Constants To Update

 Source File| Description
---------- | -----------
`mobile/apps/iOS/OwlFinance/OwlFinance/Helpers/EnvironmentConstants.cs` | Auth0 and Azure Push Notification Hub variables. Azure Push Notifications are optional. 
`mobile/projects/Twilio.OwlFinance.Domain/OwlFinanceUris.cs`| `ApiBaseUri` example: `https://www.owlfinance.io/api/` `SignalRUri` example: `https://agent.owlfinance.io/` All other constants can remain the same.

## Create New Customer & Agent Accounts
For demoing purposes, Customer and Agent accounts are paired together. This guarantees that when multiple people run the demo at the same time, the incoming request goes to the correct account. This feature can be undone in the Twilio TaskRouter console. Removing the Routing Configuration Attributes will open up task assignments to any Agent that is available to accept the task.

Update `CreateAgentAsync(CreateAgentRequest)` in `api/projects/Twilio.OwlFinance.Services/AdminService.cs` if you want to send an email to the supplied email address with login information and a HockeyApp download link if this is used for beta distribution.

 Endpoint | Description
---------- | -----------
`https://financeapi.YOUR-URL-HERE/api/admin/agent` | This endpoint is on the TwilioOwlFinanceAPI project.

### Required Attributes For Account Creation

Attribute | Description
---------- | -----------
`Email` | An email address to link to this account. 2 Accounts will be created: YourEmail+Agent@ and YourEmail+Customer@
`FirstName` | First name
`LastName` | Last name
`Password` | Password shared for both Customer and Agent accounts.
`PhoneNumber` | Phone number for customer for PSTN feature.

Creating an agent with this endpoint and required attributes will create a linked Customer account seeded with transaction data.

## First Run: Seeding The Database
You'll need to seed the database with customer information: addresses, accounts, (fake) credit card numbers, merchants, statements, and transactions. This is all done for you.

### Auth0 ID
You will need to update the Auth0 ID you created for your account. The Seed data can be found in: `api/projects/Twilio.OwlFinance.Infrastructure.DataAccess/Configuration/Database/SeedData/DevelopmentData.cs` You are specifically looking to update every instance of `IdentityID`. There are three customers and three agents provided for you. You only need one customer and one agent to run the app. Moving forward you can use the Account Creation API below to create more customers and agents. 

### TaskRouter Worker SID
A TaskRouter [Worker](https://www.twilio.com/docs/api/taskrouter/workers) is also required. A Worker represent an entity that is able to perform tasks, such as an agent working in a call center, or a salesperson handling leads. Task are how Agent/Customer interactions take place. Go to your [Twilio TaskRouter Workspace](https://www.twilio.com/console/taskrouter/workspaces), select Workers, and click the red + button to create a new Worker. A SID will generate for you after you save the Worker.

## Deploying to Azure
Need help deploying to Azure? Microsoft has great [documentation](https://docs.microsoft.com/en-us/azure/app-service-web/web-sites-dotnet-get-started#create-the-azure-resources) to help you through the process.

## High-level Technical Walk-through
The application is built with a decoupled architecture in mind. Below is an architectural diagram that will jumpstart your understanding of how the different Lego pieces of the application fit together:

![Owl Finance Architecture Diagram](https://github.com/jonedavis/OwlFinance/blob/master/images/owlfinance-arch.jpg?raw=true)

All the interactions for both the mobile and web client go through a set of APIs. The APIs acts as central hub for allowing the necessary service integrations with third party providers such as Twilio, DarkSky API (previously known as Forecast), ZipcodeAPI, DocuSign, and Auth0. 

#### 1. Agent Portal
All web assets are hosted on Azure services and the agent portal is built with AngularJS (1.4.x). When Auth0’s SDK, for JWT based authentication and authorization, is invoked the agent generates a token that is passed to the APIs to get any available data when the agent logs in.
#### 2. Authentication / Authorization
APIs use Auth0’s SDK to validate the JWT token and start serving the requests to the portal.
#### 3. Mobile App
The customer downloads the mobile app and logs in. Auth0’s SDK are again used to validate and generate a JWT token which is sent to the API to be validated and serve any requests. 
#### 4. Agent Sends "Fraudulent" SMS
Customer receives alert with deeplink about a "fraudulent" transaction. The customer clicks the link, the mobile app opens, and the customer attempts to reach out to an agent.
#### 5. Twilio TaskRouter
TaskRouter communicates the request to the Twilio TaskRouter engine. A task is created with using the C# helper libraries. TaskRouter then looks at the request and assigns it to an available agent based on the skills that are defined and required by the customer's request.
#### 6. Agent Portal
The matched agent receives a notification to start interacting with the customer using the TaskRouter's worker.js library.
#### 7. Twilio Programmable Chat
The customer and the agent now communicate via a secure Programmable Chat channel which is used to send text based message back and forth.
#### 8. Twilio Programmable Voice & Video
The agent has the option to chat with the customer in the mobile app or by traditional phone lines. Accepting an in app chat displays a swipeable conversation widget on top of the customer's mobile app.
#### 9. DocuSign
The agent then sends a DocuSign using pub/sub events to the customer. The customer signs the document and the agent can view the signature.
#### 10. Finished
The agent can close the case, continue to conversate with the customer, or view past customer/agent interactions.

## Code Architecture
The entire app, mobile and web client, follow a pattern called “Onion Architecture”. Onion architecture is a software design pattern that “layers” the application using bunch of lego pieces.

![Owl Finance Architecture Diagram](https://github.com/jonedavis/OwlFinance/blob/master/images/onion-arch.png?raw=true)
> http://jeffreypalermo.com/blog/the-onion-architecture-part-1/

Both the mobile and web client are built with maximum reusability and dependency injection in mind. The core part of the architecture contains the domain objects which define the model and contracts of the entire application. There are a bunch of Domain Services, also called infrastructure to take care of HTTP request, database interaction, logging, messaging interaction, and Docusign Services. These layers are then wrapped using a façade called Application Services which is the single point of entry for the mobile and web projects. All of the application follows a strict “new is glue” principal and only feeds dependencies via constructors and dependency injection.
### Helpful Links
1. [Twilio](https://www.twilio.com)
    * [Console](https://www.twilio.com/console)
    * [TaskRouter](https://www.twilio.com/console/taskrouter/dashboard)
      * [Workspaces](https://www.twilio.com/console/taskrouter/workspaces)
        * TaskQueue 
            * https://www.twilio.com/console/taskrouter/workspaces/{WS--YOURID}/taskqueues
        * Workflow
            * https://www.twilio.com/console/taskrouter/workspaces/{WS--YOURID}/workflows
        * Workers
            * https://www.twilio.com/console/taskrouter/workspaces/{WS--YOURID}/workers 
        * Tasks
            * https://www.twilio.com/console/taskrouter/workspaces/{WS15--YOURID}/tasks
    * [Twilio Phone Number](https://www.twilio.com/console/phone-numbers/incoming)
    * [IP Messaging](https://www.twilio.com/console/ip-messaging/dashboard)
    * A Programmable Chat (formerly known as IP Messaging) Service
        * https://www.twilio.com/console/chat/services/{IS--YOURID}
    * [Programmable Video](https://www.twilio.com/console/video/dashboard)
    * [Programmable Voice](https://www.twilio.com/console/voice/dashboard)
    * [Programmable SMS](https://www.twilio.com/console/sms/dashboard)
2. Azure
    * [Azure Portal](https://portal.azure.com/)
    * [App Services](https://portal.azure.com/#blade/HubsExtension/Resources/resourceType/Microsoft.Web%2Fsites)
      * For both APIs and Web Interface
   * [SQL Server Database](https://portal.azure.com/#blade/HubsExtension/Resources/resourceType/Microsoft.Sql%2Fservers%2Fdatabases)
   * [A Storage Account](https://portal.azure.com/#blade/HubsExtension/Resources/resourceType/Microsoft.Storage%2FStorageAccounts)
     * For Assets
3. [Xamarin](https://www.xamarin.com)
   * For iOS Mobile Client
4. [Auth0](https://auth0.com/)
   * A [Client](https://manage.auth0.com/#/clients) with Auth0
5. [DarkSky](https://darksky.net/dev/)
6. [Zipcodeapi](https://www.zipcodeapi.com/)
