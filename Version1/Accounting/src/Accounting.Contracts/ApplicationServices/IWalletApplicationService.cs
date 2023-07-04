using Accounting.Contracts.DTOs;

namespace Accounting.Domain.Services
{
    public interface IWalletApplicationService
    {
        void CreateWallet(CreateWalletDTO createWalletDto);
    }
}