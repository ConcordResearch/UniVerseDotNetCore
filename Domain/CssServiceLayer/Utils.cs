using System;
using System.IO;
using IBMU2.UODOTNET;
using UniVerseDotNetCore.Domain.CssServiceLayer.Models;
using UniVerseDotNetCore.Domain.Models;
using UniVerseDotNetCore.Models;

namespace UniVerseDotNetCore.Domain.CssServiceLayer
{
    public static class Utils
    {
        public static string GetRandomString()
        {
            string path = Path.GetRandomFileName().ToUpperInvariant();
            path = path.Split(".")[0];
            return path;
        }

        public static CssFileResult GetData(AccountList list, CssAccountFile file, CssCredentials cssCredentials)
        {
            var lHostName = cssCredentials.Hostname;
            var lAccount = cssCredentials.Account;
            var lUser = cssCredentials.User;
            var lPassword = cssCredentials.UserPassword;
            var lServiceType = cssCredentials.ServiceType;

            var response = new CssFileResult();
            var fileResults = new CssFile {FileName = list.AccountListName};

            UniSession us = null;
            try
            {

                //get the session object
                us = UniObjects.OpenSession(lHostName, lUser, lPassword, lAccount, lServiceType);

                // create select list
                UniSelectList selectList = us.CreateUniSelectList(2);
                selectList.GetList(list.AccountListName);

                // read records using array of records ids
                var ids = selectList.ReadListAsStringArray();

                //open file
                UniFile cssFile = us.CreateUniFile(file.FileName);
                var lSet = cssFile.ReadRecords(ids);

                //var dict = us.CreateUniDictionary(file.FileName);
                //var field1 = dict.ReadField(1);

                //use foreach statement to construct data to be displayed
                foreach (UniRecord item in lSet)
                {
                    var record = new CssRecord(item.RecordID, "");
                    var numFields = item.Record.Dcount();
                    for (int i = 0; i < numFields; i++)
                    {
                        var field = item.Record.Extract(i);

                        var valuesCount = field.Dcount();
                        if (valuesCount > 1)
                        {
                            throw new Exception("This field has sub-values" + field);
                        }
                        record.Fields.Add(new CssField(i.ToString(), field.ToString()));
                    }
                    fileResults.Records.Add(record);

                }

                response.FileContents = fileResults;

                return response;
            }
            catch (Exception ex)
            {
                response.CssResponses.Results.Add(new CommandResponse("Exception", ex.ToString()));

            }
            finally
            {
                if (us != null && us.IsActive)
                {
                    UniObjects.CloseSession(us);
                }
            }
            response.CssResponses.Results.Add(new CommandResponse("Error", "There was an error procesing your request."));
            return response;
        }
    }
}
