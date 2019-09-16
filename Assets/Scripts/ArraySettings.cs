using UnityEngine;

[CreateAssetMenu(fileName = "Array Settings", menuName = "Project Settings/Array Settings", order = 1)]
public class ArraySettings : ScriptableObject
{
    public int NumberOf_MinimumElements = 5;
    public int NumberOf_MaximumElements = 12;
}