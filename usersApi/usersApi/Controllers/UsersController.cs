using Microsoft.AspNetCore.Mvc;
using usersApi.CasosDeUso;
using usersApi.Dtos;
using usersApi.Repositories;

namespace usersApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UsersController : Controller
    {
        private readonly UserDatabaseContext _UserDatabaseContext;
        private readonly IUpdateUserUseCase _UpdateUserUseCase;

        public UsersController(UserDatabaseContext UserDatabaseContext, IUpdateUserUseCase UpdateUserUseCase)
        {
            _UpdateUserUseCase = UpdateUserUseCase;
            _UserDatabaseContext = UserDatabaseContext;
        }


        //obtener un solo user
        //ap/users/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(long id)
        {
            var result = await _UserDatabaseContext.Get(id);

            return new OkObjectResult(result.ToDto());
        }

        //api/users/
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK,Type =typeof(List<UsersDto>))]
        public async Task<IActionResult> GetUsers()
        {
           var result=  _UserDatabaseContext.users.Select(u=>u.ToDto()).ToList();
            return new OkObjectResult(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UsersDto))]
        public async Task<IActionResult> CreateUser(CreateUsersDto user)
        {
            var result = await _UserDatabaseContext.Add(user);


            return new CreatedResult($"https://localhost:5256/api/users/{result.Id}", null);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(UsersDto user)
        {
            var result = await _UpdateUserUseCase.Execute(user);
            if (result == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type =typeof(bool))]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var result = await _UserDatabaseContext.Delete(id);
            return new OkObjectResult(result);
        }

    }
}
