﻿using UnityEngine;
using System.Collections.Generic;

namespace SceneExport{
	public class ObjectMapper<Resource>{
		public delegate void OnNewObjectAdded(Resource res);
		public int nextId = 0;
		public Dictionary<Resource, int> objectMap = new Dictionary<Resource, int>();
		public List<Resource> objectList = new List<Resource>();

		public bool isValidObjectId(int id){
			return (id >= 0) && (id < objectList.Count);
		}
		
		public int numObjects{
			get{
				return objectList.Count;
			}
		}
		
		public Resource getObject(int id){
			if (!isValidObjectId(id))
				throw new System.ArgumentException(string.Format("Invalid object id {0}", id));
			return objectList[id];
		}
		
		public bool hasObject(Resource obj){
			return ExportUtility.isValidId(getId(obj, false));
		}
			
		public int findId(Resource obj){
			return getId(obj, false, null);
		}
		
		public int getId(Resource obj){
			return getId(obj, true, null);
		}
		
		//public void registerObj(Resource obj
		
		public int getId(Resource obj, bool createMissing, OnNewObjectAdded onAddCallback = null){
			int result = -1;
			if (Object.Equals(obj, null))
				return result;
			if (objectMap.TryGetValue(obj, out result))
				return result;
			if (!createMissing)
				return -1;
			result = nextId;
			objectMap[obj] = result;
			objectList.Add(obj);
			nextId++;
			if (onAddCallback != null)
				onAddCallback(obj);
			return result;						
		}
	}
}