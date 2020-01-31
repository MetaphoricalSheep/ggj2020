using UnityEngine;

public class ScatterComponent : MonoBehaviour
{
    [SerializeField] private Vector2 _range = new Vector2(0, 5);

    private void Awake()
    {
        var rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = RandomVector();
    }

    private Vector3 RandomVector()
    {
        var x = Random.Range(_range.x, _range.y);
        var y = Random.Range(_range.x, _range.y);
        var z = Random.Range(_range.x, _range.y);

        return new Vector3(x, y, z);
    }
}
