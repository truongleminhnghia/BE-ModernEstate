
namespace ModernEstate.Common.Models.Responses
{
    public class PropertyOwnerRegisterResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public static PropertyOwnerRegisterResponse Fail(string message) => new() { Success = false, Message = message };
        public static PropertyOwnerRegisterResponse Ok(string message = "") => new() { Success = true, Message = message };
    }
}
