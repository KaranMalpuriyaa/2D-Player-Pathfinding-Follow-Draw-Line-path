using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
[RequireComponent (typeof (LineRenderer))]
public class Player : MonoBehaviour
{
    private NavMeshAgent myNavMeshAgent;
    private LineRenderer lineRenderer;
    public Rigidbody2D rb;

    Vector2 pos;
    Vector2 pointPosition;

    private void Start () {

        myNavMeshAgent = GetComponent<NavMeshAgent> ();
        lineRenderer = GetComponent<LineRenderer> ();

        myNavMeshAgent.updateRotation = false;
        myNavMeshAgent.updateUpAxis = false;
        lineRenderer.positionCount = 0;
    }

    private void Update () {

        if(Input.GetMouseButton(0)) {

            pos = new Vector3(Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y, transform.position.z + 0.5f);

            myNavMeshAgent.SetDestination (pos);

            Vector2 lookdir = pos - rb.position;
            float angle = Mathf.Atan2 (lookdir.y, lookdir.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }

        if(Vector3.Distance(pos, transform.position) <= myNavMeshAgent.stoppingDistance) {

        }else if(myNavMeshAgent.hasPath) {
            DrawPath ();
        }
    }

    void SetDestination(Vector3 target) {
        myNavMeshAgent.SetDestination (target);
    }

    void DrawPath() {
      
        lineRenderer.positionCount = myNavMeshAgent.path.corners.Length;
        lineRenderer.SetPosition (0, transform.position);
      
        if(myNavMeshAgent.path.corners.Length < 2) {
            return;
        }

        for(int i = 0; i < myNavMeshAgent.path.corners.Length; i++) {
            pointPosition = new Vector3 (myNavMeshAgent.path.corners[i].x, myNavMeshAgent.path.corners[i].y, myNavMeshAgent.path.corners[i].z);
            lineRenderer.SetPosition (i, pointPosition);
            Vector2 lookdir = pointPosition - rb.position;
            float angle = Mathf.Atan2 (lookdir.y, lookdir.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }
}
