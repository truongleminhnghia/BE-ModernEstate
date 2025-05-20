

namespace ModernEstate.Common.Models.Responses
{
    public class BrokerRegisterResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public static BrokerRegisterResponse Fail(string message) => new() { Success = false, Message = message };
        public static BrokerRegisterResponse Ok(string message = "") => new() { Success = true, Message = message };

    }
}
