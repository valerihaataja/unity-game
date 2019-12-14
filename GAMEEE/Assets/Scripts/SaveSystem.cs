using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SavePlayer (PlayerHealth playerhealth)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(playerhealth);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveLoot (Looting looting)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/loot.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(looting);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;

        } else
        {
            //Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static PlayerData LoadLoot()
    {
        string path = Application.persistentDataPath + "/loot.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;

        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static bool CheckSave()
    {
        string path = Application.persistentDataPath + "/player.bin";
        if(File.Exists(path))
        {
            return true;
        }else
        {
            return false;
        }
    }

    public static void DeleteData()
    {
        string path = Application.persistentDataPath + "/player.bin";
        File.Delete(path);
        path = Application.persistentDataPath + "/loot.bin";
        File.Delete(path);
    }

}
