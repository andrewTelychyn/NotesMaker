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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BisnessLogicLibrary;

namespace PostMakerWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainBody Body { get; set; }// three fields 

        List<PartHelper> Parts { get; set; }//list of proxy of parts

        Part PresentPart { get; set; }// the part

        ContentHelper Main { get; set; }//main content

        DataAccess Data { get; set; }//access to data object

        List<AdditionalTextWindow> AdditionalTextBlocks { get; set; }

        List<AuthorBookModel> Models { get; set; }// list of data from db


        private delegate void MyWindowClosing();

        private MyWindowClosing ClosingWindowDelegate;

        public MainWindow()
        {
            InitializeComponent();

            Parts = new List<PartHelper>();

            AdditionalTextBlocks = new List<AdditionalTextWindow>();

            this.Left = -5;

            Data = new DataAccess();

            Models = Data.GetNames();

            this.Closing += (s, args) => closeWindows();

            //List<int> inter = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8};
            //inter.Insert(2, 8);
            //foreach (var item in inter)
            //{
            //    Console.WriteLine(item);
            //}
        }

        private void addNoteButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (PartHelper ph in Parts)
            {
                if (ph.part.Title == Validator.CheckSpaces(subpartRichTextBox.Text))
                {
                    PresentPart = ph.part;
                }
            }
            if (PresentPart == null)
            {
                PresentPart = new Part(Validator.CheckSpaces(subpartRichTextBox.Text, ':'));
            }

            try
            {
                PresentPart.Notes.Add(new Note(Validator.ValidateEmpty(noteContentRichTextBox.Text)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            partContentRichTextBox.Text = new PartHelper(PresentPart).Build();

            noteContentRichTextBox.Clear();
        }

        private void addContentButton_Click(object sender, RoutedEventArgs e)
        {
            if (PresentPart != null)
            {
                bool found = false;

                foreach (PartHelper ph in Parts)
                {
                    if (ph.part.Title == Validator.CheckSpaces(subpartRichTextBox.Text))//why presentpart isnt used
                    {
                        found = true;
                        ph.part.Notes = PresentPart.Notes;
                    }
                }
                if (found != true)
                {
                    Parts.Add(new PartHelper(PresentPart));
                }
            }

            if (Main == null)
            {
                try
                {
                    Body = new MainBody(
                    Validator.ValidateEmpty(bookNameTextBox.Text),
                    Validator.ValidateEmpty(authorNameTextBox.Text),
                    Validator.ValidateEmpty(partRichTextBox.Text));// why are we creating body?
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                bool found = false;

                foreach (AuthorBookModel model in Models)
                {
                    if (model.BookName == Body.Name)
                        found = true;
                    
                }
                if (!found)
                    Data.SaveName(new AuthorBookModel { AuthorName = Body.Author, BookName = Body.Name, LastUse = DateTime.Now });
                

                Main = new ContentHelper(Parts, Body);
            }
            else
                Main.Parts = Parts;
            

            Main.MainBuild();

            if (Main.Attempt > 0)
            {
                if (AdditionalTextBlocks.Count == 0 && Main.Blocks.Count == 2)
                    addAdditionalWindow(Main.Blocks[1], 1);
                
                else 
                {
                    for (int i = 0; i < AdditionalTextBlocks.Count; i++)
                        AdditionalTextBlocks[i].UpdateWindow(Main.Blocks[i + 1], i + 1);
                    
                    if (AdditionalTextBlocks.Count + 1 < Main.Blocks.Count)
                        addAdditionalWindow(Main.Blocks[AdditionalTextBlocks.Count + 1],
                            AdditionalTextBlocks.Count + 1);
                    
                }
            }
            outcomeRichTextBox.Text = Main.Blocks[0];

            mainProgressBar.Value = Main.Blocks[0].ToCharArray().Length;
        }

        private void addAdditionalWindow(string text, int index)
        {
            AdditionalTextWindow form = new AdditionalTextWindow(text, index);
            AdditionalTextBlocks.Add(form);
            form.Show();

            ClosingWindowDelegate += () =>
            {
                form.Close();
            };
        }

        private void closeWindows()
        {
            if (ClosingWindowDelegate != null)
            {
                ClosingWindowDelegate();
                ClosingWindowDelegate = null;
            }
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            PresentPart = null;
            //Parts = new List<PartHelper>();
            noteContentRichTextBox.Clear();
            partContentRichTextBox.Clear();
            subpartRichTextBox.Clear();

            AdditionalTextBlocks.Clear();
        }

        private void clearAllButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            closeWindows();
            var form = new MainWindow();
            form.Closing += (s, args) => this.Close();
            form.Show();

        }

        private void mainCopyTextBoxButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (outcomeRichTextBox.Text != null)
                Clipboard.SetText(outcomeRichTextBox.Text);
        }

        private async Task<List<AuthorBookModel>> getModels()
        {
            return await Task.Run(() => Data.GetNames());
        }

        public void changeNames(int index)
        {
            var model = Models[index];

            authorNameTextBox.Text = model.AuthorName;
            bookNameTextBox.Text = model.BookName;
            updateNameAuthor(model);
        }

        private void updateNameAuthor(AuthorBookModel model)
        {
            model.LastUse = DateTime.Now;

            Data.Update(model);
        }

        private void bookNameTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AuthorChoiceWindow window = new AuthorChoiceWindow(Models, changeNames);
            window.Show();
        }

        private void subpartRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PresentPart = null;
        }

        private void partContentRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            partContentProgressBar.Value = partContentRichTextBox.Text.ToArray().Length;
        }
    }
}
