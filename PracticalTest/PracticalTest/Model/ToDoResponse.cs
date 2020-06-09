namespace PracticalTest.Model
{
    public partial class ToDoResponse
    {
        /// <summary>
        /// Initializes a new instance of the Error class.
        /// </summary>
        public ToDoResponse() { }

        /// <summary>
        /// Initializes a new instance of the Error class.
        /// </summary>
        public ToDoResponse(int userId, int id, string title, bool completed)
        {
            UserId = userId;
            Id = id;
            Title = title;
            Completed = completed;
        }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "userId")]
        public int UserId { get; set; }

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
        [Newtonsoft.Json.JsonProperty(PropertyName = "completed")]
        public bool Completed { get; set; }
    }
}