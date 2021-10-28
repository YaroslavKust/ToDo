using System.Linq;
using ToDo.Entities.Models;
using ToDo.Entities.RequestFeatures;

namespace ToDo.DataAccess.Extensions
{
    public static class CollectionExtensions
    {
        public static IQueryable<Employee> SearchEmployees(this IQueryable<Employee> employees,
            EmployeeParameters parameters)
        {
            var result = employees.Where(e =>
                (string.IsNullOrWhiteSpace(parameters.Name) || e.Name.Contains(parameters.Name))
                &&
                (string.IsNullOrWhiteSpace(parameters.Speciality) || e.Speciality.Contains(parameters.Speciality))
                &&
                (e.EmploymentDate >= parameters.MinEmploymentDate && e.EmploymentDate <= parameters.MaxEmploymentDate)
                &&
                (e.Age >= parameters.MinAge && e.Age <= parameters.MaxAge)
            );

            return result;
        }

        public static IQueryable<MyTask> SearchTasks(this IQueryable<MyTask> tasks, TaskParameters parameters)
        {
            var result = tasks.Where(t =>
                (string.IsNullOrWhiteSpace(parameters.Description) ||
                 t.Description.ToUpper().Contains(parameters.Description.ToUpper()))
                &&
                (t.Progress >= parameters.MinProgress && t.Progress <= parameters.MaxProgress)
            );

            return result;
        }
    }
}