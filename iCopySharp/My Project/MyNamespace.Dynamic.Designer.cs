using System;
using System.ComponentModel;
using System.Diagnostics;

namespace iCopy.My
{
    internal static partial class MyProject
    {
        internal partial class MyForms
        {

            [EditorBrowsable(EditorBrowsableState.Never)]
            public AboutBox m_AboutBox;

            public AboutBox AboutBox
            {
                [DebuggerHidden]
                get
                {
                    m_AboutBox = Create__Instance__(m_AboutBox);
                    return m_AboutBox;
                }
                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_AboutBox))
                        return;
                    if (value != null)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_AboutBox);
                }
            }


            [EditorBrowsable(EditorBrowsableState.Never)]
            public dlgScanMorePages m_dlgScanMorePages;

            public dlgScanMorePages dlgScanMorePages
            {
                [DebuggerHidden]
                get
                {
                    m_dlgScanMorePages = Create__Instance__(m_dlgScanMorePages);
                    return m_dlgScanMorePages;
                }
                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_dlgScanMorePages))
                        return;
                    if (value != null)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_dlgScanMorePages);
                }
            }


            [EditorBrowsable(EditorBrowsableState.Never)]
            public frmImageSettings m_frmImageSettings;

            public frmImageSettings frmImageSettings
            {
                [DebuggerHidden]
                get
                {
                    m_frmImageSettings = Create__Instance__(m_frmImageSettings);
                    return m_frmImageSettings;
                }
                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_frmImageSettings))
                        return;
                    if (value != null)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_frmImageSettings);
                }
            }


            [EditorBrowsable(EditorBrowsableState.Never)]
            public mainFrm m_mainFrm;

            public mainFrm mainFrm
            {
                [DebuggerHidden]
                get
                {
                    m_mainFrm = Create__Instance__(m_mainFrm);
                    return m_mainFrm;
                }
                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_mainFrm))
                        return;
                    if (value != null)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_mainFrm);
                }
            }


            [EditorBrowsable(EditorBrowsableState.Never)]
            public SettingsDialog m_SettingsDialog;

            public SettingsDialog SettingsDialog
            {
                [DebuggerHidden]
                get
                {
                    m_SettingsDialog = Create__Instance__(m_SettingsDialog);
                    return m_SettingsDialog;
                }
                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_SettingsDialog))
                        return;
                    if (value != null)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_SettingsDialog);
                }
            }


            [EditorBrowsable(EditorBrowsableState.Never)]
            public SplashScreen m_SplashScreen;

            public SplashScreen SplashScreen
            {
                [DebuggerHidden]
                get
                {
                    m_SplashScreen = Create__Instance__(m_SplashScreen);
                    return m_SplashScreen;
                }
                [DebuggerHidden]
                set
                {
                    if (ReferenceEquals(value, m_SplashScreen))
                        return;
                    if (value != null)
                        throw new ArgumentException("Property can only be set to Nothing");
                    Dispose__Instance__(ref m_SplashScreen);
                }
            }

        }


    }
}