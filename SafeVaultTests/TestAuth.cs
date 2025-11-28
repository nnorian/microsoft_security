using NUnit.Framework;

[TestFeature]
public class TestAuth{
    [Test]
    public void TestInvalidLogin(){
        var repo = new InMemoryUserRepository();
        var auth = new AuthService(repo);

        repo.CreateUser("test", BCrypt.Net.BCrypt.HashPassword("1234"), "user");
        bool result = auth.Login("test", "wrongpassword");
        Assert.IsFalse(result);
    }

    [Test]
    public void TestAuthorization(){
        var user = new User { Username = "admin", Role = "admin"};
        Assert.AreEqul("admin", user.Role);
    }
}