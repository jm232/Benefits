using TechTalk.SpecFlow;
using BenefitsTA.UI.Pages;
using BenefitsTA.UI.Pages.Modals;
using BenefitsTA.Common;

namespace BenefitsTA.UI.StepDefinitions
{
    [Binding]
    public class DashboardStepDefinitions : UITests
    {
        private readonly DashboardPage _dashboardPage;
        private readonly AddEmployee _addEmployee;
        private readonly EditEmployee _editEmployee;
        private readonly DeleteEmployee _deleteEmployee;
        private readonly AppConfig _appConfig;


        public DashboardStepDefinitions()
        {
            _dashboardPage = new DashboardPage(Driver, Wait); // Passing WebDriver to the page object
            _addEmployee = new AddEmployee(Driver, Wait); // Passing WebDriver to the page object
            _editEmployee = new EditEmployee(Driver, Wait); // Passing WebDriver to the page object
            _deleteEmployee = new DeleteEmployee(Driver, Wait); // Passing WebDriver to the page object
            _appConfig = new AppConfig(); // Create an instance of AppConfig
        }

        [Given(@"I am on the Benefits Dashboard page")]
        public void GivenIAmOnTheBenefitsDashboardPage()
        {
            _dashboardPage.DashboardIsOpened();
        }

        [When(@"I select Add Employee")]
        public void WhenISelectAddEmployee()
        {
            _dashboardPage.ClickAddEmployeeButton();
            var header = _addEmployee.GetModalHeaderText();
            Assert.IsTrue(header.Contains("Add Employee"));
        }

        [Then(@"I should see the employee firstname (.*), lastname (.*), dependents (.*) in the table")]
        public void WhenIShouldSeeTheEmployeeInTheTable(string firstname, string lastname, string dependents)
        {
            Console.WriteLine(firstname + "-" + lastname + "-" + dependents);
            var empId = _dashboardPage.GetCreatedEmployeeId(lastname);
            _dashboardPage.AssertEmployeePresent(empId, firstname, lastname, dependents);
        }

        [Then(@"I should be able to enter employee details firstname (.*), lastname (.*), dependents (.*)")]
        public void ThenIShouldBeAbleToEnterEmployeeDetails(string firstname, string lastname, string dependents)
        {
            Console.WriteLine(firstname + "-" + lastname + "-" + dependents);

            _addEmployee.EnterFirstName(firstname);
            _addEmployee.EnterLastName(lastname);
            _addEmployee.EnterDependents(dependents);
        }

        [Then(@"the employee (.*) should save")]
        public void ThenTheEmployeeShouldSave(string lastname)
        {
            _addEmployee.ClickAddButton();
        }

        [Then(@"the benefit cost calculations are correct for lastname (.*), dependents (.*)")]
        public void ThenTheBenefitCostCalculationAreCorrect(string lastname, string dependents)
        {
            var empId = _dashboardPage.GetCreatedEmployeeId(lastname);

            var tableCost = _dashboardPage.GetEmployeesBenefitsCost(empId);

            double baseCost = Math.Round((1000.00 / 26.00), 2);
            double dependentsCost = Math.Round(500.00 / 26.00, 2) * (double.Parse((dependents)));

            double expectedCost = baseCost + dependentsCost;

            Console.WriteLine(baseCost);
            Console.WriteLine(dependentsCost);

            Assert.AreEqual(expectedCost, tableCost);
        }

        [When(@"I select the Action Edit for (.*)")]
        public void WhenISelectTheActionEdit(string lastname)
        {
            var empId = _dashboardPage.GetCreatedEmployeeId(lastname);

            _dashboardPage.ClickEditEmployeeButton(empId);
            var header = _editEmployee.GetModalHeaderText();
            Assert.IsTrue(header.Contains("Add Employee"));
        }

        [Then(@"I can edit employee details firstname (.*), lastname (.*), dependents (.*)")]
        public void ThenICanEditEmployeeDetails(string firstname, string lastname, string dependents)
        {
            Console.WriteLine(firstname + "-" + lastname + "-" + dependents);

            _editEmployee.EnterFirstName(firstname);
            _editEmployee.EnterLastName(lastname);
            _editEmployee.EnterDependents(dependents);

            _editEmployee.ClickUpdateButton();
        }

        [Then(@"the data should change in the table for firstname (.*), lastname (.*), dependents (.*)")]
        public void ThenTheDataShouldChangeInTheTable(string firstname, string lastname, string dependents)
        {
            var empId = _dashboardPage.GetCreatedEmployeeId(lastname);
            _dashboardPage.AssertEmployeePresent(empId, firstname, lastname, dependents);
        }

        [When(@"I click the Action X for (.*)")]
        public void WhenISelectTheActionX(string lastname)
        {
            var empId = _dashboardPage.GetCreatedEmployeeId(lastname);

            _dashboardPage.ClickDeleteEmployeeButton(empId);
            _deleteEmployee.DeleteModalIsOpened();
        }

        [Then(@"the employee firstname (.*), lastname (.*) should be deleted")]
        public void ThenTheEmployeeShouldBeDeleted(string firstname, string lastname)
        {
            _deleteEmployee.ClickDeleteButton(lastname);

            bool a = _deleteEmployee.NameIsNotDisplayed(firstname);
            bool b = _deleteEmployee.NameIsNotDisplayed(lastname);

            Assert.IsFalse(a, "Element " + firstname + " was not deleted");
            Assert.IsFalse(b, "Element " + lastname + " was not deleted");
        }
    }
}