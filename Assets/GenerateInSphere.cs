using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateInSphere : MonoBehaviour {

    public enum RotationMode { Random, Identity, ToCenter }

    public GameObject[] prefabs = null;
    public int count = 10;
    public float innerRadius;
    public float outerRadius;
    public float minSeparation = 100;
    public RotationMode rotationMode = RotationMode.Random;
    public bool checkCollision = true;

    public float rotateAllSpeed = 0;

    private Transform thisTransform = null;

	void Start () {
        thisTransform = GetComponent<Transform>();
        int remainingToSpawn = count;
        int attempts = 0;
        while (remainingToSpawn > 0)
        {
            Vector3 spawnPosition = thisTransform.position + (Random.rotation * Vector3.forward * Random.Range(innerRadius, outerRadius));            

            if(!checkCollision || Physics.OverlapSphere(spawnPosition, minSeparation).Length == 0)
            {
                Quaternion rotation = Quaternion.identity;
                switch (rotationMode)
                {
                    case RotationMode.Random:
                        rotation = Random.rotation;
                        break;
                    case RotationMode.Identity:
                        rotation = Quaternion.identity;
                        break;
                    case RotationMode.ToCenter:
                        rotation = Quaternion.LookRotation(spawnPosition - thisTransform.position);
                        break;
                }
                GameObject.Instantiate(prefabs[Random.Range(0,prefabs.Length)], spawnPosition, rotation, thisTransform);
            }
            attempts++;
            remainingToSpawn--;
            // ensure we don't end up in an infinite loop if there isn't space to spawn the full count
            if (attempts > count * 10) break;
        }
	}

    private void Update()
    {
        if (rotateAllSpeed > 0) thisTransform.Rotate(thisTransform.forward, rotateAllSpeed * Time.deltaTime);
    }

}
