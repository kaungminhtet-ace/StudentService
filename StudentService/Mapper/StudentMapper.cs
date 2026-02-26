using System.Security.Cryptography;
using Npgsql;
using StudentService.Entity;

namespace StudentService.Mapper;

public class StudentMapper()
{
    public static StudentEntity MapFromReader(NpgsqlDataReader reader, string sql)
    {
        var fullname = reader.GetString(reader.GetOrdinal("full_name"));
        var email = reader.GetString(reader.GetOrdinal("email"));
        var phone = reader.GetString(reader.GetOrdinal("phone"));

        return new StudentEntity(fullname, email, phone)
        {
            Id = reader.GetString(reader.GetOrdinal("id")),
            IsActive =  reader.GetBoolean(reader.GetOrdinal("is_active")),
            CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
            UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"))
        };
    }

    public static NpgsqlCommand MapToParameters(NpgsqlConnection conn, string sql, StudentEntity student)
    {
        var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("i", student.Id);
        cmd.Parameters.AddWithValue("f", student.FullName);
        cmd.Parameters.AddWithValue("e", student.Email);
        cmd.Parameters.AddWithValue("p", student.Phone);
        cmd.Parameters.AddWithValue("a", student.IsActive);
        cmd.Parameters.AddWithValue("c", student.CreatedAt);
        cmd.Parameters.AddWithValue("u", student.UpdatedAt);

        return cmd;
    }
}