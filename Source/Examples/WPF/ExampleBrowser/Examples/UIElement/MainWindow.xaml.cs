// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Helix 3D Toolkit">
//   http://helixtoolkit.codeplex.com, license: MIT
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace UIElementDemo
{
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media.Media3D;

    using ExampleBrowser;

    using HelixToolkit.Wpf;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Example(null, "Test of UIElement3D in HelixViewport3D.")]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var container = new ContainerUIElement3D();
            var element = new ModelUIElement3D();
            var geometry = new GeometryModel3D();
            var meshBuilder = new MeshBuilder();
            meshBuilder.AddSphere(new Point3D(0, 0, 0), 2, 100, 50);
            geometry.Geometry = meshBuilder.ToMesh();
            geometry.Material = Materials.Green;
            element.Model = geometry;
            element.Transform = new TranslateTransform3D(5, 0, 0);
            element.MouseDown += this.ContainerElementMouseDown;
            container.Children.Add(element);
            view1.Children.Add(container);
            pipetexture();
        }

        private void ContainerElementMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var element = sender as ModelUIElement3D;
                var model = element.Model as GeometryModel3D;
                model.Material = model.Material == Materials.Green ? Materials.Gray : Materials.Green;
                e.Handled = true;
            }
        }

        private void ZoomExtents_Click(object sender, RoutedEventArgs e)
        {
            view1.ZoomExtents(500);
        }

        private void pipetexture()
        {
            Rectangle exampleRectangle = new Rectangle();
            exampleRectangle.Width = 75;
            exampleRectangle.Height = 75;

            // Create a DrawingBrush and use it to
            // paint the rectangle.
            DrawingBrush myBrush = new DrawingBrush();

            GeometryDrawing backgroundSquare = new GeometryDrawing(System.Windows.Media.Brushes.White, null, new RectangleGeometry(new Rect(0, 0, 100, 100)));

            GeometryGroup aGeometryGroup = new GeometryGroup();
            aGeometryGroup.Children.Add(new RectangleGeometry(new Rect(0, 0, 50, 50)));
            aGeometryGroup.Children.Add(new RectangleGeometry(new Rect(50, 50, 50, 50)));

            LinearGradientBrush checkerBrush = new LinearGradientBrush();
            checkerBrush.GradientStops.Add(new GradientStop(Colors.Black, 0.0));
            checkerBrush.GradientStops.Add(new GradientStop(Colors.Gray, 1.0));

            GeometryDrawing checkers = new GeometryDrawing(checkerBrush, null, aGeometryGroup);

            DrawingGroup checkersDrawingGroup = new DrawingGroup();
            checkersDrawingGroup.Children.Add(backgroundSquare);
            checkersDrawingGroup.Children.Add(checkers);

            myBrush.Drawing = checkersDrawingGroup;
            myBrush.Viewport = new Rect(0, 0, 0.25, 0.25);
            myBrush.TileMode = TileMode.Tile;

            Pipemodel.Fill = myBrush;
        }
    }
}