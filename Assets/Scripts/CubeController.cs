using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {

    public Material idleMaterial;
    public Material activatedMaterial;

    private GvrAudioSource gvrAudio;

	void Start () {
        gvrAudio = GetComponent<GvrAudioSource>();
	}

    void LateUpdate()
    {
        GvrViewer.Instance.UpdateState();
        if (GvrViewer.Instance.BackButtonPressed)
        {
            Application.Quit();
        }
    }

    public void PlayNavigateToSound()
    {
        if(gvrAudio != null)
        {
            gvrAudio.Play();
        }
    }

    public void SetGazedAt(bool gazedAt)
    {
        var renderer = GetComponent<Renderer>();
        if (gazedAt)
        {
            renderer.material = activatedMaterial;
        }else
        {
            renderer.material = idleMaterial;
        }

    }

    public void Clicked()
    {

        Debug.Log("Cube clicked");
    }

    public void StartDrag()
    {
        /*
        var eventSystem = GameObject.FindGameObjectWithTag("EventSystem");
        var gazeInput = eventSystem.GetComponent<GazeInputModule>();
        Vector3 newPosition = gazeInput.pointerData.pointerCurrentRaycast.worldPosition;
        newPosition.y = this.transform.position.y;
        this.transform.position = newPosition;
        */
        Debug.Log("Cube dragged");
    }

    public void Dropped()
    {
        Debug.Log("Cube dropped");
    }
    
}
