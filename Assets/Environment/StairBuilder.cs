using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StairDirections { Forward, Left, Right, Reverse }

[System.Serializable]
public struct StairSection
{
    public StairDirections direction;
    public int numberOfStairs;
}

[ExecuteInEditMode]
public class StairBuilder : MonoBehaviour {

    public Transform stairSectionPrefab;
    public Transform landingPrefab;
    public float horzSpacing = 0.5f;
    public float vertSpacing = 0.3f;
    public float stairWidth = 2.4f;
    public float stairHeight = 0.1f;

    public StairSection[] sections;

    void Start ()
    {
        DestroyChildren(transform);
        BuildStairs();
    }

    private void BuildStairs()
    {
        Vector3 buildPosition = Vector3.zero;
        Quaternion buildDirection = Quaternion.identity;
        for (int i = 0; i < sections.Length; i++)
        {
            BuildSection(i, ref buildPosition, ref buildDirection);
        }
    }

    private void BuildSection(int index, ref Vector3 buildPosition, ref Quaternion buildDirection)
    {

        if (sections[index].direction == StairDirections.Right)
            buildDirection *= Quaternion.Euler(0, 90, 0);
        else if (sections[index].direction == StairDirections.Left)
            buildDirection *= Quaternion.Euler(0, -90, 0);

        buildPosition += buildDirection * new Vector3(0, 0, (stairWidth / 2) );

        for (int i = 0; i < sections[index].numberOfStairs; i++)
        {
            Transform stairSection = Instantiate(stairSectionPrefab, Vector3.zero, transform.rotation * buildDirection, transform);
            stairSection.localPosition = buildPosition;
            buildPosition += buildDirection * new Vector3(0,vertSpacing, horzSpacing);
        }

        buildPosition += buildDirection * new Vector3(0, 0, (stairWidth / 2) - 0.1f);

        Instantiate(landingPrefab,transform.position + buildPosition, transform.rotation, transform);

    }

    /// <summary>
    /// Calls GameObject.Destroy on all children of transform. and immediately detaches the children
    /// from transform so after this call tranform.childCount is zero.
    /// </summary>
    public static void DestroyChildren(Transform transform)
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
        {
            GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }


}
