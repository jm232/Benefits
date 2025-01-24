# Introduction
Paylocity exercise for hiring purposes
## Bug challenge testing part
Please see file `BugsAndImprovements` folder divided due to bug's priority
## Automation part
C# & Specflow based framework driven by BDD Gherkin language for UI and API tests.
- UI part used `selenium` library and chrome browser with Page Object pattern. Failed step is screenshoted, and saved.
- API part used `HttpClient` as library

### Prerequisites
1. net9.0 installed
2. build solution
3. execute tests

### Expected test results
1 failed (_reported in `BugsAndImprovements`_ ), 4 passed

### Known Issues
1. Token should be rather saved in pipeline, not in code, same applies for UI - username and password
2. Missing cleanup() in AfterScenario (out of this exercise scope)
