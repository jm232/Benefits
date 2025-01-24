@UI
Feature: E2E test for employees list

    Scenario: Validate add, edit and delete employee functionality

        Given an Employer
        And I am on the Benefits Dashboard page

        #add
        When I select Add Employee
        Then I should be able to enter employee details firstname Harry, lastname Sharkey, dependents 4
        And the employee Sharkey should save
        And I should see the employee firstname Harry, lastname Sharkey, dependents 4 in the table
        And the benefit cost calculations are correct for lastname Sharkey, dependents 4

        #edit
        When I select the Action Edit for Sharkey
        Then I can edit employee details firstname Josh, lastname Fender, dependents 4
        And the data should change in the table for firstname Josh, lastname Fender, dependents 4

        #delete
        When I click the Action X for Fender
        Then the employee firstname Josh, lastname Fender should be deleted