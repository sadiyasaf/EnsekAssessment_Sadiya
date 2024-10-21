Feature: Order

Background: To get bearer token
Given user login with '' '' '200'
#
Scenario: Verify the orders created before current date
Given Retrieve the orders from orders endpoint and return the number of orders created before current date
#Examples: 
# | StatusCode | Comments |
# |   200         |   Order id avalable       |

