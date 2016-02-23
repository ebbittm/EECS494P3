using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

    public float FieldOfView = 110f;
    public bool PlayerInView;
    public bool PlayerInRadius;
    public Vector3 LastSeen;

    private GameObject Player;
    private SphereCollider VisionRange;
    [SerializeField] private GameObject Self;

	void Awake () {
        Player = GameObject.FindGameObjectWithTag("Player");
        VisionRange = GetComponent<SphereCollider>();
	}
	
	void OnTriggerStay (Collider other) {
	    if(other.gameObject == Player)
        {
            PlayerInView = false;
            PlayerInRadius = true;
            Vector3 direction = other.transform.position - Self.transform.position;
            float angle = Vector3.Angle(direction, Self.transform.forward);
            if(angle < FieldOfView * 0.5f)
            {
                if(CheckIfVisible())
                {
                    print("can see player");
                    PlayerInView = true;
                    FieldOfView = 180f;
                    LastSeen = Player.transform.position;
                }
            }
        }
	}

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            PlayerInView = false;
            PlayerInRadius = false;
            if (CheckIfVisible())
            {
                LastSeen = Player.transform.position;
            }
        }
    }

    public bool CheckIfVisible()
    {
        Vector3 direction = Player.transform.position - Self.transform.position;
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, direction.normalized, out hitInfo, VisionRange.radius))
        {
            if (hitInfo.collider.gameObject == Player)
            {
                return true;
            }
        }
        FieldOfView = 110f;
        return false;
    }
}
