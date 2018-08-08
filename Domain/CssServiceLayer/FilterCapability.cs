using System;
using System.Collections.Generic;
using System.Linq;
using IBMU2.UODOTNET;
using UniVerseDotNetCore.Domain.CssServiceLayer.Models;
using UniVerseDotNetCore.Domain.Models;
using UniVerseDotNetCore.Models;

namespace UniVerseDotNetCore.Domain.CssServiceLayer
{
    public static class FilterCapability
    {
        private static string _cssUserName;
        private static string _cssUserPassword;
        private static string _cssHostname;
        private static string _cssAccount;
        private const string CssServiceType = "uvcs";

        public static bool SetEnvironmentValues(string userName, string userPassword, string hostname, string account)
        {
            _cssUserName = userName;
            _cssUserPassword = userPassword;
            _cssHostname = hostname;
            _cssAccount = account;

            return true; //in case we need validation in future
        }

        public static CssCommandResult FilterMaint(AccountFilterCriteria<Maint> mainFilter, AccountList saveList,
                CssCredentials cssCredentials)
        {
            return Filter("MACTIVE.IDX", mainFilter.DeveloperCode, mainFilter.ToString(), saveList, cssCredentials);
        }

        public static CssCommandResult FilterContracts(AccountFilterCriteria<Contracts> contractsFilter, AccountList saveList,
            CssCredentials cssCredentials)
        {
            return Filter("ACTIVE.IDX", contractsFilter.DeveloperCode, contractsFilter.ToString(), saveList, cssCredentials);
        }
        public static CssCommandResult MakeCustomGetList(IEnumerable<string> list, AccountList saveList, CssCredentials cssCredentials)
        {
            return CustomGetList(list, saveList, cssCredentials);
        }
        private static CssCommandResult Filter(string indexFile, string developerCode, string filterCriteria,
            AccountList saveListName, CssCredentials cssCredentials)
        {
            var result = new CssCommandResult();
            var lHostName = _cssHostname ?? cssCredentials.Hostname;
            var lAccount = _cssAccount ?? cssCredentials.Account;
            var lUser = _cssUserName ?? cssCredentials.User;
            var lPassword = _cssUserPassword ?? cssCredentials.UserPassword;
            const string lServiceType = CssServiceType;

            UniSession us = null;

            try
            {
                Console.WriteLine($"{lHostName}, {lUser}, {lAccount}, {lServiceType}");

                //get the session object
                us = UniObjects.OpenSession(lHostName, lUser, lPassword, lAccount, lServiceType);

                UniCommand cmd = us.CreateUniCommand();
                cmd.Command = $"SELECT {indexFile} WITH DEV.CODE = \"{developerCode}\"";
                cmd.Execute();
                result.Results.Add(new CommandResponse(cmd.Command, cmd.Response));

                cmd.Command = $"QSELECT {indexFile}";
                cmd.Execute();
                result.Results.Add(new CommandResponse(cmd.Command, cmd.Response));

                cmd.Command = filterCriteria;
                cmd.Execute();
                result.Results.Add(new CommandResponse(cmd.Command, cmd.Response));

                cmd.Command = $"SAVE-LIST {saveListName}";
                cmd.Execute();
                result.Results.Add(new CommandResponse(cmd.Command, cmd.Response));

                return result;

            }
            catch (Exception ex)
            {
                result.Results.Add(new CommandResponse("Exception", ex.ToString()));
            }
            finally
            {
                if (us != null && us.IsActive)
                {
                    UniObjects.CloseSession(us);
                }
            }

            result.Results.Add(new CommandResponse("Error", "There was an error procesing your request."));
            return result;
        }

        private static CssCommandResult CustomGetList(IEnumerable<string> accountList, AccountList saveListName, CssCredentials cssCredentials)
        {
            var result = new CssCommandResult();
            var lHostName = _cssHostname ?? cssCredentials.Hostname;
            var lAccount = _cssAccount ?? cssCredentials.Account;
            var lUser = _cssUserName ?? cssCredentials.User;
            var lPassword = _cssUserPassword ?? cssCredentials.UserPassword;
            const string lServiceType = CssServiceType;

            UniSession us = null;

            try
            {
                Console.WriteLine($"{lHostName}, {lUser}, {lAccount}, {lServiceType}");
                //get the session object
                us = UniObjects.OpenSession(lHostName, lUser, lPassword, lAccount, lServiceType);

                UniCommand cmd = us.CreateUniCommand();
                cmd.Command = $"EDIT-LIST {saveListName.AccountListName}";
                cmd.Execute();
                result.Results.Add(new CommandResponse(cmd.Command, cmd.Response));

                cmd.Reply("I"); //Get list ready for insert

                foreach (var acct in accountList)
                {
                    if (!acct.Contains("=")) continue;
                    var account = acct.Split("=")[1].Replace("]","");
                    cmd.Reply($"{account}");
                }
                cmd.Reply(""); // Send empty marker to close list

                cmd.Reply("FI"); // Save list
                result.Results.Add(new CommandResponse(cmd.CommandStatus.ToString(), cmd.Response));

                return result;

            }
            catch (Exception ex)
            {
                result.Results.Add(new CommandResponse("Exception", ex.ToString()));
            }
            finally
            {
                if (us != null && us.IsActive)
                {
                    UniObjects.CloseSession(us);
                }
            }

            result.Results.Add(new CommandResponse("Error", "There was an error procesing your request."));
            return result;
        }

    }
}