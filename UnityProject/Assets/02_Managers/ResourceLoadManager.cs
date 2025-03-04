using UnityEngine;

public class ResourceLoadManager : Y_Singleton<ResourceLoadManager>
{
    public PlayerStatus PlayerStatus => Resources.Load<PlayerStatus>("ScriptableObjects/PlayerStatus");
}
