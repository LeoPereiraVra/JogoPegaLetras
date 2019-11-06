using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hand_left : MonoBehaviour
{
	//[SerializeField]
	public ParticleSystem Particula;
    public PalavrasJogo Scriptjogo;
    public KinectWrapper.NuiSkeletonPositionIndex joint = KinectWrapper.NuiSkeletonPositionIndex.HandLeft;
    public configJogo configuracao;    
    // joint position at the moment, in Kinect coordinates
    public Vector3 outputPosition;
    KinectManager manager;

    // Start is called before the first frame update
    void Start()
    {        

    }

   
    // Update is called once per frame
    void Update()
    {
        // get the joint position
        manager = KinectManager.Instance;

        if (manager && manager.IsInitialized())
		{

			if(manager.IsUserDetected())
			{

				uint userId = manager.GetPlayer1ID();

				if(manager.IsJointTracked(userId, (int)joint))
				{

					// output the joint position for easy tracking
					Vector3 jointPos = manager.GetJointPosition(userId, (int)joint);
					jointPos.Set(jointPos.x * 2, jointPos.y * 2, jointPos.z);
					transform.localPosition = jointPos;

				}
			}
		}
    }
	void OnCollisionEnter (Collision col)
    {
		ParticleSystem obj;        
		obj = (ParticleSystem) Instantiate(Particula, this.transform.localPosition, Quaternion.identity);
        if (Scriptjogo.SetLetra(col.gameObject.name))
            obj.GetComponent<Renderer>().material = configuracao.PAcerto;
        else
            obj.GetComponent<Renderer>().material = configuracao.PErro;
        Destroy(col.gameObject, 0.5f);
        obj.Play();		
		Destroy(obj.gameObject, 1f); 
	
    }
}
