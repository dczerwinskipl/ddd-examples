using System.Transactions;
using Accounting.Contracts.DTOs;
using Accounting.Domain.DomainServices;
using Accounting.Domain.Policies;
using Accounting.Domain.Repositories;
using DomainDrivenDesign.Core.Infrastructure;

namespace Accounting.Domain.Services;

public class WalletTransactionApplicationService : IWalletTransactionApplicationService
{
    private readonly ITimeProvider _timeProvider;
    private readonly IWalletOwnershipRepository _ownershipRepository;
    private readonly IWalletSettlementPeriodRepository _settlementRepository;
    private readonly IWalletTypeRepository _typeRepository;
    private readonly IWalletDebtPolicyFactory _debtPolicyFactory;
    private readonly IWalletTransactionDomainService _transactionDomainService;

    public WalletTransactionApplicationService(ITimeProvider timeProvider, IWalletOwnershipRepository ownershipRepository, IWalletSettlementPeriodRepository settlementRepository, IWalletTypeRepository typeRepository, IWalletDebtPolicyFactory debtPolicyFactory, IWalletTransactionDomainService transactionDomainService)
    {
        _timeProvider = timeProvider;
        _ownershipRepository = ownershipRepository;
        _settlementRepository = settlementRepository;
        _typeRepository = typeRepository;
        _debtPolicyFactory = debtPolicyFactory;
        _transactionDomainService = transactionDomainService;
    }

    public void WithdrawMoney(WithdrawMoneyDTO withdrawMoneyDTO)
    {
        using var transactionScope = new TransactionScope();

        var date = _timeProvider.GetUtcNow();
        var ownership = _ownershipRepository.GetByWalletId(withdrawMoneyDTO.WalletId);
        var type = _typeRepository.GetByWalletId(withdrawMoneyDTO.WalletId);
        var settlement = _settlementRepository.GetByWalletIdAndPeriod(withdrawMoneyDTO.WalletId, date);

        _settlementRepository.Save(_transactionDomainService.WithdrawMoney(withdrawMoneyDTO, date, ownership, type, settlement, _debtPolicyFactory));

        transactionScope.Complete();
    }

    public void DepositMoney(DepositMoneyDTO depositMoneyDTO)
    {
        using var transactionScope = new TransactionScope();

        var ownership = _ownershipRepository.GetByWalletId(depositMoneyDTO.WalletId);
        var settlement = _settlementRepository.GetByWalletIdAndPeriod(depositMoneyDTO.WalletId, depositMoneyDTO.Date);

        _settlementRepository.Save(_transactionDomainService.DepositMoney(depositMoneyDTO, ownership, settlement));

        transactionScope.Complete();
    }
}
