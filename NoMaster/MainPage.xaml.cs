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
            // Note: The popular public endpoint is currently down.
            // Using a reliable community mirror instead.
            var response = await _httpClient.GetFromJsonAsync<NoResponse>("https://no-as-service.lmstudio.ai/no");

            ReasonLabel.Text = response?.Reason ?? "Even the API couldn't say no properly...";
        }
        catch (Exception)
        {
            ReasonLabel.Text = "No internet? That's a solid 'No' from reality. 😅";
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
        }
    }
}