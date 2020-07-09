using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using FlaUI.UIA3;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using System.Linq;
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
            Assert.IsFalse(result.Contains('0'), "The result should not contain any zeroes");
        }

        [Then(@"the result for player ""(.*)"" should be (.*) digits long")]
        public void ThenTheResultForPlayerShouldBeDigitsLong(string playerName, int digitCount)
        {
            string result = GetPlayerResult(playerName);
            Assert.AreEqual(digitCount, result.Length, $"The result should be {digitCount} digits long");
        }

        [Then(@"the player with the highest score should be declared winner")]
        public void ThenThePlayerWithTheHighestScoreShouldBeDeclaredWinner()
        {
            var results = GetAllResults();

            string winner = "";
            int maxResult = 0;
            foreach(var result in results)
            {
                if (result.Value > maxResult)
                {
                    maxResult = result.Value;
                    winner = result.Key;
                }           
            }

            var mainLabel = MainWindow.FindFirstDescendant(x => x.ByAutomationId("MainLabel")).AsLabel();
            Assert.AreEqual($"{winner} hat gewonnen!", mainLabel.Text, $"The winner should be {winner}");
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

        private Dictionary<string, int> GetAllResults()
        {
            var results = new Dictionary<string, int>();

            var playersList = MainWindow.FindFirstDescendant(x => x.ByAutomationId("PlayersList")).AsListBox();
            var items = playersList.Items.AsEnumerable();
            foreach(var item in items)
            {
                var tokens = item.Text.Split(' ');
                string playerName = tokens[0];
                try
                {
                    int result = int.Parse(tokens[tokens.Length - 2]);
                    results.Add(playerName, result);
                }
                catch (FormatException)
                {
                    Assert.Fail("Expected a number as result, but didn't get one");
                }
            }

            return results;
        }
    }
}
