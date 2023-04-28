using KafkaConsumer.Interfaces;
using KafkaConsumer.Models;
using Microsoft.IdentityModel.Tokens;

namespace KafkaConsumer.DAL.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly AppDbContext context;
        public DepartmentRepository(AppDbContext context):base(context) 
        {
            this.context = context;
        }

        public void Update(Department department)
        {
            var departmentToUpdate =this.context.Departments.FirstOrDefault(d=>d.Id == department.Id);
                departmentToUpdate.Name = department.Name;
                context.SaveChanges();
        }
    }
}
