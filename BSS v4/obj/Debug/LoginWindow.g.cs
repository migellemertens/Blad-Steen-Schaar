#pragma checksum "..\..\LoginWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D07004F9441000663D6EA51780B40DFFBB5808929C772264B9FE84169EA2405E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BSS_v4;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace BSS_v4 {
    
    
    /// <summary>
    /// LoginWindow
    /// </summary>
    public partial class LoginWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 118 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnSluiten;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CboSpelers;
        
        #line default
        #line hidden
        
        
        #line 136 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PwdLogin;
        
        #line default
        #line hidden
        
        
        #line 141 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnLogin;
        
        #line default
        #line hidden
        
        
        #line 152 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtSpelerRegistratie;
        
        #line default
        #line hidden
        
        
        #line 158 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PwdWachtwoordRegistratie;
        
        #line default
        #line hidden
        
        
        #line 163 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PwdWachtwoordRegistratieValidatie;
        
        #line default
        #line hidden
        
        
        #line 168 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnRegistreer;
        
        #line default
        #line hidden
        
        
        #line 170 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LblMeldingRegistratie;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BSS v4;component/loginwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\LoginWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 99 "..\..\LoginWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.BtnSluiten = ((System.Windows.Controls.Button)(target));
            
            #line 118 "..\..\LoginWindow.xaml"
            this.BtnSluiten.Click += new System.Windows.RoutedEventHandler(this.BtnSluiten_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CboSpelers = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.PwdLogin = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 136 "..\..\LoginWindow.xaml"
            this.PwdLogin.GotFocus += new System.Windows.RoutedEventHandler(this.PwdBoxLogin_GotFocus);
            
            #line default
            #line hidden
            return;
            case 5:
            this.BtnLogin = ((System.Windows.Controls.Button)(target));
            
            #line 141 "..\..\LoginWindow.xaml"
            this.BtnLogin.Click += new System.Windows.RoutedEventHandler(this.BtnLogin_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.TxtSpelerRegistratie = ((System.Windows.Controls.TextBox)(target));
            
            #line 152 "..\..\LoginWindow.xaml"
            this.TxtSpelerRegistratie.GotFocus += new System.Windows.RoutedEventHandler(this.TxtSpelerRegistratie_GotFocus);
            
            #line default
            #line hidden
            return;
            case 7:
            this.PwdWachtwoordRegistratie = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 158 "..\..\LoginWindow.xaml"
            this.PwdWachtwoordRegistratie.GotFocus += new System.Windows.RoutedEventHandler(this.PwdWachtwoordRegistratie_GotFocus);
            
            #line default
            #line hidden
            return;
            case 8:
            this.PwdWachtwoordRegistratieValidatie = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 163 "..\..\LoginWindow.xaml"
            this.PwdWachtwoordRegistratieValidatie.GotFocus += new System.Windows.RoutedEventHandler(this.PwdWachtwoordRegistratieValidatie_GotFocus);
            
            #line default
            #line hidden
            return;
            case 9:
            this.BtnRegistreer = ((System.Windows.Controls.Button)(target));
            
            #line 168 "..\..\LoginWindow.xaml"
            this.BtnRegistreer.Click += new System.Windows.RoutedEventHandler(this.BtnRegistreer_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.LblMeldingRegistratie = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

