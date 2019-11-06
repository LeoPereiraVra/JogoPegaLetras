using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objectMouse;
    private float distanceToCamera = 10f;
    public float smoothFactor = 5f;
    public KinectWrapper.NuiSkeletonPositionIndex TrackedJoint = KinectWrapper.NuiSkeletonPositionIndex.HandRight;
    void Start()
    {
        if (objectMouse)
        {
            distanceToCamera = (objectMouse.transform.position - Camera.main.transform.position).magnitude;
        }
    }

    // Update is called once per frame
    void Update()
    {
        KinectManager manager = KinectManager.Instance;

        if (manager && manager.IsInitialized())
        {
            
            int iJointIndex = (int)TrackedJoint;

            if (manager.IsUserDetected())
            {
                uint userId = manager.GetPlayer1ID();

                if (manager.IsJointTracked(userId, iJointIndex))
                {
                    Vector3 posJoint = manager.GetRawSkeletonJointPos(userId, iJointIndex);

                    if (posJoint != Vector3.zero)
                    {
                        // 3d position to depth
                        Vector2 posDepth = manager.GetDepthMapPosForJointPos(posJoint);

                        // depth pos to color pos
                        Vector2 posColor = manager.GetColorMapPosForDepthPos(posDepth);

                        float scaleX = (float)posColor.x / KinectWrapper.Constants.ColorImageWidth;
                        float scaleY = 1.0f - (float)posColor.y / KinectWrapper.Constants.ColorImageHeight;

                       
                        if (objectMouse)
                        {
                            Vector3 vPosOverlay = Camera.main.ViewportToWorldPoint(new Vector3(scaleX, scaleY, distanceToCamera));
                            objectMouse.transform.position = Vector3.Lerp(objectMouse.transform.position, vPosOverlay, smoothFactor * Time.deltaTime);
                        }
                    }
                }

            }

        }
    }
}
