﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.MixedReality.Toolkit.Editor;
using Microsoft.MixedReality.Toolkit.Utilities.Editor;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.WindowsMixedReality.Editor
{
    [CustomEditor(typeof(WindowsMixedRealityCameraSettingsProfile))]
    public class WindowsMixedRealityCameraSettingsProfileInspector : BaseMixedRealityToolkitConfigurationProfileInspector
    {
        private const string ProfileTitle = "Windows Mixed Reality Camera Settings";
        private const string ProfileDescription = "";

        private SerializedProperty renderFromPVCameraForMixedRealityCapture;

        private readonly GUIContent pvCameraRenderingTitle = new GUIContent("Use PV Camera Rendering For MRC");

        protected override void OnEnable()
        {
            base.OnEnable();

            renderFromPVCameraForMixedRealityCapture = serializedObject.FindProperty("renderFromPVCameraForMixedRealityCapture");
        }

        public override void OnInspectorGUI()
        {
            RenderProfileHeader(ProfileTitle, ProfileDescription, target);

            using (new GUIEnabledWrapper(!IsProfileLock((BaseMixedRealityProfile)target)))
            {
                serializedObject.Update();

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("MRC Settings", EditorStyles.boldLabel);

                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.PropertyField(renderFromPVCameraForMixedRealityCapture, pvCameraRenderingTitle);
                    InspectorUIUtility.RenderDocumentationButton(MRCDocURL);
                }

                serializedObject.ApplyModifiedProperties();
            }
        }

        protected override bool IsProfileInActiveInstance()
        {
            var profile = target as BaseMixedRealityProfile;

            return MixedRealityToolkit.IsInitialized && profile != null &&
                   MixedRealityToolkit.Instance.HasActiveProfile &&
                   MixedRealityToolkit.Instance.ActiveProfile.CameraProfile != null &&
                   MixedRealityToolkit.Instance.ActiveProfile.CameraProfile.SettingsConfigurations != null &&
                   MixedRealityToolkit.Instance.ActiveProfile.CameraProfile.SettingsConfigurations.Any(s => s.SettingsProfile == profile);
        }
    }
}