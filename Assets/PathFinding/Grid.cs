using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
	
		public bool displayGridGizmos;
		public LayerMask unwalkableMask;
		public LayerMask unitMask;
		public Vector2 gridWorldSize;
		public float nodeRadius;
		Node[,] grid;
		float nodeDiameter;
		int gridSizeX, gridSizeY;
		public List<Node> path;
		LayerMask walkableMask;
		Dictionary<int, int> walkableRegionsDictionary = new Dictionary<int,int > ();
		public TerrainType[] walkableRegions;
	
		void Awake ()
		{
				nodeDiameter = nodeRadius * 2;
				gridSizeX = Mathf.RoundToInt (gridWorldSize.x / nodeDiameter);
				gridSizeY = Mathf.RoundToInt (gridWorldSize.y / nodeDiameter);

				foreach (TerrainType region in walkableRegions) {
						walkableMask.value |= region.terrainMask.value;
						walkableRegionsDictionary.Add ((int)Mathf.Log (region.terrainMask.value, 2), region.terrainPenalty);
				}

				CreateGrid ();
		}
	
		public int MaxSize {
				get {
						return gridSizeX * gridSizeY;
				}
		}
	
		void CreateGrid ()
		{
				grid = new Node[gridSizeX, gridSizeY];
				Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

				for (int x = 0; x < gridSizeX; x ++) {
						for (int y = 0; y < gridSizeY; y ++) {
								Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
								bool walkable = !(Physics.CheckSphere (worldPoint, nodeRadius, unwalkableMask));
												
								
								int movementPenalty = 0;

								// raycast 
								if (walkable) {
										Ray ray = new Ray (worldPoint + Vector3.up * 50, Vector3.down);
										RaycastHit hit;

										if (Physics.Raycast (ray, out hit, 100, walkableMask)) {
												walkableRegionsDictionary.TryGetValue (hit.collider.gameObject.layer, out movementPenalty);
										}

								}

								grid [x, y] = new Node (walkable, worldPoint, x, y, movementPenalty);
						}
				}
		}
		
		public List<Node> GetNeighbours (Node node, int depth = 1)
		{
				List<Node> neighbours = new List<Node> ();
			
				for (int x = -depth; x <= depth; x++) {
						for (int y = -depth; y <= depth; y++) {
								if (x == 0 && y == 0)
										continue;
				
								int checkX = node.gridX + x;
								int checkY = node.gridY + y;
				
								if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
										neighbours.Add (grid [checkX, checkY]);
								}
						}
				}
		
				return neighbours;
		}
	
		public Node NodeFromWorldPoint (Vector3 worldPosition)
		{
				return NodeFromWorldPoint (worldPosition, false);
		}
		
		public Node NodeFromWorldPoint (Vector3 worldPosition, bool isUnit)
		{
				float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
				float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
				percentX = Mathf.Clamp01 (percentX);
				percentY = Mathf.Clamp01 (percentY);
		
				int x = Mathf.RoundToInt ((gridSizeX - 1) * percentX);
				int y = Mathf.RoundToInt ((gridSizeY - 1) * percentY);
				Node node = grid [x, y];
		if (isUnit) {
						node.walkable = true;
				}
				return node;
		}
	
		void OnDrawGizmos ()
		{
				Gizmos.DrawWireCube (transform.position, new Vector3 (gridWorldSize.x, 1, gridWorldSize.y));
				if (grid != null && displayGridGizmos) {
					
						foreach (Node n in grid) {
								Gizmos.color = Color.red;
								if (n.walkable)
										Gizmos.color = Color.white;
						
								Gizmos.DrawCube (n.worldPosition, Vector3.one * (nodeDiameter - .1f));

						}
				}
	
		}
		[System.Serializable]
		public class TerrainType
		{
				public LayerMask terrainMask;
				public int terrainPenalty;
		}
}