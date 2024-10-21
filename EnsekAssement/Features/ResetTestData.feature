Feature: Reset test data

Background: To get bearer token
Given user login with '' '' '200'
@mytag
Scenario Outline: Reset test data
	Given reset the test data and verify '<ExpectedStatusCode>' and '<Comments>'
	Then validate the response status
	Examples: 
	| ExpectedStatusCode | Comments                                                            |
	| 200                | Successfull response; test data reset successfull                   |
	| 401                | Request unauthorized; invalid credentials or missing authentication |
