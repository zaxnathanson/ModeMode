using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleThrow : MonoBehaviour
{
    [SerializeField] GameObject capsulePrefab;
    [SerializeField] Stats statsRef;
    [SerializeField] Animator animator;
    float attackSpeedTimer = 0;

    float rotationZ;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
    }

    public void PickupCapsule()
    {
        statsRef.playerSpecific.capsules++;
    }

    void Shooting()
    {

        Vector3 screenPosition = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2, 0));
        rotationZ = Mathf.Atan2(screenPosition.y, screenPosition.x) * Mathf.Rad2Deg;

        attackSpeedTimer += Time.deltaTime;
        if (attackSpeedTimer > 0.5f)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && statsRef.playerSpecific.capsules > 0)
            {
                statsRef.playerSpecific.capsules--;
                animator.SetTrigger("Hop");
                SpawnCapsule();
                attackSpeedTimer = 0;
            }
        }


    }

    void SpawnCapsule()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, 0, transform.position.z);

        GameObject newProjectile = Instantiate(capsulePrefab, spawnPos, Quaternion.identity);
        Capsule capsuleScript = newProjectile.GetComponent<Capsule>();

        capsuleScript.SetupBullet(statsRef.playerSpecific, rotationZ);
    }

}
