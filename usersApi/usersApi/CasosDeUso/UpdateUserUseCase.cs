using usersApi.Dtos;
using usersApi.Repositories;

namespace usersApi.CasosDeUso
{
   
        public interface IUpdateUserUseCase
        {
            Task<Dtos.UsersDto?> Execute(Dtos.UsersDto customer);
        }

        public class UpdateUserUseCase : IUpdateUserUseCase
        {

            private readonly UserDatabaseContext _UserDataBaseContext;

            public UpdateUserUseCase(UserDatabaseContext userDatabaseContext)
            {
                _UserDataBaseContext = userDatabaseContext;
            }


            public async Task<Dtos.UsersDto?> Execute(Dtos.UsersDto user)
            {
                var entity = await _UserDataBaseContext.Get(user.Id);

                if (entity == null)
                    return null;

                entity.FullName = user.FullName;
                entity.UserName = user.UserName;
                entity.Email = user.Email;
               

                await _UserDataBaseContext.Actualizar(entity);
                return entity.ToDto();

            }

        }
    

    
}
