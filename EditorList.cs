using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace UnityEditor
{
    public class EditorList<T> : IList<T> where T : Object
    {
        private List<T> list;
        private Vector2 scrollPosition;
        private float height;

        public EditorList()
        {
            list = new List<T>();
        }

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public T this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        public void Add(T item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return list.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public void Sort(System.Comparison<T> comparison)
        {
            list.Sort(comparison);
        }

        public void Sort()
        {
            list.Sort();
        }

        public T[] ToArray()
        {
            return list.ToArray();
        }

        public void AddRange(IEnumerable<T> collection)
        {
            list.AddRange(collection);
        }

        public void AddRange(T[] array)
        {
            list.AddRange(array);
        }

        public void AddRange(List<T> list)
        {
            this.list.AddRange(list);
        }

        public void AddRange(EditorList<T> list)
        {
            this.list.AddRange(list.list);
        }

        public void RemoveRange(int index, int count)
        {
            list.RemoveRange(index, count);
        }

        public void ClearList()
        {
            list.Clear();
        }

        public void RemoveAll(System.Predicate<T> match)
        {
            list.RemoveAll(match);
        }

        /// <summary>
        /// Draws a list of objects in the inspector.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="size"></param>
        /// <param name="maxHeight"></param>
        public void DrawList(string title, Rect size, float maxHeight = 60)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(CreateTitle(title));
            int count = EditorGUILayout.IntField(list.Count, GUILayout.MaxWidth(50));
            if (count < 0)
            {
                count = 0;
            }
            if (count != list.Count)
            {
                while (count < list.Count)
                {
                    list.RemoveAt(list.Count - 1);
                }
                while (count > list.Count)
                {
                    list.Add(null);
                }
            }
            EditorGUILayout.EndHorizontal();

            if (count > 0)
            {
                if (height != count * 22)
                {
                    height = count * 22;
                }

                if (maxHeight < 60)
                    maxHeight = 60;

                EditorGUILayout.BeginVertical(GUI.skin.box);
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(size.width - 10), GUILayout.Height(height > size.height - maxHeight ? size.height - maxHeight : height));
                for (int i = 0; i < list.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    list[i] = (T)EditorGUILayout.ObjectField(list[i], typeof(T), true);
                    if (GUILayout.Button("x", GUILayout.MaxWidth(20)))
                    {
                        list.RemoveAt(i);
                    }
                    EditorGUILayout.EndHorizontal();
                }
                GUILayout.EndScrollView();
                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("+", GUILayout.Width(20)))
            {
                list.Add(null);
            }
            EditorGUILayout.EndHorizontal();
        }

        private string CreateTitle(string title) => string.Join(" ", Regex.Replace(title, "(\\B[A-Z])", " $1")
                                                                          .Split(' ')
                                                                          .ToList()
                                                                          .ConvertAll(word => word.Substring(0, 1).ToUpper() + word.Substring(1)));
    }
}