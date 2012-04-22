﻿using System;
using System.Collections.Generic;
using DevExpress.ExpressApp.Security;

namespace IntecoAG.XAFExt.StateMachine {
    [Serializable]
    public class StateMachineTransitionPermissionRequest : OperationPermissionRequestBase, IStateMachineTransitionPermission {
        public StateMachineTransitionPermissionRequest(IStateMachineTransitionPermission permission)
            : base(StateMachineTransitionPermission.OperationName) {
            Modifier = permission.Modifier;
            StateCaption = permission.StateCaption;
            StateMachineName = permission.StateMachineName;
        }

        public StateMachineTransitionModifier Modifier { get; set; }
        public string StateMachineName { get; set; }
        public string StateCaption { get; set; }

        void IStateMachineTransitionPermission.SyncStateCaptions(IList<string> stateCaptions, string machineName) {

        }
    }
}