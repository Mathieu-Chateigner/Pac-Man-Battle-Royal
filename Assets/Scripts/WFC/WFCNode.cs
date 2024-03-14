using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WFCNode", menuName = "WFC/Node")]
[System.Serializable]
public class WFCNode : ScriptableObject
{
    public string Name;
    public GameObject prefab;
    public WFC_Connection Top;
    public WFC_Connection Right;
    public WFC_Connection Down;
    public WFC_Connection Left;
}

[System.Serializable]
public class WFC_Connection
{
    public List<WFCNode> CompatibleNodes = new List<WFCNode>();
}
