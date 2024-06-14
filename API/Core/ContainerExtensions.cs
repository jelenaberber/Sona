﻿using Application;
using Application.UseCases.Commands.RestauranServices;
using Application.UseCases.Commands.Rooms;
using Application.UseCases.Commands.Users;
using Application.UseCases.Queries;
using Implementation;
using Implementation.Logging.UseCase;
using Implementation.UseCases.Commands.RestaurantServices;
using Implementation.UseCases.Commands.Rooms;
using Implementation.UseCases.Commands.Users;
using Implementation.UseCases.Queries;
using Implementation.Validators;
using System.IdentityModel.Tokens.Jwt;

namespace API.Core

{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<CreateRoomDtoValidator>();
            services.AddTransient<ICreateRoomCommand, EfCreateRoomCommand>();
            services.AddTransient<UpdateRoomValidator>();
            services.AddTransient<IUpdateRoomCommand, EfUpdateRoomCommand>();
            services.AddTransient<IGetRoomsQuery, EfGetRoomsQuery>();

            services.AddTransient<UseCaseHandler>();
            services.AddTransient<IUseCaseLogger, DBUseCaseLogger>();

            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<RegisterUserDtoValidator>();
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();

            services.AddTransient<CreateRestauranServicesDtoValidator>();
            services.AddTransient<ICreateRestaurantServicesCommand, EfCreateRestauranServicesCommand>();
            services.AddTransient<UpdateRestaurantServicesDtoValidator>();
            services.AddTransient<IUpdateRestaurantServicesCommand, EfUpdateRestaurantServiceCommand>();
        }

        public static Guid? GetTokenId(this HttpRequest request)
        {
            if (request == null || !request.Headers.ContainsKey("Authorization"))
            {
                return null;
            }

            string authHeader = request.Headers["Authorization"].ToString();

            if (authHeader.Split("Bearer ").Length != 2)
            {
                return null;
            }

            string token = authHeader.Split("Bearer ")[1];

            var handler = new JwtSecurityTokenHandler();

            var tokenObj = handler.ReadJwtToken(token);

            var claims = tokenObj.Claims;

            var claim = claims.First(x => x.Type == "jti").Value;

            var tokenGuid = Guid.Parse(claim);

            return tokenGuid;
        }
    }
}
