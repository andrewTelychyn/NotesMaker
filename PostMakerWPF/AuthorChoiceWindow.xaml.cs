using Caliburn.Micro;
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
    /// Логика взаимодействия для AuthorChoiceWindow.xaml
    /// </summary>
    public partial class AuthorChoiceWindow : Window
    {
        public delegate void Changer(int index);

        public Changer delegator;


        public AuthorChoiceWindow(List<AuthorBookModel> names, Changer deleg)
        {
            InitializeComponent();

            namesDataGridView.ItemsSource = names;
            namesDataGridView.IsReadOnly = true;
            namesDataGridView.SelectionUnit = DataGridSelectionUnit.FullRow;
            namesDataGridView.SelectionMode = DataGridSelectionMode.Single;

            //namesDataGridView.Columns["Id"].Visible = false;
            //namesDataGridView.ReadOnly = true;

            delegator += deleg;

            //namesDataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //namesDataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //namesDataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //namesDataGridView.RowHeadersVisible = false;
            //namesDataGridView.ColumnHeadersVisible = false;
            //namesDataGridView.ReadOnly = true;
        }

        private void namesDataGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = namesDataGridView.SelectedIndex;
            delegator(index);
            this.Close();
        }
    }
}
