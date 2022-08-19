using Core.Utils;
using Core.WeaponSystem;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ProjectilePrefabsPreset", menuName = "InGamePresets/ProjectilePrefabsPreset")]
public class ProjectilePrefabsPreset : ScriptableObject
{
    [SerializeField] public ReadonlyRuntimeDictionary<ProjectileType, GameObject> ProjectilePrefabs;
    [SerializeField] public ReadonlyRuntimeDictionary<ProjectileType, int> InitialPoolSizes;
}