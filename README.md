# Singular.Roulette

In case of use fresh DB please run following steps:
1. Run migrations 
2. Create schema named 'singular' 
3. Finally run following commands for setup configuration tables:


INSERT INTO singular.AccountTypes (`TypeId`, `Name`) VALUES ('1', 'User');
            
INSERT INTO singular.AccountTypes (`TypeId`, `Name`) VALUES ('100', 'Game');
INSERT INTO singular.AccountTypes (`TypeId`, `Name`) VALUES ('101', 'Jackpot');

INSERT INTO singular.Accounts (`TypeId`, `Currency`, `Balance`) VALUES ('100', 'USD', '0');
INSERT INTO singular.Accounts (`TypeId`, `Currency`, `Balance`) VALUES ('101', 'USD', '0');
INSERT INTO singular.TransactionStatuses (`TransactionStatusCode`, `Description`, `isFinished`, `isFailed`) VALUES ('200', 'Success', '1', '0');
INSERT INTO singular.TransactionStatuses (`TransactionStatusCode`, `Description`, `isFinished`, `isFailed`) VALUES ('201', 'BlockedFunds', '0', '1');
INSERT INTO singular.TransactionStatuses (`TransactionStatusCode`, `Description`, `isFinished`, `isFailed`) VALUES ('40', 'Failed', '1', '1');
INSERT INTO singular.TransactionStatuses (`TransactionStatusCode`, `Description`, `isFinished`, `isFailed`) VALUES ('400', 'Reversal', '1', '0'); 
INSERT INTO singular.TransactionTypes (`TransactionTypeId`, `TypeName`) VALUES ('100', 'UserToGame');
INSERT INTO singular.TransactionTypes (`TransactionTypeId`, `TypeName`) VALUES ('101', 'UserToJackpot');
INSERT INTO singular.TransactionTypes (`TransactionTypeId`, `TypeName`) VALUES ('201', 'JackpotToUser');
