namespace linq1;

public class Student
{
    public string? Name { get; set; }
    public int Grade { get; set; }
    public string? Major { get; set; }


    public override string ToString()
    => $"Name = {Name}, Grade = {Grade} And Major = {Major}";
}