using UnityEngine;

public class CORE : MonoBehaviour
{
    [SerializeField] Sprite[] bossHurtSprites;
    [SerializeField] Transform objectToMove;
    [SerializeField] Transform verticalRail;
    [SerializeField] Transform horizontalRail;

    [Header("Movement")]
    [SerializeField] float moveSpeed;

    [Header("Turrets")]
    [SerializeField] Transform[] turretSpawnPositions;
    [SerializeField] GameObject turretPrefab;


    void Start ()
    {
	}

	void FixedUpdate ()
    {
        verticalRail.position = new Vector2(objectToMove.position.x, verticalRail.position.y);
        horizontalRail.position = new Vector2(horizontalRail.position.x, objectToMove.position.y);
    }
}
