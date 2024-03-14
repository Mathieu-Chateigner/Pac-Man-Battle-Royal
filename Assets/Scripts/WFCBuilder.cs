using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WFCBuilder : MonoBehaviour
{
   [SerializeField] private int width;
   [SerializeField] private int height;

   private WFCNode[,] _grid;

   public List<WFCNode> Nodes = new List<WFCNode>();

   private List<Vector2Int> _toCollapse = new List<Vector2Int>();

   private Vector2Int[] offsets = new Vector2Int[]
   {
       new Vector2Int(0, 1),//top
       new Vector2Int(0, -1),//down
       new Vector2Int(1, 0),//right
       new Vector2Int(-1, 0),//left
   };

   private void Start()
   {
       _grid = new WFCNode[width, height];
    
       CollapseWorld();
   }

   private void CollapseWorld()
   {
       _toCollapse.Clear();
       
       _toCollapse.Add(new Vector2Int(width / 2, height / 2));

       while (_toCollapse.Count > 0)
       {
           int x = _toCollapse[0].x;
           int y = _toCollapse[0].y;

           List<WFCNode> potentialNodes = new List<WFCNode>(Nodes);

           for (int i = 0; i < offsets.Length; i++)
           {
               Vector2Int neighbour = new Vector2Int(x + offsets[i].x, y + offsets[i].y);

               if (IsInsideGrid(neighbour))
               {
                   WFCNode neighbourNode = _grid[neighbour.x, neighbour.y];
                   
                   if (neighbourNode != null)
                   {
                       switch (i)
                       {
                           case 0:
                               //Debug.Log(potentialNodes);
                               //Debug.Log(neighbourNode);
                               WhittleNodes(potentialNodes, neighbourNode.Down.CompatibleNodes);
                               break;
                           case 1:
                               WhittleNodes(potentialNodes, neighbourNode.Left.CompatibleNodes);
                               break;
                           case 2:
                               WhittleNodes(potentialNodes, neighbourNode.Top.CompatibleNodes);
                               break;
                           case 3:
                               WhittleNodes(potentialNodes, neighbourNode.Right.CompatibleNodes);
                               break;
                       }
                   }
                   else
                   {
                       if (!_toCollapse.Contains(neighbour)) _toCollapse.Add(neighbour); 
                   }
               }
           }
           if (potentialNodes.Count < 1)
           {
               _grid[x, y] = Nodes[0];
               Debug.Log("try at " + x + " " + y);
           }
           else
           {
               _grid[x, y] = potentialNodes[Random.Range(0, potentialNodes.Count)];
           }
           GameObject newNode = Instantiate(_grid[x, y].prefab, new Vector2(x, y), Quaternion.identity);
           _toCollapse.RemoveAt(0);
       }
   }

   private void WhittleNodes(List<WFCNode> potentialNodes, List<WFCNode> validNodes)
   {
       for (int i = potentialNodes.Count - 1; i > -1; i--)
       {
           if (!validNodes.Contains(potentialNodes[i]))
           {
               potentialNodes.RemoveAt(i);
           }
       }
   }

   private bool IsInsideGrid(Vector2Int v2int)
   {
       if (v2int.x > -1 && v2int.x < width && v2int.y > -1 && v2int.y < height)
       {
           return true;
       }
       else
       {
           return false;
       }
   }
}
