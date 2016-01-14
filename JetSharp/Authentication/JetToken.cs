using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace JetSharp.Authentication
{
  public class JetToken
  {
    [JsonProperty("id_token")]
    public string Token { get; private set; }

    [JsonProperty("expires_on")]
    public DateTime ExpirationDate { get; private set; }

    public bool IsExpired => ExpirationDate >= DateTime.Now;

    public override string ToString()
      => $"Bearer {Token}";

    public override bool Equals(object obj)
      => !ReferenceEquals(obj, null)
      && (ReferenceEquals(this, obj)
          || obj.GetType() == GetType()
          && Equals((JetToken)obj));

    protected bool Equals(JetToken other)
      => string.Equals(Token, other.Token)
         && ExpirationDate.Equals(other.ExpirationDate);

    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public override int GetHashCode()
    {
      unchecked
      {
        return ((Token?.GetHashCode() ?? 0) * 397)
          ^ ExpirationDate.GetHashCode();
      }
    }

    public static bool operator==(JetToken left, JetToken right)
      => Equals(left, right);

    public static bool operator!=(JetToken left, JetToken right)
      => !Equals(left, right);
  }
}