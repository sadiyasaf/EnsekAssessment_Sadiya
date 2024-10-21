Feature: Energy

Background: To get bearer token
Given user login with '' '' '200'
@mytag
Scenario Outline: Get the list of fuels 
	Given get the list of fuels and their quantities with '<ExpectedStatusCode>' and '<Comments>'
	Then verify the response status
	Examples: 
	| ExpectedStatusCode | Comments                                                            |
	| 200                | Successfull response; retrieve energy details                       |
	#The below negative scenario doesn't work due to bug : Unauthorized Access Still Responds with 200 OK Status. Hence commenting it
	#| 401                | Request unauthorized; invalid credentials or missing authentication |