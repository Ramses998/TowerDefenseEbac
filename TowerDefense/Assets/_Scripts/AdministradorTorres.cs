using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdministradorTorres : MonoBehaviour
{
    public AdministradorToques referenciaAdminToques;
    public AdminJuego referenciaAdminJuego;
    public SpawnearEnemigos referenciaSpawnear;
    public GameObject Objetivo;

    public enum TorreSeleccionada {
        Torre1,Torre2,Torre3,Torre4,Torre5
    }

    public TorreSeleccionada torreSeleccionada;
    public List<GameObject> prefabsTorres;
    public List<GameObject> torresInstanciadas;

    public delegate void EnemigoObjetivoActualizado();
    public event EnemigoObjetivoActualizado EnEnemigoOnjetivoActualizado;

    private void OnEnable()
    {
        referenciaAdminToques.EnPlataformaTocada += CrearTorre;
        referenciaSpawnear.EnOleadaIniciada += ActualizarObjetivo;
        torresInstanciadas = new List<GameObject>();
    }

    private void OnDisable()
    {
        referenciaAdminToques.EnPlataformaTocada -= CrearTorre;
        referenciaSpawnear.EnOleadaIniciada -= ActualizarObjetivo;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ActualizarObjetivo() {
        if (referenciaSpawnear.laOleadaHaIniciado) {
            float distanciaMasCorta = float.MaxValue;
            GameObject enemigoMasCercano = null;
            foreach (GameObject enemigo in referenciaSpawnear.EnemigosGenerados) {
                float dist = Vector3.Distance(enemigo.transform.position, Objetivo.transform.position);
                if (dist < distanciaMasCorta) {
                    distanciaMasCorta = dist;
                    enemigoMasCercano = enemigo;
                }
            }
            if (enemigoMasCercano != null) {
                foreach (GameObject torre in torresInstanciadas) {
                    torre.GetComponent<TorreBase>().enemigo = enemigoMasCercano;
                    torre.GetComponent<TorreBase>().Disparar();
                }
                if (EnEnemigoOnjetivoActualizado != null) {
                    EnEnemigoOnjetivoActualizado();
                }
            }
        }
        Invoke("ActualizarObjetivo", 1);
    }

    private void CrearTorre(GameObject plataforma)
    {
        int costo = torreSeleccionada switch
        {
            TorreSeleccionada.Torre1 => 400,
            TorreSeleccionada.Torre2 => 600,
            TorreSeleccionada.Torre3 => 800,
            TorreSeleccionada.Torre4 => 1200,
            TorreSeleccionada.Torre5 => 2000,
            _ => 0
        };

        if (plataforma.transform.childCount == 0 && referenciaAdminJuego.recursos >=costo) {
            referenciaAdminJuego.ModificarRecursos(-costo);
            Debug.Log("Creando Torre");
            int indiceTorre = (int)torreSeleccionada;
            Vector3 posParaInstanciar = plataforma.transform.position;
            posParaInstanciar.y += 0.5f;
            GameObject torreInstanciada = Instantiate<GameObject>(prefabsTorres[indiceTorre], posParaInstanciar, Quaternion.identity);
            torreInstanciada.transform.SetParent(plataforma.transform);

            torresInstanciadas.Add(torreInstanciada);

        }
        
    }

    public void ConfigurarTorres(int torre) {
        if (Enum.IsDefined(typeof(TorreSeleccionada), torre))
        {
            torreSeleccionada = (TorreSeleccionada)torre;
        }
        else {
            Debug.Log("Esa torre no está definido");
        }
    }
}
