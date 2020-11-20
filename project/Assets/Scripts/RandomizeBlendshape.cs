using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeBlendshape : MonoBehaviour
{
    SkinnedMeshRenderer SKM;

    // Start is called before the first frame update
    void Awake()
    {
        SKM = GetComponent<SkinnedMeshRenderer>();
    }

    void Start()
    {
        SKM.SetBlendShapeWeight(0, Random.Range(0, 100));
        SKM.SetBlendShapeWeight(1, Random.Range(0, 100));
        SKM.SetBlendShapeWeight(2, Random.Range(0, 100));
        SKM.SetBlendShapeWeight(3, Random.Range(0, 100));
    }
}