using System;
using IBMU2.UODOTNET;
using UniVerseDotNetCore.Domain.CssServiceLayer.Models;
using UniVerseDotNetCore.Domain.Models;
using UniVerseDotNetCore.Models;

namespace UniVerseDotNetCore.Domain.CssServiceLayer
{
    public static class FilterCapability
    {
        private static string _cssUserName = null;
        private static string _cssUserPassword = null;
        private static string _cssHostname = null;
        private static string _cssAccount = null;
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
                CssCredentialsModel cssCredentialsModel)
        {
            return Filter("MACTIVE.IDX", mainFilter.DeveloperCode, mainFilter.ToString(), saveList, cssCredentialsModel);
        }

        public static CssCommandResult FilterContracts(AccountFilterCriteria<Contracts> contractsFilter, AccountList saveList,
            CssCredentialsModel cssCredentialsModel)
        {
            return Filter("ACTIVE.IDX", contractsFilter.DeveloperCode, contractsFilter.ToString(), saveList, cssCredentialsModel);
        }

        private static CssCommandResult Filter(string indexFile, string developerCode, string filterCriteria,
            AccountList saveListName, CssCredentialsModel cssCredentialsModel)
        {
            var result = new CssCommandResult();
            var lHostName = _cssHostname ?? cssCredentialsModel.Hostname;
            var lAccount = _cssAccount ?? cssCredentialsModel.Account;
            var lUser = _cssUserName ?? cssCredentialsModel.User;
            var lPassword = _cssUserPassword ?? cssCredentialsModel.UserPassword;
            const string lServiceType = CssServiceType;

            UniSession us = null;

            try
            {
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
    }
}