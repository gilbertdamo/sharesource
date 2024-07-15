namespace MyConsoleApp
{
    using System;
    using System.IO;

    class Program
    {
        static void Main()
        {
            string settings = "01000"; // Example settings string
            Console.WriteLine($"Settings: {settings}"); // True

            // Write settings to file
            WriteSettingsToFile("settings.txt", settings);
            Console.WriteLine("Settings have been written to file.");

            // Read settings from file and test if a feature is enabled
            string readSettings = ReadSettingsFromFile("settings.txt");
            Console.WriteLine($"Settings read from file: {readSettings}");

            // Test cases to check if a feature is enabled
            Console.WriteLine($"Is SMS Notifications Enabled? {IsUserSettingsEnabled(readSettings, UserSettings.SMSNotifications)}"); // false
            Console.WriteLine($"Is Push Notifications Enabled? {IsUserSettingsEnabled(readSettings, UserSettings.PushNotifications)}"); // true

            // Test cases enable bio metris
            readSettings = EnableUserSettings(readSettings, UserSettings.Biometrics);
            Console.WriteLine($"Is Bio-metrics Enabled? {IsUserSettingsEnabled(readSettings, UserSettings.Biometrics)}"); // True

            // Test cases disable bio metris
            readSettings = DisableUserSettings(readSettings, UserSettings.Biometrics);
            Console.WriteLine($"Is Bio-metrics Enabled? {IsUserSettingsEnabled(readSettings, UserSettings.Biometrics)}"); // False

            // Test cases enable loyalty
            readSettings = DisableUserSettings(readSettings, UserSettings.Loyalty);
            Console.WriteLine($"Is Loyalty Enabled? {IsUserSettingsEnabled(readSettings, UserSettings.Loyalty)}"); // True

            // Test cases disable loyalty
            readSettings = EnableUserSettings(readSettings, UserSettings.Loyalty);
            Console.WriteLine($"Is Loyalty Enabled? {IsUserSettingsEnabled(readSettings, UserSettings.Loyalty)}"); // False

            Console.WriteLine($"Read Settings: {readSettings}"); 
        }

        // Method to enable a user setting in the binary string
        public static string EnableUserSettings(string binaryString, UserSettings feature)
        {
            int position = (int)feature - 1; // Calculate zero-based index
            if (position >= binaryString.Length)
            {
                // Pad the binary string with '0's to the required length
                binaryString = binaryString.PadRight(position + 1, '0');
            }

            char[] binaryArray = binaryString.ToCharArray(); // Convert to char array for manipulation
            binaryArray[position] = '1'; // Set the specified bit to '1'
            return new string(binaryArray); // Convert back to string and return
        }

        // Method to disable a user setting in the binary string
        public static string DisableUserSettings(string binaryString, UserSettings feature)
        {
            int position = (int)feature - 1; // Calculate zero-based index
            if (position >= binaryString.Length)
            {
                // Pad the binary string with '0's to the required length
                binaryString = binaryString.PadRight(position + 1, '0');
            }

            char[] binaryArray = binaryString.ToCharArray(); // Convert to char array for manipulation
            binaryArray[position] = '0'; // Set the specified bit to '0'
            return new string(binaryArray); // Convert back to string and return
        }

        // Method to check if a specific user setting is enabled in the binary string
        public static bool IsUserSettingsEnabled(string binaryString, UserSettings feature)
        {
            int position = (int)feature - 1; // Calculate zero-based index
            if (position >= binaryString.Length)
            {
                return false; // If the position is out of range, the feature is not enabled
            }

            return binaryString[position] == '1'; // Return true if the bit is '1', false otherwise
        }

        // Method to write the settings to a file
        static void WriteSettingsToFile(string filePath, string settings)
        {
            File.WriteAllText(filePath, settings); // Write the settings string to the file
        }

        // Method to read the settings from a file
        static string ReadSettingsFromFile(string filePath)
        {
            return File.ReadAllText(filePath); // Read and return the contents of the file
        }
    }

    // Enum representing different user settings/features
    public enum UserSettings
    {
        SMSNotifications = 1,
        PushNotifications = 2,
        Biometrics = 3,
        Camera = 4,
        Location = 5,
        NFC = 6,
        Vouchers = 7,
        Loyalty = 8
    }
}
