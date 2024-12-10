using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Application.Services.Interfaces.Authentication;
using eCommerceApp.Application.Services.Interfaces.Logging;
using eCommerceApp.Application.Validations;
using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interfaces.Authentication;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Services.Implementations.Authentication
{
    public class AuthenticationService(ITokenManagement tokenManagement,IUserManagement userManagement,
        IRoleManagement roleManagement,IAppLogger<AuthenticationService> logger,IMapper mapper,IValidator<CreateUser>createUserValidator,
        IValidator<LoginUser>loginUserValidator,IValidationService validationService) : IAuthenticationService
    {
        public async Task<ServiceResponse> CreateUser(CreateUser user)
        {
            var _validationResult=await validationService.ValidateAsync(user,createUserValidator);
            if(!_validationResult.Success) return _validationResult;
            var mappedModel=mapper.Map<AppUser>(user);
            mappedModel.UserName = user.Email;
            mappedModel.PasswordHash=user.Password;
            
            var result=await userManagement.CreateUser(mappedModel);
            if (!result)
                return new ServiceResponse { Message = "Email Address might be already in use or unknown error occured" };
            
            var _user=await userManagement.GetUserByEmail(user.Email);
            var users = await userManagement.GetAllUsers();
            bool assignedRole = await roleManagement.AddUserToRole(_user!, users.Count() > 1 ? "User" : "Admin");

            if (!assignedRole)
            {
                int  removeUserResult=await userManagement.RemoveUserByEmail(_user!.Email!);
                if (removeUserResult <= 0)
                {
                    // error occured while rolling back changes
                    // then log the error
                    logger.LogError(new Exception($" User with Email as {_user.Email} failed to be remove as a result of role assigning issue"),
                        "User couled not be assigned Role");
                    return new ServiceResponse { Message = "Error occurred in creating account" };
                }

                    
            }
            return new ServiceResponse { Success = true, Message = "Account created!" };
            // verfiry Email
        
        }

        public async Task<LoginResponse> LoginUser(LoginUser user)
        {
            var _validationResult = await validationService.ValidateAsync(user, loginUserValidator);
            if (!_validationResult.Success)
                return new LoginResponse(Message: _validationResult.Message);

            var mappedModel = mapper.Map<AppUser>(user);
            mappedModel.PasswordHash = user.Password;
            bool loginResult = await userManagement.LoginUser(mappedModel);
            if (!loginResult)
                return new LoginResponse (Message: "Email not found or invalid credentials");
            var _user =await userManagement.GetUserByEmail(user.Email);
            var claims =await userManagement.GetUserClaims(_user!.Email!);

            string jwtToken =tokenManagement.GenerateToken(claims);// return jwt token 
            string refreshToken = tokenManagement.GetRefreshToken();//return rondom string 
            
            int saveToken = 0;
            bool userTokenCheck=await tokenManagement.ValidateRefreshToken(refreshToken);// check if refresh token found 
            if (userTokenCheck)// to avoid duplicate refersh token to the same user
                saveToken = await tokenManagement.UpdateRefreshToken(_user.Id, refreshToken);
            else
                saveToken = await tokenManagement.AddRefreshToken(_user.Id, refreshToken);//genrate refresh token and save it database

            
            return saveToken <= 0 ? new LoginResponse(Message: "internal error occured while authenticating"):
                new LoginResponse(Success: true,Token:jwtToken,RefreshToken:refreshToken);
 
        }

       

        public async Task<LoginResponse> RegenrateToken(string refreshToken)// regenrate new jwt token ,update refresh token 
        {
            bool validateTokenResult = await tokenManagement.ValidateRefreshToken(refreshToken);
            if (!validateTokenResult)
                return new LoginResponse(Message: "invalid token");
            
            string userId =await tokenManagement.GetUserIdByRefreshToken(refreshToken);
            AppUser? user = await userManagement.GetUserById(userId);
            var claims = await userManagement.GetUserClaims(user!.Email!);
            string newJwtToken = tokenManagement.GenerateToken(claims);
            string newRefreshToken = tokenManagement.GetRefreshToken();
            await tokenManagement.UpdateRefreshToken(userId,newRefreshToken);
            return new LoginResponse(Success: true,Token:newJwtToken,RefreshToken:newRefreshToken);

        
        }
    }

}
