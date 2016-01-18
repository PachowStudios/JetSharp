using System;
using JetSharp.Authentication;
using NUnit.Framework;

namespace JetSharp.Tests
{
  public class BaseJetTest
  {
    protected JetCredentials Credentials { get; set; }

    [OneTimeSetUp]
    public void OneTimeSetUp()
      => Credentials = new JetCredentials(
        Environment.GetEnvironmentVariable("JET_USER", EnvironmentVariableTarget.User),
        Environment.GetEnvironmentVariable("JET_PASS", EnvironmentVariableTarget.User));
  }
}