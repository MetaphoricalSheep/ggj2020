using UnityEngine;

public class ScatterComponent : MonoBehaviour
{
    [SerializeField] private Vector2 _range = new Vector2(0, 5);
    [SerializeField] private Vector2 _rangeY = new Vector2(0, 5);
    [SerializeField] private Vector2 _rotation = new Vector2(0, 90);

    private void Awake()
    {
        var rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = RandomVector();
        rigidBody.rotation = RandomQuaternion();
    }

    private Quaternion RandomQuaternion() {
        return Quaternion.Euler(Random.Range(_rotation.x, _rotation.y),
            Random.Range(_rotation.x, _rotation.y),
            Random.Range(_rotation.x, _rotation.y));
    }

    private Vector3 RandomVector()
    {
        var x = Random.Range(_range.x, _range.y);
        var y = Random.Range(_rangeY.x, _rangeY.y);
        var z = Random.Range(_range.x, _range.y);

        return new Vector3(x, y, z);
    }
}
