using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Com.GabrielBernabeu.Common.DataManagement {
    public static class LocalDataSaving
    {
        private const string DATA_PATH = "/userLocalData.dat";

        public static LocalData CurrentData { get; private set; }

        public static void SaveData()
        {
            if (CurrentData == null)
            {
                Debug.LogError("Data is null");
                return;
            }

            FileStream lFile = null;

            try
            {
                BinaryFormatter lBinaryFormatter = new BinaryFormatter();
                lFile = File.Create(Application.persistentDataPath + DATA_PATH);

                lBinaryFormatter.Serialize(lFile, CurrentData);
            } 
            catch (Exception error)
            {
                if (error != null)
                    Debug.LogError("LOCAL SAVING COULDN'T BE DONE!");
            }
            finally
            {
                if (lFile != null)
                    lFile.Close();
            }
        }

        public static void LoadData()
        {
            LocalData lSavedData = null;
            // OU Nullable<LocalData> lSavedData = null;
            FileStream lFile = null;

            try
            {
                BinaryFormatter lBinaryFormatter = new BinaryFormatter();
                lFile = File.Open(Application.persistentDataPath + DATA_PATH, FileMode.Open);

                lSavedData = lBinaryFormatter.Deserialize(lFile) as LocalData;
            }
            catch (Exception error) 
            {
                if (error != null)
                    Debug.Log("Nothing to load");
            } 
            finally
            {
                if (lFile != null)
                    lFile.Close();
            }

            CurrentData = lSavedData;
        }

        public static void DeleteData()
        {
            File.Delete(Application.persistentDataPath + DATA_PATH);
        }
    }
}
