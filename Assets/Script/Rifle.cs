using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle Things")]
    public Camera camera;
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
        }
    }

    private void ShootAtMousePosition()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shootingRange))
        {
            Debug.Log(hit.transform.name);
            ObjecToHit objecToHit = hit.transform.GetComponent<ObjecToHit>();
            if (objecToHit != null)
            {
                objecToHit.ObjectHitDamage(giveDamageOf);
                GameObject imPactGo = Instantiate(WoodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(imPactGo, 1f);
            }
        }

        muzzlePark.Play();
    }
}