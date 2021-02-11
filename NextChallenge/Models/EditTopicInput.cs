using System;
using System.ComponentModel.DataAnnotations;

namespace NextChallenge.Models {
    public class EditTopicInput {
        public Guid IdTopic { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
    }
}