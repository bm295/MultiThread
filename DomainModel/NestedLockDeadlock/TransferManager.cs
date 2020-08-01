using System;
using System.Threading;
using System.Threading.Tasks;

namespace DomainModel.Implementation.NestedLockDeadlock
{
    public class TransferManager
    {
        public TransferManager()
        {
        }

        public void DoDoubleTransfer(Account account1, Account account2)
        {
            Console.WriteLine("Starting transfer...");
            var task1 = Transfer(account1, account2, 500);
            var task2 = Transfer(account2, account1, 600);
            Task.WaitAll(task1, task2);
            Console.WriteLine("Finished transfer!");
        }

        private Task Transfer(Account account1, Account account2, int sum)
        {
            var lock1 = account1.Id < account2.Id ? account1 : account2;
            var lock2 = account1.Id < account2.Id ? account2 : account1;
            var task = Task.Run(() => {                
                lock (lock1)
                {
                    Thread.Sleep(1000);                    
                    lock (lock2)
                    {
                        Console.WriteLine($"Finished transferring sum {sum}");
                    }
                }
            });
            return task;
        }
    }
}