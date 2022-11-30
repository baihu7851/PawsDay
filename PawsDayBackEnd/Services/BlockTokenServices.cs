using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.DTO.Account;
using System;

namespace PawsDayBackEnd.Services
{
    public class BlockTokenServices
    {
        private readonly IRepository<BlockToken> _blockToken;
        public BlockTokenServices(IRepository<BlockToken> blockToken)
        {
            _blockToken = blockToken;
        }

        public void AddBlockToken(LogoutDto request)
        {
            var block =_blockToken.Add(new BlockToken
            {
                Token = request.Token,
                ExpireTime=DateTimeOffset.UtcNow
            });

        }
    }
}
