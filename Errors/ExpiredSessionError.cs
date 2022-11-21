namespace BackendPIA.Errors {
    public class ExpiredSessionError : ErrorBase {
        public int Status { get { return base.Status; } }
        public string Message { get { return base.Message; }}
        public ExpiredSessionError(int status, string message) : base(status, message) {}
    }
}