//%GenSrc:1:9mf50npS20eHjMXmw7FWzg
/*
 * This code was generated by InstinctAI.
 *
 * It is safe to edit this file.
 */

using System.Collections;
using com.kupio.instinctai;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityScript.Macros;

namespace instinctai.usr.behaviours
{
    using UnityEngine;

    public partial class MyTree : MonoBehaviour
    {
        
        private Pedestrian thisPed;
        private int talkTimer;
        public static int totalTalkTime;

        void Start()
        {
            thisPed = GetComponent<Pedestrian>();
        }


        public NodeVal Walk()
        {
            if (!thisPed.moveInDirection())
            {
                return NodeVal.Success;
            }
            else
            {
                return NodeVal.Fail;
            }
        }

        public NodeVal findFriends()
        {
            if (thisPed.checkForFriends() != null)
            {
                return NodeVal.Success;
            }
            else
            {
                return NodeVal.Fail;
            }
        }

        NodeVal startTalking()
        {
            thisPed.isTalker = true;
            thisPed.startTalk();
            thisPed.closePedScr.isTalker = false;
            thisPed.closePedScr.yourTalker = GetComponent<Pedestrian>();
            thisPed.closePedScr.startTalk();
            talkTimer = 0;
            return NodeVal.Fail;
        }

        NodeVal displayReaction()
        {
            if (thisPed.isTalker)
            {
                thisPed.reaction.transform.parent.gameObject.SetActive(true);
                thisPed.reaction.gameObject.SetActive(true);
                thisPed.closePedScr.reaction.transform.parent.gameObject.SetActive(true);
                thisPed.closePedScr.reaction.gameObject.SetActive(true);
               // Debug.Log("got here");
                thisPed.displayReaction(thisPed.checkReaction());
                thisPed.closePedScr.displayReaction(thisPed.closePedScr.checkReaction());
            }

            return NodeVal.Success;
        }

        NodeVal finishTalking()
        {
            if (thisPed.isTalker)
            {
                thisPed.isTalking = false;
                thisPed.closePedScr.isTalking = false;
                thisPed.isTalker = false;
                thisPed.closePedScr.reaction.transform.parent.gameObject.SetActive(false);
                thisPed.closePedScr.reaction.gameObject.SetActive(false);
                thisPed.reaction.gameObject.SetActive(false);
                thisPed.reaction.transform.parent.gameObject.SetActive(false);



            }

            return NodeVal.Success;
        }

        NodeVal talk()
        {
            if (thisPed.isTalker)
            {
                thisPed.startTalking = false;
                thisPed.closePedScr.startTalking = false;
            }
            return NodeVal.Success;
        }

        bool isTalking()
        {
            if (thisPed.startTalking)
            {
                return true;
            }

            return false;
        }

        public NodeVal moveToFriend()
        {
            Vector3 dirToFriend = (thisPed.closestPed.transform.position - transform.position);
            if (dirToFriend.magnitude >= 0.8)
            {
                if (!thisPed.closePedScr.checkIfOnScreen())
                {
                    return NodeVal.Fail;
                }
                transform.position += dirToFriend.normalized * thisPed.distToTravel;
                return NodeVal.Running;
            }
            else
            {
                thisPed.isTalking = true;
                thisPed.closePedScr.isTalking = true;
                return NodeVal.Success;
            }   
        }

        public NodeVal moveOffScreen()
        {
            if (!thisPed.moveInDirection())
            {
                return NodeVal.Running;
            }
            else
            {
                thisPed.reaction.transform.parent.gameObject.SetActive(false);
                thisPed.reaction.gameObject.SetActive(false);
                return NodeVal.Success;
            }
        }

        public NodeVal killYaSelf()
        {
            gameObject.SetActive(false);
            thisPed.manager.activePedestrians--;
            return NodeVal.Success;
        }

        

        
    }
}