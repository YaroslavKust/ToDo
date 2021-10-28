
namespace ToDo.Entities.Models
{
    public class MyTask
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Progress { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
