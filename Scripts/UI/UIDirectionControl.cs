using UnityEngine;

public class UIDirectionControl : MonoBehaviour
{
    public bool m_UseRelativeRotation = true;  


    private Quaternion m_RelativeRotation;     


    private void Start()
    {	//rotation of the canvas
        m_RelativeRotation = transform.parent.localRotation;
    }


    private void Update()
    {	//instaed of normal child, it's just keeping the one that it.
        if (m_UseRelativeRotation)
            transform.rotation = m_RelativeRotation;
    }
}
