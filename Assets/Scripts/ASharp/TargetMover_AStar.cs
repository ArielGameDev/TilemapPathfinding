﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using AStarTools;

/**
 * This component moves its object towards a given target position.
 */
public class TargetMover_AStar : MonoBehaviour
{
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;

    [Tooltip("The speed by which the object moves towards the target, in meters (=grid units) per second")]
    [SerializeField] float speed = 2f;

    [Tooltip("Maximum number of iterations before BFS algorithm gives up on finding a path")]
    [SerializeField] int maxIterations = 1000;

    [Tooltip("The target position in world coordinates")]
    [SerializeField] Vector3 targetInWorld;

    [Tooltip("The target position in grid coordinates")]
    [SerializeField] Vector3Int targetInGrid;

    protected bool atTarget;  // This property is set to "true" whenever the object has already found the target.

    public void SetTarget(Vector3 newTarget)
    {
        if (targetInWorld != newTarget)
        {
            targetInWorld = newTarget;
            targetInGrid = tilemap.WorldToCell(targetInWorld);
            atTarget = false;
        }
    }

    public Vector3 GetTarget()
    {
        return targetInWorld;
    }

    private TilemapGraphNode tilemapGraph = null;
    private float timeBetweenSteps;

    protected virtual void Start()
    {
        tilemapGraph = new TilemapGraphNode(tilemap, allowedTiles.Get());
        timeBetweenSteps = 1 / speed;
        StartCoroutine(MoveTowardsTheTarget());
    }

    IEnumerator MoveTowardsTheTarget()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(timeBetweenSteps);
            if (enabled && !atTarget)
                MakeOneStepTowardsTheTarget();
        }
    }

    private void MakeOneStepTowardsTheTarget()
    {
        Vector3Int startNode = tilemap.WorldToCell(transform.position);
        Vector3Int endNode = targetInGrid;

        if(startNode == endNode){
            atTarget = true;
            return;
        }


        Node startAsNode = new Node(startNode);
        Node endAsNode = new Node(endNode);

        List<Node> shortestPath = AStar.GetPath(tilemapGraph, startAsNode, endAsNode, maxIterations);

        if (shortestPath.Count >= 2)
        {
            Vector3Int nextNode = shortestPath[shortestPath.Count-2].coordinates;
            transform.position = tilemap.GetCellCenterWorld(nextNode);
        }
    }
}
