using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace G00385433
{
    public partial class MainPage : ContentPage
    {
        //global variables
        System.Timers.Timer _timer;
        int _MyInterval = 250; //this is in miliseconds
        int _countdownStart = 25;

        Random _random;

        public MainPage()
        {
            InitializeComponent();
            _random = new Random();   // r is my random number generator


            SetUpMyTimers();
        }

        private void SetUpMyTimers()
        {
            //variables
            _timer = new System.Timers.Timer();
            _timer.Interval = _MyInterval;

            LblCountdown.Text = _countdownStart.ToString();

            Device.StartTimer(TimeSpan.FromMilliseconds(1000), () =>
            {
                Device.BeginInvokeOnMainThread(() => { TimerFunctions(); }); // line 5
                return true;  // line 6
            });
        }

        private void TimerFunctions()
        {
            // change the countdown.
            //LblCountdown.Text = "TEST";
            _countdownStart--;
            LblCountdown.Text = _countdownStart.ToString();
        }
        

        private void BtnSwitch_Clicked(object sender, EventArgs e)
        {
            //Minor Issue -> Moles only appear on grid after the BtnSwitch button is clicked 

            //switch is used to chnage between grids
            switch (BtnSwitch.Text)
            {
                case "5x5": // make the 3x3 visible
                    {
                        GridMoles3.IsVisible = false;
                        GridMoles5.IsVisible = true;
                        BtnSwitch.Text = "3x3";
                        break;
                    }
                case "3x3": // make the 5x5 visible
                    {
                        ResetTheGrid3();
                        GridMoles3.IsVisible = true;
                        GridMoles5.IsVisible = false;
                        BtnSwitch.Text = "5x5";
                        break;
                    }
                default:
                    break;
            } // end switch (BtnSwitch.Text)
        }

        private void ResetTheGrid3()
        {

            // foreach statement used to make all image buttons visible


            ImageButton imageButton;    // local variable

            foreach (var item in GridMoles3.Children)
            {
                if (item.GetType() == typeof(ImageButton))
                {
                    imageButton = (ImageButton)item;
                    imageButton.IsVisible = true;
                }
            }

            // reset the score back to 0
            LblScore.Text = "0";
        }

        private void ImgBtnMole_Clicked(object sender, EventArgs e)
        {
            // give the user 10 points
            int currentScore = Convert.ToInt32(LblScore.Text);
            currentScore += 10;
            LblScore.Text = currentScore.ToString();
            LblScore.TextColor = Color.Red;
            // make the image button disappear
            // used the send object
            ImageButton i = (ImageButton)sender;  // save the sender
            //i.IsVisible = false;
            MoveTheMole();

            //unfinished
            if (currentScore == 200)
            {
                FinishedGameAlert.Text = "Congratulations! You've won the game!";
            }

        }

        private void BtnTest_Clicked(object sender, EventArgs e)
        {
            MoveTheMole();
        }

        private void MoveTheMole() //Unfixed issue -> mole on 3x3 grid won't move when clicked -> only works when the move mole button is clicked.
        {

            //variables
            int r = 0, c = 0;
            int r1 = 0, c1 = 0;
            r = _random.Next(0, 3);
            c = _random.Next(0, 3);
            r1 = _random.Next(0, 3);
            c1 = _random.Next(0, 3);


            // generate random number
            ImgBtnMole.SetValue(Grid.RowProperty, r);
            ImgBtnMole.SetValue(Grid.ColumnProperty, c);
            ImgBtnMole1.SetValue(Grid.RowProperty, r1);
            ImgBtnMole1.SetValue(Grid.ColumnProperty, c1);
            // make the button visible
            ImgBtnMole.IsVisible = true;
            ImgBtnMole1.IsVisible = true;
        }


        // seperate method for a different score value on 5x5 grid
        private void ImgBtnMole1_Clicked(object sender, EventArgs e)
        {
            //give user 20 points
            int currentScore = Convert.ToInt32(LblScore.Text);
            currentScore += 20;

            LblScore.Text = currentScore.ToString();

            ImageButton i = (ImageButton)sender;
            MoveTheMole();
        }

        private void BtnTimerStart_Clicked(object sender, EventArgs e) //Unfixed issue -> timer won't start when button is clicked ->only text color chnages to black whne button is clicked
        {
            if (BtnTimerStart.Text == "Start")
            {
                ResetTimers();
                _timer.Interval = 250;
                _timer.Start();
            }
            else if (BtnTimerStart.Text == "Stop")
            {
                ResetTimers();
                BtnTimerStart.Text = "Start";
                _timer.Stop();
            }

            //Clear the Grid
            ResetTheGrid3();
            //Move the Mole
            MoveTheMole();
        }

        private void ResetTimers()
        {
            LblCountdown.Text = _countdownStart.ToString();
            LblCountdown.TextColor = Color.Black;
            BtnTimerStart.Text = "Stop";

        }

        private void UpdateAfterTimer()
        {
            //variables
            int counter;
            counter = Convert.ToInt32(LblCountdown.Text);
            counter--; //decriment

            LblCountdown.Text = counter.ToString();

            if (counter < 6)
            {
                LblCountdown.TextColor = Color.Blue;
            }
            if (counter == 0)
            {
                _timer.Stop();
                //reset button
                BtnTimerStart.Text = "Start";
            }

        }



        private void NewGame_Clicked(object sender, EventArgs e)
        {
            //Clear the Grid
            ResetTheGrid3();
            //Move the Mole
            MoveTheMole();
            SetUpMyTimers();
        }
    }

}
