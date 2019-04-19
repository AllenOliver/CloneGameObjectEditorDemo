///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Description:       Creates an editor window to assist in placement of prefabs
//	Author:            Allen Oliver
//	Created:           Friday, April 19, 2019
//	Copyright:         Allen Oliver, 2019
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEditor;
using UnityEngine;

public class CreateBlocksWindow : EditorWindow
{
    public Vector3 dir;
    public GameObject objToSpawn;
    public int numToSpawn;
    public int unitSize;
    public int offset;
    public GameObject ParentObject;

    [MenuItem("Allen's Tools/Object Placer")]
    public static void ShowWindow()
    {
        GetWindow<CreateBlocksWindow>("Create GameObjects in the world.");
    }

    private void OnGUI()
    {
        dir = EditorGUILayout.Vector3Field("The Direction to put objects.", dir);
        EditorGUILayout.LabelField("The object you want spawned.");

        objToSpawn =
            (GameObject)EditorGUILayout.ObjectField(objToSpawn, typeof(GameObject));
        EditorGUILayout.LabelField("The Parent object.");

        ParentObject =
             (GameObject)EditorGUILayout.ObjectField(ParentObject, typeof(GameObject));
        EditorGUILayout.LabelField("Should there be an offset? (default 1)");
        offset = EditorGUILayout.IntSlider(offset, 1, 5);

        EditorGUILayout.LabelField("How many objects?");
        numToSpawn = EditorGUILayout.IntSlider(numToSpawn, 1, 5);

        EditorGUILayout.LabelField("What size are your blocks?");
        unitSize = EditorGUILayout.IntSlider(unitSize, 1, 100);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Objects!"))
        {
            PlaceObjects();
        }

        if (GUILayout.Button("Clear Selections"))
        {
            dir = Vector3.zero;

            objToSpawn = null;
            numToSpawn = 1;
            unitSize = 1;
            ParentObject = null;
        }

        GUILayout.EndHorizontal();
    }

    #region Placing object logic

    /// <summary>
    /// Places the objects.
    /// </summary>
    private void PlaceObjects()
    {
        var obj = Selection.activeGameObject;
        if (numToSpawn < 20) //This is just an arbitrary number. Felt like it might end up causing more work if you were placing this many objects.
        {
            for (int i = 0; i < numToSpawn; i++)
            {
                if (dir == Vector3.right)
                {
                    Instantiate(objToSpawn, new Vector3((obj.transform.position.x + (calculateOffset() * (i + 1))), obj.transform.position.y, obj.transform.position.z), Quaternion.identity, ParentObject.transform);
                }
                else if (dir == Vector3.left)
                {
                    Instantiate(objToSpawn, new Vector3((obj.transform.position.x + (-calculateOffset() * (i + 1))), obj.transform.position.y, obj.transform.position.z), Quaternion.identity, ParentObject.transform);
                }
                else if (dir == Vector3.up)
                {
                    Instantiate(objToSpawn, new Vector3(obj.transform.position.x, (obj.transform.position.y + (calculateOffset() * (i + 1))), obj.transform.position.z), Quaternion.identity, ParentObject.transform);
                }
                else if (dir == Vector3.down)
                {
                    Instantiate(objToSpawn, new Vector3(obj.transform.position.x, (obj.transform.position.y + (-calculateOffset() * (i + 1))), obj.transform.position.z), Quaternion.identity, ParentObject.transform);
                }
                else if (dir == Vector3.forward)
                {
                    Instantiate(objToSpawn, new Vector3(obj.transform.position.x, obj.transform.position.y, (obj.transform.position.z + (calculateOffset() * (i + 1)))), Quaternion.identity, ParentObject.transform);
                }
                else if (dir == Vector3.back)
                {
                    Instantiate(objToSpawn, new Vector3(obj.transform.position.x, obj.transform.position.y, (obj.transform.position.z + (-calculateOffset() * (i + 1)))), Quaternion.identity, ParentObject.transform);
                }
                else
                {
                    Debug.Log("Some error has occurred.");
                }
            }
        }
        else
        {
            Debug.Log("Turn down the number of objects you want to spawn please.");
        }
    }

    /// <summary>
    /// Calculates the offset.
    /// </summary>
    /// <returns></returns>
    private int calculateOffset()
    {
        return offset * unitSize;
    }

    #endregion Placing object logic
}