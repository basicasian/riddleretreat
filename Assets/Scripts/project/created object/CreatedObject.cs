using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum Colour
{
    white,
    red,
    yellow,
    green,
    blue
}

public enum Shape
{
   cube,
   sphere,
   cylinder
}

public class CreatedObject : MonoBehaviour, IPunObservable
{
    public Colour colour;
    public Shape shape;
    // public Vector3 localScale;

    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (colour)
        {
            case Colour.red:
                GetComponent<Renderer>().material.SetColor("_Color", new Color(1.0f, 0.35f, 0.35f, 1.0f));
                break;
            case Colour.yellow:
                GetComponent<Renderer>().material.SetColor("_Color", new Color(1.0f, 0.75f, 0.35f, 1.0f));
                break;
            case Colour.green:
                GetComponent<Renderer>().material.SetColor("_Color", new Color(0.5f, 1.0f, 0.5f, 1.0f));
                break;
            case Colour.blue:
                GetComponent<Renderer>().material.SetColor("_Color", new Color(0.35f, 0.75f, 1.0f, 1.0f));
                break;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsReading)
        {
            colour = (Colour)stream.ReceiveNext();
        }
        else if (stream.IsWriting)
        {
            stream.SendNext(colour);
        }
    }

    public void setColour(Colour newColour)
    {
        colour = newColour;
    }

    public Colour GetColour()
    {
        return colour;
    }

    public void setShape(Shape newShape)
    {
        shape = newShape;
    }

    public Shape GetShape()
    {
        return shape;
    }
}


