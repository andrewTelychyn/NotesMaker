using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PostMakerWPF
{
    /// <summary>
    /// Логика взаимодействия для AdditionalTextWindow.xaml
    /// </summary>
    public partial class AdditionalTextWindow : Window
    {
        public AdditionalTextWindow(string text, int index)
        {
            InitializeComponent();
            if(index == 1)
                this.Left = 1050;

            secondOutcomeRichTextBox.Text = text;
            secondProgressBar.Value = text.ToCharArray().Length;
            hereIsMore.Text = $"HERE IS MORE #{index}";
        }

        public void UpdateWindow(string text, int index)
        {
            secondOutcomeRichTextBox.Text = text;
            secondProgressBar.Value = text.ToCharArray().Length;
            hereIsMore.Text = $"HERE IS MORE #{index}";
        }

        private void secondCopyTextBoxButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (secondOutcomeRichTextBox.Text != null)
                Clipboard.SetText(secondOutcomeRichTextBox.Text);
        }
    }
}
