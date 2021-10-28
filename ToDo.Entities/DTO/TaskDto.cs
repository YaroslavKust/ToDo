namespace ToDo.Entities.DTO
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Progress { get; set; }
        public int EmployeeId { get; set; }
    }
}