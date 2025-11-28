using MySql.Data.MySalClient;

public class UserRepository{
    private readonly string _connection;
    public UserRepository(string conn) => _connection = conn;

    public void InsertUser(string username, string email){
        using var conn = new MySqlConnection(_connnection);
        conn.Open();

        var cmd = new MySqlCommand("INSERT INTO Users (Username, Email) VALUES (@username, @email)", conn);
        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@email", email);

        cmd.ExecuteNonQuery();
    }

    public User? FindUser(string username){
        using var conn = new MySqlConnection(_connection);
        conn.Open();

        var cmd = new MySqlCommand("SELECT UserID, Username, Email FROM Users WHERE Username = @username", conn);
        cmd.Parameters.AddWithValue("@Username", username);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read() return null;)

        return new User{
            UserID = reader.GetInt32("UserID"),
            Username = reader,GetString("Username"),
            Email = reader.GetString("Email")
        };
    }
}