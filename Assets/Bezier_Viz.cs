using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier_Viz : MonoBehaviour
{
    public List<Vector2> ControlPoints;
    public int CurvePointsAmount;
    public UnityEngine.Object PointPrefab;
    public float LineWidth;

    private List<GameObject> _pointGameObjects;
    private LineRenderer[] _lineRenderers;
    private Vector3[] _curvePoints;
    private List<Vector3> _controlPoints;

    // Start is called before the first frame update
    void Start()
    {
        // Init 
        _curvePoints = new Vector3[CurvePointsAmount];
        _controlPoints = new List<Vector3>();
        _pointGameObjects = new List<GameObject>();

        // Create the two LineRenderers.
        _lineRenderers = new LineRenderer[2];
        _lineRenderers[0] = CreateLine(Color.gray);
        _lineRenderers[1] = CreateLine(Color.green);

        // Set a name to the game objects for the LineRenderers
        _lineRenderers[0].gameObject.name = "LineRenderer_obj_0";
        _lineRenderers[1].gameObject.name = "LineRenderer_obj_1";

        // Create the instances of PointPrefab
        for (int i = 0; i < ControlPoints.Count; i++)
        {
            GameObject obj = (GameObject)Instantiate(PointPrefab, ControlPoints[i], Quaternion.identity);
            obj.name = "ControlPoint_" + i.ToString();
            _pointGameObjects.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        LineRenderer lineRenderer = _lineRenderers[0];
        LineRenderer curveRenderer = _lineRenderers[1];

        // Maybe some point were moved or added, so we need to update _controlPoints
        _controlPoints.Clear();

        for (int k = 0; k < _pointGameObjects.Count; k++)
        {
            _controlPoints.Add(_pointGameObjects[k].transform.position);
        }

        // Try to create our bezier curve. When to many points are added handle the exception or handle some other exception
        curveRenderer.positionCount = CurvePointsAmount;
        try
        {
            _curvePoints = new BezierCurve2D(_pointGameObjects.Count - 1, CurvePointsAmount, _controlPoints).CurvePointsVector3D;
        }
        catch (System.ArgumentException ex) when (ex.Message.Equals("Curve order is to high!"))
        {
            Debug.Log(ex.Message.ToString());
            GameObject Clone = _pointGameObjects[_pointGameObjects.Count - 1];
            _pointGameObjects.RemoveAt(_pointGameObjects.Count - 1);
            _controlPoints.RemoveAt(_controlPoints.Count - 1);
            Destroy(Clone);
        }
        catch (System.ArgumentException ex) when (ex.Message.Equals("To few points amount!"))
        {
            Debug.Log(ex.Message.ToString());
            Debug.Log("Pass more points amount into corresponded field!");
        }


        for (int k = 0; k < CurvePointsAmount; k++)
        {
            curveRenderer.SetPosition(k, _curvePoints[k]);
        }

        // Create a line renderer for showing the straight lines between _controlPoints
        lineRenderer.positionCount = _controlPoints.Count;

        for (int i = 0; i < _controlPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, _controlPoints[i]);
        }

    }

    private LineRenderer CreateLine(Color color)
    {
        GameObject obj = new GameObject();
        LineRenderer lr = obj.AddComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = LineWidth;
        lr.endWidth = LineWidth;
        return lr;
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isMouse)
        {
            if (e.clickCount == 2 && e.button == 0)
            {
                Vector2 rayPos = new Vector2(
                    Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

                InsertNewControlPoint(rayPos);
            }
        }
    }

    void InsertNewControlPoint(Vector2 controlPoint)
    {
        GameObject obj = (GameObject)Instantiate(PointPrefab, controlPoint, Quaternion.identity);
        obj.name = "ControlPoint_" + _pointGameObjects.Count.ToString();
        _pointGameObjects.Add(obj);
    }
}