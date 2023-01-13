using Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Interface
{
    public interface IUserService
    {
        UserDto Get(int Id);
        List<UserDto> GetAll();
        void Add(UserDto model);
        void Update(UserDto model);
        void Delete(UserDto model);
        string Login(LoginDto model);
        void ChangePassword(ChangePassword model);
        bool SignUp(UserDto model);

    }
}
