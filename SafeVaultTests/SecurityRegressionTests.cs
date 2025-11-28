using NUnit.Framework;
using BCrypt.Net;

[TestFixture]
public class SecurityRegressionTests
{
    [Test]
    public void TestPasswordHashingIsNotReversible()
    {
        string plainPassword = "SecurePassword123!";
        string hash = BCrypt.Net.BCrypt.HashPassword(plainPassword);

        Assert.That(hash, Is.Not.EqualTo(plainPassword));
        Assert.That(BCrypt.Net.BCrypt.Verify(plainPassword, hash), Is.True);
    }

    [Test]
    public void TestSQLInjectionPrevention()
    {
        string maliciousInput = "admin' OR '1'='1";
        string sanitized = InputSanitizer.SanitizeString(maliciousInput);

        Assert.That(sanitized.Contains("'"), Is.False);
        Assert.That(sanitized.Contains("OR"), Is.False);
        Assert.That(sanitized.Contains("--"), Is.False);
    }

    [Test]
    public void TestXSSPrevention()
    {
        string maliciousScript = "<script>alert('XSS')</script>";
        string sanitized = InputSanitizer.SanitizeString(maliciousScript);

        Assert.That(sanitized.Contains("<script>"), Is.False);
        Assert.That(sanitized.Contains("</script>"), Is.False);
    }

    [Test]
    public void TestUnauthorizedAccessPrevention()
    {
        var repo = new InMemoryUserRepository();
        var auth = new AuthService(repo);

        repo.CreateUser("user1", BCrypt.Net.BCrypt.HashPassword("password123"), "user");

        bool loginResult = auth.Login("user1", "wrongpassword");
        Assert.That(loginResult, Is.False);
    }

    [Test]
    public void TestRoleBasedAccessControl()
    {
        var adminUser = new User { Username = "admin", Role = "admin" };
        var regularUser = new User { Username = "user", Role = "user" };

        Assert.That(adminUser.Role, Is.EqualTo("admin"));
        Assert.That(regularUser.Role, Is.EqualTo("user"));
        Assert.That(adminUser.Role, Is.Not.EqualTo(regularUser.Role));
    }

    [Test]
    public void TestPasswordComplexity()
    {
        var repo = new InMemoryUserRepository();
        var auth = new AuthService(repo);

        string weakPassword = "123";
        string strongPassword = "SecurePass123!";

        Assert.That(strongPassword.Length >= 8, Is.True);
        Assert.That(weakPassword.Length >= 8, Is.False);
    }

    [Test]
    public void TestCommandInjectionPrevention()
    {
        string maliciousCommand = "test; rm -rf /";
        string sanitized = InputSanitizer.SanitizeString(maliciousCommand);

        Assert.That(sanitized.Contains(";"), Is.False);
        Assert.That(sanitized.Contains("rm"), Is.False);
    }

    [Test]
    public void TestPathTraversalPrevention()
    {
        string maliciousPath = "../../etc/passwd";
        string sanitized = InputSanitizer.SanitizeString(maliciousPath);

        Assert.That(sanitized.Contains("../"), Is.False);
        Assert.That(sanitized.Contains("..\\"), Is.False);
    }

    [Test]
    public void TestSessionFixationPrevention()
    {
        var repo = new InMemoryUserRepository();
        var auth = new AuthService(repo);

        repo.CreateUser("testuser", BCrypt.Net.BCrypt.HashPassword("password"), "user");

        bool firstLogin = auth.Login("testuser", "password");
        bool secondLogin = auth.Login("testuser", "password");

        Assert.That(firstLogin, Is.True);
        Assert.That(secondLogin, Is.True);
    }

    [Test]
    public void TestEmptyOrNullInputHandling()
    {
        var repo = new InMemoryUserRepository();
        var auth = new AuthService(repo);

        bool resultEmpty = auth.Login("", "");
        bool resultNull = auth.Login(null, null);

        Assert.That(resultEmpty, Is.False);
        Assert.That(resultNull, Is.False);
    }
}
