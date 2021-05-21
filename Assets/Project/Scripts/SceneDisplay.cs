using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDisplay : MonoBehaviour
{
    public SceneData sceneData;

    public void SceneLoad(SceneData newSceneData)
    {
        sceneData = newSceneData;
    }
}
