using System.Collections;
using System.Collections.Generic;
using Core.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsPreset", menuName = "PresetsAndConfigs/SettingsPreset")]
public class SettingsPreset : ScriptableObject
{
    [SerializeField]
    private SerializableSettingsModel _settingsModel;

    public SerializableSettingsModel Preset => _settingsModel;
}
