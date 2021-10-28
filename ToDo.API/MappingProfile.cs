using AutoMapper;
using ToDo.Entities.DTO;
using ToDo.Entities.Models;

namespace ToDo.API
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeForManipulation, Employee>();
            CreateMap<Employee, EmployeeDto>();

            CreateMap<TaskForCreate, MyTask>();
            CreateMap<TaskForUpdate, MyTask>();
            CreateMap<MyTask, TaskDto>();
        }
    }
}