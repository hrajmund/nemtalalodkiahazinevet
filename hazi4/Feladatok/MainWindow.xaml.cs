using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MultiThreadedApp.AppLogic;
using System;
using System.Collections.Generic;
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MultiThreadedApp
{
    /// <summary>
    /// Az alkalmazás főablaka.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        // A megjelenített játék.
        private Game game = new Game();

        // Az egyes Game osztálybeli biciklikhez tartozó TextBlock objektumok.
        private List<TextBlock> bikeTextBlocks = new List<TextBlock>();
        private DispatcherTimer timer;

        public MainWindow()
        {
            this.InitializeComponent();

            startLineRect.Margin = new Thickness(Game.StartLinePosition, 15, 0, 10);
            depoPanel.Margin = new Thickness(Game.DepoPosition - 47, 15, 0, 0);
            finishLineRect.Margin = new Thickness(Game.FinishLinePosition, 15, 0, 10);
            AddKeyboardAcceleratorToChangeTheme();


            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(100);
            //timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            System.Diagnostics.Trace.WriteLine($"Time: {DateTime.Now.ToString("hh:mm:ss.fff")}");

            UpdateUI();
        }

        private void AddKeyboardAcceleratorToChangeTheme()
        {
            // A bit of extra fun (not related to threading): change theme with ctrl+T key combination
            rootPanel.RequestedTheme = Application.Current.RequestedTheme == ApplicationTheme.Light ? ElementTheme.Light : ElementTheme.Dark;
            var accelerator = new KeyboardAccelerator()
            {
                Modifiers = VirtualKeyModifiers.Control,
                Key = VirtualKey.T,
            };
            accelerator.Invoked += (UIElement, args) =>
            {
                rootPanel.RequestedTheme = rootPanel.RequestedTheme == ElementTheme.Light
                ? rootPanel.RequestedTheme = ElementTheme.Dark
                : ElementTheme.Light;
                args.Handled = true;
            };
            rootPanel.KeyboardAcceleratorPlacementMode = KeyboardAcceleratorPlacementMode.Hidden;
            rootPanel.KeyboardAccelerators.Add(accelerator);
        }

        private void PrepareRaceButton_Click(object sender, RoutedEventArgs e)
        {
            prepareRaceButton.IsEnabled = false;

            game.PrepareRace(UpdateBikeUI);
            foreach (var bike in game.Bikes)
            {
                var bikeTextBlock = new TextBlock()
                {
                    Text = "b",
                    FontFamily = new FontFamily("Webdings"),
                    FontSize = 64,
                    Margin = new Thickness(10, 0, 0, 0)
                };
                bikesPanel.Children.Add(bikeTextBlock);
                bikeTextBlocks.Add(bikeTextBlock);
            }


        }

        private void StartRaceButton_Click(object sender, RoutedEventArgs e)
        {
            startRaceButton.IsEnabled = false;
            game.StartBikes();
        }

        private void StartNextFromDepoButton_Click(object sender, RoutedEventArgs e)
        {
            game.StartNextBikeFromDepo();
        }

        /// <summary>
        /// Frissíti a felületet a játék állapotának megfelelően. Ez a biciklik
        /// pozíciójának megfelelően a biciklikhez tartozó TextBlock-ot pozíciójának
        /// beállítását jelenti.
        /// </summary>
        private void UpdateUI()
        {
            const int marginAdjustmentForWheel = 8;

            for (int i = 0; i < game.Bikes.Count; i++)
            {
                var bike = game.Bikes[i]; // bicikli
                var tbBike = bikeTextBlocks[i]; // a biciklit reprezentáló felületelem

                // Állítsuk be a tbBike TextBox baloldali margóját a bicikli aktuális pozíciójának megfelelően.
                // Az ablak 0,0 pontja az origó, ehhez képest nézzük a start/depó/finis vonalat.
                // A gomb jobb szélén van a kerék, de ezt a gomb bal oldalára kell mozgatni: ActualWidth-et ki kell vonni.
                tbBike.Margin = new Thickness(bike.Position - tbBike.ActualWidth + marginAdjustmentForWheel, 0, 0, 0);

                if (bike.IsWinner)
                    tbBike.Text = "%";
            }
        }

        /// <summary>
        /// Frissíti a bicikli megjelenítését a felületen.
        /// </summary>
        /// <param name="bike">A megváltozott bicikli.</param>
        private void UpdateBikeUI(Bike bike)
        {
            // Előfordulhat, hogy az UpdateBikeUI olyan korán hívódik, hogy a
            // bikeTextBlocks még nincs feltöltve, ilyenkor még nem tudjuk frissíteni
            // a felületet, térjünk vissza.
            if (bikeTextBlocks.Count != game.Bikes.Count)
                return;

            int marginAdjustmentForWheel = 8;

            // Biciklihez tartozó TextBlock kikeresése (azonos tömbindex alapján).
            var tbBike = bikeTextBlocks[game.Bikes.IndexOf(bike)];

            // Akkor még ne állítsuk a bicikli pozícióját, amikor a mérete a layout során nem
            // került meghatározásra (különben ugrálna a bicikli, hiszen alább, a margó beállításakor
            // "érvénytelen" 0 szélességértékkel számolnánk.

            if (DispatcherQueue == null) 
                return;
            if (DispatcherQueue.HasThreadAccess)
            {
                if (tbBike.ActualWidth == 0)
                    return;

                // Az ablak 0,0 pontja az origó, ehhez képest nézzük a start/depó/finish vonalat.
                // A gomb jobb szélén van a kerék, de ezt a gomb bal oldalára kell mozgatni: ActualWidth-et ki kell vonni.
                tbBike.Margin = new Thickness(bike.Position - tbBike.ActualWidth + marginAdjustmentForWheel, 0, 0, 0);

                if (bike.IsWinner)
                    tbBike.Text = "%"; // display a cup
            }
            else
            {
                DispatcherQueue.TryEnqueue(() =>
                {
                    UpdateBikeUI(bike);
                });
            }
            // A DispatcherQueue-n keresztül történik a felületelemek frissítése.
            
        }
    }
}