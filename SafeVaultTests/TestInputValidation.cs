using NUnit.Framework;

[TestFixture]
public class TestInputValidation{
    [Test]
    public void TestForSQLInjection(){
        string malicious = "'; DROP TABLE Users; --";
        string sanitized = InputSanitizer.SanitizeString(malicious);

        Assert.IsFalse(sanitized.Constains("DROP"));
        Assert.IsFalse(sanitized.Constains("--"));
        Assert.IsFalse(sanitized.Constains("'"));
    }

    [Test]
    public void TestForXXS(){
        string malicious = "<script>alert('hacked');</script>";
        string sanitized = InputSanitizeString(malicious);

        Assert.IsFalse(sanitized.Constains("<script>"));
        Assert.IsFalse(sanitized.Constains("</script>"));
        Assert.IsFalse(sanitized.Constains("&lt;") || sanitized.Contains("alert"));
    }
}