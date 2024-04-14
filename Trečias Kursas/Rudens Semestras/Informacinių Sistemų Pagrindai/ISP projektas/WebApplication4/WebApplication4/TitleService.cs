// TitleService.cs

using System.Collections.Generic;
using System.Linq;

public class TitleService
{
    public static List<Title> AvailableTitles = new List<Title>
    {
        new Title { Name = "Newbie", PointThreshold = 1 },
        new Title { Name = "Somewhat less new", PointThreshold = 3 },
        new Title { Name = "Even less new", PointThreshold = 5 },
        new Title { Name = "Advanced", PointThreshold = 10 },
        new Title { Name = "Slightly more advanced", PointThreshold = 20 },
        new Title { Name = "Super advanced", PointThreshold = 30 },
        new Title { Name = "Expert", PointThreshold = 40 },
        new Title { Name = "God", PointThreshold = 50 },
        new Title { Name = "Zy0x", PointThreshold = 100 },
        // Add more titles and thresholds as needed
    };



    public List<string> GetAvailableTitles(int userPoints)
    {
        // Filter titles based on user's point threshold
        var availableTitles = AvailableTitles.Where(title => userPoints >= title.PointThreshold)
                                            .Select(title => title.Name)
                                            .ToList();

        return availableTitles;

    }
    public string GetUserTitle(int userPoints)
    {
        foreach (var title in AvailableTitles.OrderByDescending(t => t.PointThreshold))
        {
            if (userPoints >= title.PointThreshold)
            {
                return title.Name;
            }
        }

        return "No Title";
    }
}
