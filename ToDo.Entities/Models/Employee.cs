using System;
using System.Collections.Generic;

namespace ToDo.Entities.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Speciality { get; set; }
        public DateTime EmploymentDate { get; set; }
        public byte[] Avatar { get; set; }
        public IEnumerable<MyTask> Tasks { get; set; }
    }
}