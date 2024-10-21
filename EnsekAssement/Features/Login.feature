Feature: Login

Scenario Outline: Login to Ensek 
Given user login with '<Username>' '<Password>' '<ExpectedStatusCode>'
Then verify if login is successfull
Examples: 
| Username    | Password       | ExpectedStatusCode |
|             |                | 200                |
| testInvalid | testingInvalid | 401                |

