using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Data
{

    [CreateAssetMenu(fileName = "DataPaths", menuName = "PresetsAndConfigs/DataPaths")]
    public class DataPaths : ScriptableObject
    {
        public enum FileExtension
        {
            Json
        }
        [SerializeField] private FileExtension fileExtension;
        
        [SerializeField]
        private string _settingsFileName;
        [SerializeField]
        private string _saveFileName;

        private string GetExtensionString()
        {
            return fileExtension switch
            {
                FileExtension.Json => ".json",
                _ => throw new Exception()
            };
        }

        public string SettingsFileName => _settingsFileName;
        public string SaveFileName => _saveFileName;
        
        public string SettingsFilePath => Application.persistentDataPath + "/" + _settingsFileName + GetExtensionString();
    }
}