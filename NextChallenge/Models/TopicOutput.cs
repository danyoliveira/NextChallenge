using System;

namespace NextChallenge.Models {
    public class TopicOutput {
        public Guid IdTopic { get; set; }
        public string Username { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid IdUser { get; set; }


    }
}