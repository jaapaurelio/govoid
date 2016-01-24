using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System;

public class LevelLoader : MonoBehaviour {

	// Use this for initialization
	public void Start () 
	{
		try
		{
			TextAsset xmlData = new TextAsset();
			xmlData = (TextAsset)Resources.Load("Levels/Group0/Level1", typeof(TextAsset));

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xmlData.text);

			var names = xmlDoc.SelectNodes("//GridHouse/@number");


			 XmlNodeList Level = xmlDoc.GetElementsByTagName("level");
	


			foreach (XmlNode GridRow in Level)
			{
				foreach (XmlNode GridHouse in GridRow.ChildNodes)
				{
					string teste = GridHouse.Attributes["number"].Value;
					Debug.Log(teste);
				}
			}
		}
		catch(Exception e)
		{
			Debug.Log(e);
		}
	}
		
	private void ReadLevel(string t)
	{
		Debug.Log(t);
	}
}
