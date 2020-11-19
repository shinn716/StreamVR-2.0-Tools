// https://www.youtube.com/watch?v=ryfUXr5yvKw

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(FixedJoint))]
public class ShiHand : MonoBehaviour
{
    public SteamVR_Action_Boolean TriggerAction = null;
    public SteamVR_Action_Boolean MenuAction = null;

    private SteamVR_Behaviour_Pose m_pose = null;
    private FixedJoint m_joint = null;
    private ShiInteractable m_CurrentInteractable = null;
    private List<ShiInteractable> m_CurrentInteractables = new List<ShiInteractable>();

    private bool m_ColliderTrigger = false;

    private void Awake()
    {
        m_pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_joint = GetComponent<FixedJoint>();
    }

    private void FixedUpdate()
    {
        if (TriggerAction.GetLastStateDown(m_pose.inputSource))
        {
            if (m_CurrentInteractable != null)
            {
                m_CurrentInteractable.Action();
                return;
            }

            Pickup();
        }

        if (TriggerAction.GetLastStateUp(m_pose.inputSource))
        {
            if (m_CurrentInteractable == null)
                return;
            
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
            return;

        if (!m_ColliderTrigger)
        {
            m_ColliderTrigger = true;
            other.gameObject.GetComponent<ShiInteractable>().EnableHightLight();
            m_CurrentInteractables.Add(other.gameObject.GetComponent<ShiInteractable>());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
            return;

        m_ColliderTrigger = false;
        other.gameObject.GetComponent<ShiInteractable>().DisableHightLight();
        m_CurrentInteractables.Remove(other.gameObject.GetComponent<ShiInteractable>());
    }


    private void Pickup()
    {
        m_CurrentInteractable = GetNearestInteractable();

        if (!m_CurrentInteractable)
            return;

        if (m_CurrentInteractable.m_ActiveHand)
            m_CurrentInteractable.m_ActiveHand.Drop();

        if (m_CurrentInteractable != null)
        {
            m_CurrentInteractable.ApplyTransform(transform);
            m_CurrentInteractable.m_ActiveHand = this;
            
            Rigidbody rigidbody = m_CurrentInteractable.GetComponent<Rigidbody>();
            m_joint.connectedBody = rigidbody;
        }

        m_CurrentInteractable.m_ActiveHand = this;
    }

    private void Drop()
    {
        if (!m_CurrentInteractable)
            return;

        //Rigidbody rigidbody = m_CurrentInteractable.GetComponent<Rigidbody>();
        //rigidbody.velocity = m_pose.GetVelocity();
        //rigidbody.angularVelocity = m_pose.GetAngularVelocity();

        m_CurrentInteractable.m_ActiveHand = null;
        m_CurrentInteractable = null;
        m_joint.connectedBody = null;
    }

    private ShiInteractable GetNearestInteractable()
    {
        ShiInteractable nearest = null;
        float minDistance = float.MaxValue;
        float distance = .0f;

        foreach (ShiInteractable interactable in m_CurrentInteractables)
        {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interactable;
            }
        }
        return nearest;
    }
}
 