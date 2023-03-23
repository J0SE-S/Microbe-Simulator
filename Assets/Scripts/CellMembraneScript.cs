using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMembraneScript : MonoBehaviour {//sin(i2pi/n) cos(i2pi/n)
    LineRenderer line;
    List<GameObject> points;
    List<Vector3> tempPoints;
    public float PRESSURE_CONSTANT;
    float currentArea;

    void Start() {
        line = GetComponent<LineRenderer>();
        points = new List<GameObject>();
        RedrawLine(10);
    }

    void Update() {
        tempPoints = new List<Vector3>();
        foreach (GameObject point in points) {
            tempPoints.Add(point.transform.position);
        }
        line.SetPositions(tempPoints.ToArray());
    }

    void FixedUpdate() {
        currentArea = CalculateArea();
        Debug.Log(currentArea);
        for (int i = 0; i < points.Count; i++) {
            /*Vector2 force = new Vector2(
                    (points[(int)Mathf.Repeat((i-1), points.Count)].transform.position.x
                    + points[(int)Mathf.Repeat((i+1), points.Count)].transform.position.x)/2,
                    (points[(int)Mathf.Repeat((i-1), points.Count)].transform.position.y
                    + points[(int)Mathf.Repeat((i+1), points.Count)].transform.position.y)/2
            );*/
            /*Vector2 direction = new Vector2(
                    points[(int)Mathf.Repeat((i-1), points.Count)].transform.position.y
                    -points[(int)Mathf.Repeat((i+1), points.Count)].transform.position.y,
                    -points[(int)Mathf.Repeat((i-1), points.Count)].transform.position.x
                    +points[(int)Mathf.Repeat((i-1), points.Count)].transform.position.x
            );
            direction.Normalize();*/
            Vector2 v1 = points[(int)Mathf.Repeat((i+1), points.Count)].transform.position-points[(int)Mathf.Repeat((i-1), points.Count)].transform.position;
            Vector2 v2 = points[(int)Mathf.Repeat((i), points.Count)].transform.position-points[(int)Mathf.Repeat((i-1), points.Count)].transform.position;
            Vector2 direction = v2 - (v1 * (Vector2.Dot(v1, v2) / Vector2.Dot(v1, v1)));
            points[i].GetComponent<Rigidbody2D>().AddForce(direction / (Vector2.Dot(direction, direction) + 0.1f) * -PRESSURE_CONSTANT / currentArea);
        }
    }

    void RedrawLine(int pointNum) {
        line.positionCount = pointNum;
        foreach (GameObject point in points) {
            Destroy(point);
        }
        points = new List<GameObject>();
        for (int i = 0; i < pointNum; i++) {
            points.Add(new GameObject("node" + i));
            points[i].transform.SetParent(gameObject.transform);
            points[i].transform.position = new Vector3(Mathf.Sin(i*2*Mathf.PI/pointNum),Mathf.Cos(i*2*Mathf.PI/pointNum),0);//when adding map, add the offsets to the point where the cell is
            points[i].AddComponent<Rigidbody2D>();
            points[i].AddComponent<SpringJoint2D>();
            points[i].AddComponent<SpringJoint2D>();
            SpringJoint2D[] joints = points[i].GetComponents<SpringJoint2D>();
            joints[0].autoConfigureDistance = false;
            joints[1].autoConfigureDistance = false;
            joints[0].dampingRatio = 1;
            joints[1].dampingRatio = 1;
            if (i > 0) {
                joints[0].connectedBody = points[i-1].GetComponent<Rigidbody2D>();
                points[i-1].GetComponents<SpringJoint2D>()[1].connectedBody = points[i].GetComponent<Rigidbody2D>();
            }
            if (i == pointNum-1) {
                joints[1].connectedBody = points[0].GetComponent<Rigidbody2D>();
                points[0].GetComponents<SpringJoint2D>()[0].connectedBody = points[i].GetComponent<Rigidbody2D>();
            }
        }
    }

    float CalculateArea() {
        float area = 0;
        for (int i = 0; i < points.Count - 1; i++) {
            area += -points[i].transform.position.y*points[i+1].transform.position.x + points[i].transform.position.x*points[i+1].transform.position.y;
        }
        area += -points[points.Count-1].transform.position.y*points[0].transform.position.x + points[points.Count-1].transform.position.x*points[0].transform.position.y;
        return Mathf.Abs(area)/2;
    }
}