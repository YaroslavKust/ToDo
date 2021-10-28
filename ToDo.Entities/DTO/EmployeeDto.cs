using System;

namespace ToDo.Entities.DTO
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Speciality { get; set; }
        public DateTime EmploymentDate { get; set; }
        public byte[] Avatar { get; set; }
    }
}