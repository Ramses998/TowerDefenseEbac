using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreBase : MonoBehaviour
{
    public GameObject enemigo;
    public GameObject prefabBala;
    public List<GameObject> puntasCanon;

    private void Update()
    {
        if (enemigo != null) {
            Apuntar();
        }
    }

    public void Apuntar() {
        transform.LookAt(enemigo.transform);
    }

    public virtual void Disparar() {
        foreach (GameObject punta in puntasCanon) {
            var tempBala = Instantiate<GameObject>(prefabBala, punta.transform.position,Quaternion.identity);
            tempBala.transform.rotation = Quaternion.Euler((enemigo.transform.position - punta.transform.position).normalized);
            tempBala.GetComponent<Bala>().destino = enemigo.transform.position;
        }
    }
}
