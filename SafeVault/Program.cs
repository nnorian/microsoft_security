class Program
{
    static void Main()
    {
        Console.WriteLine("SafeVault terminal app");
        Console.WriteLine("1. Register");
        Console.WriteLine("2. Login");
        var choice = Console.ReadLine();

        var repo = new UserRepository(DbConfig.Conn);
        var auth = new AuthService(repo);

        if (choice == "1")
        {
            Console.Write("Username: ");
            var user = InputSanitizer.SanitizeString(Console.ReadLine() ?? "");

            Console.Write("Password: ");
            var pass = Console.ReadLine() ?? "";

            if (auth.Register(user, pass))
                Console.WriteLine("User registered securely.");
            else
                Console.WriteLine("Registration failed.");
        }
        else if (choice == "2")
        {
            Console.Write("Username: ");
            var user = InputSanitizer.SanitizeString(Console.ReadLine() ?? "");

            Console.Write("Password: ");
            var pass = Console.ReadLine() ?? "";

            if (auth.Login(user, pass))
                Console.WriteLine("Login successful.");
            else
                Console.WriteLine("Invalid login attempt.");
        }
    }
}