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
using System.Data.SqlClient;
using System.Data;

namespace Profil_TopoM
{
    /// <summary>
    /// Logique d'interaction pour Accueil.xaml
    /// </summary>
    public partial class Accueil : UserControl
    {
        Trace trr = new Trace();
        BitmapImage imgg;
        int aym = -1;
        SqlConnection cnx = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Fujitsu\Desktop\Profil_topo_MAKER25\Profil_topo_MAKER\Profil_TopoM\BDDtopo.mdf;Integrated Security=True");
       
            public Accueil()
        {
            InitializeComponent();
           
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cnx.Open();

            string readString3 = "select * from Trace  where nom ='" + nomTrace.Text + "'";

            SqlCommand readCommand3 = new SqlCommand(readString3, cnx);
            int nbs = 1000;

            using (SqlDataReader dataRead3 = readCommand3.ExecuteReader())

            {
                if (dataRead3 != null)
                {
                    while (dataRead3.Read())
                    {

                        string xas = dataRead3["Id"].ToString();
                        nbs = int.Parse(xas);
                        string xas2 = dataRead3["echelle"].ToString();
                        double eche = double.Parse(xas2);
                        string xas22 = dataRead3["echellecarte"].ToString();
                        double echecarte = double.Parse(xas22);
                        string xas4 = dataRead3["equidistance"].ToString();
                        int equi = int.Parse(xas4);
                        string xas6 = dataRead3["min"].ToString();
                        int mi = int.Parse(xas6);
                        string xas8 = dataRead3["max"].ToString();
                        int ma = int.Parse(xas8);
                        string xas10 = dataRead3["creation"].ToString();
                        string xas105 = dataRead3["modification"].ToString();




                        aym = nbs;
                        string xasa = dataRead3["image"].ToString();
                        Uri ur = new Uri(xasa);
                        imgg = new BitmapImage(ur);
                        Trace trace5 = new Trace(nomTrace.Text, xas10, xas105, mi, ma, eche, echecarte, equi, xasa);
                        trr = trace5;
                    }
                }
            }
            cnx.Close();
            if (aym == -1)
            {
                MessageBox.Show("il n'existe pas un tracé avec ce nom  !");
            }
            else
            {
                tracecourbes imp = new tracecourbes(imgg, trr);
                var parent = (Grid)this.Parent;
                parent.Children.Clear();
                parent.Children.Add(imp);
            }
        }
    }
}
