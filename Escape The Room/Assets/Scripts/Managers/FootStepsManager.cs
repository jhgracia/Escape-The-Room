using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> floorSteps = new List<AudioClip>();
    [SerializeField] List<AudioClip> grassSteps = new List<AudioClip>();

    List<AudioClip> currentList;

    enum Surface { floor, grass};
    Surface surface;

    public void PlayStep()
    {
        SelectSurface();
        SelectStepList();
        AudioClip clip = currentList[Random.Range(0, currentList.Count)];
        MasterManager.main.audioManager.playPlayerSound(clip);
    }

    void SelectSurface()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 2.0f))
        {
            if (hit.transform.CompareTag("Floor")) surface = Surface.floor;
            if (hit.transform.CompareTag("Grass")) surface = Surface.grass;
        }
    }

    void SelectStepList()
    {
        switch (surface)
        {
            case Surface.floor:
                currentList = floorSteps;
                break;
            case Surface.grass:
                currentList = grassSteps;
                break;
        }
    }
}
