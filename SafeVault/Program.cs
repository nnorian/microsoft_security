using SafeVault.Services;

class program{
    static void Main(){
        Console.WriteLine("SafeVault terminal app");
        Console.WriteLine("1. Register");
        Console.WriteLine("1. Login");
        Console.WriteLine("1. Register");
        var choice = Console.ReadLine();

        var repo = new UserRepository(dbconfig.Conn);
        var auth = new AuthService(repo);

        if (choice == "1"){
            Console.Write("Usernane:");
            var user = InputSanitizer.SanitizeString(Console.ReadLine());

            Console.Write("Password: ");
            var pass = Console.ReadLine();

            auth.Register(user, pass);
            Console.WriteLine("User registered securely.");
        }

        else if (choice == "2"){
            Console.Write("Usernane:");
            var user = InputSanitizer.SanitizeString(Console.ReadLine());

            Console.Write("Password: ");
            var pass = Console.ReadLine();

            if (auth.Login(user, pass))
                Console.WriteLine("Login successful.");
            else
                Console.WriteLine("Invalid login attempt.");
        }
    }
}