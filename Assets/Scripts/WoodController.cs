using UnityEngine;

public class WoodController : MonoBehaviour, IInteractive
{
    public void Highlight()
    {
        Debug.Log("Highlighting");
    }

    public void Interact()
    {
        Debug.Log("Picking Up or Putting Down or Feeding Fire");
    }
}
