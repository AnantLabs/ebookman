using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace EBookMan
{
    public partial class StarRating : UserControl
    {
        #region inner class
        public class StarRatingException : ApplicationException
        {
            public StarRatingException(string message) : base(message)
            {

            }
        }
        #endregion

        #region constructor
        public StarRating()
        {
            InitializeComponent();
        }
        #endregion

        #region public properties
        private Image imageEmpty;
        [CategoryAttribute("Appearance"), DescriptionAttribute("Image that will be used for unavailable points")]
        public Image ImageEmpty
        {
            get { return this.imageEmpty; }
            set
            {
                this.imageEmpty = value;
                if (imageChecked != null)
                {
                    if (imageEmpty.Size.Equals(imageChecked.Size))
                    {
                        this.Invalidate();
                    }
                    else
                    {
                        throw new StarRatingException("Images must have the same size!");
                    }
                }
            }
        }

        private Image imageChecked;
        [CategoryAttribute("Appearance"), DescriptionAttribute("Image that will be used for available points")]
        public Image ImageChecked
        {
            get { return this.imageChecked; }
            set
            {
                this.imageChecked = value;
                if (imageEmpty != null)
                {
                    if (imageEmpty.Size.Equals(imageChecked.Size))
                    {
                        this.Invalidate();
                    }
                    else
                    {
                        throw new StarRatingException("Images must have the same size!");
                    }
                }
            }
        }

        private int maxStars = 5;
        [CategoryAttribute("Appearance"), DescriptionAttribute("The maximum numbers of stars")]
        public int MaximumStars
        {
            get { return this.maxStars; }
            set { this.maxStars = value; }
        }

        private int stars = 0;
        [CategoryAttribute("Appearance"), DescriptionAttribute("Currently selected stars")]
        public int Stars
        {
            get { return this.stars; }
            set
            {
                if (value > maxStars)
                {
                    throw new StarRatingException("Value can't be higher than the maximum number of stars!");
                }
                this.stars = value;
                this.Invalidate();
            }
        }

        public event EventHandler StarsChanged;

        #endregion

        #region variables
        private int tempStars = -1;
        #endregion

        #region methods
        private void StarRating_Paint(object sender, PaintEventArgs e)
        {
            // make sure we don't get an exception in case the images
            // haven't been set
            if (imageChecked == null || imageEmpty == null) return;
            
            for (int i = 0; i < maxStars; i++)
            {
                Point newPoint = new Point(imageChecked.Width * i, 0);

                // take care of tempStars! If it's not equal to -1 it means
                // the user is selecting a new value
                if (i < (tempStars == -1 ? stars : tempStars))
                {
                    e.Graphics.DrawImage(imageChecked, newPoint);
                }
                else
                {
                    e.Graphics.DrawImage(imageEmpty, newPoint);
                }
            }  
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            if (imageChecked == null || imageEmpty == null) return new Size();

            return new Size(this.imageChecked.Width * this.maxStars, this.imageChecked.Height);
        }

        private void StarRating_MouseMove(object sender, MouseEventArgs e)
        {
            double tempStarsD = (e.X + imageChecked.Width - 5) / imageChecked.Width;
            int newTempStars = Convert.ToInt32(Math.Floor(tempStarsD));

            // just redraw in case we really have to
            if (!newTempStars.Equals(tempStars))
            {
                tempStars = newTempStars;
                this.Invalidate();
            }
        }

        private void StarRating_MouseLeave(object sender, EventArgs e)
        {
            // user has left the control so remove the temporary value and redraw the control
            tempStars = -1;
            this.Invalidate();
        }

        private void StarRating_MouseDown(object sender, MouseEventArgs e)
        {
            double sStarsD = (e.X + imageChecked.Width - 5) / imageChecked.Width;
            int newStars = Convert.ToInt32(Math.Floor(sStarsD));

            // check if the user has really selected a different value
            if (!newStars.Equals(stars))
            {
                stars = newStars;

                // in case the control is wider than imageWidth*maxStars this is necessary
                // otherwise a StarRatingException will be raised
                if (stars > maxStars)
                {
                    stars = maxStars;
                }
                this.Invalidate();

                EventHandler h = this.StarsChanged;
                if ( h != null ) h(this, EventArgs.Empty);
            }
        }
        #endregion
    }
}
