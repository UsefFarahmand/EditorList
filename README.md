# EditorList
[![Unity 2020.3+](https://img.shields.io/badge/unity-2020.3%2B-blue.svg)](https://unity3d.com/get-unity/download)

With this class, from now on you can display your lists in the editor window visually!!

### Usage
1. Add [EditorList](EditorList.cs) to your project in Editor folder.
2. Create a Sample Editor Window Class
3. Create variable from them

#### Sample
``` C#
using UnityEditor;
using UnityEngine;

public class Preview : EditorWindow
{
    EditorList<GameObject> gameObjects = new EditorList<GameObject>();

    [MenuItem("Tools/Preview")]
    public static void ShowWindow()
    {
        Preview window = (Preview)GetWindow(typeof(Preview));
        window.Show();
    }

    private void OnGUI()
    {
        gameObjects.DrawList(nameof(gameObjects), position);
    }
}
```
![Preview](/Preview/Preview%20window.png)

