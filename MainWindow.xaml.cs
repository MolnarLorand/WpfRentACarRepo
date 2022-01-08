using RentACarModel;
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
using System.Data.Entity;
using System.Data;

namespace WpfRentACar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        RentACarEntitiesModel ctx = new RentACarEntitiesModel();
        CollectionViewSource customerVSource;
        CollectionViewSource carVSource;
        CollectionViewSource carRentOrdersVSource;
        CollectionViewSource customerPenaltiesVSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource customerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customerViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // customerViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource carViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("carViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // carViewSource.Source = [generic data source]

            customerVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customerViewSource")));
            customerVSource.Source = ctx.Customers.Local;
            ctx.Customers.Load();

            carVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("carViewSource")));
            carVSource.Source = ctx.Cars.Local;
            ctx.Cars.Load();

            carRentOrdersVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("carRentOrdersViewSource")));

            //customerRentOrdersVSource.Source = ctx.RentOrders.Local;
            ctx.RentOrders.Load();
            ctx.Cars.Load();

            cmbCustomers.ItemsSource = ctx.Customers.Local;
            //cmbCustomers.DisplayMemberPath = "FirstName";
            cmbCustomers.SelectedValuePath = "CustId";

            cmbCars.ItemsSource = ctx.Cars.Local;
            //cmbCars.DisplayMemberPath = "Model";
            cmbCars.SelectedValuePath = "CarId";

            customerPenaltiesVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customerPenaltiesViewSource")));

            BindDataGrid();
            System.Windows.Data.CollectionViewSource penaltyViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("penaltyViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // penaltyViewSource.Source = [generic data source]
        }




        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            customerVSource.View.MoveCurrentToPrevious();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            customerVSource.View.MoveCurrentToNext();
        }



        private void btnPrev1_Click(object sender, RoutedEventArgs e)
        {
            carVSource.View.MoveCurrentToPrevious();
        }

        private void btnNext1_Click(object sender, RoutedEventArgs e)
        {
            carVSource.View.MoveCurrentToNext();
        }

        private void SaveCustomer()
        {
            Customer customer = null;
            if (action == ActionState.New)
            {
                try
                {
                    customer = new Customer()
                    {
                        FirstName = firstNameTextBox.Text.Trim(),
                        LastName = lastNameTextBox.Text.Trim(),
                        PhoneNumber = phoneNumberTextBox.Text.Trim()
                    };
                    ctx.Customers.Add(customer);
                    customerVSource.View.Refresh();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Edit)
            {
                try
                {
                    customer = (Customer)customerDataGrid.SelectedItem;
                    customer.FirstName = firstNameTextBox.Text.Trim();
                    customer.LastName = lastNameTextBox.Text.Trim();
                    customer.PhoneNumber = phoneNumberTextBox.Text.Trim();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    customer = (Customer)customerDataGrid.SelectedItem;
                    ctx.Customers.Remove(customer);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                customerVSource.View.Refresh();
            }
        }

        private void SaveCar()
        {
            Car car = null;
            if (action == ActionState.New)
            {
                try
                {
                    car = new Car()
                    {
                        Model = modelTextBox.Text.Trim(),
                        Color = colorTextBox.Text.Trim(),
                        PlateNumber = plateNumberTextBox.Text.Trim()
                    };
                    ctx.Cars.Add(car);
                    carVSource.View.Refresh();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Edit)
            {
                try
                {
                    car = (Car)carDataGrid.SelectedItem;
                    car.Model = modelTextBox.Text.Trim();
                    car.Color = colorTextBox.Text.Trim();
                    car.PlateNumber = plateNumberTextBox.Text.Trim();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    car = (Car)carDataGrid.SelectedItem;
                    ctx.Cars.Remove(car);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                carVSource.View.Refresh();
            }
        }

       

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
        }


        private void gbOperations_Click(object sender, RoutedEventArgs e)
        {
            Button SelectedButton = (Button)e.OriginalSource;
            Panel panel = (Panel)SelectedButton.Parent;

            foreach (Button B in panel.Children.OfType<Button>())
            {
                if (B != SelectedButton)
                    B.IsEnabled = false;
            }
            gbActions.IsEnabled = true;
        }

        private void ReInitialize()
        {

            Panel panel = gbOperations.Content as Panel;
            foreach (Button B in panel.Children.OfType<Button>())
            {
                B.IsEnabled = true;
            }
            gbActions.IsEnabled = false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReInitialize();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = tbCtrlRentACar.SelectedItem as TabItem;

            switch (ti.Header)
            {
                case "Customers":
                    SaveCustomer();
                    break;
                case "Cars":
                    SaveCar();
                    break;
                case "RentOrders":
                    SaveRentOrders();
                    break;

            }
            ReInitialize();
        }

        private void SaveRentOrders()
        {
            RentOrder rentOrder = null;
            if (action == ActionState.New)
            {
                try
                {
                    Customer customer = (Customer)cmbCustomers.SelectedItem;
                    Car car = (Car)cmbCars.SelectedItem;
                    //instantiem Order entity
                    rentOrder = new RentOrder()
                    {

                        CustId = customer.CustId,
                        CarId = car.CarId
                        
                    };
                    //adaugam entitatea nou creata in context
                    ctx.RentOrders.Add(rentOrder);
                    //salvam modificarile
                    ctx.SaveChanges();
                    BindDataGrid();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                dynamic selectedOrder = rentOrdersDataGrid.SelectedItem;
                try
                {
                    int curr_id = selectedOrder.RentOrderId;
                    var editedOrder = ctx.RentOrders.FirstOrDefault(s => s.RentOrderId == curr_id);
                    if (editedOrder != null)
                    {
                        editedOrder.CustId = Int32.Parse(cmbCustomers.SelectedValue.ToString());
                        editedOrder.CarId = Convert.ToInt32(cmbCars.SelectedValue.ToString());
                        //salvam modificarile
                        ctx.SaveChanges();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                BindDataGrid();
                // pozitionarea pe item-ul curent
                carRentOrdersVSource.View.MoveCurrentTo(selectedOrder);
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    dynamic selectedOrder = rentOrdersDataGrid.SelectedItem;
                    int curr_id = selectedOrder.RentOrderId;
                    var deletedOrder = ctx.RentOrders.FirstOrDefault(s => s.RentOrderId == curr_id);
                    if (deletedOrder != null)
                    {
                        ctx.RentOrders.Remove(deletedOrder);
                        ctx.SaveChanges();
                        MessageBox.Show("RentOrder Deleted Successfully", "Message");
                        BindDataGrid();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BindDataGrid()
        {
            var queryOrder = from ord in ctx.RentOrders
                             join cust in ctx.Customers on ord.CustId equals
                             cust.CustId
                             join inv in ctx.Cars on ord.CarId
                 equals inv.CarId
                             select new
                             {
                                 ord.RentOrderId,
                                 ord.CarId,
                                 ord.CustId,
                                 cust.FirstName,
                                 cust.LastName,
                                 cust.PhoneNumber,
                                 inv.Model,
                                 inv.Color,
                                 inv.PlateNumber
                             };
            carRentOrdersVSource.Source = queryOrder.ToList();
        }

        private void btnPrev2_Click(object sender, RoutedEventArgs e)
        {
            customerVSource.View.MoveCurrentToPrevious();
        }

        private void btnNext2_Click(object sender, RoutedEventArgs e)
        {
            customerVSource.View.MoveCurrentToNext();
        }
    }
}
