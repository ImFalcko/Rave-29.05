using Rave.Services;
using Rave.Views;
using System;
using Xamarin.Forms;
using Xamarin;
using SQLitePCL;
using System.IO;
using Xamarin.Forms.Maps;
using Rave.Models;

namespace Rave
{
    public partial class App : Application
    {
        User User = new User();
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt+QHJqVk1mQ1NHaV1CX2BZd1l2R2lddk4BCV5EYF5SRHNdRl1qSXpRf0JkUXo=;Mgo+DSMBPh8sVXJ1S0R+X1pCaV1BQmFJfFBmTGlZfVR0fUU3HVdTRHRcQlhhTX5adkFhUHZdc3E=;ORg4AjUWIQA/Gnt2VFhiQlJPcEBAXnxLflF1VWpTf1x6cFxWESFaRnZdQV1mS3pTf0VnWn9eeHZS;MjE3MTQ5N0AzMjMxMmUzMjJlMzNUSFJ0K25aZ0h6NnR3aW0yeCtSelhMVTZ4WE1oeXRwNGEyaXZWT1FNaGRjPQ==;MjE3MTQ5OEAzMjMxMmUzMjJlMzNsMVNUT2xKYWlSWlRqMXNvWE5MSXNPaFdjMENUbjFPUm03K1hTOXpVR3Y0PQ==;NRAiBiAaIQQuGjN/V0d+Xk9HfVldX2BWfFN0RnNQdV5yflFPcC0sT3RfQF5jTHxXdkxjXXxedX1XQQ==;MjE3MTUwMEAzMjMxMmUzMjJlMzNGK1QvMTZCSTRFWDM1aDVwYWYyZUJEUm9pdlQrNWZZbHBlY2xEUjNHNjl3PQ==;MjE3MTUwMUAzMjMxMmUzMjJlMzNXT0EyWFRVVk1vb2YyZ3ByeFBsYnRrKzQ1eVJURXhzeHc0c2NQZk42bXRNPQ==;Mgo+DSMBMAY9C3t2VFhiQlJPcEBAXnxLflF1VWpTf1x6cFxWESFaRnZdQV1mS3pTf0VnWn5feHZQ;MjE3MTUwM0AzMjMxMmUzMjJlMzNpWEl6Y0k3RWpvUC8yQ2l5OHdQOFNyZWxEeC9vUzkrZkdsUmJ4c3Q0RnRjPQ==;MjE3MTUwNEAzMjMxMmUzMjJlMzNQNXdyTDIzZU1SQTlaNzIzYlJpNW81ajN2UXFrTWpMdEVyV0IxbzZlYlEwPQ==;MjE3MTUwNUAzMjMxMmUzMjJlMzNGK1QvMTZCSTRFWDM1aDVwYWYyZUJEUm9pdlQrNWZZbHBlY2xEUjNHNjl3PQ==");
            InitializeComponent();
            SqLiteService sqLiteService = new SqLiteService();
            sqLiteService.CreateDatabase();

            DependencyService.Register<MockDataStore2>();
            DependencyService.Register<MockDataStore>();
            MainPage = new LoginPage();
        }
          
            
            protected override void OnStart()
            {
                // Handle when your app starts
            }

            protected override void OnSleep()
            {
                // Handle when your app sleeps
            }

            protected override void OnResume()
            {
                // Handle when your app resumes
            }
        }
    }
