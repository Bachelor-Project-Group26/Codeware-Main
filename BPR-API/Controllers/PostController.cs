using BPR_API.APIModels;
using BPR_API.DataAccess;
using BPR_API.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BPR_API.Controllers
{
    public class PostController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private DatabaseContext _dbContext;

        public PostController(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = new DatabaseContext();
        }
    }
}
