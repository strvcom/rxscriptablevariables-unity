using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace STRV.Variables.Utils
{
	public static class JsonHelper {

		public static T ObjectFromJson<T>(string json) {
			return JsonUtility.FromJson<T>(json);
		}

		public static string ObjectToJson<T>(T obj) {
			return JsonUtility.ToJson(obj);
		}

		public static T[] ArrayFromJson<T>(string json) {
			var wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
			return wrapper.Items;
		}

		public static string ArrayToJson<T>(T[] array) {
			var wrapper = new Wrapper<T> {Items = array};
			return JsonUtility.ToJson(wrapper);
		}
		
		public static Dictionary<string, string> DictionaryFromJson(string json) {
			var wrapper = JsonUtility.FromJson<Wrapper<DictionaryWrapper>>(json);
			var dict = new Dictionary<string, string>();
			if (wrapper != null)
			{
				foreach (var item in wrapper.Items)
				{
					dict[item.Key] = item.Value;
				}
			}
			
			return dict;
		}

		public static string DictionaryToJson(Dictionary<string, string> dictionary) {
			var items = new List<DictionaryWrapper>();
			foreach (var keyValue in dictionary)
			{
				items.Add(new DictionaryWrapper
				{
					Key = keyValue.Key,
					Value = keyValue.Value
				});
			}
			
			var wrapper = new Wrapper<DictionaryWrapper> { Items = items.ToArray()};
			return JsonUtility.ToJson(wrapper);
		}

		[Serializable]
		private class Wrapper<T> {
			public T[] Items;
		}
		
		// This was originally generic but Unity was unable to handle that
		[Serializable]
		private class DictionaryWrapper
		{
			public string Key;
			public string Value;
		}
	}
}
