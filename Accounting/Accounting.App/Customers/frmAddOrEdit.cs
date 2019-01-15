using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;
using Accounting.DataLayer;
using Accounting.DataLayer.Context;


namespace Accounting.App
{
    public partial class frmAddOrEdit : Form
    {
        public int CustomerId = 0;
        UnitOfWork db = new UnitOfWork();
        public frmAddOrEdit()
        {
            InitializeComponent();
        }

        private void frmAddOrEdit_Load(object sender, EventArgs e)
        {
            if (CustomerId != 0)
            {
                this.Text = "ویرایش شخص";
                btnSave.Text = "ویرایش";
                var Customer = db.CustomerRepository.GetCustomerById(CustomerId);
                txtAddress.Text = Customer.Address;
                txtEmail.Text = Customer.Email;
                txtMobile.Text = Customer.Mobile;
                txtName.Text = Customer.FullName;
                pcCustomer.ImageLocation = Application.StartupPath + "/Images/" + Customer.CustomerImage;
            }
        }

        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                pcCustomer.ImageLocation = OpenFile.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
                if (BaseValidator.IsFormValid(this.components))
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(pcCustomer.ImageLocation);
                string path = Application.StartupPath + "/Images/";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                pcCustomer.Image.Save(path + imageName);
                Customers Customers = new Customers()
                {
                    Address = txtAddress.Text,
                    Email = txtEmail.Text,
                    FullName = txtName.Text,
                    Mobile = txtMobile.Text,
                    CustomerImage = imageName
                };
                if (CustomerId == 0)
                {
                    db.CustomerRepository.InsertCustomer(Customers);
                }
                else
                {
                   
                        Customers.CustomerID = CustomerId;
                        db.CustomerRepository.UpdateCustomer(Customers);
                    }
                }
                db.Save();
                DialogResult = DialogResult.OK;
            }
        }
    }


