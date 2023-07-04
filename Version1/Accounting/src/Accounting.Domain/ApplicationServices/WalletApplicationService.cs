using System.Transactions;
using Accounting.Contracts.DTOs;
using Accounting.Contracts.IntegrationEvents;
using Accounting.Domain.Aggregates;
using Accounting.Domain.DataContext;
using Accounting.Domain.Entities;
using Accounting.Domain.Repositories;
using DomainDrivenDesign.Core.Infrastructure;
using DomainDrivenDesign.Core.Messaging;

namespace Accounting.Domain.Services;

public class WalletApplicationService : IWalletApplicationService
{
    private readonly IEventPublisher _eventPublisher;
    private readonly ITimeProvider _timeProvider;
    private readonly IWalletDataContext _walletDataContext;
    private readonly IWalletOwnershipRepository _ownershipRepository;
    private readonly IWalletSettlementRepository _settlementRepository;
    private readonly IWalletTypeRepository _stateRepository;

    public WalletApplicationService(IEventPublisher eventPublisher, ITimeProvider timeProvider, IWalletDataContext dataConext, IWalletOwnershipRepository ownershipRepository, IWalletSettlementRepository settlementRepository, IWalletTypeRepository stateRepository)
    {
        _eventPublisher = eventPublisher;
        _timeProvider = timeProvider;
        _walletDataContext = dataConext;
        _ownershipRepository = ownershipRepository;
        _settlementRepository = settlementRepository;
        _stateRepository = stateRepository;
    }

    public void CreateWallet(CreateWalletDTO createWalletDto)
    {
        using var transactionScope = new TransactionScope();

        if (_walletDataContext.Get(createWalletDto.WalletId) is not null)
            return;

        var wallet = new Wallet
        {
            Id = createWalletDto.WalletId,
            Name = createWalletDto.WalletName,
            Description = createWalletDto.WalletDescription
        };
        _walletDataContext.Save(wallet);
        _ownershipRepository.Save(WalletOwnership.CreateFor(wallet.Id, createWalletDto.OwnerId));
        _settlementRepository.Save(WalletSettlement.CreateFromDate(wallet.Id, _timeProvider.GetUtcNow()));
        _stateRepository.Save(WalletType.CreateStandardWallet(wallet.Id));

        _eventPublisher.PublishAsync(new WalletCreated(wallet.Id, wallet.Name, wallet.Description, createWalletDto.OwnerId));

        transactionScope.Complete();
    }
}
