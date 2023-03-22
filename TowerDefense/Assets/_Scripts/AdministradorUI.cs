using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdministradorUI : MonoBehaviour
{
    public GameObject canvasPrincipal;
    public GameObject menuGameOver;
    public GameObject MenuOlaGanada;
    public GameObject MensajeFinOla;
    public SpawnearEnemigos referenciaSpawner;
    public Objetivo referenciaObjetivo;
    public AdminJuego referenciaAdminJuego;
    public TMPro.TMP_Text textoRecursos;
    public TMPro.TMP_Text textoOleada;
    public TMPro.TMP_Text textoEnemigos;
    public TMPro.TMP_Text textoJefes;

    public void OnEnable()
    {
        referenciaObjetivo.EnObjetivoDestruido += MostrarMenuGameOver;
        referenciaSpawner.EnOleadaIniciada += ActualizarOla;
        referenciaSpawner.EnOleadaTerminada += MostrarMensajeUltimoEnemigo;
        referenciaSpawner.EnOleadaGanada += MostrarMenuOlaGanada;
        referenciaAdminJuego.EnRecursosModificados += ActualizarRecursos;
    }

    public void ActualizarRecursos()
    {
        textoRecursos.text = $"Recursos: {referenciaAdminJuego.recursos}";
    }

    public void MostrarMensajeUltimoEnemigo()
    {
        MensajeFinOla.SetActive(true);
        Invoke("OcultarMensaheUltimoEnemigo",3);
    }

    public void MostrarMenuOlaGanada()
    {
        textoEnemigos.text = $"ENEMIGOS: \t {referenciaAdminJuego.enemigosBaseDerrotados}";
        textoJefes.text = $"JEFES \t\t {referenciaAdminJuego.enemigosJefeDerrotados}";
        MenuOlaGanada.SetActive(true);
    }

    public void OcultarMenuOlaGanada() {
        MenuOlaGanada.SetActive(false);
    }

    public void ActualizarOla()
    {
        textoOleada.text = $"Ola: {referenciaSpawner.oleada}";
        OcultarMenuOlaGanada();
    }

    public void OnDisable()
    {
        referenciaObjetivo.EnObjetivoDestruido -= MostrarMenuGameOver;
        referenciaSpawner.EnOleadaIniciada -= ActualizarOla;
        referenciaSpawner.EnOleadaTerminada -= MostrarMensajeUltimoEnemigo;
        referenciaSpawner.EnOleadaGanada -= MostrarMenuOlaGanada;
        referenciaAdminJuego.EnRecursosModificados -= ActualizarRecursos;
    }

    public void MostrarMenuFinOleada() {

    }

    public void OcultarMenuFinOleada() {

    }

    public void MostrarMenuGameOver() {
        menuGameOver.SetActive(true);

    }

    public void OcultarMenuGameOver() {
        menuGameOver.SetActive(false);
    }

    public void FinalizarJuego(){
        Application.Quit();
    }

    public void CargarMenuPrincipal() {
        SceneManager.LoadScene(0);
    }

    public void ReintentarNivel() {
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(escenaActual);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
