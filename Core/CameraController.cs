using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Room kamera
    [SerializeField] private float speed; //kamera hýzý
    private float currentPosX; //kamera current x pozisyonu
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
    
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed); //smoothdamp, x yönünde kaydýr, girilen speede göre

    }

    public void MoveToNewRoom(Transform _newRoom) //kaydýrýlan odanýn X'ini tut
    {
        currentPosX = _newRoom.position.x;
    }
}