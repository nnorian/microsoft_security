using NUnit.Framework;

[TestFixture]
public class TestAuth
{
    [Test]
    public void TestInvalidLogin()
    {
        var repo = new InMemoryUserRepository();
        var auth = new AuthService(repo);

        repo.CreateUser("test", BCrypt.Net.BCrypt.HashPassword("1234"), "user");
        bool result = auth.Login("test", "wrongpassword");
        Assert.That(result, Is.False);
    }

    [Test]
    public void TestAuthorization()
    {
        var user = new User { Username = "admin", Role = "admin" };
        Assert.That(user.Role, Is.EqualTo("admin"));
    }
}