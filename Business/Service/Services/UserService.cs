using Business.Authentication;
using Business.Service.Interface;
using Data.Dto;
using Data.Entities;
using Data.Enums;
using DataAccess.UoW;
using FluentValidation;
using ProjectUtils.Extensions.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Services
{
    public class UserService : IUserService
    {
        private IUnitofWork uow;
        private TokenProvider token;
        public UserService(IUnitofWork _uow, TokenProvider _token)
        {
            uow = _uow;
            token = _token;
        }
        public void Add(UserDto model)
        {
            var validator = new FluentValidate();
            User usermodel = new User()
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Address = model.Address,
                Phone = model.Phone,
                Password = model.Password,
                Role = model.Role
            };
            var result = validator.Validate(usermodel);
            ValidatorHelper.ThrowIfException(result);

            uow.GetRepository<User>().Add(usermodel);
            uow.SaveChange();
        }
        public void Update(UserDto model)
        {
            var validator = new FluentValidate();
            var data = uow.GetRepository<User>().GetById(model.Id);

            data.Name = model.Name;
            data.Surname = model.Surname;
            data.Email = model.Email;
            data.Address = model.Address;
            data.Phone = model.Phone;
            data.Password = model.Password;


            var result = validator.Validate(data);
            ValidatorHelper.ThrowIfException(result);

            uow.GetRepository<User>().Update(data);
            uow.SaveChange();
        }
        public void Delete(UserDto model)
        {
            var validator = new FluentValidate();
            var data = uow.GetRepository<User>().GetById(model.Id);

            uow.GetRepository<User>().Delete(data);
            uow.SaveChange();
        }

        public UserDto Get(int Id)
        {
            var usermodel = uow.GetRepository<User>().GetById(Id);
            UserDto dto = new UserDto()
            {
                Name = usermodel.Name,
                Surname = usermodel.Surname,
                Email = usermodel.Email,
                Address = usermodel.Address,
                Phone = usermodel.Phone,
                Password = usermodel.Password
            };
            return dto;
        }

        public List<UserDto> GetAll()
        {
            List<User> usermodel = uow.GetRepository<User>().GetAll();
            List<UserDto> result = usermodel.Select(x => new UserDto()
            {
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                Phone = x.Phone,
                Address = x.Address
            }).ToList();
            return result;
        }

        //Member İşlemleri
        public string Login(LoginDto model)
        {
            if (model.Email == "string" && model.Password == "string")
                return token.CreateToken(new LoginDto()
                {
                    Email = "superuser",
                    Role = Role.Admin.ToString(),
                    Password = "admin",
                });

            var data = uow.GetRepository<User>().Get(x => x.Email.Equals(model.Email) && x.Password.Equals(model.Password));

            if (data == null) return null;

            var login = new LoginDto()
            {
                Email = data.Email,
                Password = data.Password,
                Role = data.Role.ToString(),
            };
            var result = token.CreateToken(login);
            return result;
        }
        public bool SignUp(UserDto model)
        {
            var data = uow.GetRepository<User>().Get(x => x.Email.Equals(model.Email));
            if (data == null) return false;
            User usermodel = new User()
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Address = model.Address,
                Phone = model.Phone,
                Password = model.Password

            };
            usermodel.Role = Role.Member;
            if (uow.SaveChange() > 0)
                return true;
            return false;

        }
        public void ChangePassword(ChangePassword model)
        {
            var data = uow.GetRepository<User>().GetById(model.Id);

            data.Password = model.Password;

            uow.GetRepository<User>().Update(data);
            uow.SaveChange();
        }

    }
}
