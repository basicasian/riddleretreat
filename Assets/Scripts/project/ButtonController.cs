using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ButtonController : MonoBehaviour, IPunObservable
{
    public bool isClicked;
    public GameObject button;

    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isClicked)
        {
            button.SetActive(false);
        }

    }

    public void SetIsClicked()
    {
        if (!isClicked)
        {
            isClicked = true;
        }
        
        Debug.Log(isClicked);
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsReading)
        {
            isClicked = (bool)stream.ReceiveNext();
        }
        else if (stream.IsWriting)
        {
            stream.SendNext(isClicked);
        }
    }
}
