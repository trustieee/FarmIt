using UnityEngine;

public class Place : MonoBehaviour
{
    public void PerformPlace()
    {
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out RaycastHit hit, 3f))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Instantiate(Resources.Load("Plantables/Tree"), hit.point, Quaternion.identity);
            }
        }
    }
}