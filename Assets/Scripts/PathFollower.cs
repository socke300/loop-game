using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{

    Node[] nodes;

    public GameObject player;
    public GameObject manager;
    Manager managerScript;
    public float moveSpeed = 1f;
    public bool mustLoop = true;

    int currentNodeIndex;
    static Vector3 currentPosition;
    static Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        nodes = GetComponentsInChildren<Node>();
        managerScript = manager.GetComponent<Manager>();
        CheckNextNode();
    }

    void CheckNextNode()
    {
        previousPosition = currentPosition;
        currentPosition = nodes[currentNodeIndex].transform.position;

        FindObjectOfType<PlayerAnimation>().setDirection(currentPosition - previousPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if(managerScript.running){
            if (nodes[currentNodeIndex].mustStopHere)
            {
                return;
            }

            if (player.transform.position != currentPosition)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, currentPosition, 0.001f * moveSpeed);
            } else
            {
                if(currentNodeIndex == nodes.Length - 1 && mustLoop)
                {
                    currentNodeIndex = -1;
                }

                if (currentNodeIndex < nodes.Length - 1)
                {
                    currentNodeIndex++;
                    CheckNextNode();
                }
            }
        }
    }
}
