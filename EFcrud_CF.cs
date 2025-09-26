using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace EFcrud2
{
    public partial class EFCRUD : Form
    {
        Customer model = new Customer();

        public EFCRUD()
        {
            InitializeComponent();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Clear();
        }

        void Clear()
        {
            txtFN.Text = txtLN.Text = txtC.Text = txtA.Text = "";
            saveBtn.Text = "Save";
            deleteBtn.Enabled = false;
            model.CustomerID = 0;
        }

        private void EFCRUD_Load(object sender, EventArgs e)
        {
            Clear();
            this.ActiveControl = txtFN;
            LoadData();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            model.FirstName = txtFN.Text.Trim();
            model.LastName = txtLN.Text.Trim();
            model.City = txtC.Text.Trim();
            model.Address = txtA.Text.Trim();

            using (AppDbContext db = new AppDbContext())
            {
                if (model.CustomerID == 0)
                    db.Customers.Add(model);
                else
                    db.Entry(model).State = EntityState.Modified;

                db.SaveChanges();
            }

            Clear();
            LoadData();
            MessageBox.Show("Submitted successfully");
        }

        void LoadData()
        {
            dataGridView1.AutoGenerateColumns = false;
            using (AppDbContext db = new AppDbContext())
            {
                dataGridView1.DataSource = db.Customers.ToList();
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                model.CustomerID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["dgCustomerID"].Value);

                using (AppDbContext db = new AppDbContext())
                {
                    model = db.Customers.Where(x => x.CustomerID == model.CustomerID).FirstOrDefault();
                    txtFN.Text = model.FirstName;
                    txtLN.Text = model.LastName;
                    txtC.Text = model.City;
                    txtA.Text = model.Address;
                }

                saveBtn.Text = "Update";
                deleteBtn.Enabled = true;
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete this record", "Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (AppDbContext db = new AppDbContext())
                {
                    var entry = db.Entry(model);
                    if (entry.State == EntityState.Detached)
                        db.Customers.Attach(model);

                    db.Customers.Remove(model);
                    db.SaveChanges();
                }

                LoadData();
                Clear();
                MessageBox.Show("Deleted successfully");
            }
        }
    }
}
