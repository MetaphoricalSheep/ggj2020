using UnityEngine;

public class WoodController : MonoBehaviour, IPickable
{
    public void Pick()
    {
        Destroy(gameObject);
    }
}
