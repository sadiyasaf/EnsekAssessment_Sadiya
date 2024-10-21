Feature: Buy 

Background: To get bearer token
Given user login with '' '' '200'

Scenario Outline: Buy the quantity of each fuel
	Given Retrieve energyIds '<EnergyId>' and quantity '<Quantity>' for '<Fuel>' and buy each fuel and verify '<ExpectedStatusCode>' with '<Comments>'
	Then verify the orders list contains the purchased and verify the status code '<ExpectedStatusCode>' with '<Comments>'
	Examples: 
	| EnergyId | Quantity | Fuel     | ExpectedStatusCode | Comments                                                            |
	| 1        | 5        | electric | 200                | Successful response; Buy fuel                                       |
	| 1        | 2        | gas      | 200                | Successful response; Buy fuel    |
	| 1        | 6        | nuclear  | 200                | Successful response; Buy fuel |
	| 1        | 1        | oil      | 200                | Successful response; Buy fuel |
	| 9999     | 1        | Electric | 400                | Bad response; Buy fuel                                              |