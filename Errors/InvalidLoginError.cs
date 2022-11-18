namespace BackendPIA.Errors {
    public class InvalidLoginError : ErrorBase {
        public InvalidLoginError(int status, string message) : base(status, message) {}
    }
}