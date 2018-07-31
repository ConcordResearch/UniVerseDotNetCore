using System;
using System.IO;
using System.Linq;
using IBMU2.UODOTNET;
using UniVerseDotNetCore.Domain.CssServiceLayer.Models;
using UniVerseDotNetCore.Domain.Models;
using UniVerseDotNetCore.Models;

namespace UniVerseDotNetCore.Domain.CssServiceLayer
{
    public static class CssCaller
    {
        public static string GetRandomString()
        {
            string path = Path.GetRandomFileName().ToUpperInvariant();
            path = path.Split(".")[0];
            return path;
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

        public static CssCommandResult ChangeLenderCode(CSSAccountFile file, AccountList list, LenderCode newCode, Note changeNote, CssCredentialsModel cssCredentialsModel)
        {
            return ChangeIt2(file, list, "", newCode.Code, "", changeNote, cssCredentialsModel);
        }
        public static CssCommandResult ChangeProjectCode(CSSAccountFile file, AccountList list, ProjectCode newCode, Note changeNote, CssCredentialsModel cssCredentialsModel)
        {
            return ChangeIt2(file, list, newCode.Code, "", "", changeNote, cssCredentialsModel);
        }
        public static CssCommandResult ChangeAccountCode(CSSAccountFile file, AccountList list, AccountCode newCode, Note changeNote, CssCredentialsModel cssCredentialsModel)
        {
            return ChangeIt2(file, list, "", "", newCode.Code, changeNote, cssCredentialsModel);
        }
        private static CssCommandResult ChangeIt2(CSSAccountFile file, AccountList list, string projectCode, string lenderCode, string accountCode, Note changeNote, CssCredentialsModel cssCredentialsModel)
        {
            var result = new CssCommandResult();
            var lHostName = cssCredentialsModel.Hostname;
            var lAccount = cssCredentialsModel.Account;
            var lUser = cssCredentialsModel.User;
            var lPassword = cssCredentialsModel.UserPassword;
            var lServiceType = cssCredentialsModel.ServiceType;
            string fileInitial = file.FileName.ToCharArray()[0].ToString();
            UniSession us = null;

            try
            {
                //get the session object
                us = UniObjects.OpenSession(lHostName, lUser, lPassword, lAccount, lServiceType);

                UniCommand cmd = us.CreateUniCommand();
                cmd.Command = $"CHANGE.IT2";
                cmd.Execute();
                //result.Results.Add(new CommandResponse(cmd.Command, cmd.Response));

                //ENTER THE LIST NAME YOU ARE USING 
                cmd.Reply($"{list.AccountListName}");
                //result.Results.Add(new CommandResponse("", cmd.Response));

                //'C'ONTRACT OR 'M'AINTENANCE IDS? ?
                cmd.Reply($"{fileInitial}");
                //result.Results.Add(new CommandResponse($"{fileInitial}", cmd.Response));

                //CHANGE MAINTENANCE TOO? (IF APPLICABLE) ? 
                cmd.Reply("N");
                //result.Results.Add(new CommandResponse("N", cmd.Response));

                //ENTER NEW DEVELOPER CODE, IF ANY ?
                cmd.Reply("");
                //result.Results.Add(new CommandResponse("Developer", cmd.Response));

                //Project?
                cmd.Reply($"{projectCode}");
                //result.Results.Add(new CommandResponse($"{projectCode}", cmd.Response));

                //Lender
                cmd.Reply($"{lenderCode}");
                //result.Results.Add(new CommandResponse($"{lenderCode}", cmd.Response));

                //AccountCode
                cmd.Reply($"{accountCode}");
                //result.Results.Add(new CommandResponse($"{accountCode}", cmd.Response));

                //Note
                cmd.Reply($"{changeNote.Message}");
                //result.Results.Add(new CommandResponse($"{changeNote.Message}", cmd.Response));


                //Note Type
                cmd.Reply("G");
                //result.Results.Add(new CommandResponse($"NoteType", cmd.Response));

                if(cmd.Response.Contains("OKAY TO PROCEED? (Y/N)")){
                    //OKAY TO PROCEED? (Y/N) 
                    cmd.Reply("Y");
                }
                else
                {
                    throw new Exception($"Could not get confirmation message. Raw result: {cmd.Response}");
                }

                int lineNumber = 0;
                foreach (var line in cmd.Response.Split("\r\n"))
                {
                    result.Results.Add(new CommandResponse($"Response Line: {++lineNumber}", line));

                }

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


        public static FileResult GetData(AccountList list, CSSAccountFile file, CssCredentialsModel cssCredentialsModel)
        {

            var lHostName = cssCredentialsModel.Hostname;
            var lAccount = cssCredentialsModel.Account;
            var lUser = cssCredentialsModel.User;
            var lPassword = cssCredentialsModel.UserPassword;
            var lServiceType = cssCredentialsModel.ServiceType;

            var response = new FileResult();
            var fileResults = new CssFile();
            fileResults.FileName = list.AccountListName;

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
                response.Results.Add(new CommandResponse("Exception", ex.ToString()));

            }
            finally
            {
                if (us != null && us.IsActive)
                {
                    UniObjects.CloseSession(us);
                }
            }
            response.Results.Add(new CommandResponse("Error", "There was an error procesing your request."));
            return response;
        }
    }
}
