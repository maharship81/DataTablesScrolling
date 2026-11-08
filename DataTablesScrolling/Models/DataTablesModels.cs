namespace DataTablesScrolling.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Office { get; set; } = string.Empty;
        public int Age { get; set; }
        public string StartDate { get; set; } = string.Empty;
        public string Salary { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class DataTablesRequest
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public SearchParam? Search { get; set; }
    }

    public class SearchParam
    {
        public string? Value { get; set; }
    }
}
