using System.Text.RegularExpressions;

public static class InputSanitizer{
    public static string SanitizeString(string input){
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        input = Regex.Replace(input, "<.*?>", string.Empty);
        input = Regex.Replace(input, @"[;'\-]", string.Empty);
        return input.Trim();
    }
}