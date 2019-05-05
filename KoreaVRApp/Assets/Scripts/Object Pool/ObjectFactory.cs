using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ATTACH TO ObjectFactory gameObject
public class ObjectFactory: MonoBehaviour {

	public static ObjectFactory instance;

	void Awake()
	{
		instance = this;
	}

	public enum PrefabType
	{
		None,

		UserVideoUI,

		LocalVideoUI,

		DownloadVideoUI,

		InboxVideoUI,

		FavoriteVideoUI,
	}

	public Dictionary<PrefabType,string> PrefabPaths = new Dictionary<PrefabType, string> {
		
		{ PrefabType.None, "" },

		// Effect
		{ PrefabType.UserVideoUI, "Prefabs/UI/UserVideo" },
		{ PrefabType.LocalVideoUI, "Prefabs/UI/LocalVideo" },
		{ PrefabType.DownloadVideoUI, "Prefabs/UI/DownloadVideo" },
		{ PrefabType.InboxVideoUI, "Prefabs/UI/InboxVideo" },
		{ PrefabType.FavoriteVideoUI, "Prefabs/UI/FavoriteVideo" },
	};

	// Make GameObject from Resources
	public GameObject MakeObject(PrefabType type)
	{
		string path;
		if (PrefabPaths.TryGetValue (type, out path)) {
			return (Instantiate (Resources.Load (path, typeof(GameObject))) as GameObject);
		}
		print ("NULL");
		return null;
	}

	// Load gameObject from resources
	public GameObject LoadObject(PrefabType type)
	{
		string path;
		if (PrefabPaths.TryGetValue (type, out path)) {
			return (Resources.Load (path, typeof(GameObject))) as GameObject;
		}
		print ("NULL");
		return null;
	}

}
