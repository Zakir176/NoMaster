using NoMaster.Models;
using System.Net.Http.Json;

namespace NoMaster;

/// <summary>
/// The main (and only) page of the app.
/// This is where the UI logic lives: tapping the button fetches a new "No" from the API.
/// </summary>
public partial class MainPage : ContentPage
{
    // Shared HttpClient instance – best practice to reuse one for the whole app
    private readonly HttpClient _httpClient = new();

    /// <summary>
    /// Constructor – called when the page is created
    /// </summary>
    public MainPage()
    {
        InitializeComponent(); // Links the XAML UI to this code file (required!)
    }

    /// <summary>
    /// Event handler for when the user taps the "Get a No!" button
    /// </summary>
    private async void OnGetNoClicked(object sender, EventArgs e)
    {
        // Show the loading spinner and give feedback while waiting
        LoadingIndicator.IsRunning = true;
        ReasonLabel.Text = "Fetching a polite rejection...";

        try
        {
            // Trick to prevent caching: add a unique timestamp to the URL
            // This forces a fresh request every time → new funny response!
            var url = $"https://naas.isalman.dev/no?t={DateTime.Now.Ticks}";

            // Call the API and automatically convert JSON → NoResponse object
            var noResponse = await _httpClient.GetFromJsonAsync<NoResponse>(url);

            // Display the funny reason (or fallback if something went wrong)
            ReasonLabel.Text = noResponse?.Reason ?? "The API is feeling shy today...";
        }
        catch (Exception)
        {
            // If no internet or API is down, show a friendly error
            ReasonLabel.Text = "No internet? That's a hard 'No' from the universe. 😅";
        }
        finally
        {
            // Always stop the spinner, even if there was an error
            LoadingIndicator.IsRunning = false;
        }
    }
}