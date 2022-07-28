using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsMenuBehaviour : MonoBehaviour
{
    public GameObject icon;

    public const int ICON_COUNT = 9;

    public List<GameObject> icons = new List<GameObject>();

    public struct RowColumnCount
    {
        public int rowCount;

        public int columnCount;
    }

    void Start()
    {
        instantiateIcons();
    }

    void instantiateIcons()
    {
        var rowColumnCount = new RowColumnCount();
        determineRowAndColumnCount (ref rowColumnCount);

		var x = transform.position.x + 0.1f;
		var initialY = transform.position.y / 2;
		var initialZ = transform.position.z / 2;

        for (var i = 0; i < rowColumnCount.rowCount; i++)
        {
            for (var j = 0; j < rowColumnCount.columnCount; j++)
            {
                var instantiatedIcon =
                    Instantiate(icon,
                    new Vector3(x,
                        initialY,
                        initialZ),
                    Quaternion.identity);
                instantiatedIcon.transform.parent = gameObject.transform;
                instantiatedIcon.transform.localScale =
                    new Vector3(0.1f, 0.35f, 0.35f);
                icons.Add (instantiatedIcon);
            }
        }
    }

    void determineRowAndColumnCount(ref RowColumnCount rowColumnCount)
    {
        while (true)
        {
            if (
                rowColumnCount.rowCount * rowColumnCount.columnCount >=
                ICON_COUNT
            )
            {
                break;
            }
            rowColumnCount.rowCount++;
            if (
                rowColumnCount.rowCount * rowColumnCount.columnCount >=
                ICON_COUNT
            )
            {
                break;
            }
            rowColumnCount.columnCount++;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
