using System;
using System.Threading;

/* 
Exceeding Requirements:

1. Added a fourth activity type: GratitudeActivity
   - This activity helps users develop gratitude by reflecting on positive aspects of their day
   - It provides random prompts focused on gratitude and gives users time to write down their thoughts
   - I utilized AI to help come up with a list of gratitude prompts.

2. Implemented an ActivityLog class to track usage statistics
   - The program now keeps count of how many times each activity has been performed
   - Added a menu option to view the activity log

3. Created a rotating spinner using different characters (|, /, -, \)
   - Added appropriate pauses between activities
*/

class Program
{
    static void Main(string[] args)
    {
        // Init activity log
        ActivityLog activityLog = new ActivityLog();
        
        bool quit = false;
        while (!quit)
        {
            // menu
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("==================");
            Console.WriteLine("1. Start breathing activity");
            Console.WriteLine("2. Start reflection activity");
            Console.WriteLine("3. Start listing activity");
            Console.WriteLine("4. Start gratitude activity");
            Console.WriteLine("5. View activity log");
            Console.WriteLine("6. Quit");
            Console.Write("Select a choice from the menu: ");
            
            string choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    BreathingActivity breathingActivity = new BreathingActivity();
                    breathingActivity.Run();
                    activityLog.LogActivity("Breathing Activity");
                    break;
                case "2":
                    ReflectionActivity reflectionActivity = new ReflectionActivity();
                    reflectionActivity.Run();
                    activityLog.LogActivity("Reflection Activity");
                    break;
                case "3":
                    ListingActivity listingActivity = new ListingActivity();
                    listingActivity.Run();
                    activityLog.LogActivity("Listing Activity");
                    break;
                case "4":
                    GratitudeActivity gratitudeActivity = new GratitudeActivity();
                    gratitudeActivity.Run();
                    activityLog.LogActivity("Gratitude Activity");
                    break;
                case "5":
                    activityLog.DisplayLog();
                    break;
                case "6":
                    quit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Thread.Sleep(2000);
                    break;
            }
        }
    }
}