using System;
using System.Collections;
using System.IO;
using System.Text;
using IBMU2.UODOTNET;

using Newtonsoft.Json;
using UniVerseDotNetCore.Models;


namespace UniVerseDotNetCore.CSSLayer
{
    public static class CssCaller
    {
        public static string GetRandomString()
        {
            string path = Path.GetRandomFileName().ToUpperInvariant();
            path = path.Split(".")[0];
            return path;
        }
        public static string FilterMaint(AccountFilterCriteria<Maint> mainFilter, AccountList saveList,
            CssCredentialsModel cssCredentialsModel)
        {
            return Filter("MACTIVE.IDX", mainFilter.DeveloperCode, mainFilter.ToString(), saveList, cssCredentialsModel);
        }
        public static string FilterContracts(AccountFilterCriteria<Contracts> contractsFilter, AccountList saveList,
            CssCredentialsModel cssCredentialsModel)
        {
            return Filter("ACTIVE.IDX", contractsFilter.DeveloperCode, contractsFilter.ToString(), saveList, cssCredentialsModel);
        }
        private static string Filter(string indexFile, string developerCode, string filterCriteria, AccountList saveListName, CssCredentialsModel cssCredentialsModel)
        {

            var lHostName = cssCredentialsModel.Hostname;
            var lAccount = cssCredentialsModel.Account;
            var lUser = cssCredentialsModel.User;
            var lPassword = cssCredentialsModel.UserPassword;
            var lServiceType = cssCredentialsModel.ServiceType;
            UniSession us = null;
            var lStrValue = new StringBuilder();

            try
            {
                //get the session object
                us = UniObjects.OpenSession(lHostName, lUser, lPassword, lAccount, lServiceType);

                UniCommand cmd = us.CreateUniCommand();
                cmd.Command = $"SELECT {indexFile} WITH DEV.CODE = \"{developerCode}\"";
                cmd.Execute();
                lStrValue.Append($"{cmd.Response} ");

                cmd.Command = $"QSELECT {indexFile}";
                cmd.Execute();
                lStrValue.Append($"{cmd.Response} ");

                cmd.Command = filterCriteria;
                cmd.Execute();
                lStrValue.Append($"{cmd.Response} ");

                cmd.Command = $"SAVE-LIST {saveListName}";
                cmd.Execute();
                lStrValue.Append($"{cmd.Response} ");

                return lStrValue.ToString();

            }
            catch (Exception ex)
            {
                lStrValue.Append(ex.Message);

            }
            finally
            {
                if (us != null && us.IsActive)
                {
                    UniObjects.CloseSession(us);
                }
            }

            return lStrValue.Append("There was an error procesing your request.").ToString();
        }
        public static string GetFileData(AccountList accountlist, CssCredentialsModel cssCredentialsModel)
        {

            var lHostName = cssCredentialsModel.Hostname;
            var lAccount = cssCredentialsModel.Account;
            var lUser = cssCredentialsModel.User;
            var lPassword = cssCredentialsModel.UserPassword;
            var lServiceType = cssCredentialsModel.ServiceType;

            var lStrValue = new StringBuilder();
            var lRecIdList = new ArrayList();
            UniSession us = null;
            try
            {

                //get the session object
                us = UniObjects.OpenSession(lHostName, lUser, lPassword, lAccount, lServiceType);

                //open file
                UniFile fl = us.CreateUniFile(accountlist.AccountListName);

                // create select list
                var sl = us.CreateUniSelectList(2);
                sl.Select(fl);

                // create array of Record IDs
                var lLastRecord = sl.LastRecordRead;
                while (!lLastRecord)
                {
                    var s = sl.Next();
                    lRecIdList.Add(s);
                    lLastRecord = sl.LastRecordRead;
                }
                var lRecIdArray = new string[lRecIdList.Count];
                lRecIdList.CopyTo(0, lRecIdArray, 0, lRecIdList.Count);

                // read records using array of records ids
                var lSet = fl.ReadRecords(lRecIdArray);

                //use foreach statement to construct data to be displayed
                foreach (UniRecord item in lSet)
                {
                    lStrValue.Append(item.Record.ToString());
                    lStrValue.Append("\r\n");

                }
                return lStrValue.ToString();
            }
            catch (Exception ex)
            {
                lStrValue.Append(ex.Message);

            }
            finally
            {
                if (us != null && us.IsActive)
                {
                    UniObjects.CloseSession(us);
                }
            }
            return lStrValue.Append("There was an error procesing your request.").ToString();
        }
    }
}
