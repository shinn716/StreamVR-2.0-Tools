using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class ShiInteractable : MonoBehaviour
{
    public enum Type
    {
        Grabble,
        Trigger
    }
    
    public Type type = Type.Grabble;
    public UnityEvent unityEvent;

    public ShiHand m_ActiveHand { get; set; } = null;

    private GameObject m_hightlightGo = null;
    private bool m_hightlight = false;

    public virtual void Action()
    {
        print("Action");
    }

    public void ApplyTransform(Transform hand)
    {
        if (type.Equals(Type.Grabble))
        {
            if (m_hightlightGo != null)
                DisableHightLight();

            transform.SetParent(hand);
            transform.SetParent(null);
            unityEvent.Invoke();

            //transform.position = Vector3.Lerp(transform.position, hand.position, Time.deltaTime * 20);
            //transform.rotation = Quaternion.Lerp(transform.rotation, hand.rotation, Time.deltaTime * 20);
            //transform.SetPositionAndRotation(hand.position, hand.rotation);
        }
        else if (type.Equals(Type.Trigger))
        {
            unityEvent.Invoke();
        }
    }
    
    public void EnableHightLight()
    {
        if (!m_hightlight)
        {
            m_hightlight = true;
            m_hightlightGo = Instantiate(gameObject);
            m_hightlightGo.name = $"HightLight_{gameObject.name}(Clone)";
            m_hightlightGo.GetComponent<Renderer>().material = Resources.Load<Material>("SteamVR_HoverHighlight");
            m_hightlightGo.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }
    }
    
    public void DisableHightLight()
    {
        Destroy(m_hightlightGo);
        m_hightlightGo = null;
        m_hightlight = false;
    }
}
