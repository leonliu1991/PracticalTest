using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PracticalTest.Common;
using PracticalTest.Model;
using RestSharp;

namespace PracticalTest.Actions
{
    class ToDoAPI : HttpRequestWrapper
    {
        private string resource = "/todos";
        private string url = "https://jsonplaceholder.typicode.com";
        private const string SchemaPathOfToDoList = @"JSONSchemas\ToDosListJsonSchema.json";
        private const string SchemaPathOfToDo = @"JsonSchemas\ToDoJsonSchema.json";
        private IRestResponse restResponse;
        JsonSchemaComparer schemaComparer = new JsonSchemaComparer();

        public ToDoAPI()
        {
            System.Console.WriteLine($"The base url is '{url}'");
            Debug.WriteLine($"The base url is '{url}'");
        }

        public void SendAGetRequest()
        {
            ClearRequest();
            SetMethod(Method.GET);
            SetResourse(resource);
            restResponse = Execute(url);
        }

        public void SendAGetRequestById(string id)
        {
            ClearRequest();
            SetMethod(Method.GET);
            SetResourse(string.Format("{0}/{1}", resource, id));
            restResponse = Execute(url);
        }

        public void SendAPostRequest(ToDoRequest todo)
        {
            ClearRequest();
            SetMethod(Method.POST);
            SetResourse(resource);
            AddJsonContent(todo);
            restResponse = Execute(url);
        }

        public void SendAPutRequest(ToDoRequest todo)
        {
            ClearRequest();
            SetMethod(Method.PUT);
            SetResourse(string.Format("{0}/{1}", resource, todo.Id));
            AddJsonContent(todo);
            restResponse = Execute(url);
        }

        public void SendADeleteRequest(string id)
        {
            ClearRequest();
            SetMethod(Method.DELETE);
            SetResourse(string.Format("{0}/{1}", resource, id));
            restResponse = Execute(url);
        }

        public void VerifyStatusCode(HttpStatusCode statusCode)
        {
            Assert.AreEqual(statusCode, restResponse.StatusCode,
                $"{restResponse.Request.Method} : {restResponse.ResponseUri} => {restResponse.Content}");
        }

        public void VerifyAListOfToDos(int count)
        {
            List<ToDoResponse> result = JsonConvert.DeserializeObject<List<ToDoResponse>>(restResponse.Content);

            //Check if it returns 200 to-dos
            Assert.IsNotNull(result, "Empty data is returned in the response.");
            Assert.AreEqual(200, count);
        }

        public void VerifyASingleToDo(int id)
        {
            ToDoResponse result = JsonConvert.DeserializeObject<ToDoResponse>(restResponse.Content);

            //Check if it returns a to-do with correct id
            Assert.IsNotNull(result, "Empty data is returned in the response.");
            Assert.AreEqual(id, result.Id);
        }

        public void VerifyASingleToDo(int id, string title, string body)
        {
            ToDoRequest result = JsonConvert.DeserializeObject<ToDoRequest>(restResponse.Content);

            //Check if it returns a to-do with correct id
            Assert.IsNotNull(result, "Empty data is returned in the response.");
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual(title, result.Title);
            Assert.AreEqual(body, result.Body);
        }

        public void VerifyEmptyResponse()
        {
            Assert.AreEqual("{}", restResponse.Content);
        }

        public void VerifySchemaOfToDoList()
        {
            //Check if the schema matches
            schemaComparer.AssertArraySchema(JArray.Parse(restResponse.Content), SchemaPathOfToDoList);
        }

        public void VerifySchemaOfToDo()
        {
            //Check if the schema matches
            schemaComparer.AssertObjectSchema(JObject.Parse(restResponse.Content), SchemaPathOfToDo);
        }
    }
}