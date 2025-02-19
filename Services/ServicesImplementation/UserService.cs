using AutoMapper;
using HabitsTracker.DTOs.CreateDto;
using HabitsTracker.DTOs.ResponseDto;
using HabitsTracker.DTOs.UpdateDto;
using HabitsTracker.Models;
using HabitsTracker.Repository.GenericRepository;
using HabitsTracker.Repository.Implementations;
using HabitsTracker.Services.IServices;
using Microsoft.IdentityModel.Tokens;

namespace HabitsTracker.Services.ServicesImplementation
{
    public class UserService(IGenericRepository<User> genericRepository, IMapper mapper) : IUserService
    {
        private readonly IGenericRepository<User> _genericRepository = genericRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseUserDto> GetAllUsersAsyc()
        {
            try
            {
                /* log fetching */
                var user = await _genericRepository.GetAllAsync();
                /* validating */
                if(user is null)
                {
                    //middleware or empty
                }
                //map the user to ResponseUserDto
                /* log return users */ 
                //return users (for now)
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception($"here is where the excepcions comes, {ex}");
            }
        }

        public Task<ResponseUserDto> GetUsersByIdAsync(int id)
        {            
            //try
                /*log validating*/
                //validate if there is a match with the requested Id
                    //map the entity to the dto for response
                    /*log returned user*/
                    //If so, return the current user
                //
                /*log current user didn't found*/
                //empty list[] (?)
            //catch
                //middleware to redirect

            throw new NotImplementedException();
        }
        public Task<CreateUserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            //try
                /*log validating*/
                //validate there is not another user with the same email
                //if there is another user with that email
                    /*Unable to create because posting a user with the same email*/
                    //unable to create another account with the same email
                /*log creating*/
                //map the recieved data to entity
                //create user
                /*log created*/
            //catch
                //middleware to redirect
            throw new NotImplementedException();
        }
        public Task<UpdateUserDto> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            //try
                /* validating log */
                //validate the requested id
                //if no matches
                    /* log didn't found */
                    //didn't found any user with that id
                //if it marches 
                    /* updating log */
                    //map the recieved data to entity
                    //update user
                    /* log updated */
            //catch
                //middleware to redirect
            throw new NotImplementedException();
        }
        public Task DeleteUserAsync(int id)
        {
            //try 
                /* validating log */
                //validate the requested id 
                //if no matches 
                    /* log didn't found */
                    //didn't found any user with that id
                //if matches 
                    /* log deleting*/
                    //delete user
            //catch
                //middlware to redirect
            throw new NotImplementedException();
        }
    }
}