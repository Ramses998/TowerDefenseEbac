using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnearEnemigos : MonoBehaviour
{
    public List<GameObject> prefabsEnemigos;
    public int oleada;
    public List<int> enemigosPorOleada;

    private int enemigosDuranteEstaOleada;

    public delegate void OleadaTerminada();
    public event OleadaTerminada EnOleadaTerminada;

    // Start is called before the first frame update
    void Start()
    {
        oleada = 0;
        ConfigurarCantidadDeEnemigos();
        InstanciarEnemigos();
    }

    public void TerminarOla() {
        if (EnOleadaTerminada != null) {
            EnOleadaTerminada();
        }
    }

    public void ConfigurarCantidadDeEnemigos() {
        enemigosDuranteEstaOleada = enemigosPorOleada[oleada];
    }

    public void InstanciarEnemigos() {
        int indiceAleatorio = Random.Range(0, prefabsEnemigos.Count);
        Instantiate<GameObject>(prefabsEnemigos[indiceAleatorio], transform.position, Quaternion.identity);
        enemigosDuranteEstaOleada--;
        if (enemigosDuranteEstaOleada < 0) {
            oleada++;
            ConfigurarCantidadDeEnemigos();
            TerminarOla();
            return;
        }
        Invoke("InstanciarEnemigos", 2);
    }
}
