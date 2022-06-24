using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BezierCurve2D
{
    private int _curveOrder;
    private Vector2[] _controlPoints;
    private int _numPoints;
    private Vector2[] _curvePoints;

    // Calculating Binomial coef's
    private static ulong Binomial(int n, int i)
    {
        // Assume that n <= 67

        if (n == 1 || n == 0)
        {
            return 1;
        }

        // Using pascal triangle to find binomial coef
        ulong[,] triangleArr = new ulong[2, n + 1];

        int currentIdx = 1;
        int prevIdx = 0;

        // Init 
        triangleArr[0, 0] = 1;
        triangleArr[0, 1] = 1;
        triangleArr[1, 0] = 1;
        triangleArr[1, 1] = 2;
        triangleArr[1, 2] = 1;

        if (n == 2)
        {
            return triangleArr[1, i];
        }

        for (int j = 3; j <= n; j++)
        {
            if (j % 2 != 0)
            {
                currentIdx = 0;
                prevIdx = 1;
            }
            else
            {
                currentIdx = 1;
                prevIdx = 0;
            }

            for (int k = 0; k <= j; k++)
            {
                triangleArr[currentIdx, k] = 0;
                if (k - 1 >= 0)
                {
                    triangleArr[currentIdx, k] += triangleArr[prevIdx, k - 1];
                }
                if (k < j)
                {
                    triangleArr[currentIdx, k] += triangleArr[prevIdx, k];
                }
            }
        }

        return triangleArr[currentIdx, i];
    }

    // Calculating Bernstein's coef in order to calculate BezierCurve
    private static float Bernstein(int n, int i, float t)
    {
        float t_i = Mathf.Pow(t, i);
        float t_n_minus_i = Mathf.Pow((1 - t), (n - i));

        float basis = Binomial(n, i) * t_i * t_n_minus_i;
        return basis;
    }


    private void CalculateBezierCurve()
    {
        Vector2[] res = new Vector2[_numPoints];
        

        float[,] resFloat = new float[_numPoints, 2];
        float bernsteinKoef;

        int j = 0;
        while (j < _numPoints)
        {
            for (int i = 0; i <= _curveOrder; i++)
            {
                bernsteinKoef = Bernstein(_curveOrder, i, (float)j / (_numPoints - 1));

                resFloat[j, 0] += bernsteinKoef * _controlPoints[i].x;
                resFloat[j, 1] += bernsteinKoef * _controlPoints[i].y;
            }
            j++;
        }

        for (int i = 0; i < _numPoints; i++)
        {
            res[i] = new Vector2(resFloat[i, 0], resFloat[i, 1]);
        }

        _curvePoints = res;
    }


    // Bezier Curve Constructor
    public BezierCurve2D(int curveOrder, int numPoints, List<Vector3> controlPoints)
    {
        if (numPoints < 2)
        {
            throw new System.ArgumentException("To few points amount!");
        }

        if (curveOrder > 67)
        {
            throw new System.ArgumentException("Curve order is to high!");
        }

        if (controlPoints.Count != curveOrder + 1)
        {
            throw new System.ArgumentException("Wrong amount of controlPoints list!");
        }

        _curveOrder = curveOrder;
        _numPoints = numPoints;
        _controlPoints = CastToVector2Array(controlPoints);
        CalculateBezierCurve();
    }

    public Vector2[] CurvePointsVector2D
    {
        get { return _curvePoints; }
    }

    public Vector3[] CurvePointsVector3D
    {
        get {
            Vector3[] res = new Vector3[_numPoints];
            for (int k = 0; k < _numPoints; k++)
            {
                res[k] = new Vector3(_curvePoints[k].x, _curvePoints[k].y, 0);
            }

            return res;
        }
    }

    private Vector2[] CastToVector2Array(List<Vector3> controlPoints)
    {
        Vector2[] res = new Vector2[controlPoints.Count];

        for (int k = 0; k < controlPoints.Count; k++)
        {
            res[k] = new Vector2(controlPoints[k].x, controlPoints[k].y);
        }
        return res;
    }
}
