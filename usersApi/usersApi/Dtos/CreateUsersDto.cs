using System.ComponentModel.DataAnnotations;

namespace usersApi.Dtos
{
    public class CreateUsersDto
    {
        [Required(ErrorMessage ="El nombre es obligatorio")]
            public string FullName { get; set; }

        [Required(ErrorMessage = "El username es obligatorio")]
        public string UserName { get; set; }

        [RegularExpression("^[^@]+@[^@]+\\.[a-zA-Z]{2,}$", ErrorMessage =("Este email no es valido"))]
            public string Email { get; set; }
        
    }
}
