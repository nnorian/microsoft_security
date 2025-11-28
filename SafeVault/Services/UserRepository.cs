using MySql.Data.MySqlClient;

public class UserRepository
{
    private readonly string _connection;
    public UserRepository(string conn) => _connection = conn;

    public virtual void CreateUser(string username, string passwordHash, string role)
    {
        using var conn = new MySqlConnection(_connection);
        conn.Open();

        var cmd = new MySqlCommand("INSERT INTO Users (Username, PasswordHash, Role) VALUES (@username, @passwordHash, @role)", conn);
        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@passwordHash", passwordHash);
        cmd.Parameters.AddWithValue("@role", role);

        cmd.ExecuteNonQuery();
    }

    public void InsertUser(string username, string email)
    {
        using var conn = new MySqlConnection(_connection);
        conn.Open();

        var cmd = new MySqlCommand("INSERT INTO Users (Username, Email) VALUES (@username, @email)", conn);
        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@email", email);

        cmd.ExecuteNonQuery();
    }

    public virtual User? FindUser(string username)
    {
        using var conn = new MySqlConnection(_connection);
        conn.Open();

        var cmd = new MySqlCommand("SELECT UserID, Username, PasswordHash, Role FROM Users WHERE Username = @username", conn);
        cmd.Parameters.AddWithValue("@username", username);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) return null;

        return new User
        {
            UserID = reader.GetInt32("UserID"),
            Username = reader.GetString("Username"),
            PasswordHash = reader.GetString("PasswordHash"),
            Role = reader.GetString("Role")
        };
    }
}

public class InMemoryUserRepository : UserRepository
{
    private readonly List<User> _users = new();
    private int _nextId = 1;

    public InMemoryUserRepository() : base(string.Empty) { }

    public override void CreateUser(string username, string passwordHash, string role)
    {
        _users.Add(new User
        {
            UserID = _nextId++,
            Username = username,
            PasswordHash = passwordHash,
            Role = role
        });
    }

    public override User? FindUser(string username)
    {
        return _users.FirstOrDefault(u => u.Username == username);
    }
}