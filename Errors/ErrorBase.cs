namespace BackendPIA.Errors {
    public abstract class ErrorBase {
        protected int Status { get; }
        protected string Message { get; }

        public ErrorBase(int status, string message) {
            Status = status;
            Message = message;
        }
    }
}