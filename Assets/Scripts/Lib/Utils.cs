using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour {

    public static Vector2Int coordsToTile( Vector3 v )
    {
        int col = Mathf.RoundToInt(v.x / 1.5f) + 30;
        int row = Mathf.RoundToInt(v.z / 1.5f);
        if (row < 0) row += 100;
        return new Vector2Int(row,col);
    }

    public static T GetComponentAddIfNotExists<T>(GameObject gameObj) where T: Component
    {
        T c;
        if (gameObj.GetComponent<T>() != null)
        {
            c = gameObj.GetComponent<T>();
        }
        else c = gameObj.AddComponent<T>();

        return c;
    } 

}
