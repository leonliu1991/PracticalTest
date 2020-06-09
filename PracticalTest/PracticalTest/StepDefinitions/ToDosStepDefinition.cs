using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using PracticalTest.Actions;
using PracticalTest.Model;
using TechTalk.SpecFlow.Bindings;

namespace PracticalTest.StepDefinitions
{
    [Binding]
    public sealed class ToDosStepDefinition: Steps
    {
        private readonly ScenarioContext context;
        private ToDoAPI toDoAPI;
        
        public ToDosStepDefinition(ScenarioContext injectedContext)
        {
            TestContext testContext = ScenarioContext.Current.ScenarioContainer.Resolve<TestContext>();
            toDoAPI = new ToDoAPI();
        }

        [When(@"I call GET a list of to-dos")]
        public void WhenICallGETAListOfTo_Dos()
        {
            toDoAPI.SendAGetRequest();
        }

        [Then(@"it should return (.*) and a list of (.*) to-dos")]
        public void ThenItShouldReturnAndAListOfTo_Dos(HttpStatusCode statusCode, int count)
        {
            //Verify if the schema matches
            toDoAPI.VerifySchemaOfToDoList();

            //Verify if the status code is correct
            toDoAPI.VerifyStatusCode(statusCode);

            //Verify if returns 200 to-dos
            toDoAPI.VerifyAListOfToDos(count);
        }

        [When(@"I call GET a to-do by (.*)")]
        public void WhenICallGETATo_DoBy(string id)
        {
            toDoAPI.SendAGetRequestById(id);
        }

        [Then(@"it should return (.*) and a single to-do with correct (.*)")]
        public void ThenItShouldReturnAndASingleTo_Do(HttpStatusCode statusCode, int id)
        {
            //Verify if the schema matches
            toDoAPI.VerifySchemaOfToDo();

            //Verify if the status code is correct
            toDoAPI.VerifyStatusCode(statusCode);

            //Verify if returns a to-do
            toDoAPI.VerifyASingleToDo(id);
        }

        [Then(@"it should return (.*) and a single to-do with (.*), (.*), (.*)")]
        public void ThenItShouldReturnAndASingleTo_DoWith(HttpStatusCode statusCode, int id, string title, string body)
        {
            //Verify if the status code is correct
            toDoAPI.VerifyStatusCode(statusCode);

            //Verify if returns a to-do
            toDoAPI.VerifyASingleToDo(id, title, body);
        }

        [When(@"I call POST a to-do with (.*), (.*), (.*)")]
        public void WhenICallPOSTATo_DoWith(int id, string title, string body)
        {
            ToDoRequest todo = new ToDoRequest()
            {
                Id = id,
                Title = title,
                Body = body
            };
            toDoAPI.SendAPostRequest(todo);
        }

        [When(@"I call PUT a to-do with (.*), (.*), (.*)")]
        public void WhenICallPUTATo_DoWith(int id, string title, string body)
        {
            ToDoRequest todo = new ToDoRequest()
            {
                Id = id,
                Title = title,
                Body = body
            };
            toDoAPI.SendAPutRequest(todo);
        }

        [When(@"I call DELETE a to-do with (.*)")]
        public void WhenICallDELETEATo_DoWith(string id)
        {
            toDoAPI.SendADeleteRequest(id);
        }

        [Then(@"it should return (.*) and empty response")]
        public void ThenItShouldReturnAndEmptyResponse(HttpStatusCode statusCode)
        {
            //Verify if the status code is correct
            toDoAPI.VerifyStatusCode(statusCode);

            //Verify if response is empty
            toDoAPI.VerifyEmptyResponse();
        }
    }
}
