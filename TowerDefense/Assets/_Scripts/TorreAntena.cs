using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreAntena : TorreBase, IAtacante
{
    public float divisionesRayo = 10;
    public LineRenderer LRRayo;
    public List<Vector3> puntos;
    public int potenciaRayo;

    public void Start()
    {
        LRRayo = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        if (enemigo != null)
        {
            Disparar();
            Danar(potenciaRayo);
        }
        else {
            LRRayo.positionCount = 0;
        }
    }

    public override void Disparar()
    {
        puntos = ObtenerPuntos();
        puntos.Insert(0, puntasCanon[0].transform.position);
        var posEnemigo = enemigo.transform.position;
        posEnemigo.y += 1;
        puntos.Add(posEnemigo);
        LRRayo.positionCount = puntos.Count;
        LRRayo.SetPositions(puntos.ToArray());
    }

    private List<Vector3> ObtenerPuntos() {
        List<Vector3> tempPuntos = new List<Vector3>();
        float divider = 1f / divisionesRayo;
        float linear = 0f;
        bool esPositivo = false;
        if (divisionesRayo == 0)
        {
            Debug.LogError("no podemos aceptar una division entre 0 o numeros negativos");
            return null;
        }

        if (divisionesRayo == 1)
        {
            var punto = Vector3.Lerp(puntasCanon[0].transform.position, enemigo.transform.position, 0.5f); //Return half/middle point
            tempPuntos.Add(punto);
            return tempPuntos;
        }

        for (int i = 0; i < divisionesRayo; i++)
        {
            if (i == 0)
            {
                linear = divider / 2;
            }
            else
            {
                linear += divider;
            }

            var punto = Vector3.Lerp(puntasCanon[0].transform.position, enemigo.transform.position, linear);
            if (esPositivo)
            {
                punto.x += Random.value * 2;
                esPositivo = false;
            }
            else
            {
                punto.x -= Random.value * 2;
                esPositivo = true;
            }
            tempPuntos.Add(punto);
        }
        return tempPuntos;
    }

    public void Danar(int dano = 0)
    {
        enemigo.GetComponent<EnemigoBase>().RecibirDano(dano);
    }
}
