using System;

namespace ToDo.Entities.DTO
{
    public class EmployeeForManipulation
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Speciality { get; set; }
        public DateTime EmploymentDate { get; set; }
        public byte[] Avatar { get; set; }
    }
}