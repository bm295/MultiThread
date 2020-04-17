﻿using DomainModel.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Implementation.NestedLockDeadlock
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
