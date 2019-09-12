using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sort Settings", menuName = "Project Settings/Sort Settings", order = 1)]
public class SortSettings : ScriptableObject
{
    public int NumberOf_MinimumElements = 5;
    public int NumberOf_MaximumElements = 12;
}