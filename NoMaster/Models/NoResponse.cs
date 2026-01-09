namespace NoMaster.Models;

/// <summary>
/// Simple data model to hold the response from the No-as-a-Service API.
/// The API returns JSON like: { "reason": "Some funny rejection text" }
/// </summary>
public class NoResponse
{
    // The actual funny "No" message from the API
    public string Reason { get; set; } = string.Empty;
}