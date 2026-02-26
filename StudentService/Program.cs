using StudentService;
using StudentService.Dto;
using StudentService.Entity;

const string ConnectionString = "Host=localhost;Username=ixox;Password=12345678;Database=student";

using var pg = new Postgres(ConnectionString);

var reposity = new Repository(pg.Connection);

// var studentDto = new CreateStudentDto("Kaung Min Htet", "kaungminhtet@gmail.com", "09123456789");
// reposity.Create(studentDto);

const string id = "86cda9bc-b082-4e66-8302-c4532137289e";
var kaungminhtet = reposity.Get("86cda9bc-b082-4e66-8302-c4532137289e");
if (kaungminhtet != null)
{
    kaungminhtet.Email = "KaungMinHtet@gmail.com";
    reposity.Update(id, kaungminhtet);
}

kaungminhtet = reposity.Get("86cda9bc-b082-4e66-8302-c4532137289e");
Console.WriteLine(kaungminhtet);