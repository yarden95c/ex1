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

namespace ClientWpf
{
    /// <summary>
    /// Interaction logic for DialogBox.xaml
    /// </summary>
    public partial class DialogBox : Window
    {
        private string dBmessage;
        public DialogBox()
        {
            InitializeComponent();
        }
        public string DB_Message
        {
            get
            {
                return this.dBmessage;
            }
            set
            {
                this.dBmessage = value;
            }
        }
        public static readonly DependencyProperty DB_MessageD =
            DependencyProperty.Register("DB_Message", typeof(string), typeof(DialogBox),
                new PropertyMetadata(DBMessageChanges));
        private static void DBMessageChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DialogBox db = (DialogBox)d;
            db.DB_Message = (string)e.NewValue;
        }
    }
}
