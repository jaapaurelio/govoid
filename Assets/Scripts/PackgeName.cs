using UnityEngine;
using System.Collections;

public class PackgeName : ButtonText
{

    private int packageNumber = 0;

    override public void OnTouch()
    {
        GameObject.Find("PackageLister").GetComponent<PackageLister>().OpenPackage(packageNumber);
    }

    public void SetNumber(int num, string levelsInfo)
    {
        gameObject.GetComponent<TextMesh>().text = "package " + num + " " + levelsInfo;
        packageNumber = num;
    }

}
