using NoMaster.Models;
using System.Net.Http.Json;

namespace NoMaster;

public partial class MainPage : ContentPage
{
    private readonly HttpClient _httpClient = new();

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnGetNoClicked(object sender, EventArgs e)
    {
        LoadingIndicator.IsRunning = true;
        ReasonLabel.Text = "Fetching a polite rejection...";

        try
        {
            // Simple anti-cache trick: add a unique query param
            var url = $"https://naas.isalman.dev/no?t={DateTime.Now.Ticks}";
            var noResponse = await _httpClient.GetFromJsonAsync<NoResponse>(url);

            ReasonLabel.Text = noResponse?.Reason ?? "The API is feeling shy today...";
        }
        catch (Exception)
        {
            ReasonLabel.Text = "No internet? That's a hard 'No' from the universe. 😅";
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
        }
    }
}