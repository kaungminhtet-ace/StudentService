using Npgsql;

namespace StudentService;

public class Postgres : IDisposable
{
    private static void Init(NpgsqlConnection conn)
    {
        string sql = @"
            CREATE TABLE IF NOT EXISTS students (
                id TEXT PRIMARY KEY,
                full_name VARCHAR(100) NOT NULL,
                email TEXT NOT NULL UNIQUE,
                phone TEXT NOT NULL,
                is_active BOOLEAN NOT NULL DEFAULT TRUE,
                created_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
                updated_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP
            );

            CREATE INDEX IF NOT EXISTS idx_students_email ON students(email);
        ";
        
        using var cmd = new NpgsqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Student database initialized successfully.");
    }

    public Postgres(string connectionString)
    {
        try
        {
            Connection = new NpgsqlConnection(connectionString);
            Connection.Open();
            Console.WriteLine($"Successfully connected to PostgreSQL, State: {Connection.State}");
            Init(Connection);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public NpgsqlConnection Connection { get; }

    public void Dispose()
    {
        Connection.Dispose();
        Console.WriteLine($"Successfully disconnected to PostgreSQL, State: {Connection.State}");
    }
}