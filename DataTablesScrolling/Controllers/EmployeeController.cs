using Microsoft.AspNetCore.Mvc;
using static DataTables_Scrolling.Models.DataTablesModels;

namespace DataTablesScrolling.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public IActionResult GetEmployeeData([FromForm] DataTablesRequest request)
        {
            try
            {
                // Simulated database collection
                var query = Enumerable.Range(1, 120).Select(i => new Employee
                {
                    Id = i,
                    FullName = $"Employee {i} Smith",
                    Position = i % 2 == 0 ? "Principal Engineer" : "Systems Analyst",
                    Department = i % 3 == 0 ? "Platform Infrastructure" : "Engineering",
                    Office = "San Francisco Hub, Building 4",
                    Age = 25 + (i % 35),
                    StartDate = DateTime.UtcNow.AddDays(-i * 15).ToString("yyyy-MM-dd"),
                    Salary = $"${(85000 + (i * 950)):N0}",
                    Email = $"employee.{i}@enterprise-systems.internal"
                }).AsQueryable();

                int totalRecords = query.Count();

                // Apply Search Filter if present
                if (!string.IsNullOrWhiteSpace(request.Search?.Value))
                {
                    string searchValue = request.Search.Value.ToLower();
                    query = query.Where(e =>
                        e.FullName.ToLower().Contains(searchValue) ||
                        e.Department.ToLower().Contains(searchValue) ||
                        e.Position.ToLower().Contains(searchValue));
                }

                int filteredRecords = query.Count();

                // Pagination logic
                var data = query
                    .Skip(request.Start)
                    .Take(request.Length > 0 ? request.Length : 10)
                    .ToList();

                // Return standard DataTables response
                return Json(new
                {
                    draw = request.Draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = filteredRecords,
                    data = data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
