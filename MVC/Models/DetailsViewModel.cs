using System.Collections.Generic;
using ToDo.Entities.DTO;

namespace MVC.Models
{
    public class DetailsViewModel
    {
        public EmployeeDto Employee { get; set; }
        public IEnumerable<TaskDto> Tasks { get; set; }
    }
}