using System.Transactions;
using Accounting.Domain.Events;
using Accounting.Domain.Repositories;
using DomainDrivenDesign.Core.Messaging;

namespace Accounting.Domain.EventHandlers;

public class WalletOpeningBalanceEventHandler : IEventHandler<WalletBalanceChanged>
{
    private readonly IWalletSettlementPeriodRepository _settlementPeriodRepository;

    public WalletOpeningBalanceEventHandler(IWalletSettlementPeriodRepository settlementPeriodRepository)
    {
        _settlementPeriodRepository = settlementPeriodRepository;
    }

    public async Task HandleAsync(WalletBalanceChanged @event, CancellationToken cancellationToken)
    {
        using var transaction = new TransactionScope();

        var settlement = await _settlementPeriodRepository.GetByWalletIdAndPeriodAsync(@event.WalletId, @event.Period.GetNextPeriod(), cancellationToken);
        if (settlement == null)
            return;

        settlement.ChangeOpeningBalance(@event.Balance);
        await _settlementPeriodRepository.SaveAsync(settlement, cancellationToken);

        transaction.Complete();
    }
}
