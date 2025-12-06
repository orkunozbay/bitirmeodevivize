using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies; //roomdaki enemies için
    private Vector3[] initialPosition; //enemy pozisyon

    private void Awake()
    {
       
        initialPosition = new Vector3[enemies.Length]; //düþmanlarý sýrayla gezerek konumlarý kaydet döngü ile
        for (int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i] != null)
                initialPosition[i] = enemies[i].transform.position;
        }

        if (transform.GetSiblingIndex() != 0) //kaçýncý odada isen diðerlerini deaktif et
            ActivateRoom(false);
    }
    public void ActivateRoom(bool _status) //girdiðin-çktýðýn odalarda düþmanlarý aç-kapa
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialPosition[i];
            }
        }
    }
}