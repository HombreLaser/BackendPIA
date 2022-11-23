namespace BackendPIA.Errors {
    public class NotFoundError : ErrorBase {
        public int Status { get { return base.Status; } }
        public string Message { get { return base.Message; }}
        public NotFoundError(int status, string message) : base(status, message) {}
    }
}