using BankApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace BankApi.Services
{
    public class AccountService
    {
        private readonly AccountContext _context;
        public AccountService(AccountContext context)
        {
            _context = context;
        }

        public void AddAccounting(AccountingVM accounting)
        {
            //Logic
            #region logic
            var _accounting = new Accounting()
            {
                senderAccountNumber = accounting.senderAccountNumber,
                receiverAccountNumber = accounting.receiverAccountNumber,
                amount = accounting.amount
            };
            
            int sender = accounting.senderAccountNumber;
            int receiver = accounting.receiverAccountNumber;
            int amount = accounting.amount;            

            var isSenderValid = _context.Accounts.Where(x => x.accountNumber == sender).FirstOrDefault();
            var isReceiverValid = _context.Accounts.Where(x => x.accountNumber == receiver).FirstOrDefault();

            bool senderOK = false;
            bool receiverOK = false;
            bool balanceOK = false;
            bool currencyOK = false;

            if (isSenderValid.accountNumber == sender)
            {
                senderOK = true;
            }
            else
            {
                NotFoundSender(sender);
            }
            if (isReceiverValid.accountNumber == receiver)
            {
                receiverOK = true;
            }
            else
            {
                 NotFoundReceiver(receiver);
            }
            if (isSenderValid.currencyCode == isReceiverValid.currencyCode)
            {
                currencyOK = true;
            }
            else
            {
                NotFoundCurrency(isReceiverValid.currencyCode);
            }
            if (isSenderValid.balance >= amount)
            {
                balanceOK = true;
                isSenderValid.balance = isSenderValid.balance - amount;
                isReceiverValid.balance = isReceiverValid.balance + amount;
            }
            else
            {
                NotFoundBalance(sender);
            }
            #endregion
            if (senderOK && receiverOK && currencyOK && balanceOK)
            {
             _context.Add(_accounting);
             _context.SaveChanges();
            } else
            {
                NotFound();
            }            
        }

        public List<Accounting> GetAllAccountings() => _context.Accounting.ToList();

        public Accounting GetAccountingById(int accountingId) => _context.Accounting.FirstOrDefault(n => n.Id == accountingId);

        public Accounting UpdateAccountingById(int accountingId, AccountingVM accounting)
        {
            var _accounting = _context.Accounting.FirstOrDefault(n => n.Id == accountingId);
            if(_accounting != null)
            {
                _accounting.senderAccountNumber = accounting.senderAccountNumber;
                _accounting.receiverAccountNumber = accounting.receiverAccountNumber;
                _accounting.amount = accounting.amount;

                _context.SaveChanges();
            }
            return _accounting;
        }

        //error codes
        #region error
        private void NotFound()
        {
            throw new NotImplementedException();
        }

        private void NotFoundSender(int sender)
        {
            throw new ArgumentException("There is no sender id for " + sender);
        }

        private void NotFoundReceiver(int receiver)
        {
            throw new ArgumentException("There is no receiver id for " + receiver);
        }

        private void NotFoundCurrency(string currency)
        {
            throw new ArgumentException("There is no receiver currency account for " + currency);
        }

        private void NotFoundBalance(int sender)
        {
            throw new ArgumentException("There is no enough balance for this " + sender + " account");
        }
        #endregion
    }
}
