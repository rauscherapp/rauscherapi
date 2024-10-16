﻿using System.Security.Cryptography;

namespace APIs.Security.JWT;

public class UserRequest
{

  public string? Email { get; set; }
  public string? Name { get; set; }
  public string? Avatar { get; set; }
  public string? Status { get; set; }
  public string? Password { get; set; }
  public int? UserType{ get; set; }
}
public class UserResponse
{
  public string Token { get; set; }
  public int UserId { get; set; }
  public string? Email { get; set; }
  public string? Name { get; set; }
  public Boolean HasValidStripeSubscription { get; set; }
  public string StripeSubscriptionLink { get; set; }
}

public static class Roles
{
  public const string ROLE_ACESSO_APIS = "Acesso-APIs";
}

public class TokenConfigurations
{
  public string? Audience { get; set; }
  public string? Issuer { get; set; }
  public int Seconds { get; set; }
  public string? SecretJwtKey { get; set; }

  public void GenerateSecretJwtKey(int keySize = 32)
  {
    using (var rng = new RNGCryptoServiceProvider())
    {
      var key = new byte[keySize];
      rng.GetBytes(key);
      SecretJwtKey = Convert.ToBase64String(key);
    }
  }
}

public class Token
{
  public bool Authenticated { get; set; }
  public string? Created { get; set; }
  public string? Expiration { get; set; }
  public string? AccessToken { get; set; }
  public string? Message { get; set; }
  public UserResponse? User { get; set; }
}