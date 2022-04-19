using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BowlingWFA
{
    public partial class Form1 : Form
    {
        private int _i = 0;
        List<string> throws = new();
        List<int> roundPoints = new() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        ThisThrow thisThrows;
        //number of current throw from 0 - 20
        public int CurrentThrow { get => _i; set => _i = value; }
        //Contains all the labels used to show throw points
        public List<string> Throws { get => throws; set => throws = value; }
        //Contain points for all of the rounds
        public List<int> RoundPoints { get => roundPoints; set => roundPoints = value; }
        public enum ThisThrow { strike = 1, spare, numbers, twoNumbersAfterAStrike, numberAfterTwoStrikes, spareAfterSpareOrNumber, pinFallOne, pinFallTwo }
        public Form1()
        {
            InitializeComponent();
        }
        //Button to call NewRound
        private void StartOverBtn_Click(object sender, EventArgs e)
        {
            NewRound();
        }
        #region Buttons to choose how many pins the user has knocked down
        private void ZeroPinBtn_Click(object sender, EventArgs e)
        {
            HideShowPin(0);
            Pinsknocked("0");

        }
        private void OnePinBtn_Click(object sender, EventArgs e)
        {
            HideShowPin(1);
            Pinsknocked("1");
        }
        private void TwoPinBtn_Click(object sender, EventArgs e)
        {
            HideShowPin(2);
            Pinsknocked("2");
        }

        private void ThreePinBtn_Click(object sender, EventArgs e)
        {
            HideShowPin(3);
            Pinsknocked("3");
        }

        private void FourPinBtn_Click(object sender, EventArgs e)
        {
            HideShowPin(4);
            Pinsknocked("4");
        }

        private void FivePinBtn_Click(object sender, EventArgs e)
        {
            HideShowPin(5);
            Pinsknocked("5");
        }

        private void SixPinBtn_Click(object sender, EventArgs e)
        {
            HideShowPin(6);
            Pinsknocked("6");
        }

        private void SevenPinBtn_Click(object sender, EventArgs e)
        {
            HideShowPin(7);
            Pinsknocked("7");
        }

        private void EightPinBtn_Click(object sender, EventArgs e)
        {
            HideShowPin(8);
            Pinsknocked("8");
        }

        private void NinePinBtn_Click(object sender, EventArgs e)
        {
            HideShowPin(9);
            Pinsknocked("9");
        }

        private void TenPinBtn_Click(object sender, EventArgs e)
        {
            Pinsknocked("X");
        }
        #endregion
        //Hides and shows the buttons to make sure the user can't knock down more than 10 pins per turn
        public void HideShowPin(int i)
        {
            if (CurrentThrow % 2 == 0 & i != 0)
            {
                TenPinBtn.Hide();
                if (i == 1) return;
                NinePinBtn.Hide();
                if (i == 2) return;
                EightPinBtn.Hide();
                if (i == 3) return;
                SevenPinBtn.Hide();
                if (i == 4) return;
                SixPinBtn.Hide();
                if (i == 5) return;
                FivePinBtn.Hide();
                if (i == 6) return;
                FourPinBtn.Hide();
                if (i == 7) return;
                ThreePinBtn.Hide();
                if (i == 8) return;
                TwoPinBtn.Hide();
            }
            else
            {
                TenPinBtn.Show();
                NinePinBtn.Show();
                EightPinBtn.Show();
                SevenPinBtn.Show();
                SixPinBtn.Show();
                FivePinBtn.Show();
                FourPinBtn.Show();
                ThreePinBtn.Show();
                TwoPinBtn.Show();
            }
        }
        //This method is passing either, /, X or a number to ThrowScore()
        public void Pinsknocked(string str)
        {
            ThrowScore(str);
            List<string> throws = new();
            Throws = PopulateThrowsList(throws);
            try
            {
                if (CurrentThrow > 0)
                {
                    int throwOne = int.Parse(Throws[CurrentThrow - 1]);
                    int throwTwo = int.Parse(Throws[CurrentThrow]);

                    if (throwOne + throwTwo == 10 & CurrentThrow % 2 == 1 || throwOne + throwTwo == 10 & CurrentThrow % 2 == 0 & CurrentRound() == 9 & CurrentThrow != 18)
                    {
                        str = "/";
                        ThrowScore(str);
                    }
                }
            }
            catch (FormatException)
            {

            }
            if (CurrentRound() == 10)
            {
                //ThrowScore(str);
            }
            else if (CurrentThrow % 2 == 0 & str == "X" & CurrentRound() != 9)
            {
                str = "";
                ThrowScore(str);
                CurrentThrow++;
                str = "X";
                ThrowScore(str);
            }
            else if (CurrentThrow % 2 == 1 & str == "X" & CurrentRound() != 9)
            {
                str = "/";
                ThrowScore(str);
            }
            SrikeSpareOrNumber();
            CurrentThrow++;
    }
        //Checking if the user hit a strike, spare or a number and then calling the respective method
        public void SrikeSpareOrNumber()
        {
            Throws = PopulateThrowsList(Throws);

            if (Throws[CurrentThrow] == "X")
            {
                Strike();
            }
            else if (Throws[CurrentThrow] == "/")
            {
                Spare();
            }
            else
            {
                Number();
            }
        }
        //Checking for all of the rules for traditional bowling and then passing an appropriate variable to CalculateRoundScore()
        public void Number()
        {
            if (CurrentThrow == 1)
            {
                int currentRoundScore = int.Parse(Throws[CurrentThrow]) + int.Parse(Throws[CurrentThrow - 1]);
                CalculateRoundScore(currentRoundScore, ThisThrow.numbers);
                return;
            }
            if (CurrentThrow - 2 < 0)
            {
                return;
            }
            else if (Throws[CurrentThrow - 1] == "/")
            {
                int currentRoundScore = int.Parse(Throws[CurrentThrow]);
                CalculateRoundScore(currentRoundScore, ThisThrow.twoNumbersAfterAStrike);
            }
            else if (Throws[CurrentThrow - 2] == "X" & Throws[CurrentThrow - 1] != "X")
            {
                int currentRoundScore = int.Parse(Throws[CurrentThrow]) + int.Parse(Throws[CurrentThrow - 1]);
                CalculateRoundScore(currentRoundScore, ThisThrow.twoNumbersAfterAStrike);
            }
            if (CurrentThrow - 3 < 0)
            {
                return;
            } 
            else if (Throws[CurrentThrow - 1] == "X" & Throws[CurrentThrow - 3] == "X" || Throws[CurrentThrow - 1] == "X" & Throws[CurrentThrow - 2] == "X")
            {
                int currentRoundScore = int.Parse(Throws[CurrentThrow]);
                CalculateRoundScore(currentRoundScore, ThisThrow.numberAfterTwoStrikes);
            }
            if (CurrentThrow - 1 < 0)
            {
                return;
            }
            else if (CurrentThrow % 2 == 1 & Throws[CurrentThrow - 1] != "X")
            {
                int currentRoundScore = int.Parse(Throws[CurrentThrow]) + int.Parse(Throws[CurrentThrow - 1]);
                CalculateRoundScore(currentRoundScore, ThisThrow.numbers);
            }

        }
        //Checking for all of the rules for traditional bowling and then passing an appropriate variable to CalculateRoundScore()
        public void Spare()
        {
            if (CurrentThrow - 2 < 0)
            {
                CalculateRoundScore(10, ThisThrow.spare);
                return;
            }
            if (Throws[CurrentThrow - 2] == "/" || Throws[CurrentThrow - 2] != "/" & Throws[CurrentThrow - 2] != "X")
            {
                CalculateRoundScore(10, ThisThrow.spareAfterSpareOrNumber);
            }
            if (Throws[CurrentThrow - 2] == "X")
            {
                CalculateRoundScore(10, ThisThrow.pinFallOne);
            }
            if (CurrentThrow != 1 & CurrentThrow % 2 == 1)
            {
                CalculateRoundScore(10, ThisThrow.spare);
            }
            else if (CurrentThrow - 4 < 0)
            {
                return;
            }
            else if (Throws[CurrentThrow - 2] != "X" & Throws[CurrentThrow - 4] != "X") 
            {
                CalculateRoundScore(10, ThisThrow.spare);
            }
        }
        //Checking for all of the rules for traditional bowling and then passing an appropriate variable to CalculateRoundScore()
        public void Strike()
        {
            Throws = PopulateThrowsList(Throws);
            
            if (CurrentThrow - 2 < 0)
            {
                CalculateRoundScore(10, ThisThrow.strike);
                return;
            }
            else if (Throws[CurrentThrow - 2] == "/")
            {
                CalculateRoundScore(10, ThisThrow.pinFallOne);
            }
            else if (CurrentRound() > 0 & Throws[CurrentThrow - 2] != "X" & CurrentRound() != 9)
            {
                RoundPoints[CurrentRound()] += RoundPoints[CurrentRound() - 1];
            }
            else if (Throws[CurrentThrow - 2] == "X" || Throws[CurrentThrow - 1] == "X")
            {
                CalculateRoundScore(10, ThisThrow.pinFallOne);
                //debugLabel.Text = "dobble Strike";
            }
            if (CurrentThrow - 4 < 0)
            {
                CalculateRoundScore(10, ThisThrow.strike);
                return;
            }
            else if (Throws[CurrentThrow - 2] == "X" & Throws[CurrentThrow - 4] == "X" & CurrentThrow != 19 || Throws[CurrentThrow - 1] == "X" & Throws[CurrentThrow - 3] == "X" || Throws[CurrentThrow - 1] == "X" & Throws[CurrentThrow - 2] == "X")
            {
                CalculateRoundScore(10, ThisThrow.pinFallTwo);
                //debugLabel.Text = "tripple Strike";
            }
            if (CurrentRound() != 9)
            {
               CalculateRoundScore(10, ThisThrow.strike);
            }
            else
            {
                CalculateRoundScore(10, ThisThrow.strike);
            }
        }
        //Populates the list, Throws with all of the lables showing throws
        public List<string> PopulateThrowsList(List<string> throws)
        {
            throws = new List<string> () { R1T1.Text, R1T2.Text,
                                       R2T1.Text, R2T2.Text,
                                       R3T1.Text, R3T2.Text,
                                       R4T1.Text, R4T2.Text,
                                       R5T1.Text, R5T2.Text,
                                       R6T1.Text, R6T2.Text,
                                       R7T1.Text, R7T2.Text,
                                       R8T1.Text, R8T2.Text,
                                       R9T1.Text, R9T2.Text,
                                       R10T1.Text, R10T2.Text, R10T3.Text, };
            return throws;
        }
        //ThrowScore() displays all the throws to the user, one by one as the user is bowling
        public void ThrowScore(string str)
        {
            switch (CurrentThrow)
            {
                case 0:
                    R1T1.Text = str;
                    break;
                case 1:
                    R1T2.Text = str;
                    break;
                case 2:
                    R2T1.Text = str;
                    break;
                case 3:
                    R2T2.Text = str;
                    break;
                case 4:
                    R3T1.Text = str;
                    break;
                case 5:
                    R3T2.Text = str;
                    break;
                case 6:
                    R4T1.Text = str;
                    break;
                case 7:
                    R4T2.Text = str;
                    break;
                case 8:
                    R5T1.Text = str;
                    break;
                case 9:
                    R5T2.Text = str;
                    break;
                case 10:
                    R6T1.Text = str;
                    break;
                case 11:
                    R6T2.Text = str;
                    break;
                case 12:
                    R7T1.Text = str;
                    break;
                case 13:
                    R7T2.Text = str;
                    break;
                case 14:
                    R8T1.Text = str;
                    break;
                case 15:
                    R8T2.Text = str;
                    break;
                case 16:
                    R9T1.Text = str;
                    break;
                case 17:
                    R9T2.Text = str;
                    break;
                case 18:
                    R10T1.Text = str;
                    break;
                case 19:
                    R10T2.Text = str;
                    break;
                case 20:
                    R10T3.Text = str;
                    break;
            }
        }
        //This method add points based on the current throw for the current round according to the rules for traditional bowling
        public void CalculateRoundScore(int currentThrow, ThisThrow thisThrow)
        {
            int currentRound = CurrentRound();
            if (thisThrow == ThisThrow.strike || thisThrow == ThisThrow.spare)
            {
                switch (currentRound)
                {
                    case 0:
                        RoundPoints[0] += currentThrow;
                        roundOnePointsL.Text = RoundPoints[0].ToString();
                        break;
                    case 1:
                        RoundPoints[1] += currentThrow;
                        roundTwoPointsL.Text = RoundPoints[1].ToString();
                        break;
                    case 2:
                        RoundPoints[2] += currentThrow;
                        roundThreePointsL.Text = RoundPoints[2].ToString();
                        break;
                    case 3:
                        RoundPoints[3] += currentThrow;
                        roundFourPointsL.Text = RoundPoints[3].ToString();
                        break;
                    case 4:
                        RoundPoints[4] += currentThrow;
                        roundFivePointsL.Text = RoundPoints[4].ToString();
                        break;
                    case 5:
                        RoundPoints[5] += currentThrow;
                        roundSixPointsL.Text = RoundPoints[5].ToString();
                        break;
                    case 6:
                        RoundPoints[6] += currentThrow;
                        roundSevenPointsL.Text = RoundPoints[6].ToString();
                        break;
                    case 7:
                        RoundPoints[7] += currentThrow;
                        roundEightPointsL.Text = RoundPoints[7].ToString();
                        break;
                    case 8:
                        RoundPoints[8] += currentThrow;
                        roundNinePointsL.Text = RoundPoints[8].ToString();
                        break;
                    case 9:
                        if (CurrentThrow == 20)
                        {
                            HidePinSelection(0);
                        }
                        if (CurrentThrow == 20 & thisThrow == ThisThrow.strike) break;
                        if (thisThrow == ThisThrow.spare)
                        {
                            RoundPoints[9] += RoundPoints[8];
                        }
                        RoundPoints[9] += currentThrow;
                        roundTenPointsL.Text = RoundPoints[9].ToString();
                        break;
                }
            }
            else if (thisThrow == ThisThrow.pinFallOne || thisThrow == ThisThrow.twoNumbersAfterAStrike || thisThrow == ThisThrow.spareAfterSpareOrNumber)
            {
                switch (currentRound)
                {
                    case 0:
                    default:
                        break;
                    case 1:
                        if (thisThrow != ThisThrow.spareAfterSpareOrNumber)
                        {
                            RoundPoints[0] += currentThrow;
                        }
                        roundOnePointsL.Text = RoundPoints[0].ToString();
                        if (thisThrow != ThisThrow.twoNumbersAfterAStrike)
                        {
                            RoundPoints[1] += RoundPoints[0];
                        }
                        roundTwoPointsL.Text = RoundPoints[1].ToString();
                        break;
                    case 2:
                        if (thisThrow != ThisThrow.spareAfterSpareOrNumber)
                        {
                            RoundPoints[1] += currentThrow;
                        }
                        roundTwoPointsL.Text = RoundPoints[1].ToString();
                        if (thisThrow != ThisThrow.twoNumbersAfterAStrike)
                        {
                            RoundPoints[2] += RoundPoints[1];
                        }
                        roundThreePointsL.Text = RoundPoints[2].ToString();
                        break;
                    case 3:
                        if (thisThrow != ThisThrow.spareAfterSpareOrNumber)
                        {
                            RoundPoints[2] += currentThrow;
                        }
                        roundThreePointsL.Text = RoundPoints[2].ToString();
                        if (thisThrow != ThisThrow.twoNumbersAfterAStrike)
                        {
                            RoundPoints[3] += RoundPoints[2];
                        }
                        roundFourPointsL.Text = RoundPoints[3].ToString();
                        break;
                    case 4:
                        if (thisThrow != ThisThrow.spareAfterSpareOrNumber)
                        {
                            RoundPoints[3] += currentThrow;
                        }
                        roundFourPointsL.Text = RoundPoints[3].ToString();
                        if (thisThrow != ThisThrow.twoNumbersAfterAStrike)
                        {
                            RoundPoints[4] += RoundPoints[3];
                        }
                        roundFivePointsL.Text = RoundPoints[4].ToString();
                        break;
                    case 5:
                        if (thisThrow != ThisThrow.spareAfterSpareOrNumber)
                        {
                            RoundPoints[4] += currentThrow;
                        }
                        roundFivePointsL.Text = RoundPoints[4].ToString();
                        if (thisThrow != ThisThrow.twoNumbersAfterAStrike)
                        {
                            RoundPoints[5] += RoundPoints[4];
                        }
                        roundSixPointsL.Text = RoundPoints[5].ToString();
                        break;
                    case 6:
                        if (thisThrow != ThisThrow.spareAfterSpareOrNumber)
                        {
                            RoundPoints[5] += currentThrow;
                        }
                        roundSixPointsL.Text = RoundPoints[5].ToString();
                        if (thisThrow != ThisThrow.twoNumbersAfterAStrike)
                        {
                            RoundPoints[6] += RoundPoints[5];
                        }
                        roundSevenPointsL.Text = RoundPoints[6].ToString();
                        break;
                    case 7:
                        if (thisThrow != ThisThrow.spareAfterSpareOrNumber)
                        {
                            RoundPoints[6] += currentThrow;
                        }
                        roundSevenPointsL.Text = RoundPoints[6].ToString();
                        if (thisThrow != ThisThrow.twoNumbersAfterAStrike)
                        {
                            RoundPoints[7] += RoundPoints[6];
                        }
                        roundEightPointsL.Text = RoundPoints[7].ToString();
                        break;
                    case 8:
                        if (thisThrow != ThisThrow.spareAfterSpareOrNumber)
                        {
                            RoundPoints[7] += currentThrow;
                        }
                        roundEightPointsL.Text = RoundPoints[7].ToString();
                        if (thisThrow != ThisThrow.twoNumbersAfterAStrike)
                        {
                            RoundPoints[8] += RoundPoints[7];
                        }
                        roundNinePointsL.Text = RoundPoints[8].ToString();
                        break;
                    case 9:
                        if (thisThrow != ThisThrow.spareAfterSpareOrNumber & CurrentThrow != 20)
                        {
                            RoundPoints[8] += currentThrow;
                        }
                        else if (CurrentThrow == 20 & thisThrow == ThisThrow.pinFallOne)
                        {
                            RoundPoints[9] += currentThrow;
                            TenPinBtn.Show();
                            NinePinBtn.Show();
                            EightPinBtn.Show();
                            SevenPinBtn.Show();
                            SixPinBtn.Show();
                            FivePinBtn.Show();
                            FourPinBtn.Show();
                            ThreePinBtn.Show();
                            TwoPinBtn.Show();
                            OnePinBtn.Show();
                            ZeroPinBtn.Show();
                            HidePinSelection(0);
                        }
                        else if (CurrentThrow == 20 & thisThrow == ThisThrow.twoNumbersAfterAStrike || CurrentThrow == 19 & thisThrow == ThisThrow.twoNumbersAfterAStrike)
                        {
                            RoundPoints[9] += currentThrow;
                            RoundPoints[9] += currentThrow;
                            TenPinBtn.Show();
                            NinePinBtn.Show();
                            EightPinBtn.Show();
                            SevenPinBtn.Show();
                            SixPinBtn.Show();
                            FivePinBtn.Show();
                            FourPinBtn.Show();
                            ThreePinBtn.Show();
                            TwoPinBtn.Show();
                            OnePinBtn.Show();
                            ZeroPinBtn.Show();
                            HidePinSelection(0);
                        }
                        roundNinePointsL.Text = RoundPoints[8].ToString();
                        if (thisThrow != ThisThrow.twoNumbersAfterAStrike & CurrentThrow != 19 & CurrentThrow != 20 || thisThrow == ThisThrow.pinFallOne & CurrentThrow != 19 & CurrentThrow != 20)
                        {
                            RoundPoints[9] += RoundPoints[8];
                        }
                        roundTenPointsL.Text = RoundPoints[9].ToString();
                        break;
                }
            }
            else if (thisThrow == ThisThrow.pinFallTwo || thisThrow == ThisThrow.numberAfterTwoStrikes)
            {
                switch (currentRound)
                {
                    case 1:
                    default:
                        break;
                    case 2:
                        RoundPoints[0] += currentThrow;
                        roundOnePointsL.Text = RoundPoints[0].ToString();
                        RoundPoints[1] += currentThrow;
                        roundTwoPointsL.Text = RoundPoints[1].ToString();
                        if (thisThrow != ThisThrow.pinFallTwo) break;
                        RoundPoints[2] += currentThrow;
                        roundThreePointsL.Text = RoundPoints[2].ToString();
                        break;
                    case 3:
                        RoundPoints[1] += currentThrow;
                        roundTwoPointsL.Text = RoundPoints[1].ToString();
                        RoundPoints[2] += currentThrow;
                        roundThreePointsL.Text = RoundPoints[2].ToString();
                        if (thisThrow != ThisThrow.pinFallTwo) break;
                        RoundPoints[3] += currentThrow;
                        roundFourPointsL.Text = RoundPoints[3].ToString();
                        break;
                    case 4:
                        RoundPoints[2] += currentThrow;
                        roundThreePointsL.Text = RoundPoints[2].ToString();
                        RoundPoints[3] += currentThrow;
                        roundFourPointsL.Text = RoundPoints[3].ToString();
                        if (thisThrow != ThisThrow.pinFallTwo) break;
                        RoundPoints[4] += currentThrow;
                        roundFivePointsL.Text = RoundPoints[4].ToString();
                        break;
                    case 5:
                        RoundPoints[3] += currentThrow;
                        roundFourPointsL.Text = RoundPoints[3].ToString();
                        RoundPoints[4] += currentThrow;
                        roundFivePointsL.Text = RoundPoints[4].ToString();
                        if (thisThrow != ThisThrow.pinFallTwo) break;
                        RoundPoints[5] += currentThrow;
                        roundSixPointsL.Text = RoundPoints[5].ToString();
                        break;
                    case 6:
                        RoundPoints[4] += currentThrow;
                        roundFivePointsL.Text = RoundPoints[4].ToString();
                        RoundPoints[5] += currentThrow;
                        roundSixPointsL.Text = RoundPoints[5].ToString();
                        if (thisThrow != ThisThrow.pinFallTwo) break;
                        RoundPoints[6] += currentThrow;
                        roundSevenPointsL.Text = RoundPoints[6].ToString();
                        break;
                    case 7:
                        RoundPoints[5] += currentThrow;
                        roundSixPointsL.Text = RoundPoints[5].ToString();
                        RoundPoints[6] += currentThrow;
                        roundSevenPointsL.Text = RoundPoints[6].ToString();
                        if (thisThrow != ThisThrow.pinFallTwo) break;
                        RoundPoints[7] += currentThrow;
                        roundEightPointsL.Text = RoundPoints[7].ToString();
                        break;
                    case 8:
                        RoundPoints[6] += currentThrow;
                        roundSevenPointsL.Text = RoundPoints[6].ToString();
                        RoundPoints[7] += currentThrow;
                        roundEightPointsL.Text = RoundPoints[7].ToString();
                        if (thisThrow != ThisThrow.pinFallTwo) break;
                        RoundPoints[8] += currentThrow;
                        roundNinePointsL.Text = RoundPoints[8].ToString();
                        break;
                    case 9:
                        if (CurrentThrow < 19)
                        {
                            RoundPoints[7] += currentThrow;
                            RoundPoints[8] += currentThrow;
                        }
                        if (CurrentThrow == 19 & thisThrow == ThisThrow.numberAfterTwoStrikes)
                        {
                            RoundPoints[8] += currentThrow;
                            RoundPoints[9] += currentThrow;
                            roundTenPointsL.Text = RoundPoints[9].ToString();
                        }
                        roundEightPointsL.Text = RoundPoints[7].ToString();
                        roundNinePointsL.Text = RoundPoints[8].ToString();
                        if (thisThrow != ThisThrow.pinFallTwo) break;
                        RoundPoints[9] += currentThrow;
                        roundTenPointsL.Text = RoundPoints[9].ToString();
                        break;
                }
            }
            else if (thisThrow == ThisThrow.numbers)
            {
                switch (currentRound)
                {
                    case 0:
                        RoundPoints[0] += currentThrow;
                        roundOnePointsL.Text = RoundPoints[0].ToString();
                        break;
                    case 1:
                        RoundPoints[1] += RoundPoints[0] += currentThrow;
                        roundTwoPointsL.Text = RoundPoints[1].ToString();
                        break;
                    case 2:
                        RoundPoints[2] += RoundPoints[1] += currentThrow;
                        roundThreePointsL.Text = RoundPoints[2].ToString();
                        break;
                    case 3:
                        RoundPoints[3] += RoundPoints[2] += currentThrow;
                        roundFourPointsL.Text = RoundPoints[3].ToString();
                        break;
                    case 4:
                        RoundPoints[4] += RoundPoints[3] += currentThrow;
                        roundFivePointsL.Text = RoundPoints[4].ToString();
                        break;
                    case 5:
                        RoundPoints[5] += RoundPoints[4] += currentThrow;
                        roundSixPointsL.Text = RoundPoints[5].ToString();
                        break;
                    case 6:
                        RoundPoints[6] += RoundPoints[5] += currentThrow;
                        roundSevenPointsL.Text = RoundPoints[6].ToString();
                        break;
                    case 7:
                        RoundPoints[7] += RoundPoints[6] += currentThrow;
                        roundEightPointsL.Text = RoundPoints[7].ToString();
                        break;
                    case 8:
                        RoundPoints[8] += RoundPoints[7] += currentThrow;
                        roundNinePointsL.Text = RoundPoints[8].ToString();
                        break;
                    case 9:
                        RoundPoints[9] += RoundPoints[8] += currentThrow;
                        roundTenPointsL.Text = RoundPoints[9].ToString();
                        if (CurrentThrow == 20)
                        {
                            RoundPoints[9] += currentThrow;
                            HidePinSelection(0);
                        }
                        if (CurrentThrow > 18)
                        {
                            HidePinSelection(0);
                        }
                        break;
                }
            }
        }
        //this method calculates and returns the current round based on the number of throws 
        public int CurrentRound()
        {
            int currentRound;
            if (CurrentThrow == 20)
            {
                currentRound = (CurrentThrow / 2) - 1;
                return currentRound;
            }
            else if (CurrentThrow % 2 == 0)
            {
                currentRound = ((CurrentThrow + 2) / 2) - 1;
                return currentRound;
            }
            else if (CurrentThrow % 2 == 1)
            {
                currentRound = ((CurrentThrow + 1) / 2) - 1;
                return currentRound;
            }
            return 50;
        }
        //Reset all the variables and remove any user input, to restart the game
        public void NewRound()
        {
            //MessageBox.Show("Content", "Title", MessageBoxIcon.Error);
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show("This game will not be saved, are you sure you want to restart?", "Warning!!!", buttons, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                CurrentThrow = 0;
                R1T1.Text = "";
                R1T2.Text = "";
                R2T1.Text = "";
                R2T2.Text = "";
                R3T1.Text = "";
                R3T2.Text = "";
                R4T1.Text = "";
                R4T2.Text = "";
                R5T1.Text = "";
                R5T2.Text = "";
                R6T1.Text = "";
                R6T2.Text = "";
                R7T1.Text = "";
                R7T2.Text = "";
                R8T1.Text = "";
                R8T2.Text = "";
                R9T1.Text = "";
                R9T2.Text = "";
                R10T1.Text = "";
                R10T2.Text = "";
                R10T3.Text = "";
                roundOnePointsL.Text = "";
                roundTwoPointsL.Text = "";
                roundThreePointsL.Text = "";
                roundFourPointsL.Text = "";
                roundFivePointsL.Text = "";
                roundSixPointsL.Text = "";
                roundSevenPointsL.Text = "";
                roundEightPointsL.Text = "";
                roundNinePointsL.Text = "";
                roundTenPointsL.Text = "";
                roundPoints = new() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                HidePinSelection(1);
                TenPinBtn.Show();
                NinePinBtn.Show();
                EightPinBtn.Show();
                SevenPinBtn.Show();
                SixPinBtn.Show();
                FivePinBtn.Show();
                FourPinBtn.Show();
                ThreePinBtn.Show();
                TwoPinBtn.Show();
                OnePinBtn.Show();
                ZeroPinBtn.Show();
            }
        }
        //disables or enable all pins from the selection, the method takes a 1 to select and a 0 to deselect
        public void HidePinSelection(int i)
        {
            if (i == 0)
            {
                TenPinBtn.Enabled = false;
                NinePinBtn.Enabled = false;
                EightPinBtn.Enabled = false;
                SevenPinBtn.Enabled = false;
                SixPinBtn.Enabled = false;
                FivePinBtn.Enabled = false;
                FourPinBtn.Enabled = false;
                ThreePinBtn.Enabled = false;
                TwoPinBtn.Enabled = false;
                OnePinBtn.Enabled = false;
                ZeroPinBtn.Enabled = false;
            }
            else if (i == 1)
            {
                TenPinBtn.Enabled = true;
                NinePinBtn.Enabled = true;
                EightPinBtn.Enabled = true;
                SevenPinBtn.Enabled = true;
                SixPinBtn.Enabled = true;
                FivePinBtn.Enabled = true;
                FourPinBtn.Enabled = true;
                ThreePinBtn.Enabled = true;
                TwoPinBtn.Enabled = true;
                OnePinBtn.Enabled = true;
                ZeroPinBtn.Enabled = true;
            }
        }
    }
}
