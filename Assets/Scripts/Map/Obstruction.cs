using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is designed to be a component of a tile solely containing an
/// obstruction object. For dynamic generation, each obstructor will be given a
/// different position, scale and rotation on its own tile.
/// </summary>
public class Obstruction : MonoBehaviour
{
    private Transform obstructor;

    // Start is called before the first frame update
    void Start()
    {
        // Find obstructor in children
        obstructor = transform.Find("Obstructor");

        // Get values
        float positionX = Random.Range(-GetHigherBound(), GetHigherBound());
        float positionZ = Random.Range(-GetHigherBound(), GetHigherBound());
        float scaleX = Random.Range(1.0f, GetHigherBound());
        float scaleZ = Random.Range(1.0f, GetHigherBound());
        Vector3 rotation = new Vector3(0.0f, Random.Range(0.0f, 90.0f), 0.0f);

        // Set values
        obstructor.transform.localPosition = new Vector3(positionX, 0.0f, positionZ);
        obstructor.transform.localScale = new Vector3(scaleX, 3.0f, scaleZ);
        obstructor.transform.Rotate(rotation);
    }

    /// <summary>
    /// So the obstructor does not go out of the tile, this function returns half
    /// the scale of the tile so it is used as the higher bound when placing and
    /// scaling the obstructor.
    /// </summary>
    /// <returns>Float value of half the scale of the tile</returns>
    private float GetHigherBound()
    {
        return Mathf.Ceil(transform.localScale.x / 2.0f);
    }
}
