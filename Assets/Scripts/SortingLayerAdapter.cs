using UnityEngine;
using System.Collections;

public class SortingLayerAdapter : MonoBehaviour
{

    public string sortingLayerName = "Default";
    public int sortingOrder = 0;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().sortingLayerName = sortingLayerName;
        gameObject.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;

        // Validation to prevent errors when refactoring
        if (gameObject.GetComponent<MeshRenderer>().sortingLayerName != sortingLayerName)
        {
            Debug.LogError("Error adding " + sortingLayerName + " as sortingLayerName");
        }
    }

}
