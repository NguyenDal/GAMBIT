using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyFlickerWhenNear : MonoBehaviour
{

    private GameObject player;
    private ArrayList cubes;
    private FlickerScript[] flickerCubes;
    private Transform playerPos;
    private double differenceX;
    private double differenceZ;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cubes = new ArrayList();
        flickerCubes = (FlickerScript[]) GameObject.FindObjectsOfType(typeof(FlickerScript));
        foreach(FlickerScript flickerCube in flickerCubes){
            cubes.Add(flickerCube.gameObject);
            flickerCube.flicker = false;
            Debug.Log(flickerCube.tag);
        }
    }

    void Update()
    {
        playerPos = player.transform;
        foreach(GameObject cube in cubes){
            if (cube != null) {
                differenceZ = cube.transform.position.z - playerPos.position.z;
                differenceX = cube.transform.position.x - playerPos.position.x;
                if (differenceX < 2 && differenceX > -2 && differenceZ > -2 && differenceZ < 2) {
                    cube.GetComponent<FlickerScript>().flicker = true;
                }
                else{
                    cube.GetComponent<FlickerScript>().flicker = false;
                }
            }
        } 
    }
}
