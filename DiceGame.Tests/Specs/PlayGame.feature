﻿Feature: Play Game
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

Scenario: Radio buttons disabled correctly
	When I select the "Zehner" digit and toss
	Then the "Zehner" radio should be disabled

Scenario: Entire game round
	When I select the "Hunderter" digit and toss
	And I select the "Einer" digit and toss
	And I select the "Zehner" digit and toss
	And I select the "Zehner" digit and toss
	And I select the "Hunderter" digit and toss
	And I select the "Einer" digit and toss
	And I select the "Zehner" digit and toss
	And I select the "Einer" digit and toss
	And I select the "Hunderter" digit and toss
	Then the player with the highest score should be declared winner
