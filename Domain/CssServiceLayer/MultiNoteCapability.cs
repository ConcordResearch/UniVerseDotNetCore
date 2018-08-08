using System;
using IBMU2.UODOTNET;
using UniVerseDotNetCore.Domain.CssServiceLayer.Models;
using UniVerseDotNetCore.Domain.Models;
using UniVerseDotNetCore.Models;

namespace UniVerseDotNetCore.Domain.CssServiceLayer
{
    public static class MultiNoteCapability
    {
        private static string _cssUserName;
        private static string _cssUserPassword;
        private static string _cssHostname;
        private static string _cssAccount;
        private const string CssServiceType = "uvcs";

        // ReSharper disable once UnusedMember.Global
        public static bool SetEnvironmentValues(string userName, string userPassword, string hostname, string account)
        {
            _cssUserName = userName;
            _cssUserPassword = userPassword;
            _cssHostname = hostname;
            _cssAccount = account;

            return true; //in case we need validation in future
        }

        public static CssCommandResult MakeNote(AccountList list, Note note, CssCredentials cssCredentials)
        {

            return MakeMultiNote(list, note, cssCredentials);
        }
        private static CssCommandResult MakeMultiNote(AccountList list, Note note, CssCredentials cssCredentials)
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

                us = UniObjects.OpenSession(lHostName, lUser, lPassword, lAccount, lServiceType);

                UniCommand cmd = us.CreateUniCommand();
                cmd.Command = $"GET-LIST {list.AccountListName}";
                cmd.Execute();

                cmd.Command = "MULTI.NOTES";
                cmd.Execute();

                cmd.Reply($"{note.Message}");

                cmd.Reply("AGHERRERA");

                cmd.Reply("G");

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

    }
}