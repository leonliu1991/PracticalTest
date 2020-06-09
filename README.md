### Description:<br>
The tests include verifying positive flows of GET, POST, PUT, DELETE to-do endpoints, and also a negative flow test case trying to validate an invalid id scenario.
<br>
### Assertion:<br>
In the tests, the below things are validated:
- Status code
- Response content
- Response schema format

### Framework design explanation<br>
SpecFlow + MS Test 3 layers framework is used: features, step definitions and actions along with common library which encapsulates REST API methods and a json schema checker.   

All tests have been executed locally and they all passed.
