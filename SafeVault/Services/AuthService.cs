using BCrypt.Net;

public class AuthService{
    private readonly UserRepository _repo;
    public AuthService(UserRepository repo) => _repo=repo;

    public bool Register(string username, string password){
        string hash = BCrypt.Net.BCrypt.HashPassword(password);

        _repo.CreateUser(username, hash, "user");
        return true;
    }

    public bool Login(string username, string password){
        var user = _repo.FindUser(username);
        if (user == null) return false;

        return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        
    }
}