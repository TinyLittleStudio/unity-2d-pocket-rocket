using UnityEngine;

public class CometFragments : MonoBehaviour
{
    [Header("Comet Fragments Settings")]
    // LifeTime
    public double time = 1.0d;

    // Update
    private void Update()
    {
        // Check for LifeTime
        time -= Time.deltaTime;

        if (time <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
