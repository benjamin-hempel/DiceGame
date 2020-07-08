Feature: PlayGame
	In order to see who travels the farthest
	As a traveller
	I want to play the dice game.

Background:
	Given the dice game application is started

Scenario: No zeroes and right amount of digits
	When I select the "Zehner" digit and toss
	And I select the "Hunderter" digit and toss
	And I select the "Einer" digit and toss
	Then the result for player "Jim" should not contain any zeroes
	And the result for player "Jim" should be 3 digits long

