﻿#pragma checksum "..\..\..\Page3.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C619F3991545B48799D210883AA58FDEBAEC1A27"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using WpfApp2;


namespace WpfApp2 {
    
    
    /// <summary>
    /// Page3
    /// </summary>
    public partial class Page3 : WpfApp2.Room, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\Page3.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas MyCanvas;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Page3.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border Target;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Page3.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ImageContainer;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Page3.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel DropTargets;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp2;component/page3.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Page3.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MyCanvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 11 "..\..\..\Page3.xaml"
            this.MyCanvas.KeyDown += new System.Windows.Input.KeyEventHandler(this.CanvasKeyDown);
            
            #line default
            #line hidden
            
            #line 11 "..\..\..\Page3.xaml"
            this.MyCanvas.KeyUp += new System.Windows.Input.KeyEventHandler(this.CanvasKeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Target = ((System.Windows.Controls.Border)(target));
            
            #line 14 "..\..\..\Page3.xaml"
            this.Target.DragEnter += new System.Windows.DragEventHandler(this.Target_DragEnter);
            
            #line default
            #line hidden
            
            #line 15 "..\..\..\Page3.xaml"
            this.Target.Drop += new System.Windows.DragEventHandler(this.Target_Drop);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ImageContainer = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            
            #line 17 "..\..\..\Page3.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseDown);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\Page3.xaml"
            ((System.Windows.Controls.Image)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.Image_MouseMove);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\Page3.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseUp);
            
            #line default
            #line hidden
            return;
            case 5:
            this.DropTargets = ((System.Windows.Controls.StackPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

