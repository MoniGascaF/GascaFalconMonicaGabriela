using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace CalcuTabla
{
    public partial class ViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate
    {
        int numero;
        List<string> lista;
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            lista = new List<string>();
            tableView.DataSource = this;
            tableView.Delegate = this;
        }
        partial void btnAdd(NSObject sender)
        {
            Opciones();
        }

        void Opciones()
        {

            var alert = UIAlertController.Create(null, null, UIAlertControllerStyle.ActionSheet);
            alert.AddAction(UIAlertAction.Create("1", UIAlertActionStyle.Default, Action => Multi(1)));
            alert.AddAction(UIAlertAction.Create("2", UIAlertActionStyle.Default, Action => Multi(2)));
            alert.AddAction(UIAlertAction.Create("3", UIAlertActionStyle.Default, Action => Multi(3)));
            alert.AddAction(UIAlertAction.Create("4", UIAlertActionStyle.Default, Action => Multi(4)));
            alert.AddAction(UIAlertAction.Create("5", UIAlertActionStyle.Default, Action => Multi(5)));
            alert.AddAction(UIAlertAction.Create("6", UIAlertActionStyle.Default, Action => Multi(6)));
            alert.AddAction(UIAlertAction.Create("7", UIAlertActionStyle.Default, Action => Multi(7)));
            alert.AddAction(UIAlertAction.Create("8", UIAlertActionStyle.Default, Action => Multi(8)));
            alert.AddAction(UIAlertAction.Create("9", UIAlertActionStyle.Default, Action => Multi(9)));
            alert.AddAction(UIAlertAction.Create("10", UIAlertActionStyle.Default, Action => Multi(10)));
            alert.AddAction(UIAlertAction.Create("11", UIAlertActionStyle.Default, Action => Multi(11)));
            alert.AddAction(UIAlertAction.Create("12", UIAlertActionStyle.Default, Action => Multi(12)));
            alert.AddAction(UIAlertAction.Create("Cancelar", UIAlertActionStyle.Cancel, null));
            PresentViewController(alert, true, null);

        }

        void Multi(int num)
        {
            numero = num;
            var modal = UIAlertController.Create("", "Ingrese hasta que numero quiere que se multiplique", UIAlertControllerStyle.Alert);
            UITextField x = new UITextField();
            modal.AddTextField((textField) =>
            {
                // Save the field
                x = textField;
                // Initialize field
                x.Placeholder = "Ingrese numero";
            });
            modal.AddAction(UIAlertAction.Create("Aceptar", UIAlertActionStyle.Default, Action => resultado(x.Text.ToString())));
            modal.AddAction(UIAlertAction.Create("Cancelar", UIAlertActionStyle.Cancel, null));
            //refresh
            PresentViewController(modal, animated: true, completionHandler: null);
            ActualizarTabla();

        }

        void resultado(string x)
        {
            lista = new List<string>();
            if (int.TryParse(x, out int num))
            {
                for (int i = 0; i < num+1; i++)
                {
                    lista.Add(numero + " x " + i + " = " + (numero * i));
                }
            }
            else
            {
                lista.Add("valor invalido");
            }
            ActualizarTabla();
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            return lista.Count;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            /*var cell = tableView.DequeueReusableCell("BasicTableViewCell", indexPath);
            cell.TextLabel.Text = $"Hola I'm in Section: {indexPath.Section} and Row: {indexPath.Row}";*/
            var cell = tableView.DequeueReusableCell("BasicTableViewCell", indexPath);
            cell.TextLabel.Text = lista[indexPath.Row];
            return cell;
        }


        void ActualizarTabla()
        {
            InvokeOnMainThread(() =>
            {
                tableView.ReloadData();
            });
        }
	}
}
