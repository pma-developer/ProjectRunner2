using System;
using System.Collections;
using System.Collections.Generic;
using Core.Utils;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageLayersPreset", menuName = "InGamePresets/DamageLayersPreset")]
public class DamageLayersPreset : ScriptableObject
{
    [Header("Key layer mask is layer and value layer mask is it's ally layers")] [SerializeField]
    public ReadonlyRuntimeDictionary<LayerMask, LayerMask> LayerAllies;
}