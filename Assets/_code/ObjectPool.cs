using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	[SerializeField] GameObject _object;
	[SerializeField] int amount;
	[SerializeField] bool active = false;
	GameObject[] _objects;

	int _nextObject = 1;

	public List<GameObject> instantiatedObjects = new();

	void Awake()
	{
		InstantiateObjects();
	}

	Vector3 zero = new Vector3(0, 0, 0);
	void InstantiateObjects()
	{
		_objects = new GameObject[amount];
		for (int i = 0; i < amount; i++)
		{
			_objects[i] = Instantiate(_object, zero, Quaternion.identity);
			_objects[i].transform.parent = gameObject.transform;
			_objects[i].gameObject.SetActive(active);
			instantiatedObjects.Add(_objects[i]);
		}
	}

	public void SetActiveObject(Vector3 position, Quaternion rotation)
	{
		GetAvailable();
		GameObject newObject = _objects[_nextObject];
		newObject.transform.position = position;
		newObject.transform.rotation = rotation;
		newObject.gameObject.SetActive(true);
	}

	public GameObject GrabActiveObject(Vector3 position, Quaternion rotation)
	{
		GetAvailable();
		GameObject newObject = _objects[_nextObject];
		newObject.transform.position = position;
		newObject.transform.rotation = rotation;
		newObject.gameObject.SetActive(true);
		return newObject;
	}

	void GetAvailable()
	{
		for (int i = 0; i < _objects.Length; i++)
		{
			if (!_objects[i].gameObject.activeInHierarchy)
			{
				_nextObject = i;
				return;
			}
		}
	}
}
