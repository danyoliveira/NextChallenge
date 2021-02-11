using System.ComponentModel.DataAnnotations;

namespace NextChallenge.Models {
    public class CreateTopicInput {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
    }
}