﻿using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static object _instance;

	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(T)) as T;

				if (_instance == null)
				{
					GameObject host = SingletonManager.Instance.CreateSingletonHost();
					host.AddComponent(typeof(T));
					host.name += " [autogenerated]";
				}
			}

			return (T)_instance;
		}
	}

	protected virtual bool isGlobalScope
	{
		get
		{
			return true;
		}
	}

	protected virtual void onAwake()
	{
		if (_instance != null && _instance != this)
		{
			DebugManager.Instance.Log ("Deleting duplicate instance {0} of singleton class {1}", name, typeof(T));
			Destroy(gameObject);
		}
		else
		{
			_instance = this;
			gameObject.name = "_" + this.GetType ().ToString();

			if (isGlobalScope)
				DontDestroyOnLoad(gameObject);
		}
	}
}
