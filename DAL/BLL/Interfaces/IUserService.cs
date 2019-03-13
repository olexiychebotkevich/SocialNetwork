﻿using BLL.DTO;
using BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);


        Task<ClaimsIdentity> Authenticate(UserDTO userDto);

        SmallUserDTO GetUser(string Email);

        Task SetInitialData(UserDTO adminDto, List<string> roles);
    }
}
