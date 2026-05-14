using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Students")]
public class Student
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }

    public string Department { get; set; }
}