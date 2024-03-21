using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Shooting target")]
    public GameObject objectAim;

    [Header("Rifle Things")]
    public float giveDamageOf = 10f;
    public float shootingRange = 100f;

    [Header("Rifle Effect ")]
    public ParticleSystem muzzlePark;
    public GameObject WoodEffect;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootAtMousePosition();
            muzzlePark.Play();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            muzzlePark.Stop();
        }
    }

    private void ShootAtMousePosition()
    {
        Ray ray = new Ray(objectAim.transform.position, objectAim.transform.TransformDirection(Vector3.forward));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shootingRange))
        {
            Debug.Log("Target hit: " + hit.transform.name);

            if (hit.transform.CompareTag("Capsule"))
            {
                ObjecToHit objecToHit = hit.transform.GetComponent<ObjecToHit>();
                if (objecToHit != null)
                {
                    objecToHit.ObjectHitDamage(giveDamageOf);
                    Debug.Log("Target health: " + objecToHit.ObjectHealth);
                    GameObject imPactGo = Instantiate(WoodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(imPactGo, 1f);
                }
            }
            else
            {
                Debug.Log("Missed Capsule");
            }
        }
    }
}