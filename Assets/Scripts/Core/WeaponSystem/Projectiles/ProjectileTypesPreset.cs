using Core.Utils;
using Core.WeaponSystem;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ProjectileTypesPreset", menuName = "InGamePresets/ProjectileTypesPreset")]
public class ProjectileTypesPreset : ScriptableObject
{
    [SerializeField] public ReadonlyRuntimeDictionary<ProjectileType, GameObject> ProjectilePrefabs;
    [SerializeField] public ReadonlyRuntimeDictionary<ProjectileType, int> InitialPoolSizes;
}