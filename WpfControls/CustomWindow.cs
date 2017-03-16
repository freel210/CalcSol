using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfControls
{
    public class CustomWindow: Window
    {
        protected void MinimizeClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        protected void RestoreClick(object sender, RoutedEventArgs e)
        {
            WindowState = (WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
        }

        protected void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        static CustomWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomWindow),
                new FrameworkPropertyMetadata(typeof(CustomWindow)));
        }

        public override void OnApplyTemplate() 
        { 
            Button minimizeButton = GetTemplateChild("minimizeButton") as Button; 
            if (minimizeButton != null)       
                minimizeButton.Click += MinimizeClick; 
            
            Button restoreButton = GetTemplateChild("restoreButton") as Button; 
            if (restoreButton != null)     
                restoreButton.Click += RestoreClick; 
            
            Button closeButton = GetTemplateChild("closeButton") as Button;
            if (closeButton != null)       
                closeButton.Click += CloseClick; 
            
            base.OnApplyTemplate();
        }

        private void moveRectangle_PreviewMouseDown(object sender, MouseButtonEventArgs e) 
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)   
                DragMove(); 
        }

        public CustomWindow() : base() 
        {
            PreviewMouseMove += OnPreviewMouseMove; 
        }
        
        protected void OnPreviewMouseMove(object sender, MouseEventArgs e) 
        { 
            if (Mouse.LeftButton != MouseButtonState.Pressed)   
                Cursor = Cursors.Arrow; 
        }
    }
}
