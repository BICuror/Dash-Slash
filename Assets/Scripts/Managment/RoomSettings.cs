using UnityEngine;

public sealed class RoomSettings : MonoBehaviour
{
    [SerializeField] private float _roomHeight;
 
    [SerializeField] private float _roomWidth;

    private void Awake()
    {
        Camera cam = Camera.main;
        _roomHeight = cam.orthographicSize;
        _roomWidth = _roomHeight * cam.aspect;
    }

    public Vector3 GetRandomRoomPosition()
    {
        float x = Random.Range(-_roomWidth + 1f, _roomWidth - 1f);
        float y = Random.Range(-_roomHeight + 1f, _roomHeight - 1f);

        return new Vector3(x, y, 0f);
    }

    public float GetHeight() => _roomHeight;
    
    public float GetWidth() => _roomWidth;
}
