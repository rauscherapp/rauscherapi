namespace APIs.Security.JWT
{
  public interface IAccessManager
  {
    Task<bool> CreateUser(UserRequest model);
    Task<(UserResponse? user, bool isValid)> ValidateCredentials(UserRequest user);
    Token GenerateToken(UserResponse user);
    Task<bool> CheckSubscription(UserRequest model);
    //Task<(UserResponse? user, bool isValid)> ValidateCredentials(UserRequest user);
    //Token GenerateToken(UserResponse user);
  }
}
