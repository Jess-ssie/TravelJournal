using System;

namespace TravelJournal.Validate;
public static class ValidateDate
{
    public static bool IsValidDateFormat(string input)
    {
        try
        {
            DateTime.ParseExact(input, "yyyy-MM-dd", null);
            return true;
        }
        catch
        {
            Console.WriteLine($"[ERROR] Invalid date {input}. Use format yyyy-MM-dd.");
            return false;
        }
    }
}
