using UnityEngine;
using System.Collections;

public class Number : MonoBehaviour
{

    public void SetNumber(int newNumber)
    {

        if (newNumber == 0)
        {
            GetComponent<TextMesh>().text = "";
        }
        else
        {
            GetComponent<TextMesh>().text = newNumber.ToString();
        }
    }

}
