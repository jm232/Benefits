@ApiTest
Feature: API calls

    Scenario: Make a POST call on endpoint and check correct 201 response

        Given I have the API URL "https://wmxrwq14uc.execute-api.us-east-1.amazonaws.com/Prod/api/employees"
        And I have Basic Authentication token VGVzdFVzZXI3MTE6Zkp1ekhKfCk0PVJM
        And I have the following request body:
          | firstName | lastname | dependants |
          | Julie    | Anneq   | 0          |
        When I send a POST request
        # test will fail, as it returns incorrectly 200 now - bug
        Then the response status code should be 201


    Scenario: Make a POST call on endpoint and then make a GET call and check employee is present

        Given I have the API URL "https://wmxrwq14uc.execute-api.us-east-1.amazonaws.com/Prod/api/employees"
        And I have Basic Authentication token VGVzdFVzZXI3MTE6Zkp1ekhKfCk0PVJM
        And I have the following request body:
          | firstName | lastname | dependants |
          | Ribbon    | Kreaky   | 1          |
        When I send a POST request
        Then the response body should contain "Ribbon"
        And the response body should contain "Kreaky"
        And the response body should contain "1"
        And I store the employee's ID from the response

        # call GET on created employee

        When I send a GET request
        Then the response status code should be 200
        And the response body should contain "Ribbon"
        And the response body should contain "Kreaky"
        And the response body should contain "1"

    Scenario: Make a POST call on endpoint and make DELETE on same employee

        Given I have the API URL "https://wmxrwq14uc.execute-api.us-east-1.amazonaws.com/Prod/api/employees"
        And I have Basic Authentication token VGVzdFVzZXI3MTE6Zkp1ekhKfCk0PVJM
        And I have the following request body:
          | firstName | lastname | dependants |
          | Jason    | Hear   | 4          |
        When I send a POST request
        Then I store the employee's ID from the response

        # call DELETE on created employee
        When I send a DELETE request on stored employee ID
        Then the response status code should be 200

    Scenario: Make a POST call on endpoint and make PUT on same employee with changes

        Given I have the API URL "https://wmxrwq14uc.execute-api.us-east-1.amazonaws.com/Prod/api/employees"
        And I have Basic Authentication token VGVzdFVzZXI3MTE6Zkp1ekhKfCk0PVJM
        And I have the following request body:
          | firstName | lastname | dependants |
          | Dog    | Cat   | 2          |
        When I send a POST request
        Then I store the employee's ID from the response

        # call PUT on created employee
        Given I have the following request body:
          | firstName | lastname | dependants |
          | Fish       | Ant      | 5          |
        When I send a PUT request on stored employee ID
        Then the response status code should be 200
        Then the response body should contain "5"
        And the response body should contain "Fish"
        And the response body should contain "Ant"
