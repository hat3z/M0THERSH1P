using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class MS_PlayerProfileController : MonoBehaviour
{

    string ProfileFilePath;
    string ProfileFileName = "Profile.save";

    private void Awake()
    {
        ProfileFilePath = Application.persistentDataPath;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadProfileFromFile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region --- SAVE AND LOAD HANDLING ---

    public void SavePlayerProfileToFile()
    {
        File.Delete(ProfileFilePath + Path.DirectorySeparatorChar + ProfileFileName);
        MS_PlayerProfile profile = MS_PlayerShipController.Instance.PlayerProfile;
        BinaryFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(ProfileFilePath + Path.DirectorySeparatorChar + ProfileFileName, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, profile);
        stream.Close();
        Debug.Log("Profile has been saved!");
    }

    public void LoadProfileFromFile()
    {
        if (File.Exists(ProfileFilePath + Path.DirectorySeparatorChar + ProfileFileName))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(ProfileFilePath + Path.DirectorySeparatorChar + ProfileFileName, FileMode.Open, FileAccess.Read);
            MS_PlayerProfile loadedProfile = (MS_PlayerProfile)formatter.Deserialize(stream);
            MS_PlayerShipController.Instance.PlayerProfile = loadedProfile;
            stream.Close();
            Debug.Log("Profile loaded successfully!");
        }
        else
        {
            SavePlayerProfileToFile();
        }
    }

    #endregion

}
