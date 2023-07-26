using System.Transactions;
using Accounting.Domain.Aggregates;
using Accounting.Domain.Events;
using Accounting.Domain.Policies;
using Accounting.Domain.Repositories;
using DomainDrivenDesign.Core.Messaging;
using DomainDrivenDesign.Core.ValueObjects;

namespace Accounting.Domain.EventHandlers;

public class WalletNewSettlementPeriodEventHandler : IEventHandler<WalletCurrentPeriodChanged>
{
    private readonly IWalletSettlementPeriodRepository _settlementPeriodRepository;
    private readonly IWalletTypeRepository _typeRepository;
    private readonly IWalletDebtPolicyFactory _debtPolicyFactory;

    public WalletNewSettlementPeriodEventHandler(IWalletSettlementPeriodRepository settlementPeriodRepository, IWalletTypeRepository walletTypeRepository, IWalletDebtPolicyFactory walletDebtPolicyFactory)
    {
        _settlementPeriodRepository = settlementPeriodRepository;
        _typeRepository = walletTypeRepository;
        _debtPolicyFactory = walletDebtPolicyFactory;
    }

    public async Task HandleAsync(WalletCurrentPeriodChanged @event, CancellationToken cancellationToken)
    {
        using var transaction = new TransactionScope();

        var previousSettlement = await _settlementPeriodRepository.GetByWalletIdAndPeriodAsync(@event.WalletId, @event.CurrentPeriod.GetPreviousPeriod(), cancellationToken);

        await _settlementPeriodRepository.SaveAsync(WalletSettlementPeriod.CreateNewPeriod(@event.WalletId, @event.CurrentPeriod, previousSettlement?.GetSnapshot().ClosingBalance ?? Money.Zero), cancellationToken);

        transaction.Complete();
    }
}
