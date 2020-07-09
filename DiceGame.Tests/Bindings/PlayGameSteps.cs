using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using FlaUI.UIA3;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using System.Linq;
using System.Threading;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiceGame.Tests.Bindings
{
    [Binding]
    class PlayGameSteps
    {
        private readonly UIA3Automation Automation = new UIA3Automation();
        private Application App;
        private Window MainWindow;

        [Given(@"the dice game application is started")]
        public void GivenTheDiceGameApplicationIsStarted()
        {
            App = Application.Launch("../../../../DiceGame/bin/Debug/netcoreapp3.1/DiceGame.exe");
            App.WaitWhileMainHandleIsMissing(TimeSpan.FromSeconds(1));
            MainWindow = App.GetAllTopLevelWindows(Automation).First();
        }

        [When(@"I select the ""(.*)"" digit and toss")]
        public void WhenISelectTheDigitAndToss(string digitName)
        {
            var radio = MainWindow.FindFirstDescendant(x => x.ByText(digitName)).AsRadioButton();
            radio.IsChecked = true;

            var tossButton = MainWindow.FindFirstDescendant(x => x.ByAutomationId("TossButton")).AsButton();
            tossButton.Invoke();
        }

        [Then(@"the result for player ""(.*)"" should not contain any zeroes")]
        public void ThenTheResultForPlayerShouldNotContainAnyZeroes(string playerName)
        {
            string result = GetPlayerResult(playerName);
            Assert.IsFalse(result.Contains('0'));
        }

        [Then(@"the result for player ""(.*)"" should be (.*) digits long")]
        public void ThenTheResultForPlayerShouldBeDigitsLong(string playerName, int digitCount)
        {
            string result = GetPlayerResult(playerName);
            Assert.AreEqual(digitCount, result.Length);
        }

        [AfterScenario]
        public void CloseDiceGameApplication()
        {
            MainWindow.Close();
        }

        private string GetPlayerResult(string playerName)
        {
            var playersList = MainWindow.FindFirstDescendant(x => x.ByAutomationId("PlayersList")).AsListBox();
            var items = playersList.Items.AsEnumerable();
            var playerItem = items.Where(x => x.Text.StartsWith(playerName)).First();

            var tokens = playerItem.Text.Split(' ');
            string result = tokens[tokens.Length - 2];

            return result;
        }
    }
}
