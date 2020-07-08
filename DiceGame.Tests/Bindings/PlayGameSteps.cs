using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using FlaUI.UIA3;

namespace DiceGame.Tests.Bindings
{
    [Binding]
    class PlayGameSteps
    {
        [Given(@"the dice game application is started")]
        public void GivenTheDiceGameApplicationIsStarted()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I select the ""(.*)"" digit and toss")]
        public void WhenISelectTheDigitAndToss(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the result for player ""(.*)"" should not contain any zeroes")]
        public void ThenTheResultForPlayerShouldNotContainAnyZeroes(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the result for player ""(.*)"" should be (.*) digits long")]
        public void ThenTheResultForPlayerShouldBeDigitsLong(string p0, int p1)
        {
            ScenarioContext.Current.Pending();
        }

    }
}
