using AutoMapper;
using NextChallenge.Helpers;
using NextChallenge.Models;
using System.Linq;
using System.Net;
using System.Web.Http;


namespace NextChallenge.Database.Repository {
    public class UserRepository : ApiController {
        private readonly NextChallengeEntities _database;
        private readonly IMapper mapper;

        public UserRepository()
        {
            _database = new NextChallengeEntities();
        }

        [HttpGet]
        public HttpStatusCode Login(LoginInput input)
        {
            var securityPassword = Security.MD5Hash(input.Password);
            var user = _database.Users.SingleOrDefault(l => l.Username == input.Username && l.Password == securityPassword);

            if (user == null)
                return HttpStatusCode.Unauthorized;

            Keys.IdUser = user.IdUser;
            Keys.Username = user.Username;
            return HttpStatusCode.OK;
        }
        [HttpPost]
        public HttpStatusCode Register(RegisterInput input)
        {
            if (CheckUser(input.Username) == HttpStatusCode.Ambiguous)
                return HttpStatusCode.Ambiguous;

            var newUser = new User
            {
                Username = input.Username,
                Password = Security.MD5Hash(input.Password)
            };

            try
            {
                var data = _database.Users.Add(newUser);
                _database.SaveChanges();
                Keys.IdUser = data.IdUser;
                Keys.Username = data.Username;
                return HttpStatusCode.OK;
            }
            catch (System.Exception)
            {
                return HttpStatusCode.InternalServerError;
            }

        }
        [HttpGet]
        public UserInfoOutput UserInfo(UserInfoInput input)
        {
            return _database.Users.Where(x => x.IdUser == input.IdUser).Select(x => new UserInfoOutput { Username = x.Username }).Single();
        }

        public HttpStatusCode CheckUser(string username)
        {
            if (_database.Users.Any(x => x.Username == username))
                return HttpStatusCode.Ambiguous;
            return HttpStatusCode.OK;

        }
        public new void Dispose()
        {
            _database.Dispose();
        }
    }
}