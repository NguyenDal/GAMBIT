using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : MonoBehaviour
{
    public Transform shooter, player, firePoint;
    public GameObject bullet;
    private bool inRange = false;
    private bool isShooting = false;
    private Coroutine shootingCoroutine;

    void Update(){
        //If the player enters the shooters range (box collider), then rotate the shooter to face the player 
        if (inRange){
            shooter.transform.LookAt(new Vector3(player.position.x, player.position.y, player.position.z));
        }
    }

    //If the player enters the shooters range (box collider), then start shooting
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            inRange = true;
            if (!isShooting){
                shootingCoroutine = StartCoroutine(ShootAtPlayer());
                isShooting = true;
            }
        }
    }
    
    //When the player leaves the shooters range, stop following, and stop shooting
    void OnTriggerExit(Collider other){
        if (other.CompareTag("Player")){
            inRange = false;
            if (isShooting){
                StopCoroutine(shootingCoroutine);
                isShooting = false;
            }
            Debug.Log("Player left the shooter zone");
            shooter.transform.LookAt(Vector3.zero); 
        }
    }

    IEnumerator ShootAtPlayer(){
        while (inRange){
            //Create a new bullet, and spawn it on the eye of the shooter
            GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            StartCoroutine(MoveBullet(newBullet, new Vector3(player.position.x, player.position.y, player.position.z), 5));
            yield return new WaitForSeconds(3f); //You can change the rate of fire here
        }
    }

    IEnumerator MoveBullet(GameObject bullet, Vector3 endPos, float time){
        Vector3 beginPos = bullet.transform.position;
        for (float t = 0; t < 1; t += Time.deltaTime / time){
            bullet.transform.position = Vector3.Lerp(beginPos, endPos, t);
            yield return null;
        }
        Destroy(bullet); 
    }
}