using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SeleniumCallHub
{   
    [Binding]
    class CallHubStep
    {
        CallHubFunctions chf = new CallHubFunctions();

        [Given(@"I navingate to the site")]
        public void GivenINavingateToTheSite()
        {
            chf.GivenINavingateToTheSite();
        }

        [When(@"I select ""(.*)"" subscription")]
        public void WhenISelectSubscription(string subscription)
        {
            chf.WhenISelectSubscription(subscription);
        }

        [Then(@"I verify the pricing for ""(.*)"" subscription")]
        public void ThenIVerifyThePricingForSubscription(string subscription)
        {
            chf.ThenIVerifyThePricing(subscription);
        }

        [Then(@"I close the browser")]
        public void ThenICloseTheBrowser()
        {
            chf.ThenICloseTheBrowser();
        }

    }
}
