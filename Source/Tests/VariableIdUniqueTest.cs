using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using STRV.Variables.Persistance;
using UnityEditor;

namespace Variables.Source.Tests
{
    public class VariableIdUniqueTest
    {
        [Test]
        public void VariableAllIdsUnique()
        {
            var persistor = GetPersistor();

            var idsSet = new HashSet<string>();
            var items = persistor.TestOnly_GetItems();

            foreach (var item in items)
            {
                var key = item.GetKey();
                Assert.IsTrue(!string.IsNullOrEmpty(key), $"Missing key on object \"{item.name}\"");
                
                if (idsSet.Contains(key))
                {
                    Assert.Fail($"Duplicated key for serialization {key}");
                }
                else
                {
                    idsSet.Add(key);
                }
            }
        }
        
        private VariablePersistor GetPersistor()
        {
#if UNITY_EDITOR
            string[] guids = AssetDatabase.FindAssets("t:"+ typeof(VariablePersistor).Name);  //FindAssets uses tags check documentation for more info
            VariablePersistor[] a = new VariablePersistor[guids.Length];
            for(int i =0;i<guids.Length;i++)         //probably could get optimized 
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<VariablePersistor>(path);
            }
 
            return a.First();
#else
            return null;
#endif
        }

    }
}