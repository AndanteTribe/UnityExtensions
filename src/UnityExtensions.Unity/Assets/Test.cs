using UnityEngine;
using UnityExtensions;

public class Test : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(gameObject.GetHierarchyPath());
    }
}