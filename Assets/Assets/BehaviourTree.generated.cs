//%GenSrc:1:lOUBuphgD0a5wpEMGFXsDg
//%CodeKey:s7CZiIZHskuNlH73xSgafQ
/*
 * This code was generated by InstinctAI
 *
 *         DO NOT MODIFY BY HAND
 *     THIS FILE WILL BE REGENERATED
 *
 *      DO NOT REMOVE/EDIT COMMENTS
 *
 * If you do need to modify this file, note that it is a partial class.
 * See https://msdn.microsoft.com/en-us/library/wa80x488.aspx
 * You can edit the file BehaviourTree.cs
 */

namespace instinctai.usr.behaviours
{
    using System.Collections;
    using UnityEngine;

    public partial class BehaviourTree : MonoBehaviour
    {
        private IEnumerator _state = null;

        void Update()
        {
            if (_state == null || _state.MoveNext() == false) {
                _state = StateGen();
                _state.MoveNext();
            }
        }
        private IEnumerator StateGen()
        {
            int node = 0;

            do {
                switch (node) {
                    case 0:
                        yield return null;
                        break;
                }
            } while (node >= 0);
        }

        public void ResetTree()
        {
            /* noop */
        }
    }
}