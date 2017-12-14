using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStair : MonoBehaviour, IReactToCursor
{

    public float spawnDelay = 0.5f;
    public GameObject stairPrefab;
    public GameObject preview;
    public float horzSpacing = 0.6f;
    public float vertSpacing = 0.3f;

    public bool active = true;

    public void CursorEnter(RaycastHit hitInfo) {
        if (active)
        {
            StartCoroutine(StairPreview());
        }
    }

    private IEnumerator StairPreview()
    {
        preview.SetActive(true);
        yield return new WaitForSeconds(spawnDelay);
        CreateStair();        
    }

    private void CreateStair()
    {
        preview.SetActive(false);
        GameObject newStair = Instantiate(stairPrefab, transform);
        newStair.transform.localPosition = new Vector3(0, vertSpacing, horzSpacing);
        active = false;
    }

    public void CursorOn(RaycastHit hitInfo, bool active) { }
    public void CursorExit() {
        preview.SetActive(false);
        StopAllCoroutines();
    }
    public void CursorHit(RaycastHit hitInfo) { }
}

