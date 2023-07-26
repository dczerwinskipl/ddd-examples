using System.Transactions;
using Accounting.Contracts.DTOs;
using Accounting.Contracts.IntegrationEvents;
using Accounting.Contracts.ValueObjects;
using Accounting.Domain.DataContext;
using Accounting.Domain.Entities;
using DomainDrivenDesign.Core.Messaging;

namespace Accounting.Domain.ApplicationServices;

public class WalletOwnerApplicationService
{
    private readonly IWalletOwnerDataContext _dataContext;
    private readonly IEventPublisher _eventPublisher;

    public WalletOwnerApplicationService(IWalletOwnerDataContext dataContext, IEventPublisher eventPublisher)
    {
        _dataContext = dataContext;
        _eventPublisher = eventPublisher;
    }

    public async Task AddWalletOwnerAsync(AddWalletOwner addWalletOwner)
    {
        using var transaction = new TransactionScope();
        if (addWalletOwner.WalletOwnerId is not null && await _dataContext.GetAsync(addWalletOwner.WalletOwnerId) is not null)
            return;

        var walletOwner = new WalletOwner
        {
            Id = addWalletOwner.WalletOwnerId ?? WalletOwnerId.NewId(),
            PersonalData = addWalletOwner.PersonalData
        };
        await _dataContext.SaveAsync(walletOwner);
        await _eventPublisher.PublishAsync(new WalletOwnerAdded(walletOwner.Id, walletOwner.PersonalData));

        transaction.Complete();
    }

    public async Task EditWalletOwnerAsync(EditWalletOwner editWalletOwner)
    {
        using var transaction = new TransactionScope();

        var walletOwner = await _dataContext.GetAsync(editWalletOwner.WalletOwnerId) ?? throw new ArgumentException();
        walletOwner.PersonalData = editWalletOwner.PersonalData;

        await _dataContext.SaveAsync(walletOwner);
        await _eventPublisher.PublishAsync(new WalletOwnerPersonalDataChanged(walletOwner.Id, walletOwner.PersonalData));

        transaction.Complete();
    }

    public async Task ChangeWalletOwnerPhoneNumberAsync(WalletOwnerId walletOwnerId, string phoneNumber)
    {
        using var transaction = new TransactionScope();

        var walletOwner = await _dataContext.GetAsync(walletOwnerId) ?? throw new ArgumentException();
        walletOwner.PersonalData = walletOwner.PersonalData with { PhoneNumber = phoneNumber };

        await _dataContext.SaveAsync(walletOwner);
        await _eventPublisher.PublishAsync(new WalletOwnerPersonalDataChanged(walletOwner.Id, walletOwner.PersonalData));

        transaction.Complete();
    }
}
