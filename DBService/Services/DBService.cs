using System.Threading.Tasks;
using Grpc.Core;
using DBService.DBManager;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace DBService.Services
{
    // DB.proto에서 생성된 DBBase 상속
    public class MyDBService : DB.DB.DBBase
    {
        private readonly ILogger<MyDBService> _logger;
        private DBManager.DBManager _dbManager;

        public MyDBService(ILogger<MyDBService> logger, DBManager.DBManager dbManager)
        {
            _logger = logger;
            _dbManager = dbManager;
        }

        // CreateUser RPC
        public override Task<DB.CreateUserReply> CreateUser(DB.CreateUserRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"CreateUser called with ID={request.ID}");

            bool retval = _dbManager.CreateCharacter(request.ID);

            if(retval == false)
            {
                Debugger.Break();
            }

            // 예시: 임시 메시지 반환
            var reply = new DB.CreateUserReply
            {
                Message = $"User {request.ID} created successfully"
            };

            

            return Task.FromResult(reply);
        }

        // DeleteUser RPC
        public override Task<DB.DeleteUserReply> DeleteUser(DB.DeleteUserRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"DeleteUser called with ID={request.ID}");

            bool retval = _dbManager.DeleteCharacter(request.ID);

            if(retval == false)
            {
                Debugger.Break();
            }

            var reply = new DB.DeleteUserReply
            {
                ID = request.ID
            };

            return Task.FromResult(reply);
        }

        // UpdateUser RPC
        public override Task<DB.UpdateUserReply> UpdateUser(DB.UpdateUserRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"UpdateUser called with ID={request.ID}, Level={request.Level}, HP={request.Hp}");

            bool retval = _dbManager.UpdateCharacter(request.ID, request.Level, request.Hp);

            if(retval == false)
            {
                Debugger.Break();
            }

            var reply = new DB.UpdateUserReply
            {
                ID = request.ID,
                Level = request.Level,
                Hp = request.Hp
            };

            return Task.FromResult(reply);
        }

        // GetUser RPC
        public override Task<DB.GetUserReply> GetUser(DB.GetUserRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"GetUser called with ID={request.ID}");

            


            // 예시: 임시 데이터 반환
            var reply = new DB.GetUserReply
            {
                ID = request.ID,
                Level = 10, // 임시 값
                Hp = 500    // 임시 값
            };

            return Task.FromResult(reply);
        }
    }
}