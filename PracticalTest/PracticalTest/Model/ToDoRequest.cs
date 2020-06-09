namespace PracticalTest.Model
{
    public partial class ToDoRequest
    {
        /// <summary>
        /// Initializes a new instance of the Error class.
        /// </summary>
        public ToDoRequest() { }

        /// <summary>
        /// Initializes a new instance of the Error class.
        /// </summary>
        public ToDoRequest(int id, string title, string body)
        {
            Id = id;
            Title = title;
            Body = Body;
        }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "body")]
        public string Body { get; set; }
    }
}