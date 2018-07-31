namespace UniVerseDotNetCore.Models
{
    public class CommandResponse
    {
        public CommandResponse(string action, string response)
        {
            Action = action;
            Resopose = response;
        }
        public string Action { get; set; }
        public string  Resopose { get; set; }
    }
}