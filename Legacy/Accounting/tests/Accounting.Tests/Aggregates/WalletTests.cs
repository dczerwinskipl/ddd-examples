using Accounting.Domain.Aggregates;
using Accounting.DTOs;

namespace Accounting.Tests.Aggregates;

public class WalletTests
{
    [Fact]
    public void WhenDeposit_ShouldSettleTransactionAmount()
    {
        // arrange
        var personId = Guid.NewGuid();
        var wallet = Wallet.Create(
            Guid.NewGuid(), 
            "Wallet name", 
            "Wallet description", 
            new PersonDTO(personId, "FirstName", "MiddleName", "LastName", "1234567")
        );

        // act
        wallet.Deposit(personId, new TransactionDTO(Guid.NewGuid(), DateTimeOffset.UtcNow, 200));

        // assert
        Assert.Equal(200, wallet.Balance);
    }
}
