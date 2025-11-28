using NUnit.Framework;

[TestFixture]
public class TestInputValidation
{
    [Test]
    public void TestForSQLInjection()
    {
        string malicious = "'; DROP TABLE Users; --";
        string sanitized = InputSanitizer.SanitizeString(malicious);

        Assert.That(sanitized.Contains("DROP"), Is.False);
        Assert.That(sanitized.Contains("--"), Is.False);
        Assert.That(sanitized.Contains("'"), Is.False);
    }

    [Test]
    public void TestForXSS()
    {
        string malicious = "<script>alert('hacked');</script>";
        string sanitized = InputSanitizer.SanitizeString(malicious);

        Assert.That(sanitized.Contains("<script>"), Is.False);
        Assert.That(sanitized.Contains("</script>"), Is.False);
        Assert.That(sanitized.Contains("&lt;") || sanitized.Contains("alert"), Is.False);
    }
}