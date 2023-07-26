using System.Transactions;
using Accounting.Domain.Events;
using Accounting.Domain.Policies;
using Accounting.Domain.Repositories;
using DomainDrivenDesign.Core.Messaging;

namespace Accounting.Domain.EventHandlers;

////TODO: DELETE
//public class WalletVIPStatusEventHandler : IEventHandler<WalletPromotedToVIP>, IEventHandler<WalletDemotedFromVIP>
//{
//    private readonly IWalletSettlementRepository _settlementRepository;
//    private readonly IWalletSettlementPeriodRepository _settlementPeriodRepository;
//    private readonly IWalletDebtPolicyFactory _walletDebtPolicyFactory;

//    public WalletVIPStatusEventHandler(IWalletSettlementRepository settlementRepository, IWalletSettlementPeriodRepository settlementPeriodRepository, IWalletDebtPolicyFactory walletDebtPolicyFactory)
//    {
//        _settlementRepository = settlementRepository;
//        _settlementPeriodRepository = settlementPeriodRepository;
//        _walletDebtPolicyFactory = walletDebtPolicyFactory;
//    }

//    public async Task HandleAsync(WalletPromotedToVIP @event, CancellationToken cancellationToken)
//    {
//        using var transaction = new TransactionScope();

//        var settlement = await _settlementRepository.GetByWalletIdAsync(@event.WalletId, cancellationToken);
//        var settlementPeriod = await _settlementPeriodRepository.GetByWalletIdAndPeriodAsync(@event.WalletId, settlement.GetCurrentPeriod(), cancellationToken);

//        settlementPeriod.ChangeAvailableDebt(_walletDebtPolicyFactory.GetVIPWalletPolicy());
//        await _settlementRepository.SaveAsync(settlement, cancellationToken);

//        transaction.Complete();
//    }

//    public async Task HandleAsync(WalletDemotedFromVIP @event, CancellationToken cancellationToken)
//    {
//        using var transaction = new TransactionScope();

//        var settlement = await _settlementRepository.GetByWalletIdAsync(@event.WalletId, cancellationToken);
//        var settlementPeriod = await _settlementPeriodRepository.GetByWalletIdAndPeriodAsync(@event.WalletId, settlement.GetCurrentPeriod(), cancellationToken);

//        settlementPeriod.ChangeAvailableDebt(_walletDebtPolicyFactory.GetDefaultPolicy());
//        await _settlementRepository.SaveAsync(settlement, cancellationToken);

//        transaction.Complete();
//    }
//}
