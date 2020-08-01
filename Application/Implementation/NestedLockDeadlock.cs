using Application.Interface;
using DomainModel.Implementation.NestedLockDeadlock;

namespace Application.Implementation.NestedLockDeadlock
{
    public class NestedLockDeadlock : IProgram
    {
        public void Run()
        {
            var account1 = new Account { Id = 1 };
            var account2 = new Account { Id = 2 };
            var transferManager = new TransferManager();
            transferManager.DoDoubleTransfer(account1, account2);
        }
    }
}
