using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class hand : MonoBehaviour
{
    // the joint we want to track
	public KinectWrapper.NuiSkeletonPositionIndex joint = KinectWrapper.NuiSkeletonPositionIndex.HandRight;

    // joint position at the moment, in Kinect coordinates
	public Vector3 outputPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // get the joint position
		KinectManager manager = KinectManager.Instance;

		if(manager && manager.IsInitialized())
		{
			if(manager.IsUserDetected())
			{
				uint userId = manager.GetPlayer1ID();

				if(manager.IsJointTracked(userId, (int)joint))
				{
					// output the joint position for easy tracking
					Vector3 jointPos = manager.GetJointPosition(userId, (int)joint);
					outputPosition = jointPos;

					//debug.log(outputPosition);
				}
			}
		}
    }
}
