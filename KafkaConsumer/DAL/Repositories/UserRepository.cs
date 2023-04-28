using KafkaConsumer.Interfaces;
using KafkaConsumer.Models;
using Microsoft.IdentityModel.Tokens;

namespace KafkaConsumer.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppDbContext context;
        public UserRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(User user)
        {
            var userToUpdate = this.context.Users.FirstOrDefault(u => u.Id == user.Id);
           
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;  
            userToUpdate.Email = user.Email;
            userToUpdate.Password = user.Password;
            userToUpdate.Role = user.Role;
            userToUpdate.DepartmentId = user.DepartmentId;
            context.SaveChanges();
        }
    }
}
