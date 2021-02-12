using NextChallenge.Models;
using System.Net;

namespace NextChallenge.Database.Repository {
    public interface IUserRepository {
        HttpStatusCode Login(LoginInput input);
        HttpStatusCode Register(RegisterInput input);
        UserInfoOutput UserInfo(UserInfoInput input);
        HttpStatusCode CheckUser(string username);
        void Dispose();

    }
}