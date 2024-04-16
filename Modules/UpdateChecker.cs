using System.Diagnostics;
using System.Reflection;
using Octokit;

namespace Mouse_Mender.Modules;

public class UpdateChecker
{
    // Declaring
    private Version version = Assembly.GetExecutingAssembly().GetName().Version;
    private Version versionFormatted;
    Version latestGitHubVersion;
    int versionComparison;
    private string url = "https://github.com/TheCodingCarson/MouseMender/releases";

    public void CheckForUpdates()
    {
        // Format Version
        versionFormatted = new Version(version.Major, version.Minor, version.Build);

        _ = CheckGitHubNewerVersion();
    }

    private async Task CheckGitHubNewerVersion()
    {
        try 
        {
            // Get all releases from GitHub
            GitHubClient client = new GitHubClient(new ProductHeaderValue("MouseMenderClient"));
            IReadOnlyList<Release> releases = await client.Repository.Release.GetAll("TheCodingCarson", "MouseMender");

            // Setup the versions
            latestGitHubVersion = new Version(releases[0].TagName);
            Version localVersion = versionFormatted;

            // Compare the Versions
            versionComparison = localVersion.CompareTo(latestGitHubVersion);
        }
        catch (Exception ex)
        {
            // Log error
            Debug.WriteLine("ERROR: Couldn't check for update: " + ex.Message);
        }
        
        if (versionComparison < 0)
        {
            // Update Avalabile
            Debug.WriteLine($"INF: Update Found {latestGitHubVersion}");

            // Ask to Update
            DialogResult result = MessageBox.Show("Update found. Would you like to update?", "Mouse Mender - Update Available!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                // Open Browser to GitHub
                try
                {
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url.Replace("&", "^&")}") { CreateNoWindow = true });
                }
                catch (Exception ex)
                {
                    // Log error
                    Debug.WriteLine("ERROR: Could not open the URL: " + ex.Message);
                }
            }   
        }
        else
        {
            // Local Version is up to date
        }
    }
}
