using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnearEnemigos : MonoBehaviour
{
    public List<GameObject> prefabsEnemigos;
    public int oleada;
    public List<int> enemigosPorOleada;

    private int enemigosDuranteEstaOleada;

    public bool laOleadaHaIniciado;
    public List<GameObject> EnemigosGenerados;

    public delegate void EstadoOleada();
    public event EstadoOleada EnOleadaIniciada;
    public event EstadoOleada EnOleadaTerminada;
    public event EstadoOleada EnOleadaGanada;

    // Start is called before the first frame update
    void Start()
    {
        oleada = 0;
    }

    public void FixedUpdate()
    {
        if (laOleadaHaIniciado && EnemigosGenerados.Count == 0) {
            GanarOleada();
        }
    }

    public void EmpezarOleada() {
        laOleadaHaIniciado = true;
        if (EnOleadaIniciada != null) {
            EnOleadaIniciada();
        }
        ConfigurarCantidadDeEnemigos();
        InstanciarEnemigos();
    }

    private void GanarOleada()
    {
        if (laOleadaHaIniciado && EnOleadaGanada != null) {
            EnOleadaGanada();
            laOleadaHaIniciado = false;
        }
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
        var enemigoTemporal =  Instantiate<GameObject>(prefabsEnemigos[indiceAleatorio], transform.position, Quaternion.identity);
        EnemigosGenerados.Add(enemigoTemporal);

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
