using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class PathFinding : MonoBehaviour
{


		PathRequestManager pathRequestManager;
		Grid grid;

		void Awake ()
		{
				grid = GetComponent<Grid> ();
				pathRequestManager = GetComponent<PathRequestManager> ();
		}
	
		IEnumerator FindPath (Vector3 from, Vector3 to)
		{

		UnityEngine.Debug.Log ("Start to find path from " + from + " to " + to);
		
				Stopwatch sw = new Stopwatch ();
				sw.Start ();

				Vector3[] wayPoints = new Vector3[0];

				bool pathSuccess = false;

		
				Node startNode = grid.NodeFromWorldPoint (from,true);
				Node targetNode = grid.NodeFromWorldPoint (to);
				startNode.parent = startNode;
		
		
				if (startNode.walkable && targetNode.walkable) {
						Heap<Node> openSet = new Heap<Node> (grid.MaxSize);
						HashSet<Node> closedSet = new HashSet<Node> ();
						openSet.Add (startNode);
			
						while (openSet.Count > 0) {
								Node currentNode = openSet.RemoveFirst ();
								closedSet.Add (currentNode);
				
								if (currentNode == targetNode) {
										sw.Stop ();
										print ("Path found: " + sw.ElapsedMilliseconds + " ms");
									
										pathSuccess = true;				
										break;
								}
				
								foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
										if (!neighbour.walkable || closedSet.Contains (neighbour)) {
												continue;
										}
					
										int newMovementCostToNeighbour = currentNode.gCost + GetDistance (currentNode, neighbour) + neighbour.movementPenalty;
										if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains (neighbour)) {
												neighbour.gCost = newMovementCostToNeighbour;
												neighbour.hCost = GetDistance (neighbour, targetNode);
												neighbour.parent = currentNode;
						
												if (!openSet.Contains (neighbour))
														openSet.Add (neighbour);
												else 
														openSet.UpdateItem (neighbour);
										}
								}
						}
				}

				yield return null;
				if (pathSuccess) {
						wayPoints = RetracePath (startNode, targetNode);
				}

	UnityEngine.Debug.Log ("#########PathSuccess " + pathSuccess);

				pathRequestManager.FinishedProcessingPath (wayPoints, pathSuccess);
		}

		Vector3[] RetracePath (Node startNode, Node endNode)
		{
				List<Node> path = new List<Node> ();
				Node currentNode = endNode;
		
				while (currentNode != startNode) {
						path.Add (currentNode);
						currentNode = currentNode.parent;
				}
		
				Vector3[] wayPoints = SimplifyPath (path);
				Array.Reverse (wayPoints);
				return wayPoints;
		}

		Vector3[] SimplifyPath (List<Node> path)
		{
				List<Vector3> wayPoints = new List<Vector3> ();

				Vector2 directionOld = Vector2.zero;

				for (int i = 0; i< path.Count-1; i ++) {
						Vector2 directionNew = new Vector2 (path [i + 1].gridX - path [i].gridX, path [i + 1].gridY - path [i].gridY);
						if (directionNew != directionOld) {
								wayPoints.Add (path [i].worldPosition);
						}
						directionOld = directionNew;
				}
				return wayPoints.ToArray ();
		}
	
		int GetDistance (Node nodeA, Node nodeB)
		{
				int dstX = Mathf.Abs (nodeA.gridX - nodeB.gridX);
				int dstY = Mathf.Abs (nodeA.gridY - nodeB.gridY);
		
				if (dstX > dstY)
						return 14 * dstY + 10 * (dstX - dstY);
				return 14 * dstX + 10 * (dstY - dstX);
		}
	
		public void StartFindPath (Vector3 startPosition, Vector3 targetPosition)
		{

				StartCoroutine (FindPath (startPosition, targetPosition));


		}
}