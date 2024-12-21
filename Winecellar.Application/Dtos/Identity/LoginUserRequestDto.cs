using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winecellar.Application.Dtos.Identity
{
    public class LoginUserRequestDto
    {
        public string LoginInput { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
