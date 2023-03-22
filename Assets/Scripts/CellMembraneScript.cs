using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMembraneScript : MonoBehaviour {//sin(i2pi/n) cos(i2pi/n)
    LineRenderer line;
    public List<GameObject> points;
    List<Vector3> tempPoints;

    void Start() {
        line = GetComponent<LineRenderer>();
        points = new List<GameObject>();
        RedrawLine(5);
    }

    void Update() {
        tempPoints = new List<Vector3>();
        foreach (GameObject point in points) {
            tempPoints.Add(point.transform.position);
        }
        line.SetPositions(tempPoints.ToArray());
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
}