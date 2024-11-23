using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradePickup : MonoBehaviour
{
    Upgrade upgradeScript;
    bool collected = false;
    GameObject targetPlayer;
    [SerializeField] float movementSpeedIncrease;
    [SerializeField] float movementSpeed;
    [SerializeField] Rigidbody rb;
    [SerializeField] float collectionDistance;
    [SerializeField] float upForceMax, upForceMin;
    [SerializeField] float sideForce;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Awake()
    {
        targetPlayer = GameObject.FindWithTag("Player");
        rb.AddForce(transform.up * Random.Range(upForceMin, upForceMax), ForceMode.Impulse);
        Vector3 direction = new Vector3(Random.Range(0, 360), 0, Random.Range(0, 360)).normalized;
        rb.AddForce(direction * Random.Range(-sideForce, sideForce), ForceMode.Impulse);
    }

    public void Setup(Sprite sprite, Upgrade upgradeScriptRef)
    {
        Debug.Log("Setup");
        spriteRenderer.sprite = sprite;
        upgradeScript = upgradeScriptRef;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Collect(other.gameObject);
        }
    }

    void Collect(GameObject player)
    {
        EffectManager.instance.StartCoroutine(EffectManager.instance.ScreenFade(Color.white, 0.2f, 0f, 0.05f, 0.05f));
        UpgradeHandler upgradeHandlerRef = player.GetComponent<UpgradeHandler>();
        upgradeHandlerRef.AddUpgrade(upgradeScript);


        Destroy(gameObject);

    }

    private void Update()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, targetPlayer.transform.position)) <= collectionDistance)
        {

            collected = true;

        }

        if (collected == true)
        {
            rb.isKinematic = true;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPlayer.transform.position.x, 0.4f, targetPlayer.transform.position.z), Time.deltaTime * movementSpeed);
            movementSpeed += Time.deltaTime * movementSpeedIncrease;
        }
    }
}
