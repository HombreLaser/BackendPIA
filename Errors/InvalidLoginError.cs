namespace BackendPIA.Errors {
    public class InvalidLoginError : ErrorBase {
        public int Status { get { return base.Status; } }
        public string Message { get { return base.Message; }}
        public InvalidLoginError(int status, string message) : base(status, message) {}
    }
}