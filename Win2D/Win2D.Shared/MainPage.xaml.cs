using Windows.UI.Xaml.Controls;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace Win2D
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MainTable table;

        public MainPage()
        {
            InitializeComponent();
        }

        async Task CreateResourcesAsync(CanvasAnimatedControl sender) //загрузить ресурсы
        {
            table = new MainTable(await CanvasBitmap.LoadAsync(sender, "Content\\fire.png"), await CanvasBitmap.LoadAsync(sender, "Content\\table.png"));
            Textures.CardsTexture = await CanvasBitmap.LoadAsync(sender, "Content\\Cards.png");
            Textures.BoardCardsTexture = await CanvasBitmap.LoadAsync(sender, "Content\\boardcards.png");
        }

        private void Canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args) //нарисовать
        {
            table.Draw(args.DrawingSession, args.Timing.ElapsedTime);
        }

        private void Canvas_CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e) //изменился размер
        {
            Parameters.Scale = Math.Min(e.NewSize.Height / Parameters.Original.Height, e.NewSize.Width / Parameters.Original.Width);
            Parameters.centerScreen.X = e.NewSize.Height / 2;
            Parameters.centerScreen.Y = e.NewSize.Width / 2;
        }

        private void Canvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            //position.X = e.GetCurrentPoint(null).Position.X;
            //position.Y = e.GetCurrentPoint(null).Position.Y;
        }

        private void Canvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            table.PointerCoordinates = (e.GetCurrentPoint(null).Position);
        }
    }
}