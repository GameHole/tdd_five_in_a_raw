using Five;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        new ChessSelectorView(new CameraRay(Camera.main),new BoardRaycastor(15,15));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
