using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsqueletoBullet : MonoBehaviour
{
    public GameObject player;  // Arraste o GameObject do jogador aqui
    public GameObject TiroEsqueletoPrefab;
    public float shotSpeed = 5f;
    public float shotInterval = 2f;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(shotInterval);
            Fire();
        }
    }

    private void Fire()
    {
        Vector3 shootDirection = (player.transform.position - transform.position).normalized;
        GameObject TiroEsqueleto = Instantiate(TiroEsqueletoPrefab, transform.position, Quaternion.identity);
        TiroEsqueleto.GetComponent<Rigidbody2D>().velocity = shootDirection * shotSpeed;
    }
}