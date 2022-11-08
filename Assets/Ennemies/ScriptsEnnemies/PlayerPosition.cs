using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public static PlayerPosition Instance;
    private void Awake()
    {
        Instance = this;
    }
}
