using System;
using System.Collections;
using System.Collections.Generic;
using Core.WeaponSystem;
using UnityEngine;

public class ProjectilePrefabsContainer : ScriptableObject
{
    [SerializeField]
    private List<ProjectilePrefab> projectilePrefabsList;

    private Dictionary<ProjectileType, GameObject> projectilePrefabs;


    private void InitDictionary()
    {
        projectilePrefabs = new Dictionary<ProjectileType, GameObject>();
        foreach (var projectilePrefab in projectilePrefabsList)
        {
            projectilePrefabs.Add(projectilePrefab.ProjectileType, projectilePrefab.Prefab);
        }
    }

    public GameObject GetProjectilePrefab(ProjectileType projectileType)
    {
        if(projectilePrefabs is null)
            InitDictionary();

        return projectilePrefabs[projectileType];
    }

    private void Awake()
    {
        InitDictionary();
    }

    [Serializable]
    private class ProjectilePrefab
    {
        internal ProjectileType ProjectileType;
        internal GameObject Prefab;
    }
}

