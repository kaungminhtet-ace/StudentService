using Npgsql;
using StudentService.Dto;
using StudentService.Entity;
using StudentService.Mapper;

namespace StudentService;

public class Repository(NpgsqlConnection connection)
{
    public object? Create(CreateStudentDto studentDto)
    {
        StudentEntity studentEntity = new StudentEntity(studentDto.FullName, studentDto.Email, studentDto.Phone);
        string sql = "INSERT INTO students (id, full_name, email, phone, is_active, created_at, updated_at) VALUES (@i, @f, @e, @p, @a, @c, @u) RETURNING id";
        using var cmd = StudentMapper.MapToParameters(connection, sql, studentEntity);
        return cmd.ExecuteScalar();
    }

    public StudentEntity? Get(string id)
    {
        string sql = "SELECT id, full_name, email, phone, is_active, created_at, updated_at FROM students WHERE id = @id";
        using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("id", id);
        using var reader = cmd.ExecuteReader();
        return !reader.Read() ? null : StudentMapper.MapFromReader(reader, sql);
    }

    public int Update(string id, StudentEntity studentEntity)
    {
        string sql = "UPDATE students SET full_name=@f, email=@e, phone=@p, updated_at=@u WHERE id=@i";
        using var cmd = StudentMapper.MapToParameters(connection, sql,studentEntity);
        return cmd.ExecuteNonQuery();
    }

    public int Delete(string id)
    {
        string sql = "DELETE FROM students WHERE id = @id";
        using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("id", id);
        return cmd.ExecuteNonQuery();
    }
}