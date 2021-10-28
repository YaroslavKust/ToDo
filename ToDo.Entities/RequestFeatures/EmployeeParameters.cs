using System;

namespace ToDo.Entities.RequestFeatures
{
    public class EmployeeParameters
    {
        public string Name { get; set; }
        public string Speciality { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; } = int.MaxValue;
        public DateTime MinEmploymentDate { get; set; }
        public DateTime MaxEmploymentDate { get; set; } = DateTime.Today;

    }
}