using System;

namespace NextChallenge.Models {
    public class RegisterInput {
        public string Username { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}