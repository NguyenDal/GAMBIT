using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onlyFlickerWhenNear : MonoBehaviour
{

    private GameObject player;
    private GameObject cube;
    private Transform playerPos;
    private Transform cubePos;
    private double differenceX;
    private double differenceZ;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cube = GameObject.FindGameObjectWithTag("FlickerCube");
        cubePos =  cube.transform;
    }

    void Update()
    {
        while (true) {
            playerPos = player.transform;
            differenceZ = cubePos.position.z - playerPos.position.z;
            differenceX = cubePos.position.x - playerPos.position.x;

            if (cube != null) {
                if (differenceX < 1 && differenceX > -1 && differenceZ > -1 && differenceZ < 1) {
                    cube.SetActiveRecursively(true);
                }
            }
        }
    }
}
