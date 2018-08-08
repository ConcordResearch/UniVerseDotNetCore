namespace UniVerseDotNetCore.Domain.Models {
    public class DplCode : IChangeIt2 {
        public DplCode () {

        }

        public DplCode (string dplCode) {
            Code = dplCode;
        }
        private string _code;
        public string Code {
            // ReSharper disable once UnusedMember.Global
            get => _code;
            set {
                _code = value;
                if (value.Length == 11) {
                    DeveloperCode = value.Substring (0, 4);
                    ProjectCode = value.Substring (4, 4);
                    LenderCode = value.Substring (8, 3);
                }
            }
        }
        private string DeveloperCode { get; set; }
        private string ProjectCode { get; set; }
        private string LenderCode { get; set; }
        public string GetDeveloperCode () {
            return DeveloperCode;
        }
        public string GetProjectCode () {
            return ProjectCode;
        }
        public string GetLenderCode () {
            return LenderCode;
        }
    }
}