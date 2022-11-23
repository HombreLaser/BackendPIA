namespace BackendPIA.Errors {
    public class InvalidInputError : ErrorBase {
        public int Status { get { return base.Status; } }
        public string Message { get { return base.Message; }}
        public InvalidInputError(int status, string message) : base(status, message) {}
    }
}