using Core.EnemySystem;
using Core.Utils;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTypesPreset", menuName = "InGamePresets/EnemyTypesPreset")]
public class EnemyTypesPreset : ScriptableObject
{
    [SerializeField] public ReadonlyRuntimeDictionary<EnemyType, GameObject> EnemyPrefabs;
    [SerializeField] public ReadonlyRuntimeDictionary<EnemyType, int> InitialPoolSizes;
}
