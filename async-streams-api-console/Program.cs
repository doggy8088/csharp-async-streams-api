using System.Text.Json;

var client = new HttpClient();

client.DefaultRequestHeaders.UserAgent.TryParseAdd("Duotify/1.0");

using var resp = await client.GetAsync("http://localhost:5118/WeatherForecast", HttpCompletionOption.ResponseHeadersRead);

using Stream responseStream = await resp.Content.ReadAsStreamAsync();

var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true,
    DefaultBufferSize = 128
};

var weathers = JsonSerializer.DeserializeAsyncEnumerable<WeatherForecast>(responseStream, options);

await foreach (var weather in weathers)
{
    Console.WriteLine(weather.Dump(DumpStyle.Console));
}

#nullable disable

public class WeatherForecast
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string Summary { get; set; }
}
public partial class Course
{
    public int CourseId { get; set; }

    public string Title { get; set; }

    public int Credits { get; set; }

    public int DepartmentId { get; set; }

    public string Description { get; set; }

    public string Slug { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime StartDate { get; set; }

    public virtual Department Department { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<Person> Instructors { get; set; } = new List<Person>();
}
public partial class Department
{
    public int DepartmentId { get; set; }

    public string Name { get; set; }

    public decimal Budget { get; set; }

    public DateTime StartDate { get; set; }

    public int? InstructorId { get; set; }

    public byte[] RowVersion { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual Person Instructor { get; set; }
}
public partial class Person
{
    public int Id { get; set; }

    public string LastName { get; set; }

    public string FirstName { get; set; }

    public DateTime? HireDate { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public string Discriminator { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual OfficeAssignment OfficeAssignment { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
public partial class OfficeAssignment
{
    public int InstructorId { get; set; }

    public string Location { get; set; }

    public virtual Person Instructor { get; set; }
}
public partial class Enrollment
{
    public int EnrollmentId { get; set; }

    public int CourseId { get; set; }

    public int StudentId { get; set; }

    public int? Grade { get; set; }

    public virtual Course Course { get; set; }

    public virtual Person Student { get; set; }
}