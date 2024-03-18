using Microsoft.EntityFrameworkCore;
using usersApi.Dtos;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace usersApi.Repositories
{
    public class UserDatabaseContext:DbContext

    {
        public UserDatabaseContext(DbContextOptions<UserDatabaseContext> options)
            : base(options)
        { 

           
        }
        //dbset con la tabla users
       public DbSet<UsersEntity> users { get; set; }

       public async Task<UsersEntity?> Get(long id)
        {
            return await users.FirstOrDefaultAsync(u => u.Id == id);
        }

        //añadir un elemento a al db
        public async Task<UsersEntity> Add(CreateUsersDto usersDto)
        {
            UsersEntity entity = new UsersEntity()
            {
                Id = null,
                FullName = usersDto.FullName,
                UserName = usersDto.UserName,
                Email = usersDto.Email

            };
            var response = await users.AddAsync(entity);
            await SaveChangesAsync();
            return await Get(response.Entity.Id ?? throw new Exception("No se ha podido guardar"));
        }

        //eliminar un elemento
        public async Task<bool> Delete(long id)
        {
            var entity= await Get(id);
             users.Remove(entity);
            SaveChanges();
            return true;
        }

        //aactualizar usuario
        public async Task<bool> Actualizar(UsersEntity usersEntity)
        {
             users.Update(usersEntity);
            await SaveChangesAsync();

            return true;
        }
    }

    //entity que hara el mapeo uno a uno con la base de datos
    public class UsersEntity
    {
        public long? Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public UsersDto ToDto()
        {
            return new UsersDto()
            {
                FullName = FullName,
                UserName = UserName,
                Email = Email,
                Id = Id ?? throw new Exception("el id no puede ser null")
            };
        }
    }

  
}
