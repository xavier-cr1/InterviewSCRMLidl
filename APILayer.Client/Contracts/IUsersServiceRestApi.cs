using APILayer.Entities;
using APILayer.Entities.UserService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APILayer.Client.Contracts
{
    public interface IUsersServiceRestApi
    {
        Task<SwaggerResponse> PostNewUserAsync(User userRequest);

        Task<SwaggerResponse<UserListResponse>> GetUserListAsync();
    }
}
