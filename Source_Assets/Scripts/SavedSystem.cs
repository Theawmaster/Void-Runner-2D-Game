using System.IO; 
using UnityEngine;

public class SavedSystem
{
    // Save folder
    public static readonly string SAVE_FOLDER = Application.persistentDataPath + "/SavedFolder/";

    // File extension
    public static readonly string FILE_EXT = ".json";

    // Save function
    public static void Save(string fileName, string dataToSave)
    {
        if (!Directory.Exists(SAVE_FOLDER)) // Check if directory exists
        {
            Directory.CreateDirectory(SAVE_FOLDER); // Create directory
        }

        File.WriteAllText(SAVE_FOLDER + fileName + FILE_EXT, dataToSave); // Write to file 
    }

    // Load function
    public static string Load(string fileName)
    {
        string fileLocation = SAVE_FOLDER + fileName + FILE_EXT; // File location
        if (File.Exists(fileLocation)) // Check if file exists
        {
            string loadedData = File.ReadAllText(fileLocation); // Read from file
            return loadedData; // Return loaded data
        }
        else
        {
            return null; // Return null
        }
    }
}
