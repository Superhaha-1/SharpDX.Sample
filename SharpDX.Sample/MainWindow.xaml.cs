using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace SharpDX.Sample
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += (s, e) =>
            {
                var factory = new Factory();

                var pixelFormat = new Direct2D1.PixelFormat(DXGI.Format.R8G8B8A8_UNorm, AlphaMode.Premultiplied);

                var hwndRenderTargetProperties = new HwndRenderTargetProperties();
                hwndRenderTargetProperties.Hwnd = new WindowInteropHelper(this).Handle;
                hwndRenderTargetProperties.PixelSize = new Size2((int)ActualWidth, (int)ActualHeight);

                var renderTargetProperties = new RenderTargetProperties(RenderTargetType.Default, pixelFormat,
                    96, 96, RenderTargetUsage.None, FeatureLevel.Level_DEFAULT);

                _renderTarget = new WindowRenderTarget(factory, renderTargetProperties, hwndRenderTargetProperties);

                CompositionTarget.Rendering += CompositionTarget_Rendering;
            };
        }

        private RenderTarget _renderTarget;

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            var ellipse = new Ellipse(new RawVector2(100, 100), 10, 10);

            var brush = new Direct2D1.SolidColorBrush(_renderTarget, new RawColor4(1, 0, 0, 1));
            _renderTarget.BeginDraw();

            _renderTarget.DrawEllipse(ellipse, brush, 1);

            _renderTarget.EndDraw();
        }
    }
}
