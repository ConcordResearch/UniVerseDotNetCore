using System;
using IBMU2.UODOTNET;
using UniVerseDotNetCore.Domain.CssServiceLayer.Models;
using UniVerseDotNetCore.Domain.Models;
using UniVerseDotNetCore.Models;

namespace UniVerseDotNetCore.Domain.CssServiceLayer
{
    public static class FilterCapability
    {
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
            var lHostName = cssCredentialsModel.Hostname;
            var lAccount = cssCredentialsModel.Account;
            var lUser = cssCredentialsModel.User;
            var lPassword = cssCredentialsModel.UserPassword;
            var lServiceType = cssCredentialsModel.ServiceType;
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