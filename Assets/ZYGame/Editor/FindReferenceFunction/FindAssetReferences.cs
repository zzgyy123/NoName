using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using ZYGame.ExtensionMethods;

namespace ZYGame.Extension
{
    public class FindAssetReferences
    {
        [MenuItem("CONTEXT/Component/Find component references")]
        private static void FindReferencesToComponent(MenuCommand command)
        {
            FindReferencesTo(command.context);
        }

        [MenuItem("Assets/Find Asset references")]
        private static void FindReferencesToAsset()
        {
            foreach (var assetObject in Selection.objects)
            {
                FindReferencesTo(assetObject);
            }
        }

        private static void FindReferencesTo(Object to)
        {
            if (to == null)
                return;

            var referencedBy = new List<Object>();
            var allObjects = Object.FindObjectsOfType<GameObject>();

            foreach (var go in allObjects)
            {
                if (PrefabUtility.GetPrefabType(go) == PrefabType.PrefabInstance)
                {
                    if (PrefabUtility.GetPrefabParent(go) == to)
                    {
                        Debug.Log(string.Format("{0} referenced by {1}, {2}", to, go.transform.GetPath(), go.GetType()), go);
                        referencedBy.Add(go);
                    }

                    continue;
                }

                var components = go.GetComponents<Component>();
                foreach (var component in components)
                {
                    var iterator = new SerializedObject(component).GetIterator();

                    while (iterator.NextVisible(true))
                    {
                        if (iterator.propertyType == SerializedPropertyType.ObjectReference)
                        {
                            if (iterator.objectReferenceValue == to)
                            {
                                Debug.Log(string.Format("{0} referenced by {1}, {2}", to, component.transform.GetPath(), component.GetType()), component);
                                referencedBy.Add(component);
                            }
                        }
                    }
                }
            }

            if (referencedBy.Count == 0)
                Debug.Log(to + " no references in scene");
        }
    }
}