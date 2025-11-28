using System.Text.RegularExpressions;

public static class InputSanitizer
{
    public static string SanitizeString(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;

        // Remove HTML tags
        input = Regex.Replace(input, "<.*?>", string.Empty);

        // Remove dangerous SQL and command injection characters
        input = Regex.Replace(input, @"[;'\-<>]", string.Empty);

        // Remove SQL keywords (case insensitive)
        input = Regex.Replace(input, @"\b(DROP|SELECT|INSERT|UPDATE|DELETE|OR|AND|EXEC|EXECUTE)\b", string.Empty, RegexOptions.IgnoreCase);

        // Remove script-related keywords
        input = Regex.Replace(input, @"\b(script|alert|onerror|onload)\b", string.Empty, RegexOptions.IgnoreCase);

        // Remove path traversal sequences
        input = input.Replace("../", string.Empty).Replace("..\\", string.Empty);

        // Remove command execution attempts
        input = Regex.Replace(input, @"\b(rm|del|format|exec|eval)\b", string.Empty, RegexOptions.IgnoreCase);

        return input.Trim();
    }
}