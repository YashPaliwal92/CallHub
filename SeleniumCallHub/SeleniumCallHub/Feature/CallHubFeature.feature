Feature: CallHubFeature
	Simple automation for verifying the subscription amount on UI with respect to CallHubPricing.csv file


Scenario: Verify Subscription amount
	Given I navingate to the site
	When I select "Monthly" subscription
	Then I verify the pricing for "Monthly" subscription
	When I select "Quarterly" subscription
	Then I verify the pricing for "Quarterly" subscription
	When I select "Half-yearly" subscription
	Then I verify the pricing for "Half-yearly" subscription
	When I select "Yearly" subscription
	Then I verify the pricing for "Yearly" subscription
	And I close the browser