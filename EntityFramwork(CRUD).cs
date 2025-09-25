using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFcrud2
{
    public partial class EFCRUD : Form
    {
        customer model = new customer();
        public EFCRUD()
        {
            InitializeComponent();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            clear();
        }
        void clear()
        {
            txtFN.Text= txtLN.Text = txtC.Text = txtA.Text = " ";
            saveBtn.Text = "Save";
            deleteBtn.Enabled = false;
            model.customerID = 0;
        }

        private void EFCRUD_Load(object sender, EventArgs e)
        {
            clear();
            this.ActiveControl = txtFN;
            LoadData();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            model.firstName = txtFN.Text.Trim();
            model.lastName = txtLN.Text.Trim();
            model.city = txtC.Text.Trim();
            model.address = txtA.Text.Trim();
            using (EFDBEntities db =new EFDBEntities())
            {
                if (model.customerID == 0)
                    db.customers.Add(model);
                else
                    db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            clear();
            LoadData();
            MessageBox.Show("Submitted successfully");
        }
        void LoadData()
        {
            dataGridView1.AutoGenerateColumns = false;
            using(EFDBEntities db =new EFDBEntities())
            {
                dataGridView1.DataSource = db.customers.ToList<customer>();
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow.Index != -1)
            {
                model.customerID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["dgcustomerID"].Value);
                using(EFDBEntities db=new EFDBEntities())
                {
                    model = db.customers.Where(x => x.customerID == model.customerID).FirstOrDefault();
                    txtFN.Text = model.firstName;
                    txtLN.Text = model.lastName;
                    txtC.Text = model.city;
                    txtA.Text = model.address;
                }
                saveBtn.Text = "Update";
                deleteBtn.Enabled = true;
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure to delete this record","Message",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (EFDBEntities db =new EFDBEntities())
                {
                    var entry = db.Entry(model);
                    if(entry.State == EntityState.Detached)
                    {
                        db.customers.Attach(model);
                        db.customers.Remove(model);
                        db.SaveChanges();
                        LoadData();
                        clear();
                        MessageBox.Show("Deleted successfully");
                    }
                }
                 
            }
        }
    }
}
