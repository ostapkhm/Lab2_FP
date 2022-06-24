using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class BezierCurve2DTests
{
    private static bool CheckEquality(Vector2[] first, Vector2[] second)
    {
        double eps = 10e-2;

        for (int i = 0; i < first.Rank; i++)
        {
            if (first.GetLength(i) != second.GetLength(i))
            {
                return false;
            }
        }

        for (int i = 0; i < first.GetLength(0); i++)
        {
            if (System.Math.Abs(first[i].x - second[i].x) > eps || System.Math.Abs(first[i].y - second[i].y) > eps)
            {
                return false;
            }
        }
        return true;
    }

    [Test]
    public void Test_BezierCurveConstructor1()
    {
        List<Vector3> controlPoints = new List<Vector3> { new Vector3(1.0f, 1.0f, 0.0f), new Vector3(2.0f, 2.0f, 0.0f) };
        BezierCurve2D bz = new BezierCurve2D(1, 5, controlPoints);
        Vector2[] actual = new[] { new Vector2(1.0f, 1.0f), new Vector2(1.25f, 1.25f), new Vector2(1.5f, 1.5f), new Vector2(1.75f, 1.75f), new Vector2(2f, 2f)};
        Vector2[] expected = bz.CurvePointsVector2D;

        bool equal = CheckEquality(bz.CurvePointsVector2D, actual);
        Assert.AreEqual(equal, true);
    }


    [Test]
    public void Test_BezierCurveConstructor2()
    {
        List<Vector3> controlPoints = new List<Vector3> { new Vector3(1.0f, 1.0f, 0.0f), new Vector3(2.0f, 2.0f, 0.0f) };
        BezierCurve2D bz = new BezierCurve2D(1, 2, controlPoints);
        Vector2[] actual = new[] { new Vector2(1.0f, 1.0f), new Vector2(2f, 2f) };
        Vector2[] expected = bz.CurvePointsVector2D;

        bool equal = CheckEquality(bz.CurvePointsVector2D, actual);
        Assert.AreEqual(equal, true);
    }


    [Test]
    public void Test_BezierCurveConstructor3()
    {
        List<Vector3> controlPoints = new List<Vector3> { new Vector3(1.0f, 1.0f, 0.0f), new Vector3(2.0f, 2.0f, 0.0f) };
        BezierCurve2D bz = new BezierCurve2D(1, 3, controlPoints);
        Vector2[] actual = new[] { new Vector2(1.0f, 1.0f), new Vector2(1.5f, 1.5f), new Vector2(2f, 2f) };
        Vector2[] expected = bz.CurvePointsVector2D;

        bool equal = CheckEquality(bz.CurvePointsVector2D, actual);
        Assert.AreEqual(equal, true);
    }


    [Test]
    public void Test_BezierCurveConstructor4()
    {
        List<Vector3> controlPoints = new List<Vector3> { new Vector3(1.0f, 1.0f, 0.0f), new Vector3(2.0f, 2.0f, 0.0f), new Vector3(3.0f, 3.0f, 0.0f) };
        BezierCurve2D bz = new BezierCurve2D(2, 3, controlPoints);
        Vector2[] actual = new[] { new Vector2(1.0f, 1.0f), new Vector2(2.0f, 2.0f), new Vector2(3f, 3f) };
        Vector2[] expected = bz.CurvePointsVector2D;

        bool equal = CheckEquality(bz.CurvePointsVector2D, actual);
        Assert.AreEqual(equal, true);
    }


    [Test]
    public void Test_BezierCurveConstructor5()
    {
        List<Vector3> controlPoints = new List<Vector3> { new Vector3(1.0f, 1.0f, 0.0f), new Vector3(2.0f, 2.0f, 0.0f), new Vector3(3.0f, 1.0f, 0.0f) };
        BezierCurve2D bz = new BezierCurve2D(2, 6, controlPoints);
        Vector2[] actual = new[] { new Vector2(1.0f, 1.0f), new Vector2(1.4f, 1.32f), new Vector2(1.8f, 1.48f), new Vector2(2.2f, 1.48f), new Vector2(2.6f, 1.32f), new Vector2(3.0f, 1.0f)};
        Vector2[] expected = bz.CurvePointsVector2D;

        bool equal = CheckEquality(bz.CurvePointsVector2D, actual);
        Assert.AreEqual(equal, true);
    }


    [Test]
    public void Test_BezierCurveConstructor6()
    {
        List<Vector3> controlPoints = new List<Vector3> { new Vector3(1.0f, 1.0f, 0.0f), new Vector3(1.0f, 3.0f, 0.0f), new Vector3(4.0f, 3.0f, 0.0f), new Vector3(4.0f, 1.0f, 0.0f)};
        BezierCurve2D bz = new BezierCurve2D(3, 5, controlPoints);
        Vector2[] actual = new[] { new Vector2(1.0f, 1.0f), new Vector2(1.47f, 2.13f), new Vector2(2.5f, 2.5f), new Vector2(3.53f, 2.13f), new Vector2(4.0f, 1.0f)};
        Vector2[] expected = bz.CurvePointsVector2D;

        bool equal = CheckEquality(bz.CurvePointsVector2D, actual);
        Assert.AreEqual(equal, true);
    }
}
